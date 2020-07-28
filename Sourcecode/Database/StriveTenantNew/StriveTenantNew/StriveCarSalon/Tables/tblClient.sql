CREATE TABLE [StriveCarSalon].[tblClient] (
    [ClientId]      INT                IDENTITY (1, 1) NOT NULL,
    [FirstName]     VARCHAR (50)       NULL,
    [MiddleName]    VARCHAR (50)       NULL,
    [LastName]      VARCHAR (50)       NULL,
    [Gender]        INT                NULL,
    [MaritalStatus] INT                NULL,
    [BirthDate]     DATETIME           NULL,
    [Notes]         VARCHAR (50)       NULL,
    [RecNotes]      VARCHAR (50)       NULL,
    [Score]         INT                NULL,
    [NoEmail]       BIT                NULL,
    [ClientType]    INT                NULL,
    [IsActive]      BIT                NULL,
    [IsDeleted]     BIT                NULL,
    [CreatedBy]     INT                NULL,
    [CreatedDate]   DATETIMEOFFSET (7) NULL,
    [UpdatedBy]     INT                NULL,
    [UpdatedDate]   DATETIMEOFFSET (7) NULL,
    CONSTRAINT [PK_tblClient] PRIMARY KEY CLUSTERED ([ClientId] ASC),
    CONSTRAINT [FK_tblClient_tblCodeValue] FOREIGN KEY ([Gender]) REFERENCES [StriveCarSalon].[tblCodeValue] ([id]),
    CONSTRAINT [FK_tblClient_tblCodeValue1] FOREIGN KEY ([MaritalStatus]) REFERENCES [StriveCarSalon].[tblCodeValue] ([id]),
    CONSTRAINT [FK_tblClient_tblCodeValue2] FOREIGN KEY ([ClientType]) REFERENCES [StriveCarSalon].[tblCodeValue] ([id])
);





