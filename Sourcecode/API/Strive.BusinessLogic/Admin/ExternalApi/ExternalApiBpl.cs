using Google.Apis.Auth.OAuth2;
using Google.Apis.Calendar.v3;
using System.Security.Cryptography.X509Certificates;

namespace Strive.BusinessLogic.Admin.ExternalApi
{
    public class ExternalApiBpl : IExternalApiBpl
    {
        public void GetGoogleCalendarEvents()
        {

            var gSvc = GoogleCalendarAuthentication.AuthenticateServiceAccount("mammothstrive@gmail.com", "d:\\mammothstrive-3cb770bcf118.json", new string[] { CalendarService.Scope.Calendar, CalendarService.Scope.CalendarReadonly });
            var lstCalendar = gSvc.CalendarList.Get("primary");
           // var evts = gSvc.Events.List();

            //           string[] scopes = new string[] {
            //    CalendarService.Scope.Calendar, // Manage your calendars
            //	CalendarService.Scope.CalendarReadonly // View your Calendars
            //};
            //           var certificate = new X509Certificate2(keyFilePath, "notasecret", X509KeyStorageFlags.Exportable);

            //           ServiceAccountCredential credential = new ServiceAccountCredential(
            //                new ServiceAccountCredential.Initializer("mammothstrive@gmail.com")
            //                {
            //                    Scopes = scopes
            //                }.FromCertificate(certificate));

        }

        public void GoogleCalendarApiWebHook()
        {
            Strive.Common.Utility.WriteLog("d:\\", "googlecalendar.txt", "google callback successful.");
        }
    }
}
