CREATE TABLE [StriveCarSalon].[tblChatGroupRecipient] (
    [ChatGroupRecipientId] BIGINT             IDENTITY (1, 1) NOT NULL,
    [ChatGroupId]          INT                NOT NULL,
    [RecipientId]          INT                NOT NULL,
    [IsRead]               BIT                NULL,
    [CreatedBy]            INT                NULL,
    [CreatedDate]          DATETIMEOFFSET (7) NULL,
    CONSTRAINT [PK_tblChatGroupRecipient] PRIMARY KEY CLUSTERED ([ChatGroupRecipientId] ASC),
    CONSTRAINT [FK_tblChatGroupRecipient_tblChatGroup] FOREIGN KEY ([ChatGroupId]) REFERENCES [StriveCarSalon].[tblChatGroup] ([ChatGroupId])
);

