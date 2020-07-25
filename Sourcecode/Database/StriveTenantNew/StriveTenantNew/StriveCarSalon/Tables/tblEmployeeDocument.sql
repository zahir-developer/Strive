CREATE TABLE [StriveCarSalon].[tblEmployeeDocument] (
    [DocumentId]          INT                IDENTITY (1, 1) NOT NULL,
    [EmployeeId]          INT                NULL,
    [Filename]            VARCHAR (20)       NOT NULL,
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
    CONSTRAINT [PK_tblEmployeeDocument] PRIMARY KEY CLUSTERED ([DocumentId] ASC),
    CONSTRAINT [FK_tblEmployeeDocument_tblEmployee] FOREIGN KEY ([EmployeeId]) REFERENCES [StriveCarSalon].[tblEmployee] ([EmployeeId])
);

