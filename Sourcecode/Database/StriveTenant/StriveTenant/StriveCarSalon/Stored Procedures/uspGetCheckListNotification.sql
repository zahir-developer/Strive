
-- =============================================
-- Author:		Vetriselvi
-- Create date: 22-02-2022
-- Description:	To get Checklist available for the supplied date
--  --
/*
--[uspGetCheckListNotification] '2022-02-25 10:40:01'  
*/
-- =============================================
----------History------------
-- =============================================

-- =============================================
    

CREATE PROCEDURE [StriveCarSalon].[uspGetCheckListNotification]    
@date DATETIME    
AS    
BEGIN    
 
  SELECT cl.ChecklistId,  
   cl.Name,  
   NotificationTime,  
   CheckListEmployeeId,  
   cl.RoleId,  
   cln.NotificationDate,  
   e.Token  
 FROM [tblChecklist] cl  
 JOIN [tblCheckListNotification] cln ON cl.ChecklistId = cln.ChecklistId  
 JOIN [tblCheckListEmployeeNotification] en ON en.CheckListNotificationId = cln.CheckListNotificationId  
 join tblEmployee e ON e.EmployeeId = en.employeeId  
 WHERE cl.IsActive = 1 AND cl.IsDeleted = 0  
 AND en.IsActive = 1 AND en.IsDeleted = 0  
 AND CAST(ISNULL(cln.NotificationDate,GETDATE()) AS DATE) = CAST(@date AS DATE)  
 AND datepart(minute, cln.NotificationTime)  = datepart(minute, @date)  
 AND datepart(HOUR, cln.NotificationTime)  = datepart(HOUR, @date)  
 AND ISNULL(en.IsCompleted,0) = 0 AND ISNULL(e.Token,'') != ''  
  
END    
  
