using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Dapper.Contrib.Extensions;
using KeyAttribute = Dapper.Contrib.Extensions.KeyAttribute;

namespace Strive.BusinessEntities.Location
{
    public class LocationView : Location
    {
        public LocationAddress LocationAddress { get; set; }
    }


}
