
namespace Strive.Core.Models.TimInventory
{
    public class EmployeeRole
    {
        public EmployeeRole(string Title, string ImageUri, int Tag,string ImageUriHover)
        {
            this.Title = Title;
            this.ImageUri = ImageUri;
            this.Tag = Tag;
            this.ImageUriHover = ImageUriHover;
        }
        public string Title { get; set; }

        public string ImageUri { get; set; }

        public int Tag { get; set; }

        public string ImageUriHover {get; set;}
    }
}
