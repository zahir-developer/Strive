CREATE TABLE [StriveCarSalon].[tblChatMessageRecipient] (
    [ChatRecipientId]  BIGINT IDENTITY (1, 1) NOT NULL,
    [ChatMessageId]    BIGINT NULL,
    [RecipientId]      INT    NULL,
    [RecipientGroupId] INT    NULL,
    [IsRead]           BIT    NULL,
    [SenderId]         INT    NULL,
    CONSTRAINT [PK_tblChatMessageRecipient] PRIMARY KEY CLUSTERED ([ChatRecipientId] ASC),
    CONSTRAINT [FK_tblChatMessageRecipient_ChatMessageId] FOREIGN KEY ([ChatMessageId]) REFERENCES [StriveCarSalon].[tblChatMessage] ([ChatMessageId]),
    CONSTRAINT [FK_tblChatMessageRecipient_RecipientGroupId] FOREIGN KEY ([RecipientGroupId]) REFERENCES [StriveCarSalon].[tblChatGroup] ([ChatGroupId]),
    CONSTRAINT [FK_tblChatMessageRecipient_SenderId] FOREIGN KEY ([SenderId]) REFERENCES [StriveCarSalon].[tblEmployee] ([EmployeeId])
);






GO
CREATE NONCLUSTERED INDEX [IX_tblChatMessageRecipient_ChatMessageId]
    ON [StriveCarSalon].[tblChatMessageRecipient]([ChatMessageId] ASC);


GO
CREATE NONCLUSTERED INDEX [IX_tblChatMessageRecipient_RecipientGroupId]
    ON [StriveCarSalon].[tblChatMessageRecipient]([RecipientGroupId] ASC);


GO
CREATE NONCLUSTERED INDEX [IX_tblChatMessageRecipient_SenderId]
    ON [StriveCarSalon].[tblChatMessageRecipient]([SenderId] ASC);

