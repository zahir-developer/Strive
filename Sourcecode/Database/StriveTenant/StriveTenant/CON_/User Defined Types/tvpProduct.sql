CREATE TYPE [CON].[tvpProduct] AS TABLE (
    [ProductId]           INT           NOT NULL,
    [ProductName]         NVARCHAR (60) NULL,
    [ProductCode]         NVARCHAR (10) NULL,
    [ProductDescription]  NVARCHAR (50) NULL,
    [ProductType]         INT           NULL,
    [LocationId]          INT           NULL,
    [VendorId]            INT           NULL,
    [Size]                INT           NULL,
    [SizeDescription]     NVARCHAR (10) NULL,
    [Quantity]            FLOAT (53)    NULL,
    [QuantityDescription] NVARCHAR (10) NULL,
    [Cost]                FLOAT (53)    NULL,
    [IsTaxable]           BIT           NULL,
    [TaxAmount]           FLOAT (53)    NULL,
    [IsActive]            BIT           NULL,
    [ThresholdLimit]      INT           NULL);

