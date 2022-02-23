CREATE TABLE [dbo].[tblTenantModule] (
    [ModuleId]    INT                IDENTITY (1, 1) NOT NULL,
    [ModuleName]  VARCHAR (250)      NULL,
    [IsActive]    BIT                NULL,
    [IsDeleted]   BIT                NULL,
    [CreatedBy]   INT                NULL,
    [CreatedDate] DATETIMEOFFSET (7) NULL,
    [UpdatedBy]   INT                NULL,
    [UpdatedDate] DATETIMEOFFSET (7) NULL,
    CONSTRAINT [PK_StriveCarSalon_TblModule_ModuleId] PRIMARY KEY CLUSTERED ([ModuleId] ASC)
);

