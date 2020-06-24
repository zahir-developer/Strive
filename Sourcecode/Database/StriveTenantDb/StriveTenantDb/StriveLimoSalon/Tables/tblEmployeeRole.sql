CREATE TABLE [StriveLimoSalon].[tblEmployeeRole] (
    [EmployeeId] BIGINT NULL,
    [RoleId]     INT    NULL,
    [IsDefault]  BIT    NULL,
    [IsActive]   BIT    NULL,
    CONSTRAINT [FK_tblEmployeeRole_tblEmployee] FOREIGN KEY ([EmployeeId]) REFERENCES [StriveLimoSalon].[tblEmployee] ([EmployeeId])
);

