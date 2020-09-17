CREATE TYPE [StriveCarSalonTest].[tvpLocationAddress] AS TABLE (
    [LocationAddressId] INT            NOT NULL,
    [RelationshipId]    INT            NULL,
    [Address1]          NVARCHAR (200) NULL,
    [Address2]          NVARCHAR (200) NULL,
    [PhoneNumber]       VARCHAR (50)   NULL,
    [PhoneNumber2]      VARCHAR (50)   NULL,
    [Email]             NVARCHAR (200) NULL,
    [City]              INT            NULL,
    [State]             INT            NULL,
    [Zip]               NVARCHAR (200) NULL,
    [IsActive]          BIT            NULL,
    [Country]           INT            NULL);

