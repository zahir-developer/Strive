-- =============================================
-- Author:		Zahir / Shalini
-- Create date: 07-Dec-2020
-- Description:	Role Permission access scripts
-- =============================================
------------------MANAGER,CASHIER
DECLARE @AllPermission INT = (SELECT TOP 1 PermissionId FROM tblPermission WHERE PermissionName = 'All')
DECLARE @CashierRoleId INT = (SELECT TOP 1 RoleMasterId FROM tblRoleMaster WHERE RoleName = 'Manager')

DECLARE @IsActive INT = 1;
DECLARE @IsDeleted INT = 0;

--Modules
DECLARE @Module_DashboardId INT = (SELECT TOP 1 ModuleId FROM tblModule WHERE ModuleName = 'Dashboard')
DECLARE @Module_WashesId INT = (SELECT TOP 1 ModuleId FROM tblModule WHERE ModuleName = 'Washes')
DECLARE @Module_DetailId INT = (SELECT TOP 1 ModuleId FROM tblModule WHERE ModuleName = 'Detail')
DECLARE @Module_SalesId INT = (SELECT TOP 1 ModuleId FROM tblModule WHERE ModuleName = 'Sales')
DECLARE @Module_ReportId INT = (SELECT TOP 1 ModuleId FROM tblModule WHERE ModuleName = 'Report')
DECLARE @Module_WhiteLabellingId INT = (SELECT TOP 1 ModuleId FROM tblModule WHERE ModuleName = 'WhiteLabelling')
DECLARE @Module_MessengerId INT = (SELECT TOP 1 ModuleId FROM tblModule WHERE ModuleName = 'Messenger')
DECLARE @Module_CheckoutId INT = (SELECT TOP 1 ModuleId FROM tblModule WHERE ModuleName = 'Checkout')

--Screens
DECLARE @Screen_DashboardPageId INT = (SELECT TOP 1 ModuleScreenId FROM tblModuleScreen WHERE ViewName = 'DashboardPage')
DECLARE @Screen_WashesListPageId INT = (SELECT TOP 1 ModuleScreenId FROM tblModuleScreen WHERE ViewName = 'WashesListPage')
DECLARE @Screen_AddWashPageId INT = (SELECT TOP 1 ModuleScreenId FROM tblModuleScreen WHERE ViewName = 'AddWashPage')
DECLARE @Screen_DetailListPageId INT = (SELECT TOP 1 ModuleScreenId FROM tblModuleScreen WHERE ViewName = 'DetailListPage')
DECLARE @Screen_AddDetailPageId INT = (SELECT TOP 1 ModuleScreenId FROM tblModuleScreen WHERE ViewName = 'AddDetailPage')
DECLARE @Screen_SalesId INT = (SELECT TOP 1 ModuleScreenId FROM tblModuleScreen WHERE ViewName = 'Sales')
DECLARE @Screen_EODReportId INT = (SELECT TOP 1 ModuleScreenId FROM tblModuleScreen WHERE ViewName = 'EODReport')
DECLARE @Screen_DailyStatusScreenId INT = (SELECT TOP 1 ModuleScreenId FROM tblModuleScreen WHERE ViewName = 'DailyStatusScreen')
DECLARE @Screen_DailyTipreportId INT = (SELECT TOP 1 ModuleScreenId FROM tblModuleScreen WHERE ViewName = 'DailyTipreport')
DECLARE @Screen_MonthlyTipreportId INT = (SELECT TOP 1 ModuleScreenId FROM tblModuleScreen WHERE ViewName = 'MonthlyTipreport')
DECLARE @Screen_MonthlySalesReportId INT = (SELECT TOP 1 ModuleScreenId FROM tblModuleScreen WHERE ViewName = 'MonthlySalesReport')
DECLARE @Screen_MonthlyCustomerSummaryReportId INT = (SELECT TOP 1 ModuleScreenId FROM tblModuleScreen WHERE ViewName = 'MonthlyCustomerSummaryReport')
DECLARE @Screen_MonthlyMoneyOwedReportId INT = (SELECT TOP 1 ModuleScreenId FROM tblModuleScreen WHERE ViewName = 'MonthlyMoneyOwedReport')
DECLARE @Screen_MonthlyCustomerDetailReportId INT = (SELECT TOP 1 ModuleScreenId FROM tblModuleScreen WHERE ViewName = 'MonthlyCustomerDetailReport')
DECLARE @Screen_HourlyWashreportId INT = (SELECT TOP 1 ModuleScreenId FROM tblModuleScreen WHERE ViewName = 'HourlyWashreport')
DECLARE @Screen_DailySalesreportId INT = (SELECT TOP 1 ModuleScreenId FROM tblModuleScreen WHERE ViewName = 'DailySalesreport')
DECLARE @Screen_WhiteLabellingId INT = (SELECT TOP 1 ModuleScreenId FROM tblModuleScreen WHERE ViewName = 'WhiteLabelling')
DECLARE @Screen_MessengerId INT = (SELECT TOP 1 ModuleScreenId FROM tblModuleScreen WHERE ViewName = 'Messenger')
DECLARE @Screen_CheckoutListId INT = (SELECT TOP 1 ModuleScreenId FROM tblModuleScreen WHERE ViewName = 'CheckoutList')

--Cashier,Manager

Insert into tblRolePermission (ModuleId,ModuleScreenId,FieldId,PermissionId,RoleId,IsActive,IsDeleted) values(@Module_DashboardId, @Screen_DashboardPageId, NULL, @AllPermission, @CashierRoleId, @IsActive, @IsDeleted);
Insert into tblRolePermission (ModuleId,ModuleScreenId,FieldId,PermissionId,RoleId,IsActive,IsDeleted) values(@Module_WashesId, @Screen_WashesListPageId, NULL, @AllPermission, @CashierRoleId, @IsActive, @IsDeleted);
Insert into tblRolePermission (ModuleId,ModuleScreenId,FieldId,PermissionId,RoleId,IsActive,IsDeleted) values(@Module_WashesId, @Screen_AddWashPageId, NULL, @AllPermission, @CashierRoleId, @IsActive, @IsDeleted);
Insert into tblRolePermission (ModuleId,ModuleScreenId,FieldId,PermissionId,RoleId,IsActive,IsDeleted) values(@Module_DetailId, @Screen_DetailListPageId, NULL, @AllPermission, @CashierRoleId, @IsActive, @IsDeleted);
Insert into tblRolePermission (ModuleId,ModuleScreenId,FieldId,PermissionId,RoleId,IsActive,IsDeleted) values(@Module_DetailId, @Screen_AddDetailPageId, NULL, @AllPermission, @CashierRoleId, @IsActive, @IsDeleted);
Insert into tblRolePermission (ModuleId,ModuleScreenId,FieldId,PermissionId,RoleId,IsActive,IsDeleted) values(@Module_SalesId, @Screen_SalesId, NULL, @AllPermission, @CashierRoleId, @IsActive, @IsDeleted);
Insert into tblRolePermission (ModuleId,ModuleScreenId,FieldId,PermissionId,RoleId,IsActive,IsDeleted) values(@Module_ReportId, @Screen_EODReportId, NULL, @AllPermission, @CashierRoleId, @IsActive, @IsDeleted);
Insert into tblRolePermission (ModuleId,ModuleScreenId,FieldId,PermissionId,RoleId,IsActive,IsDeleted) values(@Module_ReportId, @Screen_DailyStatusScreenId, NULL, @AllPermission, @CashierRoleId, @IsActive, @IsDeleted);
Insert into tblRolePermission (ModuleId,ModuleScreenId,FieldId,PermissionId,RoleId,IsActive,IsDeleted) values(@Module_ReportId, @Screen_DailyTipreportId, NULL, @AllPermission, @CashierRoleId, @IsActive, @IsDeleted);
Insert into tblRolePermission (ModuleId,ModuleScreenId,FieldId,PermissionId,RoleId,IsActive,IsDeleted) values(@Module_ReportId, @Screen_MonthlyTipreportId, NULL, @AllPermission, @CashierRoleId, @IsActive, @IsDeleted);
Insert into tblRolePermission (ModuleId,ModuleScreenId,FieldId,PermissionId,RoleId,IsActive,IsDeleted) values(@Module_ReportId, @Screen_MonthlySalesReportId, NULL, @AllPermission, @CashierRoleId, @IsActive, @IsDeleted);
Insert into tblRolePermission (ModuleId,ModuleScreenId,FieldId,PermissionId,RoleId,IsActive,IsDeleted) values(@Module_ReportId, @Screen_MonthlyCustomerSummaryReportId, NULL, @AllPermission, @CashierRoleId, @IsActive, @IsDeleted);
Insert into tblRolePermission (ModuleId,ModuleScreenId,FieldId,PermissionId,RoleId,IsActive,IsDeleted) values(@Module_ReportId, @Screen_MonthlyMoneyOwedReportId, NULL, @AllPermission, @CashierRoleId, @IsActive, @IsDeleted);
Insert into tblRolePermission (ModuleId,ModuleScreenId,FieldId,PermissionId,RoleId,IsActive,IsDeleted) values(@Module_ReportId, @Screen_MonthlyCustomerDetailReportId, NULL, @AllPermission, @CashierRoleId, @IsActive, @IsDeleted);
Insert into tblRolePermission (ModuleId,ModuleScreenId,FieldId,PermissionId,RoleId,IsActive,IsDeleted) values(@Module_ReportId, @Screen_HourlyWashreportId, NULL, @AllPermission, @CashierRoleId, @IsActive, @IsDeleted);
Insert into tblRolePermission (ModuleId,ModuleScreenId,FieldId,PermissionId,RoleId,IsActive,IsDeleted) values(@Module_ReportId, @Screen_DailySalesreportId, NULL, @AllPermission, @CashierRoleId, @IsActive, @IsDeleted);
Insert into tblRolePermission (ModuleId,ModuleScreenId,FieldId,PermissionId,RoleId,IsActive,IsDeleted) values(@Module_WhiteLabellingId, @Screen_WhiteLabellingId, NULL, @AllPermission, @CashierRoleId, @IsActive, @IsDeleted);
Insert into tblRolePermission (ModuleId,ModuleScreenId,FieldId,PermissionId,RoleId,IsActive,IsDeleted) values(@Module_MessengerId, @Screen_MessengerId, NULL, @AllPermission, @CashierRoleId, @IsActive, @IsDeleted);
Insert into tblRolePermission (ModuleId,ModuleScreenId,FieldId,PermissionId,RoleId,IsActive,IsDeleted) values(@Module_CheckoutId, @Screen_CheckoutListId, NULL, @AllPermission, @CashierRoleId, @IsActive, @IsDeleted);
