﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Strive.BusinessEntities.ViewModel
{
    public class MembershipServiceViewModel
    {
        public int MembershipServiceId { get; set; }
        public int MembershipId { get; set; }
        public int ServiceId { get; set; }
        public bool IsActive { get; set; }
        public bool IsDeleted { get; set; }
        public string ServiceType { get; set; }
    }
}
