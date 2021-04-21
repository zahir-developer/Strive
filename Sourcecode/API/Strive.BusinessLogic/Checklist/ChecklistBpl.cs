﻿using Microsoft.Extensions.Caching.Distributed;
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
        public Result AddChecklist(ChecklistDto checklistAdd)
        {
            return ResultWrap(new ChecklistRal(_tenant).AddChecklist, checklistAdd, "Status");
        }
        public Result UpdateChecklist(ChecklistDto checklistUpdate)
        {
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
