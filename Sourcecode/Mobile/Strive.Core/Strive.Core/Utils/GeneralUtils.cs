using System;
namespace Strive.Core.Utils
{
    public static class GeneralUtils
    {
        public static string GetTodayDateString()
        {
            var Date = DateTime.Now;
            return Date.Month.ToString("D2") + "/" + Date.Day.ToString("D2") + "/" + Date.Year;
        }
    }
}
