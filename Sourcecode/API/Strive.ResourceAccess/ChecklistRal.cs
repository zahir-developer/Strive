using Strive.BusinessEntities;
using Strive.BusinessEntities.DTO;
using Strive.BusinessEntities.ViewModel;
using Strive.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Strive.ResourceAccess
{
    public class ChecklistRal : RalBase
    {
        public ChecklistRal(ITenantHelper tenant) : base(tenant) { }
        public List<ChecklistViewModel> GetChecklist()
        {
            return db.Fetch<ChecklistViewModel>(EnumSP.Checklist.USPGETCHECKLIST.ToString(), _prm);
        }
        public bool AddChecklist(ChecklistAddDto checklistAdd)
        {
            return dbRepo.InsertPc(checklistAdd, "ChecklistId");
        }
    }
}
