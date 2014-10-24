using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using SyncMyCal.Properties;
using SyncMyCal.Calendars;

namespace SyncMyCal.Sync
{
    /// <summary>
    /// Executes all configurd syncs between calendars
    /// </summary>
    public class SyncManager
    {
        public List<SyncSetting> calendarsToSync;

        public SyncManager()
        {
            calendarsToSync = loadSyncSettings();
        }

        public void syncAllCalendars()
        {
            foreach (SyncSetting syncSetting in calendarsToSync)
            {
                syncCalendar(syncSetting);
            }
        }

        public bool syncCalendar(SyncSetting syncSetting)
        {
            syncSetting.Source.setActiveCalendar(syncSetting.SourceCalendar);
            syncSetting.Destination.setActiveCalendar(syncSetting.DestinationCalendar);

            return new Sync()
                        .from(syncSetting.Source)
                        .to(syncSetting.Destination)
                        .inTimeRangeFrom(DateTime.Now.AddDays(-syncSetting.DaysIntoPast))
                        .to(DateTime.Now.AddDays(syncSetting.DaysIntoFuture))
                        .beginSync();
        }

        public void saveSyncSettings()
        {
            var itemsSerialized = JsonConvert.SerializeObject(calendarsToSync, Formatting.None, new JsonSerializerSettings
            {
                TypeNameHandling = TypeNameHandling.Objects,
                TypeNameAssemblyFormat = System.Runtime.Serialization.Formatters.FormatterAssemblyStyle.Simple
            });

            Settings.Default.JsonSyncSettings = itemsSerialized;
            Settings.Default.Save();
        }

        public List<SyncSetting> loadSyncSettings()
        {
            try
            {
                String jsonSyncSettings = Settings.Default.JsonSyncSettings;
                List<SyncSetting> settings = JsonConvert.DeserializeObject<List<SyncSetting>>(jsonSyncSettings, new JsonSerializerSettings
                {
                    TypeNameHandling = TypeNameHandling.Objects
                });
                if (settings == null)
                {
                    return new List<SyncSetting>();
                }
                return settings;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return new List<SyncSetting>();
            }
        }
    }
}
