CREATE TABLE [StriveCarSalon].[tblLocationAddress] (
    [LocationAddressId] INT                IDENTITY (1, 1) NOT NULL,
    [LocationId]        INT                NULL,
    [Address1]          NVARCHAR (50)      NULL,
    [Address2]          NVARCHAR (50)      NULL,
    [PhoneNumber]       VARCHAR (50)       NULL,
    [PhoneNumber2]      VARCHAR (50)       NULL,
    [Email]             VARCHAR (50)       NULL,
    [City]              INT                NULL,
    [State]             INT                NULL,
    [Zip]               VARCHAR (10)       NULL,
    [Country]           INT                NULL,
    [Longitude]         DECIMAL (9, 6)     NULL,
    [Latitude]          DECIMAL (9, 6)     NULL,
    [WeatherLocationId] INT                NULL,
    [IsActive]          BIT                NULL,
    [IsDeleted]         BIT                NULL,
    [CreatedBy]         INT                NULL,
    [CreatedDate]       DATETIMEOFFSET (7) NULL,
    [UpdatedBy]         INT                NULL,
    [UpdatedDate]       DATETIMEOFFSET (7) NULL,
    CONSTRAINT [PK_tblLocationAddress] PRIMARY KEY CLUSTERED ([LocationAddressId] ASC),
    CONSTRAINT [FK_tblLocationAddress_City] FOREIGN KEY ([City]) REFERENCES [StriveCarSalon].[tblCodeValue] ([id]),
    CONSTRAINT [FK_tblLocationAddress_Country] FOREIGN KEY ([Country]) REFERENCES [StriveCarSalon].[tblCodeValue] ([id]),
    CONSTRAINT [FK_tblLocationAddress_LocationId] FOREIGN KEY ([LocationId]) REFERENCES [StriveCarSalon].[tblLocation] ([LocationId]),
    CONSTRAINT [FK_tblLocationAddress_State] FOREIGN KEY ([State]) REFERENCES [StriveCarSalon].[tblCodeValue] ([id])
);







