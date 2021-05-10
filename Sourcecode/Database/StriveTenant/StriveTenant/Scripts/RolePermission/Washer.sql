
DECLARE @AllPermission INT = 1;
DECLARE @IsActive INT = 1;
DECLARE @IsDeleted INT = 0;

DECLARE @WasherRoleId INT = (SELECT TOP 1 RoleId FROM tblRoleMaster WHERE RoleName = 'Washer')

Insert into tblRolePermissionDetail (ModuleId,ModuleScreenId,FieldId,PermissionId,RoleId,IsActive,IsDeleted) 
values(@Module_WashesId, @Screen_WashesListPageId, NULL, @AllPermission, @WasherRoleId, 1, 0);

Insert into tblRolePermissionDetail (ModuleId,ModuleScreenId,FieldId,PermissionId,RoleId,IsActive,IsDeleted) 
values(@Module_WashesId, @Screen_AddWashPageId, NULL, @AllPermission, @DetailerRoleId, 1, 0);


