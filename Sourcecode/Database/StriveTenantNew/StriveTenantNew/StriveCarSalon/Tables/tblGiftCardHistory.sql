CREATE TABLE [StriveCarSalon].[tblGiftCardHistory] (
    [GiftCardHistoryId] INT                IDENTITY (1, 1) NOT NULL,
    [GiftCardId]        INT                NULL,
    [LocationId]        INT                NULL,
    [TransactionType]   INT                NULL,
    [TransactionAmount] DECIMAL (16, 2)    NULL,
    [TransactionUserId] INT                NULL,
    [TransactionDate]   DATETIMEOFFSET (7) NULL,
    [Comments]          VARCHAR (50)       NULL,
    [IsActive]          BIT                NULL,
    [IsDeleted]         BIT                NULL,
    [CreatedBy]         INT                NULL,
    [CreatedDate]       DATETIMEOFFSET (7) NULL,
    [UpdatedBy]         INT                NULL,
    [UpdatedDate]       DATETIMEOFFSET (7) NULL
);

