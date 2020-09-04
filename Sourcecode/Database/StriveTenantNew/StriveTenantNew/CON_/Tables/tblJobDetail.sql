CREATE TABLE [CON].[tblJobDetail] (
    [JobDetailId] INT                IDENTITY (1, 1) NOT NULL,
    [JobId]       INT                NULL,
    [BayId]       INT                NULL,
    [SalesRep]    INT                NULL,
    [QABy]        INT                NULL,
    [Labour]      INT                NULL,
    [Review]      INT                NULL,
    [ReviewNote]  VARCHAR (50)       NULL,
    [IsActive]    BIT                NULL,
    [IsDeleted]   BIT                NULL,
    [CreatedBy]   INT                NULL,
    [CreatedDate] DATETIMEOFFSET (7) NULL,
    [UpdatedBy]   INT                NULL,
    [UpdatedDate] DATETIMEOFFSET (7) NULL,
    CONSTRAINT [PK_tblJobDetail] PRIMARY KEY CLUSTERED ([JobDetailId] ASC),
    CONSTRAINT [FK_tblJobDetail_BayId] FOREIGN KEY ([BayId]) REFERENCES [CON].[tblBay] ([BayId]),
    CONSTRAINT [FK_tblJobDetail_JobId] FOREIGN KEY ([JobId]) REFERENCES [CON].[tblJob] ([JobId]),
    CONSTRAINT [FK_tblJobDetail_Labour] FOREIGN KEY ([Labour]) REFERENCES [CON].[tblEmployee] ([EmployeeId])
);


GO
CREATE NONCLUSTERED INDEX [IX_tblJobDetail_Labour]
    ON [CON].[tblJobDetail]([Labour] ASC);


GO
CREATE NONCLUSTERED INDEX [IX_tblJobDetail_JobId]
    ON [CON].[tblJobDetail]([JobId] ASC);

