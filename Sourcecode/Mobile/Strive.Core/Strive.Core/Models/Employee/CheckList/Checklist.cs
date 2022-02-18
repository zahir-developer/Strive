using System;
using System.Collections.Generic;

namespace Strive.Core.Models.Employee.CheckList
{
    public class Checklist
    {
        public List<ChecklistData> ChecklistNotification { get; set; }
    }
    public class ChecklistData
    {
        public string Name { get; set; }
        public string NotificationTime { get; set; }
        public int CheckListId { get; set; }
        public int ChecklistNotificationId { get; set; }
        public int CheckListEmployeeId { get; set; }

    }

}
