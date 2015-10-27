using SyncMyCal.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SyncMyCal.Calendars
{
    public interface ICalendar
    {
        /// <summary>
        /// Connects to the Calendar Provider. Needs to be called before any data can be retrieved
        /// </summary>
        /// <returns>True when the connection could be established successfuly</returns>
        bool ConnectCalendar();

        /// <summary>
        /// Returns the name of the Calendar provider
        /// </summary>
        /// <returns></returns>
        string GetProviderName();

        /// <summary>
        /// Returns a list of all calendars available for the user
        /// </summary>
        /// <returns></returns>
        List<CalendarId> GetCalendars();

        /// <summary>
        /// Sets the calendar this instance should use.
        /// (Sometimes a user can have several calendars linked to one account)
        /// </summary>
        /// <param name="calendar"></param>
        void SetActiveCalendar(CalendarId calendar);

        /// <summary>
        /// Returns all calendar events in a given timespan
        /// </summary>
        /// <param name="from">The DateTime of the earliest calendar event</param>
        /// <param name="to">The DateTime of the last calendar event</param>
        /// <returns>List of all matching CalendarEntries</returns>
        List<CalendarEntry> GetCalendarEntriesInRange(DateTime from, DateTime to);

        /// <summary>
        /// Adds a new event to the calendar
        /// </summary>
        /// <param name="newEntry">Entry to e created</param>
        /// <returns>True when there were no errors while saving the event to the calendar</returns>
        bool AddNewCalendarEntry(CalendarEntry newEntry);

        /// <summary>
        /// Removes a calendar event from the calendar
        /// </summary>
        /// <param name="entry">Entry to be deleted</param>
        /// <returns>True when there were no errors while deleting the entry</returns>
        bool DeleteCalendarEntry(CalendarEntry entry);
    }
}
