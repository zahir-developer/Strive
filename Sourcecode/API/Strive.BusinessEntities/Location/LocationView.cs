using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Dapper.Contrib.Extensions;
using KeyAttribute = Dapper.Contrib.Extensions.KeyAttribute;

namespace Strive.BusinessEntities.Location
{
    [Table("tblLocation")]
    public class LocationView : Location
    {
        [Write(false)]
        public List<LocationAddress> LocationAddress { get; set; }
    }


}
