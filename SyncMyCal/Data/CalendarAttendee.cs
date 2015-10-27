using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Google.Apis.Calendar.v3.Data;
using Microsoft.Office.Interop.Outlook;

namespace SyncMyCal.Data
{
    public class CalendarAttendee
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Status { get; set; }
        
        //TODO: weitere Eigenschaften hinzufügen

        public CalendarAttendee(Recipient outlookAttendee)
        {
            PropertyAccessor pa = outlookAttendee.PropertyAccessor;
            Id = outlookAttendee.EntryID;
            Name = outlookAttendee.Name;
            Email = pa.GetProperty("http://schemas.microsoft.com/mapi/proptag/0x39FE001E"); //Klingt komisch, ist aber so :/
            //TODO: Alle Response-Stati abbilden
            //Da der Google-Response-Status besser lesbar ist, diesen per default speichern
            Status = (outlookAttendee.MeetingResponseStatus == OlResponseStatus.olResponseAccepted) ? "accepted" : (outlookAttendee.MeetingResponseStatus == OlResponseStatus.olResponseDeclined) ? "declined" : "needsAction";
        }

        public CalendarAttendee(EventAttendee googleAttendee)
        {
            Id = googleAttendee.Id;
            Name = googleAttendee.DisplayName;
            Email = googleAttendee.Email;
            //TODO: Alle Response-Stati abbilden
            //Da der Google-Response-Status besser lesbar ist, diesen per default speichern
            Status = googleAttendee.ResponseStatus;
        }

        public EventAttendee ToGoogleAttendee()
        {
            return new EventAttendee()
            {
                DisplayName = Name,
                Email = Email,
                ResponseStatus = Status
            };
        }
    }
}
