﻿using Strive.BusinessEntities.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Strive.BusinessEntities.DTO
{
    public class DetailsDto
    {
        public Model.Job Job { get; set; }
        public Model.JobDetail JobDetail { get; set; }
        public List<JobItem> JobItem { get; set; }
    }
}
