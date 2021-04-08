-- =============================================
-- Author:		Zahir / Shalini
-- Create date: 07-Dec-2020
-- Description:	Role Permission access scritp for Customer 
-- =============================================

DECLARE @AllPermission INT = (SELECT TOP 1 PermissionId FROM tblPermission WHERE PermissionName = 'All')
DECLARE @Module_CustomerDashboard INT = (SELECT TOP 1 ModuleId FROM tblModule WHERE ModuleName = 'Customer')
DECLARE @Screen_CustomerDashboardId INT = (SELECT TOP 1 ModuleScreenId FROM tblModuleScreen WHERE ViewName = 'CustomerDashboard')

DECLARE @CustomerRoleId INT = (SELECT TOP 1 RoleMasterId FROM tblRoleMaster WHERE RoleName = 'Customer')

Insert into tblRolePermissionDetail (ModuleId,ModuleScreenId,FieldId,PermissionId,RoleId,IsActive,IsDeleted) values(@Module_CustomerDashboard, @Screen_CustomerDashboardId, NULL, @AllPermission, @CustomerRoleId, 1, 0);
