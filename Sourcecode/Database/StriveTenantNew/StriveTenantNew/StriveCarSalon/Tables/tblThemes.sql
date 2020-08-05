CREATE TABLE [StriveCarSalon].[tblThemes] (
    [ThemeId]         INT                IDENTITY (1, 1) NOT NULL,
    [ThemeName]       VARCHAR (20)       NULL,
    [FontFace]        VARCHAR (20)       NULL,
    [PrimaryColor]    VARCHAR (10)       NULL,
    [SecondaryColor]  VARCHAR (10)       NULL,
    [DefaultLogoPath] VARCHAR (60)       NULL,
    [DefaultTitle]    VARCHAR (20)       NULL,
    [Comments]        VARCHAR (50)       NULL,
    [IsActive]        BIT                NULL,
    [IsDeleted]       BIT                NULL,
    [CreatedBy]       INT                NULL,
    [CreatedDate]     DATETIMEOFFSET (7) NULL,
    [UpdatedBy]       INT                NULL,
    [UpdatedDate]     DATETIMEOFFSET (7) NULL,
    CONSTRAINT [PK_tblThemes] PRIMARY KEY CLUSTERED ([ThemeId] ASC)
);

