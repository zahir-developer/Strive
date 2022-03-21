CREATE TABLE [StriveCarSalon].[tblGiftCardHistory] (
    [GiftCardHistoryId]  INT                IDENTITY (1, 1) NOT NULL,
    [GiftCardId]         INT                NULL,
    [LocationId]         INT                NULL,
    [TransactionType]    INT                NULL,
    [TransactionAmount]  DECIMAL (16, 2)    NULL,
    [TransactionDate]    DATE               NULL,
    [Comments]           VARCHAR (50)       NULL,
    [IsActive]           BIT                NULL,
    [IsDeleted]          BIT                NULL,
    [CreatedBy]          INT                NULL,
    [CreatedDate]        DATETIMEOFFSET (7) NULL,
    [UpdatedBy]          INT                NULL,
    [UpdatedDate]        DATETIMEOFFSET (7) NULL,
    [JobPaymentId]       INT                NULL,
    [RefGiftCardTID]     INT                NULL,
    [JobPaymentDetailId] INT                NULL,
    CONSTRAINT [PK_tblGiftCardHistory] PRIMARY KEY CLUSTERED ([GiftCardHistoryId] ASC)
);






GO
CREATE NONCLUSTERED INDEX [IX_tblGiftCardHistory_GiftCardId]
    ON [StriveCarSalon].[tblGiftCardHistory]([GiftCardId] ASC);

