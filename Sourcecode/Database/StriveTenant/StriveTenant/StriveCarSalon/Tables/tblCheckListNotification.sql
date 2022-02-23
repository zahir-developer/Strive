CREATE TABLE [StriveCarSalon].[tblCheckListNotification] (
    [CheckListNotificationId] INT                IDENTITY (1, 1) NOT NULL,
    [IsActive]                BIT                NULL,
    [IsDeleted]               BIT                NULL,
    [CreatedBy]               INT                NULL,
    [CreatedDate]             DATETIMEOFFSET (7) NULL,
    [UpdatedBy]               INT                NULL,
    [UpdatedDate]             DATETIMEOFFSET (7) NULL,
    [NotificationTime]        TIME (7)           NULL,
    [ChecklistId]             INT                NULL,
    [NotificationDate]        DATETIME           NULL,
    CONSTRAINT [PK_tblCheckListNotification] PRIMARY KEY CLUSTERED ([CheckListNotificationId] ASC),
    CONSTRAINT [FK_tblCheckListNotification_ChecklistId] FOREIGN KEY ([ChecklistId]) REFERENCES [StriveCarSalon].[tblChecklist] ([ChecklistId])
);




GO

CREATE TRIGGER [StriveCarSalon].tr_tblChecklist_InsertEmployees
    ON [StriveCarSalon].[tblCheckListNotification]
    AFTER INSERT
AS
BEGIN

   declare @CheckListNotificationId int,	@IsActive BIT,	@IsDeleted BIT,	@CreatedBy INT,@RoleId INT
    SET NOCOUNT ON;  
    SELECT @CheckListNotificationId = CheckListNotificationId from inserted  

	
	SELECT	@IsActive = cl.IsActive,
			@IsDeleted = cl.IsDeleted,
			@CreatedBy = cln.CreatedBy,
			@RoleId = cl.RoleId
	FROM	[tblChecklist] cl
	JOIN	[tblCheckListNotification] cln ON cl.ChecklistId = cln.ChecklistId
    WHERE cln.CheckListNotificationId = @CheckListNotificationId

	
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