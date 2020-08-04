using Strive.BusinessEntities.Model;

namespace Strive.BusinessEntities.DTO
{
    public class LocationDto
    {
        public Model.Location Location { get; set; }
        public LocationAddress LocationAddress { get; set; }
        public Drawer Drawer { get; set; }
    }
}
