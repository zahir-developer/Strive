CREATE TABLE [StriveCarSalon].[tblCheckListEmployeeNotification] (
    [CheckListEmployeeId]     INT                IDENTITY (1, 1) NOT NULL,
    [CheckListNotificationId] INT                NOT NULL,
    [IsActive]                BIT                NULL,
    [IsDeleted]               BIT                NULL,
    [CreatedBy]               INT                NULL,
    [CreatedDate]             DATETIMEOFFSET (7) NULL,
    [UpdatedBy]               INT                NULL,
    [UpdatedDate]             DATETIMEOFFSET (7) NULL,
    [EmployeeId]              INT                NOT NULL,
    [IsCompleted]             BIT                NULL,
    CONSTRAINT [PK_tblCheckListEmployeeNotification] PRIMARY KEY CLUSTERED ([CheckListEmployeeId] ASC),
    CONSTRAINT [FK_tblCheckListEmployeeNotification_tblCheckListNotification] FOREIGN KEY ([CheckListNotificationId]) REFERENCES [StriveCarSalon].[tblCheckListNotification] ([CheckListNotificationId]),
    CONSTRAINT [FK_tblCheckListEmployeeNotification_tblEmployee] FOREIGN KEY ([EmployeeId]) REFERENCES [StriveCarSalon].[tblEmployee] ([EmployeeId])
);

