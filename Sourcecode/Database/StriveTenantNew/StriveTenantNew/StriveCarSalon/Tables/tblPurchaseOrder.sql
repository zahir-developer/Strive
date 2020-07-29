CREATE TABLE [StriveCarSalon].[tblPurchaseOrder] (
    [PurchaseOrderId] INT                IDENTITY (1, 1) NOT NULL,
    [ProductId]       INT                NULL,
    [VendorId]        INT                NULL,
    [IsAutoRequest]   BIT                NULL,
    [IsMailSent]      BIT                NULL,
    [OrderedDate]     DATETIMEOFFSET (7) NULL,
    [OrderedBy]       INT                NULL,
    [OrderDetails]    VARCHAR (50)       NULL,
    [IsActive]        BIT                NULL,
    [IsDeleted]       BIT                NULL,
    [CreatedBy]       INT                NULL,
    [CreatedDate]     DATETIMEOFFSET (7) NULL,
    [UpdatedBy]       INT                NULL,
    [UpdatedDate]     DATETIMEOFFSET (7) NULL,
    CONSTRAINT [PK_tblPurchaseOrder] PRIMARY KEY CLUSTERED ([PurchaseOrderId] ASC),
    CONSTRAINT [FK_tblPurchaseOrder_ProductId] FOREIGN KEY ([ProductId]) REFERENCES [StriveCarSalon].[tblProduct] ([ProductId]),
    CONSTRAINT [FK_tblPurchaseOrder_VendorId] FOREIGN KEY ([VendorId]) REFERENCES [StriveCarSalon].[tblVendor] ([VendorId])
);





