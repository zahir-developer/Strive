CREATE TABLE [StriveCarSalon].[tblClientAddress] (
    [ClientAddressId] INT                IDENTITY (1, 1) NOT NULL,
    [ClientId]        INT                NULL,
    [Address1]        VARCHAR (50)       NULL,
    [Address2]        VARCHAR (50)       NULL,
    [PhoneNumber]     VARCHAR (50)       NULL,
    [PhoneNumber2]    VARCHAR (50)       NULL,
    [Email]           VARCHAR (100)      NULL,
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
    [IsNotified]      BIT                NULL,
    CONSTRAINT [PK_tblClientAddress] PRIMARY KEY CLUSTERED ([ClientAddressId] ASC),
    CONSTRAINT [FK_tblClientAddress_City] FOREIGN KEY ([City]) REFERENCES [StriveCarSalon].[tblCodeValue] ([id]),
    CONSTRAINT [FK_tblClientAddress_ClientId] FOREIGN KEY ([ClientId]) REFERENCES [StriveCarSalon].[tblClient] ([ClientId]),
    CONSTRAINT [FK_tblClientAddress_Country] FOREIGN KEY ([Country]) REFERENCES [StriveCarSalon].[tblCodeValue] ([id]),
    CONSTRAINT [FK_tblClientAddress_State] FOREIGN KEY ([State]) REFERENCES [StriveCarSalon].[tblCodeValue] ([id])
);






GO
CREATE NONCLUSTERED INDEX [IX_tblClientAddress_ClientId]
    ON [StriveCarSalon].[tblClientAddress]([ClientId] ASC);

