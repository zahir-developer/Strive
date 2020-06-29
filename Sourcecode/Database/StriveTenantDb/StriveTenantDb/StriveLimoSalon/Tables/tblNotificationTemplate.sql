CREATE TABLE [StriveLimoSalon].[tblNotificationTemplate] (
    [NotificationId]      BIGINT         IDENTITY (1, 1) NOT NULL,
    [NotificationName]    NVARCHAR (20)  NULL,
    [NotificationType]    INT            NULL,
    [NotificationMessage] NVARCHAR (500) NULL,
    [LocationId]          INT            NULL,
    [IsInternal]          BIT            NULL,
    [IsActive]            BIT            NULL,
    [CreatedDate]         DATETIME       NULL,
    CONSTRAINT [PK_tblNotificationTemplate] PRIMARY KEY CLUSTERED ([NotificationId] ASC),
    CONSTRAINT [FK_tblNotificationTemplate_tblLocation] FOREIGN KEY ([LocationId]) REFERENCES [StriveLimoSalon].[tblLocation] ([LocationId])
);

