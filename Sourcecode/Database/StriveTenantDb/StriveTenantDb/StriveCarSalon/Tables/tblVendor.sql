CREATE TABLE [StriveCarSalon].[tblVendor] (
    [VendorId]    INT            IDENTITY (1, 1) NOT NULL,
    [VIN]         VARCHAR (50)   NULL,
    [VendorName]  NVARCHAR (100) NULL,
    [VendorAlias] NVARCHAR (20)  NULL,
    [IsActive]    BIT            NULL,
    CONSTRAINT [PK_tblVendor] PRIMARY KEY CLUSTERED ([VendorId] ASC)
);

