CREATE TABLE [dbo].[tblLastAuth] (
    [AuthId]       INT    NOT NULL,
    [ActionTypeId] INT    NOT NULL,
    [LastDate]     BIGINT NOT NULL,
    CONSTRAINT [PK_LastAuth_AuthId] PRIMARY KEY CLUSTERED ([AuthId] ASC)
);


GO
CREATE NONCLUSTERED INDEX [IX_LastAuth_ActionTypeId]
    ON [dbo].[tblLastAuth]([ActionTypeId] ASC);

