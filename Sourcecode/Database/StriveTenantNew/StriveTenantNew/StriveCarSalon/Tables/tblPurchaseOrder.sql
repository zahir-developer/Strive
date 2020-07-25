CREATE TABLE [StriveCarSalon].[tblPurchaseOrder] (
    [ProductId]     INT                NULL,
    [VendorId]      INT                NULL,
    [IsAutoRequest] BIT                NULL,
    [IsMailSent]    BIT                NULL,
    [OrderedDate]   DATETIMEOFFSET (7) NULL,
    [OrderedBy]     INT                NULL,
    [OrderDetails]  VARCHAR (50)       NULL,
    [IsActive]      BIT                NULL,
    [IsDeleted]     BIT                NULL,
    [CreatedBy]     INT                NULL,
    [CreatedDate]   DATETIMEOFFSET (7) NULL,
    [UpdatedBy]     INT                NULL,
    [UpdatedDate]   DATETIMEOFFSET (7) NULL
);

