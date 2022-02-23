CREATE PROCEDURE [StriveCarSalon].uspUpdateCheckListNotification
@CheckListEmployeeId INT,
@userId INT,
@IsCompleted BIT,
@date DATETIME
AS
BEGIN

	UPDATE [tblCheckListEmployeeNotification]
	SET IsCompleted = @IsCompleted,
	UpdatedBy = @userId,
	UpdatedDate = @date
	WHERE CheckListEmployeeId = @CheckListEmployeeId

END
GO

