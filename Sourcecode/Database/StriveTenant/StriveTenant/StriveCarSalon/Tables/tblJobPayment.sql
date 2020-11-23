CREATE TABLE [StriveCarSalon].[tblJobPayment] (
    [JobPaymentId]  INT                IDENTITY (1, 1) NOT NULL,
    [JobId]         INT                NULL,
    [DrawerId]      INT                NULL,
    [PaymentType]   INT                NULL,
    [Amount]        DECIMAL (16, 2)    NULL,
    [TaxAmount]     DECIMAL (16, 2)    NULL,
    [Cashback]      DECIMAL (16, 2)    NULL,
    [Approval]      BIT                NULL,
    [CheckNumber]   VARCHAR (50)       NULL,
    [Signature]     VARCHAR (10)       NULL,
    [PaymentStatus] INT                NULL,
    [Comments]      VARCHAR (50)       NULL,
    [IsProcessed]   BIT                NULL,
    [IsRollBack]    BIT                NULL,
    [IsActive]      BIT                NULL,
    [IsDeleted]     BIT                NULL,
    [CreatedBy]     INT                NULL,
    [CreatedDate]   DATETIMEOFFSET (7) NULL,
    [UpdatedBy]     INT                NULL,
    [UpdatedDate]   DATETIMEOFFSET (7) NULL,
    [MembershipId]  INT                NULL,
    CONSTRAINT [PK_tblJobPayment] PRIMARY KEY CLUSTERED ([JobPaymentId] ASC),
    CONSTRAINT [FK_tblJobPayment_DrawerId] FOREIGN KEY ([DrawerId]) REFERENCES [StriveCarSalon].[tblDrawer] ([DrawerId]),
    CONSTRAINT [FK_tblJobPayment_JobId] FOREIGN KEY ([JobId]) REFERENCES [StriveCarSalon].[tblJob] ([JobId]),
    CONSTRAINT [FK_tblJobPayment_MembershipId] FOREIGN KEY ([MembershipId]) REFERENCES [StriveCarSalon].[tblMembership] ([MembershipId]),
    CONSTRAINT [FK_tblJobPayment_PaymentStatus] FOREIGN KEY ([PaymentStatus]) REFERENCES [StriveCarSalon].[tblCodeValue] ([id]),
    CONSTRAINT [FK_tblJobPayment_PaymentType] FOREIGN KEY ([PaymentType]) REFERENCES [StriveCarSalon].[tblCodeValue] ([id])
);




GO
CREATE NONCLUSTERED INDEX [IX_tblJobPayment_JobId]
    ON [StriveCarSalon].[tblJobPayment]([JobId] ASC);

