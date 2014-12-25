using SyncMyCal.Calendars;
using SyncMyCal.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SyncMyCal.Sync
{
    /// <summary>
    /// Contains the information needed to perform a sync between two calendars
    /// </summary>
    public class SyncSetting
    {
        /// <summary>
        /// Id only for internal use
        /// </summary>
        public int Id { get; set; }

        public ICalendar Source { get; set; }
        public ICalendar Destination { get; set; }

        public CalendarId SourceCalendar { get; set; }
        public CalendarId DestinationCalendar { get; set; }

        public int DaysIntoPast { get; set; }
        public int DaysIntoFuture { get; set; }

        public int MinutesBetweenSync { get; set; }

        public override string ToString()
        {
            if (Source != null && SourceCalendar != null && Destination != null && DestinationCalendar != null)
            {
                return string.Format("{0} ({1}) -> {2} ({3})", Source.getProviderName(), SourceCalendar.DsplayName,
                    Destination.getProviderName(), DestinationCalendar.DsplayName);
            }
            return null;
        }
    }
}
