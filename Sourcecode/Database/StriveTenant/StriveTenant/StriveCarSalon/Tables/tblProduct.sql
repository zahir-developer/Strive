CREATE TABLE [StriveCarSalon].[tblProduct] (
    [ProductId]           INT                IDENTITY (1, 1) NOT NULL,
    [ProductName]         VARCHAR (50)       NULL,
    [ProductCode]         VARCHAR (10)       NULL,
    [ProductDescription]  VARCHAR (50)       NULL,
    [ProductType]         INT                NULL,
    [FileName]            VARCHAR (100)      NULL,
    [ThumbFileName]       VARCHAR (50)       NULL,
    [LocationId]          INT                NULL,
    [VendorId]            INT                NULL,
    [Size]                INT                NULL,
    [SizeDescription]     VARCHAR (10)       NULL,
    [Quantity]            DECIMAL (18, 2)    NULL,
    [QuantityDescription] VARCHAR (10)       NULL,
    [Cost]                DECIMAL (19, 4)    NULL,
    [Price]               DECIMAL (19, 4)    NULL,
    [IsTaxable]           BIT                NULL,
    [TaxAmount]           DECIMAL (19, 4)    NULL,
    [ThresholdLimit]      INT                NULL,
    [OriginalFileName]    VARCHAR (150)      NULL,
    [IsActive]            BIT                NULL,
    [IsDeleted]           BIT                NULL,
    [CreatedBy]           INT                NULL,
    [CreatedDate]         DATETIMEOFFSET (7) NULL,
    [UpdatedBy]           INT                NULL,
    [UpdatedDate]         DATETIMEOFFSET (7) NULL,
    CONSTRAINT [PK_tblProduct] PRIMARY KEY CLUSTERED ([ProductId] ASC),
    CONSTRAINT [FK_tblProduct_LocationId] FOREIGN KEY ([LocationId]) REFERENCES [StriveCarSalon].[tblLocation] ([LocationId]),
    CONSTRAINT [FK_tblProduct_ProductType] FOREIGN KEY ([ProductType]) REFERENCES [StriveCarSalon].[tblCodeValue] ([id]),
    CONSTRAINT [FK_tblProduct_Size] FOREIGN KEY ([Size]) REFERENCES [StriveCarSalon].[tblCodeValue] ([id]),
    CONSTRAINT [FK_tblProduct_VendorId] FOREIGN KEY ([VendorId]) REFERENCES [StriveCarSalon].[tblVendor] ([VendorId])
);











