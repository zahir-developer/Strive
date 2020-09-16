CREATE TABLE [CON].[tblChatMessageRecipient] (
    [ChatRecipientId]  BIGINT IDENTITY (1, 1) NOT NULL,
    [ChatMessageId]    BIGINT NULL,
    [RecipientGroupId] INT    NULL,
    [IsRead]           BIT    NULL,
    [SenderId]         INT    NULL,
    CONSTRAINT [PK_tblChatMessageRecipient] PRIMARY KEY CLUSTERED ([ChatRecipientId] ASC),
    CONSTRAINT [FK_tblChatMessageRecipient_ChatMessageId] FOREIGN KEY ([ChatMessageId]) REFERENCES [CON].[tblChatMessage] ([ChatMessageId]),
    CONSTRAINT [FK_tblChatMessageRecipient_RecipientGroupId] FOREIGN KEY ([RecipientGroupId]) REFERENCES [CON].[tblChatUserGroup] ([ChatGroupUserId]),
    CONSTRAINT [FK_tblChatMessageRecipient_SenderId] FOREIGN KEY ([SenderId]) REFERENCES [CON].[tblEmployee] ([EmployeeId])
);


GO
CREATE NONCLUSTERED INDEX [IX_tblChatMessageRecipient_ChatMessageId]
    ON [CON].[tblChatMessageRecipient]([ChatMessageId] ASC);


GO
CREATE NONCLUSTERED INDEX [IX_tblChatMessageRecipient_RecipientGroupId]
    ON [CON].[tblChatMessageRecipient]([RecipientGroupId] ASC);


GO
CREATE NONCLUSTERED INDEX [IX_tblChatMessageRecipient_SenderId]
    ON [CON].[tblChatMessageRecipient]([SenderId] ASC);

