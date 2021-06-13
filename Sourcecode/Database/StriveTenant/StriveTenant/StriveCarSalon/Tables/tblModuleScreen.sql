CREATE TABLE [StriveCarSalon].[tblModuleScreen] (
    [ModuleScreenId] INT                IDENTITY (1, 1) NOT NULL,
    [ModuleId]       INT                NOT NULL,
    [ViewName]       VARCHAR (250)      NULL,
    [Description]    VARCHAR (50)       NULL,
    [IsActive]       BIT                NULL,
    [IsDeleted]      BIT                NULL,
    [CreatedBy]      INT                NULL,
    [CreatedDate]    DATETIMEOFFSET (7) NULL,
    [UpdatedBy]      INT                NULL,
    [UpdatedDate]    DATETIMEOFFSET (7) NULL,
    CONSTRAINT [PK_StriveCarSalon_TblModuleScreen_ModuleScreenId] PRIMARY KEY CLUSTERED ([ModuleScreenId] ASC),
    CONSTRAINT [FK_StriveCarSalon_TblModuleScreen_ModuleId] FOREIGN KEY ([ModuleId]) REFERENCES [StriveCarSalon].[tblModule] ([ModuleId])
);



