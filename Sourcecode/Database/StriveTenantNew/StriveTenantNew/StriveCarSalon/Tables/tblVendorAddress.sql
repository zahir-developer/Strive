CREATE TABLE [StriveCarSalon].[tblVendorAddress] (
    [VendorAddressId] INT                IDENTITY (1, 1) NOT NULL,
    [VendorId]        INT                NULL,
    [Address1]        NVARCHAR (50)      NULL,
    [Address2]        NVARCHAR (50)      NULL,
    [PhoneNumber]     VARCHAR (50)       NULL,
    [PhoneNumber2]    VARCHAR (50)       NULL,
    [Email]           VARCHAR (50)       NULL,
    [City]            INT                NULL,
    [State]           INT                NULL,
    [Zip]             VARCHAR (10)       NULL,
    [Fax]             VARCHAR (20)       NULL,
    [Country]         INT                NULL,
    [IsActive]        BIT                NULL,
    [IsDeleted]       BIT                NULL,
    [CreatedBy]       INT                NULL,
    [CreatedDate]     DATETIMEOFFSET (7) NULL,
    [UpdatedBy]       INT                NULL,
    [UpdatedDate]     DATETIMEOFFSET (7) NULL,
    CONSTRAINT [PK_tblVendorAddress] PRIMARY KEY CLUSTERED ([VendorAddressId] ASC),
    CONSTRAINT [FK_tblVendorAddress_tblVendor] FOREIGN KEY ([VendorId]) REFERENCES [StriveCarSalon].[tblVendor] ([VendorId])
);



