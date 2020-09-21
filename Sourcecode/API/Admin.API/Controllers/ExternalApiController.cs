using Admin.API.Helpers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Strive.BusinessLogic.Admin.ExternalApi;

namespace Admin.Api.Controllers
{
    [AllowAnonymous]
    [Route("/External/")]
    public class ExternalApiController : StriveControllerBase<IExternalApiBpl>
    {
        public ExternalApiController(IExternalApiBpl exCall, IConfiguration config) : base(exCall, config) { }

        #region POST

        /// <summary>
        /// Google Calendar Push Notification
        /// </summary>
        [HttpPost, Route("GcalendarNotify")]
        public void GcalendarNotify() => _bplManager.GoogleCalendarApiWebHook();

        /// <summary>
        /// Google Calendar Push Notification
        /// </summary>
        [HttpPost, Route("GCalendarCreateWatch")]
        public void GcalendarCreateWatch()
        {
            //HttpWebRequest request1 = (HttpWebRequest)WebRequest.Create("https://www.googleapis.com/calendar/v3/calendars/mammothstrive@gmail.com/events/watch");
            //request1.Method = "POST";

            //request1.Headers.Add("Authorization", "Bearer " + accesstoken);
            //request1.ContentType = "application/json";

            //string postData1 = "type=web_hook&id=01234567-89ab-cdef-0123456789ab&address=https://14.141.185.75:5001/External/GcalendarNotify";
            //byte[] bytes1 = Encoding.UTF8.GetBytes(postData1);
            //request1.ContentLength = bytes1.Length;

            //Stream requestStream1 = request1.GetRequestStream();
            //requestStream1.Write(bytes1, 0, bytes1.Length);

            //WebResponse response1 = request1.GetResponse();
            //Stream stream1 = response1.GetResponseStream();
            //StreamReader reader1 = new StreamReader(stream1);

            //var result1 = reader1.ReadToEnd();
            //stream1.Dispose();
            //reader1.Dispose();

        }

        [HttpPost, Route("GenbookSchedule")]
        public void GetGoogleCalendarEvents()
        {
            _bplManager.GetGoogleCalendarEvents();
        }

        #endregion


    }
}