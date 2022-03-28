CREATE TABLE [StriveCarSalon].[tblDocument] (
    [DocumentId]       INT                IDENTITY (1, 1) NOT NULL,
    [DocumentType]     INT                NULL,
    [RoleId]           INT                NULL,
    [FileName]         VARCHAR (150)      NOT NULL,
    [OriginalFileName] VARCHAR (100)      NULL,
    [FilePath]         VARCHAR (1000)     NULL,
    [DocumentName]     VARCHAR (100)      NULL,
    [IsActive]         BIT                NULL,
    [IsDeleted]        BIT                NULL,
    [CreatedBy]        INT                NULL,
    [CreatedDate]      DATETIMEOFFSET (7) NULL,
    [UpdatedBy]        INT                NULL,
    [UpdatedDate]      DATETIMEOFFSET (7) NULL,
    [DocumentSubtype]  INT                NULL,
    CONSTRAINT [PK_StriveCarSalon_tblDocument_DocumentId] PRIMARY KEY CLUSTERED ([DocumentId] ASC),
    CONSTRAINT [FK_StriveCarSalon_tblDocument_DocumentSubType] FOREIGN KEY ([DocumentSubtype]) REFERENCES [StriveCarSalon].[tblCodeValue] ([id]),
    CONSTRAINT [FK_StriveCarSalon_tblDocument_DocumentType] FOREIGN KEY ([DocumentType]) REFERENCES [StriveCarSalon].[tblCodeValue] ([id]),
    CONSTRAINT [FK_tblDocument_tblRoleMaster] FOREIGN KEY ([RoleId]) REFERENCES [StriveCarSalon].[tblRoleMaster] ([RoleMasterId])
);





