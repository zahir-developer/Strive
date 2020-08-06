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

        public static string GetStringFromDate(DateTime date)
        {  
            return date.ToString("yyyy-MM-dd'T'HH:mm:ss");
        }

        public static string GetClockInTypeString(string DateString)
        {
            var Date = GetDateFromString(DateString);
            return string.Format("{0:hh.mmtt}", Date);
        }

        public static string GetTimeDifferenceString(string OutTime, string InTime)
        {
            TimeSpan difference = GetDateFromString(OutTime) - DateUtils.GetDateFromString(InTime);
            return string.Format("{0:D2}.{1:D2}", difference.Hours, difference.Minutes);
        }

        public static TimeSpan GetTimeDifferenceValue(string OutTime, string InTime)
        {
            return GetDateFromString(OutTime) - DateUtils.GetDateFromString(InTime);
        }
    }
}
