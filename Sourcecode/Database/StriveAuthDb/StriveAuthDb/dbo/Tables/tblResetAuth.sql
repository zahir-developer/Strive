CREATE TABLE [dbo].[tblResetAuth] (
    [ResetAuthId]  INT          IDENTITY (1, 1) NOT NULL,
    [AuthId]       INT          NOT NULL,
    [ActionTypeId] INT          NOT NULL,
    [ResetHash]    VARCHAR (64) NOT NULL,
    [IsDeleted]    SMALLINT     NOT NULL,
    [ResetDate]    BIGINT       NOT NULL,
    CONSTRAINT [PK_ResetAuth_ResetAuthId] PRIMARY KEY CLUSTERED ([ResetAuthId] ASC),
    CONSTRAINT [FK_ResetAuth_AuthId] FOREIGN KEY ([AuthId]) REFERENCES [dbo].[tblAuthMaster] ([AuthId])
);


GO
CREATE NONCLUSTERED INDEX [IX_ResetAuth_ResetHash]
    ON [dbo].[tblResetAuth]([ResetHash] ASC);


GO
CREATE NONCLUSTERED INDEX [IX_ResetAuth_AuthId]
    ON [dbo].[tblResetAuth]([AuthId] ASC);


GO
CREATE NONCLUSTERED INDEX [IX_ResetAuth_ActionTypeId]
    ON [dbo].[tblResetAuth]([ActionTypeId] ASC);

