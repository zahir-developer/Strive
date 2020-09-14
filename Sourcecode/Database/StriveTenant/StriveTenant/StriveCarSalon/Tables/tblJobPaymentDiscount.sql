CREATE TABLE [StriveCarSalon].[tblJobPaymentDiscount] (
    [JobPaymentDiscountId] INT                IDENTITY (1, 1) NOT NULL,
    [JobPaymentId]         INT                NOT NULL,
    [ServiceDiscountId]    INT                NOT NULL,
    [Amount]               DECIMAL (16, 2)    NULL,
    [IsActive]             BIT                NULL,
    [IsDeleted]            BIT                NULL,
    [CreatedBy]            INT                NULL,
    [CreatedDate]          DATETIMEOFFSET (7) NULL,
    [UpdatedBy]            INT                NULL,
    [UpdatedDate]          DATETIMEOFFSET (7) NULL,
    CONSTRAINT [PK_tblGiftcardPayment_GiftCardPaymentId] PRIMARY KEY CLUSTERED ([JobPaymentDiscountId] ASC),
    CONSTRAINT [FK_tblJobPaymentDiscount_JobPaymentId] FOREIGN KEY ([JobPaymentId]) REFERENCES [StriveCarSalon].[tblJobPayment] ([JobPaymentId]),
    CONSTRAINT [FK_tblJobPaymentDiscount_ServiceDiscountId] FOREIGN KEY ([ServiceDiscountId]) REFERENCES [StriveCarSalon].[tblService] ([ServiceId])
);

