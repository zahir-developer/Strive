CREATE TABLE [dbo].[tblTenantIntegration] (
    [TenantIntegrationId] INT                                                IDENTITY (1, 1) NOT NULL,
    [TenantId]            INT                                                NOT NULL,
    [ExtAppNameId]        INT                                                NOT NULL,
    [ExtAppEnvironId]     INT                                                NOT NULL,
    [ExtClientId]         VARCHAR (64) MASKED WITH (FUNCTION = 'default()')  NOT NULL,
    [ExtClientSecret]     VARCHAR (128) MASKED WITH (FUNCTION = 'default()') NOT NULL,
    [ExtRedirectUrl]      VARCHAR (128)                                      NOT NULL,
    [ExtBaseUrl]          VARCHAR (128)                                      NOT NULL,
    [MinorVersion]        VARCHAR (8)                                        NOT NULL,
    [TimeOut]             INT                                                NOT NULL,
    [RetryCount]          SMALLINT                                           NOT NULL,
    [IsEnableLogs]        SMALLINT                                           NOT NULL,
    [IsDeleted]           SMALLINT                                           NOT NULL,
    [CreatedDate]         BIGINT                                             NOT NULL,
    CONSTRAINT [PK_TenantIntegration_TenantIntegrationId] PRIMARY KEY CLUSTERED ([TenantIntegrationId] ASC),
    CONSTRAINT [FK_TenantIntegration_TenantId] FOREIGN KEY ([TenantId]) REFERENCES [dbo].[tblTenantMaster] ([TenantId])
);


GO
CREATE NONCLUSTERED INDEX [IX_TenantIntegration_TenantId]
    ON [dbo].[tblTenantIntegration]([TenantId] ASC);


GO
CREATE NONCLUSTERED INDEX [IX_TenantIntegration_IsEnableLogs]
    ON [dbo].[tblTenantIntegration]([IsEnableLogs] ASC);


GO
CREATE NONCLUSTERED INDEX [IX_TenantIntegration_IsDeleted]
    ON [dbo].[tblTenantIntegration]([IsDeleted] ASC);


GO
CREATE NONCLUSTERED INDEX [IX_TenantIntegration_ExtAppNameId]
    ON [dbo].[tblTenantIntegration]([ExtAppNameId] ASC);


GO
CREATE NONCLUSTERED INDEX [IX_TenantIntegration_CreatedDate]
    ON [dbo].[tblTenantIntegration]([CreatedDate] ASC);

