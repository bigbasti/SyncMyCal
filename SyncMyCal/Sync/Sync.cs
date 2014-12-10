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
    /// Performs an One-Way Sync from source Calendar to destination calendar. 
    /// Only the destination calendar will be changed!
    /// </summary>
    class Sync
    {
        ICalendar source;
        ICalendar destination;

        DateTime timeFrom;
        DateTime timeTo;

        public Sync()
        {

        }

        public Sync from(ICalendar source)
        {
            this.source = source;
            return this;
        }

        public Sync to(ICalendar destination)
        {
            this.destination = destination;
            return this;
        }

        public Sync inTimeRangeFrom(DateTime from)
        {
            this.timeFrom = from;
            return this;
        }

        public Sync to(DateTime to)
        {
            this.timeTo = to;
            return this;
        }

        //TODO: Async machen
        public bool beginSync()
        {
            if (this.source == null || this.destination == null || this.timeFrom == null || this.timeTo == null)
            {
                throw new InvalidOperationException("Please provide the timespan and source and destination calendars befor starting sync");
            }

            try
            {
                //Get Entries from source
                var sourceEntries = this.source.getCalendarEntriesInRange(this.timeFrom, this.timeTo);

                //Get the entries from destination
                var destinationEntries = this.destination.getCalendarEntriesInRange(this.timeFrom, this.timeTo);

                //Identify entries which need to be created at destination
                var destinationEntriesToBeCreated = IdentifyDestinationEntriesToBeCreated(sourceEntries, destinationEntries);

                //Identify entries which need to be deleted at destination
                var destinationEntriesToBeDeleted = IdentifyDestinationEntriesToBeDeleted(sourceEntries, destinationEntries);

                if (destinationEntriesToBeDeleted.Count > 0)
                {
                    Console.WriteLine("Lösche " + destinationEntriesToBeDeleted.Count + " Google Calender Einträge...");
                    foreach (CalendarEntry entry in destinationEntriesToBeDeleted)
                    {
                        destination.deleteCalendarEntry(entry);
                    }
                    Console.WriteLine("Löschen fertig.");
                    Console.WriteLine("--------------------------------------------------");
                }

                if (destinationEntriesToBeCreated.Count > 0)
                {
                    foreach (CalendarEntry entry in destinationEntriesToBeCreated)
                    {
                        destination.addNewCalendarEntry(entry);
                    }
                    Console.WriteLine("--------------------------------------------------");
                    Console.WriteLine("Alle Einträge erfolgreich angelegt!");
                }
                else
                {
                    Console.WriteLine("--------------------------------------------------");
                    Console.WriteLine("Es wurden keine Einträge angelegt!");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw ex;
            }

            return true;
        }

        private List<CalendarEntry> IdentifyDestinationEntriesToBeDeleted(List<CalendarEntry> source, List<CalendarEntry> destination)
        {
            List<CalendarEntry> result = new List<CalendarEntry>();
            foreach (CalendarEntry g in destination)
            {
                bool found = false;
                foreach (CalendarEntry e in source)
                {
                    if (g.generateSignature() == e.generateSignature()) found = true;
                }
                if (!found) result.Add(g);
            }
            return result;
        }

        public List<CalendarEntry> IdentifyDestinationEntriesToBeCreated(List<CalendarEntry> source, List<CalendarEntry> destination)
        {
            List<CalendarEntry> result = new List<CalendarEntry>();
            foreach (CalendarEntry o in source)
            {
                bool found = false;
                foreach (CalendarEntry g in destination)
                {
                    if (g.generateSignature() == o.generateSignature()) found = true;
                }
                if (!found) result.Add(o);
            }
            return result;
        }
    }
}
