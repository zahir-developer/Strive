using Microsoft.Extensions.Caching.Distributed;
using Strive.BusinessEntities.DTO;
using Strive.Common;
using Strive.ResourceAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Strive.BusinessLogic.Checklist
{
    public class ChecklistBpl : Strivebase, IChecklistBpl
    {
        public ChecklistBpl(IDistributedCache cache, ITenantHelper tenantHelper) : base(tenantHelper, cache) { }
        public Result GetChecklist()
        {
            return ResultWrap(new ChecklistRal(_tenant).GetChecklist, "GetChecklist");
        }
        public Result GetChecklistNotification(ChecklistNotificationDto checklist)
        {
            return ResultWrap(new ChecklistRal(_tenant).GetChecklistNotification, checklist, "ChecklistNotification");
        }
        public Result UpdateChecklistNotification(ChecklistNotificationUpdateDto checklist)
        {
            return ResultWrap(new ChecklistRal(_tenant).UpdateChecklistNotification, checklist, "ChecklistNotification");
        }

        public Result AddChecklist(ChecklistDto checklistAdd)
        {
            foreach (var chk in checklistAdd.CheckListNotification)
            {
                chk.NotificationDate = DateTime.Now;
            }
            return ResultWrap(new ChecklistRal(_tenant).AddChecklist, checklistAdd, "Status");
        }
        public Result UpdateChecklist(ChecklistDto checklistUpdate)
        {
            foreach (var chk in checklistUpdate.CheckListNotification)
            {
                chk.NotificationDate = DateTime.Now;
            }
            return ResultWrap(new ChecklistRal(_tenant).UpdateChecklist, checklistUpdate, "Status");
        }
        public Result DeleteChecklist(int id)
        {
            return ResultWrap(new ChecklistRal(_tenant).DeleteChecklist, id, "DeleteChecklist");
        }

        public Result GetChecklistById(int id)
        {
            return ResultWrap(new ChecklistRal(_tenant).GetChecklistById, id, "ChecklistById");
        }
    }
}
