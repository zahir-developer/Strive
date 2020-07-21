CREATE TABLE [StriveCarSalon].[tblLocationAddress] (
    [AddressId]      INT            IDENTITY (1, 1) NOT NULL,
    [RelationshipId] INT            NULL,
    [Address1]       NVARCHAR (50)  NULL,
    [Address2]       NVARCHAR (50)  NULL,
    [PhoneNumber]    VARCHAR (50)   NULL,
    [PhoneNumber2]   VARCHAR (50)   NULL,
    [Email]          NVARCHAR (50)  NULL,
    [City]           INT            NULL,
    [State]          INT            NULL,
    [Zip]            NVARCHAR (50)  NULL,
    [IsActive]       BIT            NULL,
    [Country]        INT            NULL,
    [Longitude]      DECIMAL (9, 6) NULL,
    [Latitude]       DECIMAL (9, 6) NULL,
    CONSTRAINT [PK_tblLocationAddress] PRIMARY KEY CLUSTERED ([AddressId] ASC),
    CONSTRAINT [FK_tblLocationAddress_tblLocation] FOREIGN KEY ([RelationshipId]) REFERENCES [StriveCarSalon].[tblLocation] ([LocationId])
);



