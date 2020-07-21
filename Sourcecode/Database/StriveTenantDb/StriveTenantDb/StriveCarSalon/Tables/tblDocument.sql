CREATE TABLE [StriveCarSalon].[tblDocument] (
    [DocumentId]   BIGINT        IDENTITY (1, 1) NOT NULL,
    [EmployeeId]   BIGINT        NULL,
    [Filename]     VARCHAR (MAX) NOT NULL,
    [Filepath]     VARCHAR (MAX) NOT NULL,
    [Password]     VARCHAR (MAX) NOT NULL,
    [CreatedDate]  DATETIME      NOT NULL,
    [ModifiedDate] DATETIME      NOT NULL,
    [IsActive]     BIT           NOT NULL,
    CONSTRAINT [pKey] PRIMARY KEY CLUSTERED ([DocumentId] ASC),
    CONSTRAINT [FK_tblEmployee_EmployeeId] FOREIGN KEY ([EmployeeId]) REFERENCES [StriveCarSalon].[tblEmployee] ([EmployeeId])
);

