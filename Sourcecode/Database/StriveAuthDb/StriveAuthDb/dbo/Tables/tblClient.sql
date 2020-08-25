CREATE TABLE [dbo].[tblClient] (
    [ClientId]   INT          IDENTITY (1, 1) NOT NULL,
    [ClientName] VARCHAR (50) NULL,
    CONSTRAINT [PK_tblClient] PRIMARY KEY CLUSTERED ([ClientId] ASC)
);

