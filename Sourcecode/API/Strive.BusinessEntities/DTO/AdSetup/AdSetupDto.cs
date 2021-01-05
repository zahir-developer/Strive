﻿using Strive.BusinessEntities.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Strive.BusinessEntities.DTO.AdSetup
{
    public class AdSetupDto
    {
        public DocumentDto Document { get; set; }
        public AdSetupAddDto AdSetupAddDto { get; set; }
        public DocumentDto RemoveDocument { get; set; }
    }
}
