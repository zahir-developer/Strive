using System;
namespace Strive.Core.Utils
{
    public static class DateUtils
    {
        public static string GetTodayDateString()
        {
            var Date = DateTime.Now;
            return Date.Month.ToString("D2") + "/" + Date.Day.ToString("D2") + "/" + Date.Year;
        }

        public static DateTime GetDateFromString(string dateString)
        {
            return DateTime.Parse(dateString, System.Globalization.CultureInfo.CurrentCulture);
        }
    }
}
