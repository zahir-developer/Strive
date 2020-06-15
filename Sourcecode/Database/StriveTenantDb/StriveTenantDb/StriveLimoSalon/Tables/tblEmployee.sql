CREATE TABLE [StriveLimoSalon].[tblEmployee] (
    [EmployeeId] INT          IDENTITY (1, 1) NOT NULL,
    [FirstName]  VARCHAR (50) NULL,
    [LastName]   VARCHAR (50) NULL,
    [Role]       INT          NULL,
    CONSTRAINT [PK_tblEmployee] PRIMARY KEY CLUSTERED ([EmployeeId] ASC)
);

