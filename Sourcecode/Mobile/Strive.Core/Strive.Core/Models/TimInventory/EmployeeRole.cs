using System;
using UIKit;

namespace Strive.Core.Models.TimInventory
{
    public class EmployeeRole
    {
        public EmployeeRole(string Title, string ImageUri, int Tag,UIImage image)
        {
            this.Title = Title;
            this.ImageUri = ImageUri;
            this.Tag = Tag;
            this.image = image;
        }
        public string Title { get; set; }

        public string ImageUri { get; set; }

        public int Tag { get; set; }

        public UIImage image {get; set;}
    }
}
