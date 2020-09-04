CREATE TABLE [CON].[tblEmployeeDocument] (
    [EmployeeDocumentId]  INT                IDENTITY (1, 1) NOT NULL,
    [EmployeeId]          INT                NULL,
    [Filename]            VARCHAR (50)       NOT NULL,
    [Filepath]            VARCHAR (50)       NOT NULL,
    [FileType]            VARCHAR (10)       NOT NULL,
    [IsPasswordProtected] BIT                NULL,
    [Password]            NVARCHAR (15)      NULL,
    [Comments]            VARCHAR (5)        NULL,
    [IsActive]            BIT                NULL,
    [IsDeleted]           BIT                NULL,
    [CreatedBy]           INT                NULL,
    [CreatedDate]         DATETIMEOFFSET (7) NULL,
    [UpdatedBy]           INT                NULL,
    [UpdatedDate]         DATETIMEOFFSET (7) NULL,
    CONSTRAINT [PK_tblEmployeeDocument] PRIMARY KEY CLUSTERED ([EmployeeDocumentId] ASC),
    CONSTRAINT [FK_tblEmployeeDocument_EmployeeId] FOREIGN KEY ([EmployeeId]) REFERENCES [CON].[tblEmployee] ([EmployeeId])
);


GO
CREATE NONCLUSTERED INDEX [IX_tblEmployeeDocument_EmployeeId]
    ON [CON].[tblEmployeeDocument]([EmployeeId] ASC);

