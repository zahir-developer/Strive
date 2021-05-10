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
    CONSTRAINT [PK_tblCheckListNotification] PRIMARY KEY CLUSTERED ([CheckListNotificationId] ASC),
    CONSTRAINT [FK_tblCheckListNotification_ChecklistId] FOREIGN KEY ([ChecklistId]) REFERENCES [StriveCarSalon].[tblChecklist] ([ChecklistId])
);

