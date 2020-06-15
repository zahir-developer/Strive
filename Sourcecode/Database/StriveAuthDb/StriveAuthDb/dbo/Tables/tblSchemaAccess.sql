CREATE TABLE [dbo].[tblSchemaAccess] (
    [SchemaAccessId] INT      IDENTITY (1, 1) NOT NULL,
    [AuthId]         INT      NOT NULL,
    [SchemaId]       INT      NOT NULL,
    [IsDeleted]      SMALLINT NOT NULL,
    CONSTRAINT [PK_SchemaAccess_SchemaAccessId] PRIMARY KEY CLUSTERED ([SchemaAccessId] ASC),
    CONSTRAINT [FK_SchemaAccess_AuthId] FOREIGN KEY ([AuthId]) REFERENCES [dbo].[tblAuthMaster] ([AuthId]),
    CONSTRAINT [FK_SchemaAccess_SchemaId] FOREIGN KEY ([SchemaId]) REFERENCES [dbo].[tblSchemaMaster] ([SchemaId])
);

