CREATE TABLE [StriveCarSalon].[tblField] (
    [FieldId]        INT                IDENTITY (1, 1) NOT NULL,
    [ModuleScreenId] INT                NOT NULL,
    [FieldName]      VARCHAR (250)      NULL,
    [IsActive]       BIT                NULL,
    [IsDeleted]      BIT                NULL,
    [CreatedBy]      INT                NULL,
    [CreatedDate]    DATETIMEOFFSET (7) NULL,
    [UpdatedBy]      INT                NULL,
    [UpdatedDate]    DATETIMEOFFSET (7) NULL,
    CONSTRAINT [PK_StriveCarSalon_TblField_FieldId] PRIMARY KEY CLUSTERED ([FieldId] ASC),
    CONSTRAINT [FK_StriveCarSalon_TblField_ModuleScreenId] FOREIGN KEY ([ModuleScreenId]) REFERENCES [StriveCarSalon].[tblModuleScreen] ([ModuleScreenId])
);

