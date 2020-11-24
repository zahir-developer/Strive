using Cocoon.ORM;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Strive.BusinessEntities.Model
{
    [OverrideName("tblChecklist")]
    public class Checklist
    {
        [Column, PrimaryKey, IgnoreOnInsert, IgnoreOnUpdate]
        public int ChecklistId { get; set; }        

        [Column]
        public string Name { get; set; }

        [Column]
        public int? RoleId { get; set; }
    }
}
