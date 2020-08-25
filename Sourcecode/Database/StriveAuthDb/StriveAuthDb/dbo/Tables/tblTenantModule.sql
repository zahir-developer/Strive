CREATE TABLE [dbo].[tblTenantModule] (
    [ModuleId]       INT          IDENTITY (1, 1) NOT NULL,
    [ModuleName]     VARCHAR (20) NULL,
    [ParentModuleId] INT          NULL,
    [Comments]       VARCHAR (20) NULL,
    [IsActive]       BIT          NULL,
    [IsDeleted]      BIT          NULL,
    [CreatedDate]    DATETIME     NULL,
    CONSTRAINT [PK_TenantModule_ModuleId] PRIMARY KEY CLUSTERED ([ModuleId] ASC)
);

