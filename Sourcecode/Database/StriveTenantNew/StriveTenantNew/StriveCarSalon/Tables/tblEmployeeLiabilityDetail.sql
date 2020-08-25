CREATE TABLE [StriveCarSalon].[tblEmployeeLiabilityDetail] (
    [LiabilityDetailId]   INT                IDENTITY (1, 1) NOT NULL,
    [LiabilityId]         INT                NULL,
    [LiabilityDetailType] INT                NOT NULL,
    [Amount]              DECIMAL (19, 4)    NULL,
    [PaymentType]         INT                NULL,
    [DocumentPath]        VARCHAR (200)      NULL,
    [Description]         VARCHAR (100)      NULL,
    [IsActive]            BIT                NULL,
    [IsDeleted]           BIT                NULL,
    [CreatedBy]           INT                NULL,
    [CreatedDate]         DATETIMEOFFSET (7) NULL,
    [UpdatedBy]           INT                NULL,
    [UpdatedDate]         DATETIMEOFFSET (7) NULL,
    CONSTRAINT [PK_tblEmployeeLiabilityDetail] PRIMARY KEY CLUSTERED ([LiabilityDetailId] ASC),
    CONSTRAINT [FK_tblEmployeeLiabilityDetail_LiabilityDetailType] FOREIGN KEY ([LiabilityDetailType]) REFERENCES [StriveCarSalon].[tblCodeValue] ([id]),
    CONSTRAINT [FK_tblEmployeeLiabilityDetail_PaymentType] FOREIGN KEY ([PaymentType]) REFERENCES [StriveCarSalon].[tblCodeValue] ([id]),
    CONSTRAINT [FK_tblEmployeeLiabilityDetail_tblEmployeeLiability] FOREIGN KEY ([LiabilityId]) REFERENCES [StriveCarSalon].[tblEmployeeLiability] ([LiabilityId])
);











