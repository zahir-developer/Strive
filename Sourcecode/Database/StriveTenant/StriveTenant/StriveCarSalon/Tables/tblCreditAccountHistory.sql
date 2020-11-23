CREATE TABLE [StriveCarSalon].[tblCreditAccountHistory] (
    [CreditAccountHistoryId] INT                IDENTITY (1, 1) NOT NULL,
    [CreditAccountId]        INT                NULL,
    [Amount]                 DECIMAL (18)       NULL,
    [Comments]               VARCHAR (20)       NULL,
    [IsActive]               BIT                NULL,
    [IsDeleted]              BIT                NULL,
    [CreatedBy]              INT                NULL,
    [CreatedDate]            DATETIMEOFFSET (7) NULL,
    [UpdatedBy]              INT                NULL,
    [UpdatedDate]            DATETIMEOFFSET (7) NULL,
    PRIMARY KEY CLUSTERED ([CreditAccountHistoryId] ASC),
    CONSTRAINT [FK_tblCreditAccountHistory_CreditAccountId] FOREIGN KEY ([CreditAccountId]) REFERENCES [StriveCarSalon].[tblCreditAccount] ([CreditAccountId])
);

