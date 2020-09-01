CREATE TYPE [StriveSuperAdminTest].[tvpProduct] AS TABLE (
    [ProductId]           INT            NOT NULL,
    [ProductName]         NVARCHAR (120) NULL,
    [ProductCode]         NVARCHAR (20)  NULL,
    [ProductDescription]  NVARCHAR (100) NULL,
    [ProductType]         INT            NULL,
    [LocationId]          INT            NULL,
    [VendorId]            INT            NULL,
    [Size]                INT            NULL,
    [SizeDescription]     NVARCHAR (20)  NULL,
    [Quantity]            FLOAT (53)     NULL,
    [QuantityDescription] NVARCHAR (20)  NULL,
    [Cost]                FLOAT (53)     NULL,
    [IsTaxable]           BIT            NULL,
    [TaxAmount]           FLOAT (53)     NULL,
    [IsActive]            BIT            NULL,
    [ThresholdLimit]      INT            NULL);

