CREATE TYPE [StriveSuperAdminTest].[tvpGiftCardHistory] AS TABLE (
    [GiftCardHistoryId] INT            NOT NULL,
    [GiftCardId]        INT            NULL,
    [LocationId]        INT            NULL,
    [TransactionType]   INT            NULL,
    [TransactionAmount] DECIMAL (18)   NULL,
    [TransactionDate]   DATETIME       NULL,
    [Comments]          NVARCHAR (200) NULL,
    [IsActive]          BIT            NULL,
    [IsDeleted]         BIT            NULL);

