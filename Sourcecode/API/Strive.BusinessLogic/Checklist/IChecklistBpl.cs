using Strive.BusinessEntities.DTO;
using Strive.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Strive.BusinessLogic.Checklist
{
    public interface IChecklistBpl
    {
        Result GetChecklist();
        Result AddChecklist(ChecklistDto checklistAdd);
        Result UpdateChecklist(ChecklistDto checklistUpdate);
        Result DeleteChecklist(int id);
        Result GetChecklistById(int id);
        Result GetChecklistNotification(ChecklistNotificationDto checklistNotification);
        Result UpdateChecklistNotification(ChecklistNotificationUpdateDto checklist);
    }
}
