--[uspUpdateCheckListNotification] 524,1138,1,'2022-02-22'
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

	--IF @CheckListEmployeeId = 0 
	--BEGIN
	--INSERT INTO [tblCheckListEmployeeNotification] (CheckListNotificationId, EmployeeId, IsCompleted, IsActive, IsDeleted, CreatedDate) 
	--Values (@CheckListNotificationId, @EmployeeId, 1, 1, 0, GETDATE())
	--END
END