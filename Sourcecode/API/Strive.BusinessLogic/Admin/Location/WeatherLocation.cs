using System;
using System.Collections.Generic;
using System.Text;

namespace Strive.BusinessLogic.Location
{
    public class WeatherLocation
    {
        public string name { get; set; }
        public point point { get; set; }
    }

    public class point
    {
        public double lat { get; set; }
        public double lon { get; set; }
    }
}
