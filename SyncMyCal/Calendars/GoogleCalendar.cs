using Google.Apis.Auth.OAuth2;
using Google.Apis.Calendar.v3;
using Google.Apis.Calendar.v3.Data;
using Google.Apis.Services;
using SyncMyCal.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace SyncMyCal.Calendars
{
    public class GoogleCalendar : ICalendar
    {
        CalendarService calendarConnection;
        CalendarId activeCalendar;

        public GoogleCalendar()
        {
            ConnectCalendar();
        }

        public string GetProviderName()
        {
            return "Google";
        }

        public bool ConnectCalendar()
        {
            ClientSecrets secrets = new ClientSecrets 
            { 
                ClientId = "723833707174-6ckfm32qv6vfdv66jtq0u5nsvdpq15gh.apps.googleusercontent.com", 
                ClientSecret = "cmqb8LAJcvLNdvCy-sVToGwh" 
            };

            try
            {
                //FileDataStore store = new FileDataStore("MyFolder");
                UserCredential credential = GoogleWebAuthorizationBroker.AuthorizeAsync(
                    secrets,
                    new string[]
                    { 
                        CalendarService.Scope.Calendar
                    },
                    "user",
                    CancellationToken.None)
                .Result;

                var initializer = new BaseClientService.Initializer();
                initializer.HttpClientInitializer = credential;
                initializer.ApplicationName = "SyncMyCal";                  //TODO: in property auslagern
                calendarConnection = new CalendarService(initializer);
            }
            catch (Exception ex)
            {
                //TODO: Exceptions aufschlüsseln
                Console.WriteLine(ex.Message);
                return false;
            }

            return true;
        }

        public List<CalendarId> GetCalendars()
        {
            var availableCalendars = new List<CalendarId>();

            if (calendarConnection == null)
            {
                //Return empty list if no connection is active
                //TODO: oder besser ne Exception?
                return availableCalendars;
            }

            var calendars = calendarConnection.CalendarList.List().Execute().Items;

            foreach (CalendarListEntry entry in calendars)
            {
                availableCalendars.Add(new CalendarId()
                {
                    Provider = "google",
                    DsplayName = entry.Summary,
                    InternalId = entry.Id,
                    Description = entry.Description
                });
            }
            return availableCalendars;
        }

        public void SetActiveCalendar(CalendarId calendar)
        {
            this.activeCalendar = calendar;
        }

        public List<CalendarEntry> GetCalendarEntriesInRange(DateTime from, DateTime to)
        {
            var eventsInRange = new List<CalendarEntry>();
            Events request = null;

            if (this.calendarConnection == null || this.activeCalendar == null)
            {
                return eventsInRange;
            }

            try
            {
                EventsResource.ListRequest lr = calendarConnection.Events.List(this.activeCalendar.InternalId);

                lr.TimeMin = from;
                lr.TimeMax = to;

                request = lr.Execute();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            if (request != null)
            {
                if (request.Items != null)
                {
                    eventsInRange = request.Items.Select(ev => new CalendarEntry(ev)).ToList();
                }
            }
            return eventsInRange;
        }

        public bool AddNewCalendarEntry(CalendarEntry newEntry)
        {
            if (this.calendarConnection == null || this.activeCalendar == null)
            {
                return false;
            }

            try
            {
                calendarConnection.Events.Insert(newEntry.ToGoogleCalendarEvent() , this.activeCalendar.InternalId).Execute();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return false;
            }
            return true;
        }

        public bool DeleteCalendarEntry(CalendarEntry entry)
        {
            if (this.calendarConnection == null || this.activeCalendar == null)
            {
                return false;
            }

            try
            {
                //TODO: make sure entry.id is the actual internal google event id
                calendarConnection.Events.Delete(this.activeCalendar.InternalId, entry.Id).Execute();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return false;
            }
            return true;
        }
    }
}
