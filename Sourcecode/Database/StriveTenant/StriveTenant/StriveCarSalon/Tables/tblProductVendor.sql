CREATE TABLE [StriveCarSalon].[tblProductVendor] (
    [ProductVendorId] INT                IDENTITY (1, 1) NOT NULL,
    [ProductId]       INT                NULL,
    [VendorId]        INT                NULL,
    [IsActive]        BIT                NULL,
    [IsDeleted]       BIT                NULL,
    [CreatedBy]       INT                NULL,
    [CreatedDate]     DATETIMEOFFSET (7) NULL,
    [UpdatedBy]       INT                NULL,
    [UpdatedDate]     DATETIMEOFFSET (7) NULL,
    CONSTRAINT [PK_tbltblProductVendor] PRIMARY KEY CLUSTERED ([ProductVendorId] ASC),
    CONSTRAINT [FK_tblProductVendor_ProductId] FOREIGN KEY ([ProductId]) REFERENCES [StriveCarSalon].[tblProduct] ([ProductId]),
    CONSTRAINT [FK_tblProductVendor_VendorId] FOREIGN KEY ([VendorId]) REFERENCES [StriveCarSalon].[tblVendor] ([VendorId])
);

