CREATE TABLE [StriveCarSalon].[tblCreditAccountHistory] (
    [CreditAccountHistoryId] INT                IDENTITY (1, 1) NOT NULL,
    [Amount]                 DECIMAL (18, 2)    NULL,
    [Comments]               NVARCHAR (100)     NULL,
    [IsActive]               BIT                NULL,
    [IsDeleted]              BIT                NULL,
    [CreatedBy]              INT                NULL,
    [CreatedDate]            DATETIMEOFFSET (7) NULL,
    [UpdatedBy]              INT                NULL,
    [UpdatedDate]            DATETIMEOFFSET (7) NULL,
    [JobPaymentId]           INT                NULL,
    [TransactionType]        INT                NULL,
    [ClientId]               INT                NULL,
    PRIMARY KEY CLUSTERED ([CreditAccountHistoryId] ASC),
    CONSTRAINT [FK_tblGiftCardHistory_tblClient] FOREIGN KEY ([ClientId]) REFERENCES [StriveCarSalon].[tblClient] ([ClientId])
);



