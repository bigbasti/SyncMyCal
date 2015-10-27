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
        ICalendar _source;
        ICalendar _destination;

        DateTime _timeFrom;
        DateTime _timeTo;

        public Sync()
        {

        }

        public Sync From(ICalendar source)
        {
            _source = source;
            return this;
        }

        public Sync To(ICalendar destination)
        {
            _destination = destination;
            return this;
        }

        public Sync InTimeRangeFrom(DateTime from)
        {
            _timeFrom = from;
            return this;
        }

        public Sync To(DateTime to)
        {
            _timeTo = to;
            return this;
        }

        //TODO: Async machen
        public bool BeginSync()
        {
            if (_source == null || _destination == null || _timeFrom == null || _timeTo == null)
            {
                throw new InvalidOperationException("Please provide the timespan and source and destination calendars befor starting sync");
            }

            try
            {
                //Get Entries from source
                var sourceEntries = _source.GetCalendarEntriesInRange(_timeFrom, _timeTo);

                //Get the entries from destination
                var destinationEntries = _destination.GetCalendarEntriesInRange(_timeFrom, _timeTo);

                //Identify entries which need to be created at destination
                var destinationEntriesToBeCreated = IdentifyDestinationEntriesToBeCreated(sourceEntries, destinationEntries);

                //Identify entries which need to be deleted at destination
                var destinationEntriesToBeDeleted = IdentifyDestinationEntriesToBeDeleted(sourceEntries, destinationEntries);

                if (destinationEntriesToBeDeleted.Count > 0)
                {
                    Console.WriteLine("Lösche " + destinationEntriesToBeDeleted.Count + " Google Calender Einträge...");
                    foreach (CalendarEntry entry in destinationEntriesToBeDeleted)
                    {
                        _destination.DeleteCalendarEntry(entry);
                    }
                    Console.WriteLine("Löschen fertig.");
                    Console.WriteLine("--------------------------------------------------");
                }

                if (destinationEntriesToBeCreated.Count > 0)
                {
                    foreach (CalendarEntry entry in destinationEntriesToBeCreated)
                    {
                        _destination.AddNewCalendarEntry(entry);
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
                    if (g.GenerateSignature() == e.GenerateSignature()) found = true;
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
                    if (g.GenerateSignature() == o.GenerateSignature()) found = true;
                }
                if (!found) result.Add(o);
            }
            return result;
        }
    }
}
