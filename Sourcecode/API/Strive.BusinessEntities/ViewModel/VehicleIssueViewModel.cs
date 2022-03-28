using System;

namespace Strive.BusinessEntities.ViewModel
{
    public class VehicleIssueViewModel
    {
        public int VehicleIssueId { get; set; }

        public string Description { get; set; }

        public DateTimeOffset CreatedDate { get; set; }

        public string CreatedBy { get; set; }

    }
}