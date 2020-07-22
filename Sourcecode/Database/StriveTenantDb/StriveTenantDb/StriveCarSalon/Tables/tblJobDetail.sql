CREATE TABLE [StriveCarSalon].[tblJobDetail] (
    [JobDetailId] INT           IDENTITY (1, 1) NOT NULL,
    [JobId]       INT           NULL,
    [BayId]       INT           NULL,
    [SalesRep]    INT           NULL,
    [QABy]        INT           NULL,
    [Labour]      INT           NULL,
    [Review]      INT           NULL,
    [ReviewNote]  NVARCHAR (50) NULL,
    CONSTRAINT [PK_tblJobDetail] PRIMARY KEY CLUSTERED ([JobDetailId] ASC),
    CONSTRAINT [FK_tblJobDetail_tblBay] FOREIGN KEY ([BayId]) REFERENCES [StriveCarSalon].[tblBay] ([BayId]),
    CONSTRAINT [FK_tblJobDetail_tblJob] FOREIGN KEY ([JobId]) REFERENCES [StriveCarSalon].[tblJob] ([JobId]),
    CONSTRAINT [FK_tblJobDetail_tblJobDetail] FOREIGN KEY ([JobDetailId]) REFERENCES [StriveCarSalon].[tblJobDetail] ([JobDetailId])
);

