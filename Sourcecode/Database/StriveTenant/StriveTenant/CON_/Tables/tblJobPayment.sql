CREATE TABLE [CON].[tblJobPayment] (
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
    [CheckNumber]   VARCHAR (50)       NULL,
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
    CONSTRAINT [FK_tblJobPayment_CardType] FOREIGN KEY ([CardType]) REFERENCES [CON].[tblCodeValue] ([id]),
    CONSTRAINT [FK_tblJobPayment_DrawerId] FOREIGN KEY ([DrawerId]) REFERENCES [CON].[tblDrawer] ([DrawerId]),
    CONSTRAINT [FK_tblJobPayment_GiftCardId] FOREIGN KEY ([GiftCardId]) REFERENCES [CON].[tblGiftCard] ([GiftCardId]),
    CONSTRAINT [FK_tblJobPayment_JobId] FOREIGN KEY ([JobId]) REFERENCES [CON].[tblJob] ([JobId]),
    CONSTRAINT [FK_tblJobPayment_PaymentStatus] FOREIGN KEY ([PaymentStatus]) REFERENCES [CON].[tblCodeValue] ([id]),
    CONSTRAINT [FK_tblJobPayment_PaymentType] FOREIGN KEY ([PaymentType]) REFERENCES [CON].[tblCodeValue] ([id])
);


GO
CREATE NONCLUSTERED INDEX [IX_tblJobPayment_JobId]
    ON [CON].[tblJobPayment]([JobId] ASC);

