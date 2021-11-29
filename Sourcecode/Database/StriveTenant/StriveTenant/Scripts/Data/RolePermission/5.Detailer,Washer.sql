-- =============================================
-- Author:		Zahir / Shalini
-- Create date: 07-Dec-2020
-- Description:	Role Permission access scripts
-- =============================================

--TRUNCATE TABLE tblPermission

---DETAILER,WASH
DECLARE @AllPermission INT = (SELECT TOP 1 PermissionId FROM tblPermission WHERE PermissionName = 'All')
DECLARE @DetailererRoleId INT = (SELECT TOP 1 RoleMasterId FROM tblRoleMaster WHERE RoleName = 'Detailer')

DECLARE @IsActive INT = 0;
DECLARE @IsDeleted INT = 0;
--Modules
DECLARE @Module_DashboardId INT = (SELECT TOP 1 ModuleId FROM tblModule WHERE ModuleName = 'Dashboard')
DECLARE @Module_WashesId INT = (SELECT TOP 1 ModuleId FROM tblModule WHERE ModuleName = 'Washes')
DECLARE @Module_DetailId INT = (SELECT TOP 1 ModuleId FROM tblModule WHERE ModuleName = 'Detail')

--view
DECLARE @Screen_DashboardPageId INT = (SELECT TOP 1 ModuleScreenId FROM tblModuleScreen WHERE ViewName = 'DashboardPage')
DECLARE @Screen_WashesListPageId INT = (SELECT TOP 1 ModuleScreenId FROM tblModuleScreen WHERE ViewName = 'WashesListPage')
DECLARE @Screen_AddWashPageId INT = (SELECT TOP 1 ModuleScreenId FROM tblModuleScreen WHERE ViewName = 'AddWashPage')
DECLARE @Screen_DetailListPageId INT = (SELECT TOP 1 ModuleScreenId FROM tblModuleScreen WHERE ViewName = 'DetailListPage')
DECLARE @Screen_AddDetailPageId INT = (SELECT TOP 1 ModuleScreenId FROM tblModuleScreen WHERE ViewName = 'AddDetailPage')

--filed
DECLARE @Field_DashboardStatistics_Section2Id INT = (SELECT TOP 1 FieldId FROM tblField WHERE FieldName = 'DashboardStatisticsSection2')
DECLARE @Field_DashboardStatistics_Section3Id INT = (SELECT TOP 1 FieldId FROM tblField WHERE FieldName = 'DashboardStatisticsSection3')


Insert into tblRolePermission (ModuleId,ModuleScreenId,FieldId,PermissionId,RoleId,IsActive,IsDeleted) values(@Module_DashboardId, @Screen_DashboardPageId, @Field_DashboardStatistics_Section2Id, @AllPermission, @CashierRoleId, @IsDeActive, @IsDeleted);
Insert into tblRolePermission (ModuleId,ModuleScreenId,FieldId,PermissionId,RoleId,IsActive,IsDeleted) values(@Module_DashboardId, @Screen_DashboardPageId, @Field_DashboardStatistics_Section3Id, @AllPermission, @CashierRoleId, @IsDeActive, @IsDeleted);
Insert into tblRolePermission (ModuleId,ModuleScreenId,FieldId,PermissionId,RoleId,IsActive,IsDeleted) values(@Module_WashesId, @Screen_WashesListPageId, NULL, @AllPermission, @WaherRoleId, @IsActive, @IsDeleted);
Insert into tblRolePermission (ModuleId,ModuleScreenId,FieldId,PermissionId,RoleId,IsActive,IsDeleted) values(@Module_WashesId, @Screen_AddWashPageId, NULL, @AllPermission, @WaherRoleId, @IsActive, @IsDeleted);
Insert into tblRolePermission (ModuleId,ModuleScreenId,FieldId,PermissionId,RoleId,IsActive,IsDeleted) values(@Module_DetailId, @Screen_DetailListPageId, NULL, @AllPermission, @DetailererRoleId, @IsActive, @IsDeleted);
Insert into tblRolePermission (ModuleId,ModuleScreenId,FieldId,PermissionId,RoleId,IsActive,IsDeleted) values(@Module_DetailId, @Screen_AddDetailPageId, NULL, @AllPermission, @DetailererRoleId, @IsActive, @IsDeleted);
