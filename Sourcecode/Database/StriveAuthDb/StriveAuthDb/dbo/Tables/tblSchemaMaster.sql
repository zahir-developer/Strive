CREATE TABLE [dbo].[tblSchemaMaster] (
    [SchemaId]       INT           IDENTITY (1, 1) NOT NULL,
    [ClientId]       INT           NOT NULL,
    [SubDomain]      VARCHAR (100) NULL,
    [DBSchemaName]   VARCHAR (50)  NULL,
    [DBUserName]     VARCHAR (50)  NULL,
    [DBPassword]     VARCHAR (100) NULL,
    [SubscriptionId] INT           NOT NULL,
    [StatusId]       INT           NOT NULL,
    [IsDeleted]      SMALLINT      NOT NULL,
    [ExpiryDate]     DATETIME      NULL,
    [CreatedDate]    DATETIME      NULL,
    CONSTRAINT [PK_SchemaMaster_SchemaId] PRIMARY KEY CLUSTERED ([SchemaId] ASC),
    CONSTRAINT [UK_SchemaMaster_ClientId] UNIQUE NONCLUSTERED ([ClientId] ASC),
    CONSTRAINT [UK_SchemaMaster_SubDomain] UNIQUE NONCLUSTERED ([SubDomain] ASC)
);


GO
CREATE NONCLUSTERED INDEX [IX_SchemaMaster_SubscriptionId]
    ON [dbo].[tblSchemaMaster]([SubscriptionId] ASC);


GO
CREATE NONCLUSTERED INDEX [IX_SchemaMaster_IsDeleted]
    ON [dbo].[tblSchemaMaster]([IsDeleted] ASC);

