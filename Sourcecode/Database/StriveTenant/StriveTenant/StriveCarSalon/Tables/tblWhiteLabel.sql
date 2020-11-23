CREATE TABLE [StriveCarSalon].[tblWhiteLabel] (
    [WhiteLabelId] INT                IDENTITY (1, 1) NOT NULL,
    [LogoPath]     VARCHAR (150)      NULL,
    [Title]        VARCHAR (50)       NULL,
    [ThemeId]      INT                NULL,
    [FontFace]     VARCHAR (25)       NULL,
    [IsActive]     BIT                NULL,
    [IsDeleted]    BIT                NULL,
    [CreatedBy]    INT                NULL,
    [CreatedDate]  DATETIMEOFFSET (7) NULL,
    [UpdatedBy]    INT                NULL,
    [UpdatedDate]  DATETIMEOFFSET (7) NULL,
    CONSTRAINT [PK_tblWhiteLabel] PRIMARY KEY CLUSTERED ([WhiteLabelId] ASC),
    CONSTRAINT [FK_tblWhiteLabel_ThemeId] FOREIGN KEY ([ThemeId]) REFERENCES [StriveCarSalon].[tblThemes] ([ThemeId])
);



