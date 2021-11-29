CREATE TABLE [StriveCarSalon].[tblMobileApp] (
    [MobileAppId]   INT                IDENTITY (1, 1) NOT NULL,
    [MobileAppName] VARCHAR (250)      NULL,
    [Description]   VARCHAR (100)      NULL,
    [IsActive]      BIT                NULL,
    [IsDeleted]     BIT                NULL,
    [CreatedBy]     INT                NULL,
    [CreatedDate]   DATETIMEOFFSET (7) NULL,
    [UpdatedBy]     INT                NULL,
    [UpdatedDate]   DATETIMEOFFSET (7) NULL,
    CONSTRAINT [PK_StriveCarSalon_TblMobileApp_MobileAppId] PRIMARY KEY CLUSTERED ([MobileAppId] ASC)
);

