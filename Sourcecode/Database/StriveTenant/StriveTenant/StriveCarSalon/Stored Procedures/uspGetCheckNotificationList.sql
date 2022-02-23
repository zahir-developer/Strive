
--EXEC [StriveCarSalon].[uspGetCheckNotificationList] 1138, 1, NULL  
  
CREATE PROCEDURE [StriveCarSalon].[uspGetCheckNotificationList]  
@employeeId INT,  
@Role INT,  
@date DATETIME  
AS  
BEGIN  
  
  SELECT	cl.ChecklistId,
			cl.Name,
			NotificationTime,
			CheckListEmployeeId
	FROM	[tblChecklist] cl
	JOIN	[tblCheckListNotification] cln ON cl.ChecklistId = cln.ChecklistId
	JOIN	[tblCheckListEmployeeNotification] en ON en.CheckListNotificationId = cln.CheckListNotificationId
	WHERE	cl.RoleId= @Role 
	AND cl.IsActive = 1 AND cl.IsDeleted = 0
	AND en.IsActive = 1 AND en.IsDeleted = 0
	AND CAST(ISNULL(cln.NotificationDate,GETDATE()) AS DATE) = CAST(@date AS DATE)
	AND ISNULL(en.IsCompleted,0) = 0 and en.employeeId = @employeeId

 --SELECT cl.ChecklistId,  
 --cl.Name,  
 --NotificationTime,  
 --cln.CheckListNotificationId,  
 --en.CheckListEmployeeId,  
 --(Select IsCompleted from [tblCheckListEmployeeNotification] where CheckListNotificationId =  cln.CheckListNotificationId and employeeId = @employeeId) as IsCompleted   
 --FROM [tblChecklist] cl  
 --JOIN [tblCheckListNotification] cln ON cl.ChecklistId = cln.ChecklistId  
 --LEFT JOIN [tblCheckListEmployeeNotification] en on en.CheckListNotificationId = cln.CheckListNotificationId  
 --WHERE cl.RoleId= @Role   
 --AND cl.IsActive = 1 AND cl.IsDeleted = 0  
 --AND ((CAST(cln.NotificationDate AS DATE) = CAST(@date AS DATE)) OR @date IS NULL)  
 --AND ISNULL(en.IsCompleted ,0)= 0

 ----AND (en.IsCompleted = 0 OR en.IsCompleted is NULL)  
 -- AND en.EmployeeId = @employeeId
END