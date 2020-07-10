using System;
namespace Strive.Core.Models.TimInventory
{
    public class EmployeeRole
    {
        public EmployeeRole(string Title, string ImageUri)
        {
            this.Title = Title;
            this.ImageUri = ImageUri;
        }
        public string Title { get; set; }

        public string ImageUri { get; set; }
    }
}
