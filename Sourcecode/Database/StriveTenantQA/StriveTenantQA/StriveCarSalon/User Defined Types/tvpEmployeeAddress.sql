CREATE TYPE [StriveCarSalon].[tvpEmployeeAddress] AS TABLE (
    [EmployeeAddressId] INT           NOT NULL,
    [RelationshipId]    BIGINT        NULL,
    [Address1]          NVARCHAR (50) NULL,
    [Address2]          NVARCHAR (50) NULL,
    [PhoneNumber]       VARCHAR (50)  NULL,
    [PhoneNumber2]      VARCHAR (50)  NULL,
    [Email]             NVARCHAR (50) NULL,
    [City]              INT           NULL,
    [State]             INT           NULL,
    [Zip]               NVARCHAR (50) NULL,
    [IsActive]          BIT           NULL,
    [Country]           INT           NULL);

