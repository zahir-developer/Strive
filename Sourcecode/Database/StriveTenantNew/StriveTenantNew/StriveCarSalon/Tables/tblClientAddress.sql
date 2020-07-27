CREATE TABLE [StriveCarSalon].[tblClientAddress] (
    [ClientAddressId] INT                IDENTITY (1, 1) NOT NULL,
    [ClientId]        INT                NULL,
    [Address1]        NVARCHAR (50)      NULL,
    [Address2]        NVARCHAR (50)      NULL,
    [PhoneNumber]     VARCHAR (50)       NULL,
    [PhoneNumber2]    VARCHAR (50)       NULL,
    [Email]           VARCHAR (50)       NULL,
    [City]            INT                NULL,
    [State]           INT                NULL,
    [Country]         INT                NULL,
    [Zip]             VARCHAR (10)       NULL,
    [IsActive]        BIT                NULL,
    [IsDeleted]       BIT                NULL,
    [CreatedBy]       INT                NULL,
    [CreatedDate]     DATETIMEOFFSET (7) NULL,
    [UpdatedBy]       INT                NULL,
    [UpdatedDate]     DATETIMEOFFSET (7) NULL,
    CONSTRAINT [PK_tblClientAddress] PRIMARY KEY CLUSTERED ([ClientAddressId] ASC),
    CONSTRAINT [FK_tblClientAddress_tblClient] FOREIGN KEY ([ClientId]) REFERENCES [StriveCarSalon].[tblClient] ([ClientId])
);



