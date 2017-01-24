using System.Diagnostics;
using Google.Apis.Calendar.v3.Data;
using Microsoft.Office.Interop.Outlook;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SyncMyCal.Data
{
    /// <summary>
    /// This generic class holds all the important Event information so it can
    /// easily be translated between different calendar providers
    /// </summary>
    public class CalendarEntry
    {
        public string Id { get; set; }
        public string Subject { get; set; }
        public string Description { get; set; }
        public IList<CalendarAttendee> Attendees { get; set; }
        public DateTime Start { get; set; }
        public DateTime End { get; set; }
        public bool AllDay { get; set; }
        public bool Reminder { get; set; }
        public int RemainterMinutesBefore { get; set; }
        public string Location { get; set; }

        public CalendarEntry()
        {
            //TODO: braucht man das?
        }

        /// <summary>
        /// Creates a new CalendarEntry from a GoogleCalendar Event
        /// </summary>
        /// <param name="googleCalendarEvent"></param>
        public CalendarEntry(Event googleCalendarEvent)
        {
            Console.WriteLine("Creating CalendarEvent for " + googleCalendarEvent.Summary);
            Id = googleCalendarEvent.Id;

            if (googleCalendarEvent.Start.Date != null && googleCalendarEvent.End.Date != null)
            {
                var startDate = googleCalendarEvent.Start.Date.Split('-');
                var endDate = googleCalendarEvent.End.Date.Split('-');
                AllDay = true;
                Start = new DateTime(Convert.ToInt32(startDate[0]), Convert.ToInt32(startDate[1]), Convert.ToInt32(startDate[2]));
                End = new DateTime(Convert.ToInt32(endDate[0]), Convert.ToInt32(endDate[1]), Convert.ToInt32(endDate[2]));
            }
            else
            {
                AllDay = false;
                Start = googleCalendarEvent.Start.DateTime.GetValueOrDefault();
                End = googleCalendarEvent.End.DateTime.GetValueOrDefault();
            }

            Subject = googleCalendarEvent.Summary;
            Description = googleCalendarEvent.Description;
            Location = googleCalendarEvent.Location;

            //TODO: Als Liste implementieren, so dass unbegrenzt viele Reminder möglich sind
            Reminder = googleCalendarEvent.Reminders.Overrides.Count > 0;
            RemainterMinutesBefore = googleCalendarEvent.Reminders.Overrides[0].Minutes.Value;

            Attendees = new List<CalendarAttendee>();
            if(googleCalendarEvent.Attendees != null)
            {
                foreach (EventAttendee a in googleCalendarEvent.Attendees)
                {
                    Attendees.Add(new CalendarAttendee(a));
                }
            }
            
        }

        /// <summary>
        /// Creates a new CalendarEntry from an OutlookCalender Event
        /// </summary>
        /// <param name="outlookCalendarEvent"></param>
        public CalendarEntry(AppointmentItem outlookCalendarEvent)
        {
            try
            {
                Id = outlookCalendarEvent.EntryID;
                Start = new DateTime();
                End = new DateTime();

                if (outlookCalendarEvent.AllDayEvent)
                {
                    AllDay = true;
                    Start = outlookCalendarEvent.Start.Date;
                    End = outlookCalendarEvent.End.Date;
                }
                else
                {
                    AllDay = false;
                    Start = outlookCalendarEvent.Start;
                    End = outlookCalendarEvent.End;
                }
                Subject = outlookCalendarEvent.Subject;
                Description = outlookCalendarEvent.Body;
                Location = outlookCalendarEvent.Location;


                //Erinnerung setzen
                Reminder = outlookCalendarEvent.ReminderSet;
                RemainterMinutesBefore = outlookCalendarEvent.ReminderMinutesBeforeStart;

                //Teilnehmer
                Attendees = new List<CalendarAttendee>();
                if (outlookCalendarEvent.Recipients != null)
                {
                    foreach (Recipient r in outlookCalendarEvent.Recipients)
                    {
                        Attendees.Add(new CalendarAttendee(r));
                    }
                }
            }
            catch (System.Exception ex)
            {
                //map me if you can
                //In some cases outlook fails to map properties (e.g. a attendee). 
                //The program will continue to sync events, even if it can't sync it correctly. 
                //It is better to have a broken event, than non events at all.

                Debug.WriteLine("Failed to read a event.", ex, this);

                Subject += "[Sync Error]";
            }  
        }

        /// <summary>
        /// Creates a GoogleCalendarEvent from internal Data
        /// </summary>
        /// <returns></returns>
        public Event ToGoogleCalendarEvent()
        {
            Event googleCalendarEvent = new Event();

            googleCalendarEvent.Start = new EventDateTime();
            googleCalendarEvent.End = new EventDateTime();

            if (AllDay)
            {
                googleCalendarEvent.Start.Date = Start.ToString("yyyy-MM-dd");
                googleCalendarEvent.End.Date = End.ToString("yyyy-MM-dd");
            }
            else
            {
                googleCalendarEvent.Start.DateTime = Start;
                googleCalendarEvent.End.DateTime = End;
            }
            googleCalendarEvent.Summary = Subject;
            googleCalendarEvent.Description = Description;
            googleCalendarEvent.Location = Location;


            //Erinnerung setzen
            googleCalendarEvent.Reminders = new Event.RemindersData();
            googleCalendarEvent.Reminders.UseDefault = false;
            EventReminder reminder = new EventReminder();
            reminder.Method = "popup";
            reminder.Minutes = RemainterMinutesBefore;
            googleCalendarEvent.Reminders.Overrides = new List<EventReminder>();
            googleCalendarEvent.Reminders.Overrides.Add(reminder);

            //Teilnehmer
            googleCalendarEvent.Attendees = Attendees.Select(att => att.ToGoogleAttendee()).ToList();

            return googleCalendarEvent;
        }

        /// <summary>
        /// Creates an OutlookClencarEvent from internal Data
        /// </summary>
        /// <returns></returns>
        public AppointmentItem ToOutlookCalendarEvent()
        {
            return null;
        }

        /// <summary>
        /// Generates a representative string for the event containing the
        /// start time, end time, subject and the location of the event
        /// </summary>
        /// <returns></returns>
        public string GenerateSignature()
        {
            //REFACTOR: in isEqualTo(CalendarEntry) ändern
            return (Start + ";" + End + ";" + Subject + ";" + Location).Trim();
        }
    }
}
