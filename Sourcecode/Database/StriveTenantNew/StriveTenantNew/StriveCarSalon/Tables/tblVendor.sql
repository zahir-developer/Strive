CREATE TABLE [StriveCarSalon].[tblVendor] (
    [VendorId]       INT                IDENTITY (1, 1) NOT NULL,
    [VIN]            VARCHAR (50)       NULL,
    [VendorName]     VARCHAR (50)       NULL,
    [VendorAlias]    VARCHAR (20)       NULL,
    [IsActive]       BIT                NULL,
    [IsDeleted]      BIT                NULL,
    [CreatedBy]      INT                NULL,
    [CreatedDate]    DATETIMEOFFSET (7) NULL,
    [UpdatedBy]      INT                NULL,
    [UpdatedDate]    DATETIMEOFFSET (7) NULL,
    [websiteAddress] VARCHAR (250)      NULL,
    [AccountNumber]  VARCHAR (25)       NULL,
    CONSTRAINT [PK_tblVendor] PRIMARY KEY CLUSTERED ([VendorId] ASC)
);







