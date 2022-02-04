using Cocoon.ORM;
using Strive.Common;
using System;
using System.Collections.Generic;

namespace Strive.BusinessEntities.Model
{
    public class VehicleImageDto
    {
        public VehicleIssueImage VehicleImage { get; set; }

        public GlobalUpload.DocumentType DocumentType { get; set; }

    }
}