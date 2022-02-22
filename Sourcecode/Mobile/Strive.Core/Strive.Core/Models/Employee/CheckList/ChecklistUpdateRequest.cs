using System;
using System.Collections.Generic;

namespace Strive.Core.Models.Employee.CheckList
{
    public class ChecklistUpdateRequest
    { 
        public List<checklistupdate> CheckListNotification { get; set; }
    }
    public class checklistupdate
    {
        public int CheckListEmployeeId { get; set; }
        public bool IsCompleted { get; set; }
        public string NotificationDate{ get; set; }
        public int CheckListNotificationId { get; set; }
        public int UserId { get; set; }
    }
}
