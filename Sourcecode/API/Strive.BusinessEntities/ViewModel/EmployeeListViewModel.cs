﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Strive.BusinessEntities.ViewModel
{
    public class EmployeeListViewModel
    {
        public List<EmployeeList> Employee { get; set; }
        public List<EmployeeRoleListViewModel> EmployeeRole { get; set; }
    }
}
