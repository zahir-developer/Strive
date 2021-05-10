CREATE TABLE [StriveCarSalon].[tblVendorEmailAddress] (
    [VendorEmailAddressId] INT                IDENTITY (1, 1) NOT NULL,
    [VendorId]             INT                NULL,
    [IsActive]             BIT                NULL,
    [IsDeleted]            BIT                NULL,
    [CreatedBy]            INT                NULL,
    [CreatedDate]          DATETIMEOFFSET (7) NULL,
    [UpdatedBy]            INT                NULL,
    [UpdatedDate]          DATETIMEOFFSET (7) NULL,
    [Email]                VARCHAR (50)       NULL,
    CONSTRAINT [PK_tblVendorEmailAddress] PRIMARY KEY CLUSTERED ([VendorEmailAddressId] ASC),
    CONSTRAINT [FK_tblVendorEmailAddress_VendorId] FOREIGN KEY ([VendorId]) REFERENCES [StriveCarSalon].[tblVendor] ([VendorId])
);

