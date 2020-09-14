CREATE TYPE [StriveCarSalon].[tvpVendor] AS TABLE (
    [VendorId]    INT            NULL,
    [VIN]         VARCHAR (50)   NULL,
    [VendorName]  NVARCHAR (100) NULL,
    [VendorAlias] NVARCHAR (20)  NULL,
    [IsActive]    BIT            NULL);

