CREATE TABLE [StriveCarSalon].[tblGiftCardHistory] (
    [GiftCardHistoryId] INT             IDENTITY (1, 1) NOT NULL,
    [GiftCardId]        INT             NULL,
    [LocationId]        INT             NULL,
    [TransactionType]   INT             NULL,
    [TransactionAmount] DECIMAL (16, 2) NULL,
    [TransactionUserId] INT             NULL,
    [TransactionDate]   DATETIME        NULL,
    [CreatedBy]         INT             NULL,
    [CreatedDate]       DATETIME        NULL,
    [Comments]          NVARCHAR (50)   NULL,
    CONSTRAINT [PK_tblGiftCardHistory] PRIMARY KEY CLUSTERED ([GiftCardHistoryId] ASC),
    CONSTRAINT [FK_tblGiftCardHistory_tblGiftCard] FOREIGN KEY ([GiftCardId]) REFERENCES [StriveCarSalon].[tblGiftCard] ([GiftCardId]),
    CONSTRAINT [FK_tblGiftCardHistory_tblLocation] FOREIGN KEY ([LocationId]) REFERENCES [StriveCarSalon].[tblLocation] ([LocationId])
);

