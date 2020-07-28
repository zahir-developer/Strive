CREATE TABLE [StriveCarSalon].[tblProduct] (
    [ProductId]           INT                IDENTITY (1, 1) NOT NULL,
    [ProductName]         VARCHAR (50)       NULL,
    [ProductCode]         VARCHAR (10)       NULL,
    [ProductDescription]  VARCHAR (50)       NULL,
    [ProductType]         INT                NULL,
    [LocationId]          INT                NULL,
    [VendorId]            INT                NULL,
    [Size]                INT                NULL,
    [SizeDescription]     VARCHAR (10)       NULL,
    [Quantity]            FLOAT (53)         NULL,
    [QuantityDescription] VARCHAR (10)       NULL,
    [Cost]                FLOAT (53)         NULL,
    [IsTaxable]           BIT                NULL,
    [TaxAmount]           FLOAT (53)         NULL,
    [ThresholdLimit]      INT                NULL,
    [IsActive]            BIT                NULL,
    [IsDeleted]           BIT                NULL,
    [CreatedBy]           INT                NULL,
    [CreatedDate]         DATETIMEOFFSET (7) NULL,
    [UpdatedBy]           INT                NULL,
    [UpdatedDate]         DATETIMEOFFSET (7) NULL,
    CONSTRAINT [PK_tblProduct] PRIMARY KEY CLUSTERED ([ProductId] ASC),
    CONSTRAINT [FK_tblProduct_tblCodeValue] FOREIGN KEY ([ProductType]) REFERENCES [StriveCarSalon].[tblCodeValue] ([id]),
    CONSTRAINT [FK_tblProduct_tblCodeValue1] FOREIGN KEY ([Size]) REFERENCES [StriveCarSalon].[tblCodeValue] ([id]),
    CONSTRAINT [FK_tblProduct_tblLocation] FOREIGN KEY ([LocationId]) REFERENCES [StriveCarSalon].[tblLocation] ([LocationId]),
    CONSTRAINT [FK_tblProduct_tblVendor] FOREIGN KEY ([VendorId]) REFERENCES [StriveCarSalon].[tblVendor] ([VendorId])
);





