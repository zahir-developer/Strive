CREATE TABLE [StriveCarSalon].[tblClientAddress] (
    [AddressId]      INT           IDENTITY (1, 1) NOT NULL,
    [RelationshipId] INT           NULL,
    [Address1]       NVARCHAR (50) NULL,
    [Address2]       NVARCHAR (50) NULL,
    [PhoneNumber]    VARCHAR (50)  NULL,
    [PhoneNumber2]   VARCHAR (50)  NULL,
    [Email]          NVARCHAR (50) NULL,
    [City]           INT           NULL,
    [State]          INT           NULL,
    [Country]        INT           NULL,
    [Zip]            NVARCHAR (50) NULL,
    [IsActive]       BIT           NULL,
    CONSTRAINT [PK_tblClientAddress] PRIMARY KEY CLUSTERED ([AddressId] ASC),
    CONSTRAINT [FK_tblClientAddress_tblClient] FOREIGN KEY ([RelationshipId]) REFERENCES [StriveCarSalon].[tblClient] ([ClientId])
);

