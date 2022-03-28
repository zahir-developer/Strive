using System;

namespace Strive.BusinessEntities.ViewModel
{
    public class VehicleImageViewModel
    {
        public int VehicleIssueImageId { get; set; }

        public int VehicleIssueId { get; set; }

        public string ImageName { get; set; }

        public DateTimeOffset CreatedDate { get; set; }

        public string OriginalImageName { get; set; }

        public string ThumbnailFileName { get; set; }

        public string Base64Thumbnail { get; set; }

        public string Base64 { get; set; }
    }
}