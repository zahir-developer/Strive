CREATE TABLE [StriveCarSalon].[tblEmployeeLiabilityDetail] (
    [LiabilityDetailId]   BIGINT         IDENTITY (1, 1) NOT NULL,
    [LiabilityId]         BIGINT         NOT NULL,
    [LiabilityDetailType] INT            NOT NULL,
    [Amount]              FLOAT (53)     NULL,
    [PaymentType]         INT            NULL,
    [DocumentPath]        NVARCHAR (200) NULL,
    [Description]         NVARCHAR (100) NULL,
    [CreatedDate]         DATETIME       NOT NULL,
    [IsActive]            BIT            NULL,
    CONSTRAINT [PK_tblEmployeeLiabilityDetail] PRIMARY KEY CLUSTERED ([LiabilityDetailId] ASC),
    CONSTRAINT [FK_tblEmployeeLiabilityDetail_tblEmployeeLiability] FOREIGN KEY ([LiabilityId]) REFERENCES [StriveCarSalon].[tblEmployeeLiability] ([LiabilityId])
);



