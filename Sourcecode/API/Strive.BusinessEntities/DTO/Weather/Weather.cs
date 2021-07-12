using System;
using System.Collections.Generic;

namespace Strive.BusinessEntities
{
    public class Values
    {
        public decimal temperature { get; set; }
        public decimal precipitationProbability { get; set; }
        public int precipitationType { get; set; }
    }

    public class Interval
    {
        public DateTime startTime { get; set; }
        public Values values { get; set; }
    }

    public class Timeline
    {
        public string timestep { get; set; }
        public DateTime startTime { get; set; }
        public DateTime endTime { get; set; }
        public List<Interval> intervals { get; set; }
    }

    public class Data
    {
        public List<Timeline> timelines { get; set; }
    }

    public class Temp
    {
        public Min Min { get; set; }
        public Max Max { get; set; }
    }

    public class Precipitation
    {
        public Max Max { get; set; }
    }
    public class Min
    {

        public decimal? Value { get; set; }
        public string Units { get; set; }
    }
    public class Max
    {
        public decimal? Value { get; set; }
        public string Units { get; set; }
    }
    public class PrecipitationProbability
    {
        public int Value { get; set; }
        public string Units { get; set; }
    }
}
