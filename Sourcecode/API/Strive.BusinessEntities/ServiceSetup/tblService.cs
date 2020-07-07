using System;
using System.Collections.Generic;
using System.Text;

namespace Strive.BusinessEntities.ServiceSetup
{
    public class tblService
    {
        public int ServiceId { get; set; }
        public string ServiceName { get; set; }
        public int ServiceType { get; set; }
        public int LocationId { get; set; }
        public float Cost { get; set; }
        public bool Commision { get; set; }
        public int CommisionType { get; set; }
        public float Upcharges { get; set; }
        public int ParentServiceId { get; set; }
        public bool IsActive { get; set; }
        public DateTime DateEntered { get; set; }
    }
}
