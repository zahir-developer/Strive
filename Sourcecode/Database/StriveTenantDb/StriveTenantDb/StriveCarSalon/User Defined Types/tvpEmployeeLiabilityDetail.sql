CREATE TYPE [StriveCarSalon].[tvpEmployeeLiabilityDetail] AS TABLE (
    [LiabilityDetailId]   BIGINT         NOT NULL,
    [LiabilityId]         BIGINT         NOT NULL,
    [LiabilityDetailType] INT            NOT NULL,
    [Amount]              FLOAT (53)     NULL,
    [PaymentType]         INT            NULL,
    [DocumentPath]        NVARCHAR (400) NULL,
    [Description]         NVARCHAR (200) NULL,
    [CreatedDate]         DATETIME       NULL,
    [IsActive]            BIT            NULL);

