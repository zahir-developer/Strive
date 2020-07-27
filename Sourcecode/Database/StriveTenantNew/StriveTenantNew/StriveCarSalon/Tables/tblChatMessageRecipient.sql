CREATE TABLE [StriveCarSalon].[tblChatMessageRecipient] (
    [ChatRecipientId]  BIGINT IDENTITY (1, 1) NOT NULL,
    [ChatMessageId]    BIGINT NULL,
    [RecipientGroupId] INT    NULL,
    [IsRead]           BIT    NULL,
    [SenderId]         INT    NULL,
    CONSTRAINT [PK_tblChatMessageRecipient] PRIMARY KEY CLUSTERED ([ChatRecipientId] ASC),
    CONSTRAINT [FK_tblChatMessageRecipient_tblChatMessage] FOREIGN KEY ([ChatMessageId]) REFERENCES [StriveCarSalon].[tblChatMessage] ([ChatMessageId]),
    CONSTRAINT [FK_tblChatMessageRecipient_tblChatUserGroup] FOREIGN KEY ([RecipientGroupId]) REFERENCES [StriveCarSalon].[tblChatUserGroup] ([ChatGroupUserId]),
    CONSTRAINT [FK_tblChatMessageRecipient_tblEmployee] FOREIGN KEY ([SenderId]) REFERENCES [StriveCarSalon].[tblEmployee] ([EmployeeId])
);

