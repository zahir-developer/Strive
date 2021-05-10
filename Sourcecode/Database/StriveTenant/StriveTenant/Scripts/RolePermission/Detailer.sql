
DECLARE @AllPermission INT = 1;
DECLARE @IsActive INT = 1;
DECLARE @IsDeleted INT = 0;

DECLARE @Module_DashboardId INT = (SELECT TOP 1 ModuleId FROM tblModule WHERE ModuleName = 'Dashboard')
DECLARE @Module_WashesId INT = (SELECT TOP 1 ModuleId FROM tblModule WHERE ModuleName = 'Washes')
DECLARE @Module_DetailId INT = (SELECT TOP 1 ModuleId FROM tblModule WHERE ModuleName = 'Detail')
DECLARE @Module_SalesId INT = (SELECT TOP 1 ModuleId FROM tblModule WHERE ModuleName = 'Sales')
DECLARE @Module_ReportId INT = (SELECT TOP 1 ModuleId FROM tblModule WHERE ModuleName = 'Report')
DECLARE @Module_WhiteLabellingId INT = (SELECT TOP 1 ModuleId FROM tblModule WHERE ModuleName = 'WhiteLabelling')
DECLARE @Module_MessengerId INT = (SELECT TOP 1 ModuleId FROM tblModule WHERE ModuleName = 'Messenger')
DECLARE @Module_AdminId INT = (SELECT TOP 1 ModuleId FROM tblModule WHERE ModuleName = 'Admin')
DECLARE @Module_PayRollId INT = (SELECT TOP 1 ModuleId FROM tblModule WHERE ModuleName = 'PayRoll')
DECLARE @Module_CheckoutId INT = (SELECT TOP 1 ModuleId FROM tblModule WHERE ModuleName = 'Checkout')
DECLARE @Module_MyScheduleId INT = (SELECT TOP 1 ModuleId FROM tblModule WHERE ModuleName = 'MySchedule')
DECLARE @Module_MyProfileId INT = (SELECT TOP 1 ModuleId FROM tblModule WHERE ModuleName = 'MyProfile')
DECLARE @Module_MyTicketsId INT = (SELECT TOP 1 ModuleId FROM tblModule WHERE ModuleName = 'MyTickets')
DECLARE @Module_CustomerId INT = (SELECT TOP 1 ModuleId FROM tblModule WHERE ModuleName = 'Customer')
DECLARE @Module_CustomerHistoryId INT = (SELECT TOP 1 ModuleId FROM tblModule WHERE ModuleName = 'CustomerHistory')
DECLARE @DetailerRoleId INT = (SELECT TOP 1 RoleId FROM tblRoleMaster WHERE RoleName = 'Detailer')

Insert into tblRolePermissionDetail (ModuleId, ModuleScreenId, FieldId, PermissionId, RoleId, IsActive, IsDeleted) 
values(@Module_DetailId, @Screen_DetailListPageId, NULL, @AllPermission, @DetailerRoleId, @IsActive, @IsDeleted);

Insert into tblRolePermissionDetail (ModuleId,ModuleScreenId,FieldId,PermissionId,RoleId,IsActive,IsDeleted) 
values(@Module_DetailId, @Screen_AddDetailPageId, NULL, @AllPermission, @DetailerRoleId, @IsActive, @IsDeleted);