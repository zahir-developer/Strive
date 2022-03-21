
-- =============================================
-- Author:		Vetriselvi
-- Create date: 22-02-2022
-- Description:	To get Checklist available for specific employee
--  --
/*
--EXEC [StriveCarSalon].[uspGetCheckNotificationList] 1138, 1, '2022-03-03 10:40:01'   
*/
-- =============================================
----------History------------
-- =============================================

-- =============================================
    
CREATE PROCEDURE [StriveCarSalon].[uspGetCheckNotificationList]    
@employeeId INT,    
@Role INT,    
@date DATETIME    
AS    
BEGIN    
    
  SELECT cl.ChecklistId,  
   cl.Name,  
   NotificationTime,  
   CheckListEmployeeId  
 FROM [tblChecklist] cl  
 JOIN [tblCheckListNotification] cln ON cl.ChecklistId = cln.ChecklistId  
 JOIN [tblCheckListEmployeeNotification] en ON en.CheckListNotificationId = cln.CheckListNotificationId  
 WHERE cl.RoleId= @Role   
 AND cl.IsActive = 1 AND cl.IsDeleted = 0  
 AND en.IsActive = 1 AND en.IsDeleted = 0  
 AND cln.IsActive = 1 AND cln.IsDeleted = 0  
 AND CAST(ISNULL(cln.NotificationDate,GETDATE()) AS DATE) = CAST(@date AS DATE)  
 AND ISNULL(en.IsCompleted,0) = 0 and en.employeeId = @employeeId  
END    
