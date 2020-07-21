CREATE TABLE [StriveCarSalon].[tblEmployeeRole] (
    [EmployeeId]        BIGINT NULL,
    [RoleId]            INT    NULL,
    [IsDefault]         BIT    NULL,
    [IsActive]          BIT    NULL,
    [tblEmployeeRoleId] BIGINT IDENTITY (1, 1) NOT NULL,
    CONSTRAINT [PK_tblEmployeeRole] PRIMARY KEY CLUSTERED ([tblEmployeeRoleId] ASC),
    CONSTRAINT [FK_tblEmployeeRole_tblEmployee] FOREIGN KEY ([EmployeeId]) REFERENCES [StriveCarSalon].[tblEmployee] ([EmployeeId])
);



