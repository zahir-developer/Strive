CREATE TABLE [StriveCarSalon].[tblRolePermission] (
    [RolePermissionId] INT                IDENTITY (1, 1) NOT NULL,
    [PermissionName]   VARCHAR (250)      NULL,
    [IsActive]         BIT                NULL,
    [IsDeleted]        BIT                NULL,
    [CreatedBy]        INT                NULL,
    [CreatedDate]      DATETIMEOFFSET (7) NULL,
    [UpdatedBy]        INT                NULL,
    [UpdatedDate]      DATETIMEOFFSET (7) NULL,
    CONSTRAINT [PK_StriveCarSalon_tblRolePermission_PermissionId] PRIMARY KEY CLUSTERED ([RolePermissionId] ASC)
);





