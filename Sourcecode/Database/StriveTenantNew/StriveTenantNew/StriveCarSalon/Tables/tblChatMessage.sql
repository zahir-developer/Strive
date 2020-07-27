CREATE TABLE [StriveCarSalon].[tblChatMessage] (
    [ChatMessageId]       BIGINT             IDENTITY (1, 1) NOT NULL,
    [Subject]             VARCHAR (15)       NULL,
    [Messagebody]         NVARCHAR (1000)    NULL,
    [ParentChatMessageId] BIGINT             NULL,
    [ExpiryDate]          DATETIMEOFFSET (7) NULL,
    [IsReminder]          BIT                NULL,
    [NextRemindDate]      DATETIME           NULL,
    [ReminderFrequencyId] INT                NULL,
    [CreatedBy]           INT                NULL,
    [CreatedDate]         DATETIMEOFFSET (7) NULL,
    CONSTRAINT [PK_tblChatMessage] PRIMARY KEY CLUSTERED ([ChatMessageId] ASC),
    CONSTRAINT [FK_tblChatMessage_tblReminderFrequency] FOREIGN KEY ([ReminderFrequencyId]) REFERENCES [StriveCarSalon].[tblReminderFrequency] ([ReminderFrequencyId])
);

