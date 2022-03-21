CREATE TABLE [StriveCarSalon].[tblClient] (
    [ClientId]        INT                IDENTITY (1, 1) NOT NULL,
    [FirstName]       VARCHAR (50)       NULL,
    [MiddleName]      VARCHAR (50)       NULL,
    [LastName]        VARCHAR (50)       NULL,
    [Gender]          INT                NULL,
    [MaritalStatus]   INT                NULL,
    [BirthDate]       DATETIME           NULL,
    [Notes]           TEXT               NULL,
    [RecNotes]        TEXT               NULL,
    [Score]           INT                NULL,
    [NoEmail]         BIT                NULL,
    [ClientType]      INT                NULL,
    [AuthId]          INT                NULL,
    [IsActive]        BIT                NULL,
    [IsDeleted]       BIT                NULL,
    [CreatedBy]       INT                NULL,
    [CreatedDate]     DATETIMEOFFSET (7) NULL,
    [UpdatedBy]       INT                NULL,
    [UpdatedDate]     DATETIMEOFFSET (7) NULL,
    [Amount]          DECIMAL (18, 2)    NULL,
    [IsCreditAccount] BIT                NULL,
    [LocationId]      INT                NULL,
    CONSTRAINT [PK_tblClient] PRIMARY KEY CLUSTERED ([ClientId] ASC),
    CONSTRAINT [FK_tblClient_ClientType] FOREIGN KEY ([ClientType]) REFERENCES [StriveCarSalon].[tblCodeValue] ([id]),
    CONSTRAINT [FK_tblClient_Gender] FOREIGN KEY ([Gender]) REFERENCES [StriveCarSalon].[tblCodeValue] ([id]),
    CONSTRAINT [FK_tblClient_LocationId] FOREIGN KEY ([LocationId]) REFERENCES [StriveCarSalon].[tblLocation] ([LocationId]),
    CONSTRAINT [FK_tblClient_MaritalStatus] FOREIGN KEY ([MaritalStatus]) REFERENCES [StriveCarSalon].[tblCodeValue] ([id])
);









