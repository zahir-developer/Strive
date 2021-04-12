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
        public int AddChecklist(ChecklistAddDto checklistAdd)
        {
            return dbRepo.InsertPK(checklistAdd, "ChecklistId");
        }
        public bool DeleteChecklist(int id)
        {
            _prm.Add("ChecklistId", id.toInt());
            db.Save(EnumSP.Checklist.USPDELETECHECKLIST.ToString(), _prm);
            return true;
        }
        public List<ChecklistDetailViewModel> GetChecklistById(int id)
        {
            _prm.Add("ChecklistId", id.toInt());
            return db.Fetch<ChecklistDetailViewModel>(EnumSP.Checklist.USPGETCHECKLISTBYID.ToString(), _prm);
        }
    }
}
