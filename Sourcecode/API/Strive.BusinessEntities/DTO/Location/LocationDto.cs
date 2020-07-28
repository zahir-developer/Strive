using Model = Strive.BusinessEntities.Model;

namespace Strive.BusinessEntities.Location
{
    public class LocationDto
    {
        public Model.Location Location { get; set; }
        public Model.LocationAddress LocationAddress { get; set; }
    }
}
