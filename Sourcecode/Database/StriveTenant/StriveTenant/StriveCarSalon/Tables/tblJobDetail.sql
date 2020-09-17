CREATE TABLE [StriveCarSalon].[tblJobDetail] (
    [JobDetailId] INT                IDENTITY (1, 1) NOT NULL,
    [JobId]       INT                NULL,
    [BayId]       INT                NULL,
    [SalesRep]    INT                NULL,
    [QABy]        INT                NULL,
    [Review]      INT                NULL,
    [ReviewNote]  VARCHAR (50)       NULL,
    [IsActive]    BIT                NULL,
    [IsDeleted]   BIT                NULL,
    [CreatedBy]   INT                NULL,
    [CreatedDate] DATETIMEOFFSET (7) NULL,
    [UpdatedBy]   INT                NULL,
    [UpdatedDate] DATETIMEOFFSET (7) NULL,
    CONSTRAINT [PK_tblJobDetail] PRIMARY KEY CLUSTERED ([JobDetailId] ASC),
    CONSTRAINT [FK_tblJobDetail_BayId] FOREIGN KEY ([BayId]) REFERENCES [StriveCarSalon].[tblBay] ([BayId]),
    CONSTRAINT [FK_tblJobDetail_JobId] FOREIGN KEY ([JobId]) REFERENCES [StriveCarSalon].[tblJob] ([JobId])
);


GO
CREATE NONCLUSTERED INDEX [IX_tblJobDetail_JobId]
    ON [StriveCarSalon].[tblJobDetail]([JobId] ASC);

