CREATE TYPE [StriveSuperAdminTest].[tvpVendor] AS TABLE (
    [VendorId]    INT            NULL,
    [VIN]         VARCHAR (50)   NULL,
    [VendorName]  NVARCHAR (200) NULL,
    [VendorAlias] NVARCHAR (40)  NULL,
    [IsActive]    BIT            NULL);

