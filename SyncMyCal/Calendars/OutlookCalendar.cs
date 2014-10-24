using Microsoft.Office.Interop.Outlook;
using SyncMyCal.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SyncMyCal.Calendars
{
    public class OutlookCalendar : ICalendar
    {
        MAPIFolder calendarConnection = null;

        public OutlookCalendar()
        {
            connectCalendar();
        }

        public string getProviderName()
        {
            return "Outlook";
        }

        public bool connectCalendar()
        {
            try
            {
                Application oApp = new Application();
                NameSpace oNS = oApp.GetNamespace("mapi");

                oNS.Logon("", "", true, true);
                //Standardkalender wählen
                //TODO: ich glaube hier besteht keine Auswahl (es gibt nur einen Kalender?)
                calendarConnection = oNS.GetDefaultFolder(OlDefaultFolders.olFolderCalendar);

                oNS.Logoff();
            }
            catch (System.Exception ex)
            {
                Console.WriteLine(ex.Message);
                return false;
            }
            return true;
        }

        public List<CalendarId> getCalendars()
        {
            return new List<CalendarId>()
            {
                new CalendarId()
                {
                    Provider = "outlook", 
                    DsplayName = this.calendarConnection.Name, 
                    InternalId = this.calendarConnection.EntryID, 
                    Description=""
                }
            };
        }

        public void setActiveCalendar(CalendarId calendar)
        {
            //TODO: braucht man hier wahrscheinlich nicht -> do nothing
        }

        public List<CalendarEntry> getCalendarEntriesInRange(DateTime from, DateTime to)
        {
            var result = new List<CalendarEntry>();

            Items OutlookItems = calendarConnection.Items;
            OutlookItems.Sort("[Start]", Type.Missing);
            OutlookItems.IncludeRecurrences = true;

            if (OutlookItems != null)
            {
                DateTime min = from;
                DateTime max = to;

                string filter = "[End] >= '" + min.ToString("g") + "' AND [Start] < '" + max.ToString("g") + "'";

                foreach (AppointmentItem ai in OutlookItems.Restrict(filter))
                {
                    result.Add(new CalendarEntry(ai));
                }
            }
            return result;
        }

        public bool addNewCalendarEntry(CalendarEntry newEntry)
        {
            throw new NotImplementedException();
        }

        public bool deleteCalendarEntry(CalendarEntry entry)
        {
            throw new NotImplementedException();
        }
    }
}
