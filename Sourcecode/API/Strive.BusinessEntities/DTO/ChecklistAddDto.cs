using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Strive.BusinessEntities.DTO
{
    public class ChecklistAddDto
    {
        public Model.Checklist Checklist { get; set; }

        public List<Model.CheckListNotification> CheckListNotification { get; set; }
    }
}
