CREATE TABLE [StriveCarSalon].[tblEmployeeRole] (
    [EmployeeRoleId] INT                IDENTITY (1, 1) NOT NULL,
    [EmployeeId]     INT                NULL,
    [RoleId]         INT                NULL,
    [IsDefault]      BIT                NULL,
    [IsActive]       BIT                NULL,
    [IsDeleted]      BIT                NULL,
    [CreatedBy]      INT                NULL,
    [CreatedDate]    DATETIMEOFFSET (7) NULL,
    [UpdatedBy]      INT                NULL,
    [UpdatedDate]    DATETIMEOFFSET (7) NULL,
    CONSTRAINT [PK_tblEmployeeRole] PRIMARY KEY CLUSTERED ([EmployeeRoleId] ASC),
    CONSTRAINT [FK_tblEmployeeRole_tblEmployee] FOREIGN KEY ([EmployeeId]) REFERENCES [StriveCarSalon].[tblEmployee] ([EmployeeId]),
    CONSTRAINT [FK_tblEmployeeRole_tblRoleMaster] FOREIGN KEY ([RoleId]) REFERENCES [StriveCarSalon].[tblRoleMaster] ([RoleMasterId])
);



