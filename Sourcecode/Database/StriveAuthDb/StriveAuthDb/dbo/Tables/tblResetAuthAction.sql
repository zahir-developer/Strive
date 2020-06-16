CREATE TABLE [dbo].[tblResetAuthAction] (
    [ResetAuthId] INT      NOT NULL,
    [AuthId]      INT      NOT NULL,
    [ResetStatus] SMALLINT NOT NULL,
    [ActionDate]  BIGINT   NOT NULL,
    CONSTRAINT [PK_ResetAuthAction_ResetAuthId] PRIMARY KEY CLUSTERED ([ResetAuthId] ASC),
    CONSTRAINT [FK_ResetAuthAction_AuthId] FOREIGN KEY ([AuthId]) REFERENCES [dbo].[tblAuthMaster] ([AuthId]),
    CONSTRAINT [FK_ResetAuthAction_ResetAuthId] FOREIGN KEY ([ResetAuthId]) REFERENCES [dbo].[tblResetAuth] ([ResetAuthId])
);


GO
CREATE NONCLUSTERED INDEX [IX_ResetAuthAction_ResetStatus]
    ON [dbo].[tblResetAuthAction]([ResetStatus] ASC);


GO
CREATE NONCLUSTERED INDEX [IX_ResetAuthAction_AuthId]
    ON [dbo].[tblResetAuthAction]([AuthId] ASC);

