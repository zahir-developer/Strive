CREATE TABLE [StriveCarSalon].[tblJobPaymentCreditCard] (
    [JobPaymentCreditCardId]      INT                IDENTITY (1, 1) NOT NULL,
    [CardTypeId]                  INT                NOT NULL,
    [CardCategoryId]              INT                NOT NULL,
    [CardNumber]                  VARCHAR (50)       NOT NULL,
    [CreditCardTransactionTypeId] INT                NOT NULL,
    [Amount]                      DECIMAL (16, 2)    NULL,
    [TranRefNo]                   VARCHAR (50)       NULL,
    [TranRefDetails]              VARCHAR (100)      NULL,
    [IsActive]                    BIT                NULL,
    [IsDeleted]                   BIT                NULL,
    [CreatedBy]                   INT                NULL,
    [CreatedDate]                 DATETIMEOFFSET (7) NULL,
    [UpdatedBy]                   INT                NULL,
    [UpdatedDate]                 DATETIMEOFFSET (7) NULL,
    [JobPaymentDetailId]          INT                NULL,
    CONSTRAINT [PK_tblJobPaymentCreditCard_JobPaymentCreditCardId] PRIMARY KEY CLUSTERED ([JobPaymentCreditCardId] ASC),
    CONSTRAINT [FK_StrivecarSalon_tblJobPaymentCreditcard_JobPaymentDetailId] FOREIGN KEY ([JobPaymentDetailId]) REFERENCES [StriveCarSalon].[tblJobPaymentDetail] ([JobPaymentDetailId]),
    CONSTRAINT [FK_tblJobPaymentCreditCard_CardCategoryId] FOREIGN KEY ([CardCategoryId]) REFERENCES [StriveCarSalon].[tblCodeValue] ([id]),
    CONSTRAINT [FK_tblJobPaymentCreditCard_CardTypeId] FOREIGN KEY ([CardTypeId]) REFERENCES [StriveCarSalon].[tblCodeValue] ([id]),
    CONSTRAINT [FK_tblJobPaymentCreditCard_CreditCardTransactionTypeId] FOREIGN KEY ([CreditCardTransactionTypeId]) REFERENCES [StriveCarSalon].[tblCodeValue] ([id])
);



