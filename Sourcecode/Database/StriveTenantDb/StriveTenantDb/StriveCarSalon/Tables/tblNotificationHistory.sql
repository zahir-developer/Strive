CREATE TABLE [StriveCarSalon].[tblNotificationHistory] (
    [NotificationHistoryId] BIGINT         IDENTITY (1, 1) NOT NULL,
    [NotificationType]      INT            NULL,
    [NotificationMessage]   NVARCHAR (400) NULL,
    [LocationId]            INT            NULL,
    [CreatedDate]           DATETIME       NULL,
    CONSTRAINT [PK_tblNotificationHistory] PRIMARY KEY CLUSTERED ([NotificationHistoryId] ASC)
);

