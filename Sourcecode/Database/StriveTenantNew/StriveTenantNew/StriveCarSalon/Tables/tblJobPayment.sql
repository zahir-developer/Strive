CREATE TABLE [StriveCarSalon].[tblJobPayment] (
    [JobPaymentId]  INT                IDENTITY (1, 1) NOT NULL,
    [JobId]         INT                NULL,
    [DrawerId]      INT                NULL,
    [PaymentType]   INT                NULL,
    [Amount]        DECIMAL (16, 2)    NULL,
    [TaxAmount]     DECIMAL (16, 2)    NULL,
    [Cashback]      DECIMAL (16, 2)    NULL,
    [CardType]      INT                NULL,
    [CardNumber]    VARCHAR (50)       NULL,
    [Approval]      BIT                NULL,
    [CheckNumber]   VARBINARY (50)     NULL,
    [GiftCardId]    INT                NULL,
    [Signature]     VARCHAR (10)       NULL,
    [PaymentStatus] INT                NULL,
    [Comments]      VARCHAR (50)       NULL,
    [IsActive]      BIT                NULL,
    [IsDeleted]     BIT                NULL,
    [CreatedBy]     INT                NULL,
    [CreatedDate]   DATETIMEOFFSET (7) NULL,
    [UpdatedBy]     INT                NULL,
    [UpdatedDate]   DATETIMEOFFSET (7) NULL,
    CONSTRAINT [PK_tblJobPayment] PRIMARY KEY CLUSTERED ([JobPaymentId] ASC),
    CONSTRAINT [FK_tblJobPayment_tblDrawer] FOREIGN KEY ([DrawerId]) REFERENCES [StriveCarSalon].[tblDrawer] ([DrawerId]),
    CONSTRAINT [FK_tblJobPayment_tblGiftCard] FOREIGN KEY ([GiftCardId]) REFERENCES [StriveCarSalon].[tblGiftCard] ([GiftCardId]),
    CONSTRAINT [FK_tblJobPayment_tblJob] FOREIGN KEY ([JobId]) REFERENCES [StriveCarSalon].[tblJob] ([JobId])
);



