


DECLARE @AllPermission INT = (SELECT TOP 1 PermissionId FROM tblPermission WHERE PermissionName = 'All')

DECLARE @AdminRoleId INT = (SELECT TOP 1 RoleMasterId FROM tblRoleMaster WHERE RoleName = 'Admin')


--INSERT [tblModule] ( [ModuleName], [IsActive], [IsDeleted], [Description]) VALUES ('CustomerHistory',  1, 0,'Customer History')

DECLARE @Module_CustomerHistoryId INT = (SELECT TOP 1 ModuleId FROM tblModule WHERE ModuleName = 'CustomerHistory')

--insert into tblModuleScreen(ModuleId,ViewName,IsActive,IsDeleted,Description)values(@Module_CustomerHistoryId,'CustomerHistory',1,0,'Customer History')

DECLARE @Screen_CustomerHistoryId INT = (SELECT TOP 1 ModuleScreenId FROM tblModuleScreen WHERE ViewName = 'CustomerHistory')


Insert into tblRolePermission (ModuleId,ModuleScreenId,FieldId,PermissionId,RoleId,IsActive,IsDeleted) values(@Module_CustomerHistoryId, @Screen_CustomerHistoryId, NULL, @AllPermission, @AdminRoleId, 1, 0);

