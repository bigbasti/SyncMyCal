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
            this.Id = googleCalendarEvent.Id;

            if (googleCalendarEvent.Start.Date != null && googleCalendarEvent.End.Date != null)
            {
                var startDate = googleCalendarEvent.Start.Date.Split('-');
                var endDate = googleCalendarEvent.End.Date.Split('-');
                this.AllDay = true;
                this.Start = new DateTime(Convert.ToInt32(startDate[0]), Convert.ToInt32(startDate[1]), Convert.ToInt32(startDate[2]));
                this.End = new DateTime(Convert.ToInt32(endDate[0]), Convert.ToInt32(endDate[1]), Convert.ToInt32(endDate[2]));
            }
            else
            {
                this.AllDay = false;
                this.Start = googleCalendarEvent.Start.DateTime.GetValueOrDefault();
                this.End = googleCalendarEvent.End.DateTime.GetValueOrDefault();
            }

            this.Subject = googleCalendarEvent.Summary;
            this.Description = googleCalendarEvent.Description;
            this.Location = googleCalendarEvent.Location;

            //TODO: Als Liste implementieren, so dass unbegrenzt viele Reminder möglich sind
            this.Reminder = googleCalendarEvent.Reminders.Overrides.Count > 0;
            this.RemainterMinutesBefore = googleCalendarEvent.Reminders.Overrides[0].Minutes.Value;

            this.Attendees = new List<CalendarAttendee>();
            foreach (EventAttendee a in googleCalendarEvent.Attendees)
            {
                this.Attendees.Add(new CalendarAttendee(a));
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
                this.Id = outlookCalendarEvent.EntryID;
                this.Start = new DateTime();
                this.End = new DateTime();

                if (outlookCalendarEvent.AllDayEvent)
                {
                    this.AllDay = true;
                    this.Start = outlookCalendarEvent.Start.Date;
                    this.End = outlookCalendarEvent.End.Date;
                }
                else
                {
                    this.AllDay = false;
                    this.Start = outlookCalendarEvent.Start;
                    this.End = outlookCalendarEvent.End;
                }
                this.Subject = outlookCalendarEvent.Subject;
                this.Description = outlookCalendarEvent.Body;
                this.Location = outlookCalendarEvent.Location;


                //Erinnerung setzen
                this.Reminder = outlookCalendarEvent.ReminderSet;
                this.RemainterMinutesBefore = outlookCalendarEvent.ReminderMinutesBeforeStart;

                //Teilnehmer
                this.Attendees = new List<CalendarAttendee>();
                foreach (Recipient r in outlookCalendarEvent.Recipients)
                {
                    this.Attendees.Add(new CalendarAttendee(r));
                }
            }
            catch (System.Exception ex)
            {
                //map me if you can
                //In some cases outlook fails to map properties (e.g. a attendee). 
                //The program will continue to sync events, even if it can't sync it correctly. 
                //It is better to have a broken event, than non events at all.

                Debug.WriteLine("Failed to read a event.", ex, this);

                this.Subject += "[Sync Error]";
            }  
        }

        /// <summary>
        /// Creates a GoogleCalendarEvent from internal Data
        /// </summary>
        /// <returns></returns>
        public Event toGoogleCalendarEvent()
        {
            Event googleCalendarEvent = new Event();

            googleCalendarEvent.Start = new EventDateTime();
            googleCalendarEvent.End = new EventDateTime();

            if (this.AllDay)
            {
                googleCalendarEvent.Start.Date = this.Start.ToString("yyyy-MM-dd");
                googleCalendarEvent.End.Date = this.End.ToString("yyyy-MM-dd");
            }
            else
            {
                googleCalendarEvent.Start.DateTime = this.Start;
                googleCalendarEvent.End.DateTime = this.End;
            }
            googleCalendarEvent.Summary = this.Subject;
            googleCalendarEvent.Description = this.Description;
            googleCalendarEvent.Location = this.Location;


            //Erinnerung setzen
            googleCalendarEvent.Reminders = new Event.RemindersData();
            googleCalendarEvent.Reminders.UseDefault = false;
            EventReminder reminder = new EventReminder();
            reminder.Method = "popup";
            reminder.Minutes = this.RemainterMinutesBefore;
            googleCalendarEvent.Reminders.Overrides = new List<EventReminder>();
            googleCalendarEvent.Reminders.Overrides.Add(reminder);

            //Teilnehmer
            googleCalendarEvent.Attendees = this.Attendees.Select(att => att.toGoogleAttendee()).ToList();

            return googleCalendarEvent;
        }

        /// <summary>
        /// Creates an OutlookClencarEvent from internal Data
        /// </summary>
        /// <returns></returns>
        public AppointmentItem toOutlookCalendarEvent()
        {
            return null;
        }

        /// <summary>
        /// Generates a representative string for the event containing the
        /// start time, end time, subject and the location of the event
        /// </summary>
        /// <returns></returns>
        public string generateSignature()
        {
            //REFACTOR: in isEqualTo(CalendarEntry) ändern
            return (this.Start + ";" + this.End + ";" + this.Subject + ";" + this.Location).Trim();
        }
    }
}
