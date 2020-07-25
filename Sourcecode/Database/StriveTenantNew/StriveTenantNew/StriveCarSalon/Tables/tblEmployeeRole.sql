CREATE TABLE [StriveCarSalon].[tblEmployeeRole] (
    [EmployeeId]  INT                NULL,
    [RoleId]      INT                NULL,
    [IsDefault]   BIT                NULL,
    [IsActive]    BIT                NULL,
    [IsDeleted]   BIT                NULL,
    [CreatedBy]   INT                NULL,
    [CreatedDate] DATETIMEOFFSET (7) NULL,
    [UpdatedBy]   INT                NULL,
    [UpdatedDate] DATETIMEOFFSET (7) NULL,
    CONSTRAINT [FK_tblEmployeeRole_tblEmployee] FOREIGN KEY ([EmployeeId]) REFERENCES [StriveCarSalon].[tblEmployee] ([EmployeeId])
);

