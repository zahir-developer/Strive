CREATE TABLE [StriveCarSalon].[tblJobItem] (
    [JobItemId]  INT             IDENTITY (1, 1) NOT NULL,
    [JobId]      INT             NULL,
    [ServiceId]  INT             NULL,
    [Commission] DECIMAL (16, 2) NULL,
    [Price]      DECIMAL (16, 2) NULL,
    [Quantity]   INT             NULL,
    [ReviewNote] NVARCHAR (50)   NULL,
    CONSTRAINT [PK_tblJobItem] PRIMARY KEY CLUSTERED ([JobItemId] ASC),
    CONSTRAINT [FK_tblJobItem_tblJob] FOREIGN KEY ([JobId]) REFERENCES [StriveCarSalon].[tblJob] ([JobId]),
    CONSTRAINT [FK_tblJobItem_tblService] FOREIGN KEY ([ServiceId]) REFERENCES [StriveLimoSalon].[tblService] ([ServiceId])
);

