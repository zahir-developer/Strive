CREATE TABLE [dbo].[tblTenantFeature] (
    [FeatureId]       INT          IDENTITY (1, 1) NOT NULL,
    [ModuleId]        INT          NULL,
    [FeatureName]     VARCHAR (50) NULL,
    [ParentFeatureId] INT          NULL,
    [Comments]        VARCHAR (50) NULL,
    [IsActive]        INT          NULL,
    [IsDeleted]       INT          NULL,
    [CreatedDate]     DATETIME     NULL,
    CONSTRAINT [PK_tblTenantFeature] PRIMARY KEY CLUSTERED ([FeatureId] ASC),
    CONSTRAINT [FK_tblTenantFeature_tblTenantModule] FOREIGN KEY ([ModuleId]) REFERENCES [dbo].[tblTenantModule] ([ModuleId])
);

