CREATE TYPE [StriveCarSalonTest].[tvpClientAddress] AS TABLE (
    [AddressId]      INT            NULL,
    [RelationshipId] INT            NULL,
    [Address1]       NVARCHAR (100) NULL,
    [Address2]       NVARCHAR (100) NULL,
    [PhoneNumber]    VARCHAR (50)   NULL,
    [PhoneNumber2]   VARCHAR (50)   NULL,
    [Email]          NVARCHAR (100) NULL,
    [City]           INT            NULL,
    [State]          INT            NULL,
    [Country]        INT            NULL,
    [Zip]            NVARCHAR (100) NULL,
    [IsActive]       BIT            NULL);

