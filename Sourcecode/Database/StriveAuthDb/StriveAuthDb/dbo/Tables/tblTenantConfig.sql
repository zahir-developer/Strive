CREATE TABLE [dbo].[tblTenantConfig] (
    [ConfigId]  INT      IDENTITY (1, 1) NOT NULL,
    [TenantId]  INT      NOT NULL,
    [ModuleId]  INT      NOT NULL,
    [FeatureId] INT      NOT NULL,
    [IsDeleted] SMALLINT NOT NULL,
    CONSTRAINT [PK_TenantConfig_ConfigId] PRIMARY KEY CLUSTERED ([ConfigId] ASC),
    CONSTRAINT [FK_TenantConfig_TenantId] FOREIGN KEY ([TenantId]) REFERENCES [dbo].[tblTenantMaster] ([TenantId])
);


GO
CREATE NONCLUSTERED INDEX [IX_TenantConfig_TenantId]
    ON [dbo].[tblTenantConfig]([TenantId] ASC);


GO
CREATE NONCLUSTERED INDEX [IX_TenantConfig_ModuleId]
    ON [dbo].[tblTenantConfig]([ModuleId] ASC);


GO
CREATE NONCLUSTERED INDEX [IX_TenantConfig_IsDeleted]
    ON [dbo].[tblTenantConfig]([IsDeleted] ASC);


GO
CREATE NONCLUSTERED INDEX [IX_TenantConfig_FeatureId]
    ON [dbo].[tblTenantConfig]([FeatureId] ASC);

