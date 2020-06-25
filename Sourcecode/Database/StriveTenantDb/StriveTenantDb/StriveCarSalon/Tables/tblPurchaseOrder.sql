CREATE TABLE [StriveCarSalon].[tblPurchaseOrder] (
    [ProductId]     INT           NULL,
    [VendorId]      INT           NULL,
    [IsAutoRequest] BIT           NULL,
    [IsMailSent]    BIT           NULL,
    [OrderedDate]   DATETIME      NULL,
    [OrderedBy]     INT           NULL,
    [OrderDetails]  NVARCHAR (50) NULL,
    CONSTRAINT [FK_tblPurchaseOrder_tblProduct] FOREIGN KEY ([ProductId]) REFERENCES [StriveCarSalon].[tblProduct] ([ProductId]),
    CONSTRAINT [FK_tblPurchaseOrder_tblVendor] FOREIGN KEY ([VendorId]) REFERENCES [StriveCarSalon].[tblVendor] ([VendorId])
);

