CREATE TABLE [StriveCarSalon].[tblNotificationHistory] (
    [NotificationHistoryId] INT                IDENTITY (1, 1) NOT NULL,
    [NotificationType]      INT                NULL,
    [NotificationMessage]   VARCHAR (100)      NULL,
    [LocationId]            INT                NULL,
    [IsActive]              BIT                NULL,
    [IsDeleted]             BIT                NULL,
    [CreatedBy]             INT                NULL,
    [CreatedDate]           DATETIMEOFFSET (7) NULL,
    [UpdatedBy]             INT                NULL,
    [UpdatedDate]           DATETIMEOFFSET (7) NULL
);

