CREATE TABLE [StriveCarSalon].[tblAdSetup] (
    [AdSetupId]   INT                IDENTITY (1, 1) NOT NULL,
    [DocumentId]  INT                NULL,
    [Name]        VARCHAR (200)      NULL,
    [Description] VARCHAR (MAX)      NULL,
    [IsActive]    BIT                NULL,
    [IsDeleted]   BIT                NULL,
    [CreatedBy]   INT                NULL,
    [CreatedDate] DATETIMEOFFSET (7) NULL,
    [UpdatedBy]   INT                NULL,
    [UpdatedDate] DATETIMEOFFSET (7) NULL,
    [LaunchDate]  DATE               NULL,
    CONSTRAINT [PK_tblAdSetup] PRIMARY KEY CLUSTERED ([AdSetupId] ASC),
    CONSTRAINT [FK_tblAdSetup_DocumentId] FOREIGN KEY ([DocumentId]) REFERENCES [StriveCarSalon].[tblDocument] ([DocumentId])
);



