CREATE TABLE [StriveCarSalon].[tblJobPaymentDetail] (
    [JobPaymentDetailId] INT                IDENTITY (1, 1) NOT NULL,
    [JobPaymentId]       INT                NULL,
    [PaymentType]        INT                NULL,
    [Amount]             DECIMAL (16, 2)    NULL,
    [TaxAmount]          DECIMAL (16, 2)    NULL,
    [Signature]          VARCHAR (10)       NULL,
    [IsActive]           BIT                NULL,
    [IsDeleted]          BIT                NULL,
    [CreatedBy]          INT                NULL,
    [CreatedDate]        DATETIMEOFFSET (7) NULL,
    [UpdatedBy]          INT                NULL,
    [UpdatedDate]        DATETIMEOFFSET (7) NULL,
    [ReferenceNumber]    NVARCHAR (20)      NULL,
    CONSTRAINT [PK_tblJobPaymentDetail_JobPaymentDetailId] PRIMARY KEY CLUSTERED ([JobPaymentDetailId] ASC),
    CONSTRAINT [FK_tblJobPaymentDetail_JobPaymentId] FOREIGN KEY ([JobPaymentId]) REFERENCES [StriveCarSalon].[tblJobPayment] ([JobPaymentId]),
    CONSTRAINT [FK_tblJobPaymentDetail_PaymentType] FOREIGN KEY ([PaymentType]) REFERENCES [StriveCarSalon].[tblCodeValue] ([id])
);




GO
CREATE NONCLUSTERED INDEX [Index_tblJobPaymentDetail_JobPaymentId]
    ON [StriveCarSalon].[tblJobPaymentDetail]([JobPaymentId] ASC);

