CREATE TABLE [StriveCarSalon].[tblCreditAccount] (
    [CreditAccountId] INT                IDENTITY (1, 1) NOT NULL,
    [ClientId]        INT                NULL,
    [Amount]          DECIMAL (18)       NULL,
    [Comments]        VARCHAR (20)       NULL,
    [IsActive]        BIT                NULL,
    [IsDeleted]       BIT                NULL,
    [CreatedBy]       INT                NULL,
    [CreatedDate]     DATETIMEOFFSET (7) NULL,
    [UpdatedBy]       INT                NULL,
    [UpdatedDate]     DATETIMEOFFSET (7) NULL,
    PRIMARY KEY CLUSTERED ([CreditAccountId] ASC),
    CONSTRAINT [FK_tblCreditAccount_ClientId] FOREIGN KEY ([ClientId]) REFERENCES [StriveCarSalon].[tblClient] ([ClientId])
);

