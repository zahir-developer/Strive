CREATE TYPE [StriveCarSalonTest].[tvpEmployeeRoles] AS TABLE (
    [EmployeeId]      BIGINT NULL,
    [RoleId]          INT    NULL,
    [IsDefault]       BIT    NULL,
    [IsActive]        BIT    NULL,
    [EmployeeRolesId] BIGINT NOT NULL);

