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
        public List<SyncSetting> _calendarsToSync;

        public SyncManager()
        {
            _calendarsToSync = LoadSyncSettings();
        }

        public void SyncAllCalendars()
        {
            foreach (SyncSetting syncSetting in _calendarsToSync)
            {
                SyncCalendar(syncSetting);
            }
        }

        public bool SyncCalendar(SyncSetting syncSetting)
        {
            syncSetting.Source.SetActiveCalendar(syncSetting.SourceCalendar);
            syncSetting.Destination.SetActiveCalendar(syncSetting.DestinationCalendar);

            return new Sync()
                        .From(syncSetting.Source)
                        .To(syncSetting.Destination)
                        .InTimeRangeFrom(DateTime.Now.AddDays(-syncSetting.DaysIntoPast))
                        .To(DateTime.Now.AddDays(syncSetting.DaysIntoFuture))
                        .BeginSync();
        }

        public void SaveSyncSettings()
        {
            var itemsSerialized = JsonConvert.SerializeObject(_calendarsToSync, Formatting.None, new JsonSerializerSettings
            {
                TypeNameHandling = TypeNameHandling.Objects,
                TypeNameAssemblyFormat = System.Runtime.Serialization.Formatters.FormatterAssemblyStyle.Simple
            });

            Settings.Default.JsonSyncSettings = itemsSerialized;
            Settings.Default.Save();
        }

        public List<SyncSetting> LoadSyncSettings()
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
