﻿CREATE TABLE [StriveCarSalon].[tblRolePermission] (
    [RolePermissionId] INT                IDENTITY (1, 1) NOT NULL,
    [ModuleId]         INT                NOT NULL,
    [ModuleScreenId]   INT                NOT NULL,
    [FieldId]          INT                NULL,
    [PermissionId]     INT                NOT NULL,
    [RoleId]           INT                NOT NULL,
    [IsActive]         BIT                NULL,
    [IsDeleted]        BIT                NULL,
    [CreatedBy]        INT                NULL,
    [CreatedDate]      DATETIMEOFFSET (7) NULL,
    [UpdatedBy]        INT                NULL,
    [UpdatedDate]      DATETIMEOFFSET (7) NULL,
    CONSTRAINT [FK_StriveCarSalon_TblRolePermission_FieldId] FOREIGN KEY ([FieldId]) REFERENCES [StriveCarSalon].[tblField] ([FieldId]),
    CONSTRAINT [FK_StriveCarSalon_TblRolePermission_ModuleId] FOREIGN KEY ([ModuleId]) REFERENCES [StriveCarSalon].[tblModule] ([ModuleId]),
    CONSTRAINT [FK_StriveCarSalon_TblRolePermission_ModuleScreenId] FOREIGN KEY ([ModuleScreenId]) REFERENCES [StriveCarSalon].[tblModuleScreen] ([ModuleScreenId]),
    CONSTRAINT [FK_StriveCarSalon_TblRolePermission_PermissionId] FOREIGN KEY ([PermissionId]) REFERENCES [StriveCarSalon].[tblPermission] ([PermissionId]),
    CONSTRAINT [FK_StriveCarSalon_TblRolePermission_RoleId] FOREIGN KEY ([RoleId]) REFERENCES [StriveCarSalon].[tblRoleMaster] ([RoleMasterId])
);

