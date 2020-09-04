CREATE TABLE [CON].[tblLog] (
    [LogId]       INT                NULL,
    [Logtext]     NVARCHAR (MAX)     NULL,
    [LogDate]     DATE               NULL,
    [Schemaname]  VARCHAR (50)       NULL,
    [CreatedDate] DATETIMEOFFSET (7) NULL
);

