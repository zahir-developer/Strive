namespace Strive.BusinessEntities.ViewModel
{
    public class TimeClockWeekSummary
    {

        public string TotalWashHours { get; set; }

        public string TotalDetailHours { get; set; }

        public int OverTimeHours { get; set; }

        public decimal? WashRate { get; set; }

        public decimal? DetailRate { get; set; }

        public decimal? WashAmount { get; set; }

        public decimal? DetailAmount { get; set; }

        public decimal? OverTimePay { get; set; }

        public decimal? CollisionAmount { get; set; }

        public decimal? GrandTotal { get; set; }



    }
}