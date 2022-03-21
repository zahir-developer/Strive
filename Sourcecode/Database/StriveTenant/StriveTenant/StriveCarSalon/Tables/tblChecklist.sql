CREATE TABLE [StriveCarSalon].[tblChecklist] (
    [ChecklistId] INT           IDENTITY (1, 1) NOT NULL,
    [Name]        VARCHAR (255) NULL,
    [RoleId]      INT           NULL,
    [IsActive]    BIT           NULL,
    [IsDeleted]   BIT           NULL,
    CONSTRAINT [PK__tblCheck__4C1D499AD706CA4E] PRIMARY KEY CLUSTERED ([ChecklistId] ASC)
);




GO

  create TRIGGER [StriveCarSalon].tr_tblChecklist_UpdateEmployees
    ON [StriveCarSalon].[tblChecklist]
    AFTER  UPDATE
AS
BEGIN

   declare @ChecklistId int ,@CheckListNotificationId int,	@IsActive BIT,	@IsDeleted BIT,	@CreatedBy INT,@RoleId INT
    SET NOCOUNT ON;  
    SELECT @ChecklistId = ChecklistId from inserted  

	SELECT	@CheckListNotificationId = cln.CheckListNotificationId,
			@IsActive = cl.IsActive,
			@IsDeleted = cl.IsDeleted,
			@CreatedBy = cln.CreatedBy,
			@RoleId = cl.RoleId
	FROM	[tblChecklist] cl
	JOIN	[tblCheckListNotification] cln ON cl.ChecklistId = cln.ChecklistId
    WHERE cl.ChecklistId = @ChecklistId

	--UPDATE [tblCheckListNotification]
	--SET NotificationDate = getdate()
	--WHERE ChecklistId = @ChecklistId

	IF(ISNULL(@IsDeleted,0) = 1)
	BEGIN
		UPDATE [tblCheckListEmployeeNotification]
		SET IsDeleted = @IsDeleted
		WHERE CheckListNotificationId = @CheckListNotificationId
	END
	ELSE
	BEGIN
		UPDATE [tblCheckListEmployeeNotification]
		SET IsDeleted = 1
		WHERE CheckListNotificationId = @CheckListNotificationId

		INSERT INTO [tblCheckListEmployeeNotification] (CheckListNotificationId,IsActive,IsDeleted,CreatedBy,CreatedDate,UpdatedBy,UpdatedDate,EmployeeId,IsCompleted)
		SELECT @CheckListNotificationId,@IsActive,@IsDeleted,@CreatedBy,GETDATE(),@CreatedBy,GETDATE(),EmployeeId,0
		FROM tblEmployeeRole
		WHERE RoleId = @RoleId
	END
END