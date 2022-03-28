
-- =============================================
-- Author:		Vetriselvi
-- Create date: 22-02-2022
-- Description:	update checklist status
-- 
-- =============================================
----------History------------
-- =============================================

-- =============================================   
  
  
CREATE PROCEDURE [StriveCarSalon].[uspUpdateCheckListNotification]  
@CheckListEmployeeId INT,  
@userId INT,  
@IsCompleted BIT,  
@date DATETIME,  
@CheckListNotificationId INT = NULL,  
@EmployeeId INT =NULL  
AS  
BEGIN  
 UPDATE [tblCheckListEmployeeNotification]  
 SET IsCompleted = @IsCompleted,  
 UpdatedBy = @userId,  
 UpdatedDate = @date  
 WHERE CheckListEmployeeId = @CheckListEmployeeId  
  
END  