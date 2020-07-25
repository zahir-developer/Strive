CREATE TABLE [StriveCarSalon].[tblNotificationTemplate] (
    [NotificationId]      INT                IDENTITY (1, 1) NOT NULL,
    [NotificationName]    VARCHAR (20)       NULL,
    [NotificationType]    INT                NULL,
    [NotificationMessage] VARCHAR (100)      NULL,
    [LocationId]          INT                NULL,
    [IsInternal]          BIT                NULL,
    [IsActive]            BIT                NULL,
    [IsDeleted]           BIT                NULL,
    [CreatedBy]           INT                NULL,
    [CreatedDate]         DATETIMEOFFSET (7) NULL,
    [UpdatedBy]           INT                NULL,
    [UpdatedDate]         DATETIMEOFFSET (7) NULL
);

