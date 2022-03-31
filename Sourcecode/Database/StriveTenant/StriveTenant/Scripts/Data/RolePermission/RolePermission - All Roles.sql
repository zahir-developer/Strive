
-- =============================================
-- Author:		Zahir
-- Create date: 25-Mar-2022
-- Description:	Role Permission access scripts
-- =============================================


DECLARE @AllPermission INT = (SELECT TOP 1 PermissionId FROM tblPermission WHERE PermissionName = 'All')


--RoleMaster
DECLARE @AdminRoleId INT = (SELECT TOP 1 RoleMasterId FROM tblRoleMaster WHERE RoleName = 'Admin')
DECLARE @GreeterRoleId INT = (SELECT TOP 1 RoleMasterId FROM tblRoleMaster WHERE RoleName = 'Greet Bay')
DECLARE @ManagerRoleId INT = (SELECT TOP 1 RoleMasterId FROM tblRoleMaster WHERE RoleName = 'Manager')
DECLARE @CashierRoleId INT = (SELECT TOP 1 RoleMasterId FROM tblRoleMaster WHERE RoleName = 'Cashier')
DECLARE @WasherRoleId INT = (SELECT TOP 1 RoleMasterId FROM tblRoleMaster WHERE RoleName = 'Washer')
DECLARE @DetailerRoleId INT = (SELECT TOP 1 RoleMasterId FROM tblRoleMaster WHERE RoleName = 'Detailer')
DECLARE @ClientRoleId INT = (SELECT TOP 1 RoleMasterId FROM tblRoleMaster WHERE RoleName = 'Customer')

--Modules
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
DECLARE @Module_ScheduleId INT = (SELECT TOP 1 ModuleId FROM tblModule WHERE ModuleName = 'MySchedule')
DECLARE @Module_ProfileId INT = (SELECT TOP 1 ModuleId FROM tblModule WHERE ModuleName = 'MyProfile')
DECLARE @Module_MyTicketsId INT = (SELECT TOP 1 ModuleId FROM tblModule WHERE ModuleName = 'MyTickets')
DECLARE @Module_CustomerId INT = (SELECT TOP 1 ModuleId FROM tblModule WHERE ModuleName = 'Customer')
DECLARE @Module_CustomerHistoryId INT = (SELECT TOP 1 ModuleId FROM tblModule WHERE ModuleName = 'CustomerHistory')

--Screens


--Select * from tblModuleScreen
-- Update tblModuleScreen set ViewName = 'WashesListPage' where ModuleScreenId =2

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
DECLARE @Screen_GiftCardsId INT = (SELECT TOP 1 ModuleScreenId FROM tblModuleScreen WHERE ViewName = 'GiftCards')
DECLARE @Screen_SchedulesId INT = (SELECT TOP 1 ModuleScreenId FROM tblModuleScreen WHERE ViewName = 'Schedules')
DECLARE @Screen_VehiclesId INT = (SELECT TOP 1 ModuleScreenId FROM tblModuleScreen WHERE ViewName = 'Vehicles')
DECLARE @Screen_ClientsId INT = (SELECT TOP 1 ModuleScreenId FROM tblModuleScreen WHERE ViewName = 'Clients')
DECLARE @Screen_EmployeesId INT = (SELECT TOP 1 ModuleScreenId FROM tblModuleScreen WHERE ViewName = 'Employees')
DECLARE @Screen_TimeClockMaintenanceId INT = (SELECT TOP 1 ModuleScreenId FROM tblModuleScreen WHERE ViewName = 'TimeClockMaintenance')
DECLARE @Screen_CloseOutRegisterId INT = (SELECT TOP 1 ModuleScreenId FROM tblModuleScreen WHERE ViewName = 'CloseOutRegister')
DECLARE @Screen_CashRegisterSetupId INT = (SELECT TOP 1 ModuleScreenId FROM tblModuleScreen WHERE ViewName = 'CashRegisterSetup')
DECLARE @Screen_ProductSetupId INT = (SELECT TOP 1 ModuleScreenId FROM tblModuleScreen WHERE ViewName = 'ProductSetup')
DECLARE @Screen_ServiceSetupId INT = (SELECT TOP 1 ModuleScreenId FROM tblModuleScreen WHERE ViewName = 'ServiceSetup')
DECLARE @Screen_SystemSetupId INT = (SELECT TOP 1 ModuleScreenId FROM tblModuleScreen WHERE ViewName = 'SystemSetup')
DECLARE @Screen_ChecklistSetupId INT = (SELECT TOP 1 ModuleScreenId FROM tblModuleScreen WHERE ViewName = 'ChecklistSetup')
DECLARE @Screen_EmployeeHandbookId INT = (SELECT TOP 1 ModuleScreenId FROM tblModuleScreen WHERE ViewName = 'EmployeeHandbook')
DECLARE @Screen_BonusSetupId INT = (SELECT TOP 1 ModuleScreenId FROM tblModuleScreen WHERE ViewName = 'BonusSetup')
DECLARE @Screen_WhiteLabellingId INT = (SELECT TOP 1 ModuleScreenId FROM tblModuleScreen WHERE ViewName = 'WhiteLabelling')
DECLARE @Screen_MessengerId INT = (SELECT TOP 1 ModuleScreenId FROM tblModuleScreen WHERE ViewName = 'Messenger')
DECLARE @Screen_PayRollListId INT = (SELECT TOP 1 ModuleScreenId FROM tblModuleScreen WHERE ViewName = 'PayRollList')
DECLARE @Screen_CheckoutListId INT = (SELECT TOP 1 ModuleScreenId FROM tblModuleScreen WHERE ViewName = 'CheckoutList')
DECLARE @Screen_CustomerDashboardId INT = (SELECT TOP 1 ModuleScreenId FROM tblModuleScreen WHERE ViewName = 'CustomerDashboard')
DECLARE @Screen_CustomerHistoryId INT = (SELECT TOP 1 ModuleScreenId FROM tblModuleScreen WHERE ViewName = 'CustomerHistory')
DECLARE @Screen_AdSetupId INT = (SELECT TOP 1 ModuleScreenId FROM tblModuleScreen WHERE ViewName = 'AdSetup')
DECLARE @Screen_DealSetupId INT = (SELECT TOP 1 ModuleScreenId FROM tblModuleScreen WHERE ViewName = 'DealSetup')
DECLARE @Screen_MembershipSetupId INT = (SELECT TOP 1 ModuleScreenId FROM tblModuleScreen WHERE ViewName = 'MembershipSetup')
DECLARE @Screen_BasicSetupId INT = (SELECT TOP 1 ModuleScreenId FROM tblModuleScreen WHERE ViewName = 'BasicSetup')
DECLARE @Screen_TermsAndConditionsId INT = (SELECT TOP 1 ModuleScreenId FROM tblModuleScreen WHERE ViewName = 'TermsAndConditions')
DECLARE @Screen_VendorSetupId INT = (SELECT TOP 1 ModuleScreenId FROM tblModuleScreen WHERE ViewName = 'VendorSetup')



DECLARE @IsActive INT = 1;
DECLARE @IsDeleted INT = 0;


--GREETER
Insert into tblRolePermissionDetail (ModuleId,ModuleScreenId,FieldId,PermissionId,RoleId,IsActive,IsDeleted) values(@Module_MessengerId, @Screen_MessengerId, NULL, @AllPermission, @GreeterRoleId, @IsActive, @IsDeleted);
Insert into tblRolePermissionDetail (ModuleId,ModuleScreenId,FieldId,PermissionId,RoleId,IsActive,IsDeleted) values(@Module_CheckOutId, @Screen_CheckoutListId, NULL, @AllPermission, @GreeterRoleId, @IsActive, @IsDeleted);
Insert into tblRolePermissionDetail (ModuleId,ModuleScreenId,FieldId,PermissionId,RoleId,IsActive,IsDeleted) values(@Module_MessengerId, @Screen_MessengerId, NULL, @AllPermission, @WasherRoleId, @IsActive, @IsDeleted);
Insert into tblRolePermissionDetail (ModuleId,ModuleScreenId,FieldId,PermissionId,RoleId,IsActive,IsDeleted) values(@Module_MessengerId, @Screen_MessengerId, NULL, @AllPermission, @DetailerRoleId, @IsActive, @IsDeleted);



--ADMIN/OWNER
Insert into tblRolePermissionDetail (ModuleId,ModuleScreenId,FieldId,PermissionId,RoleId,IsActive,IsDeleted) values(@Module_DashboardId, @Screen_DashboardPageId, NULL, @AllPermission, @AdminRoleId, @IsActive, @IsDeleted);
Insert into tblRolePermissionDetail (ModuleId,ModuleScreenId,FieldId,PermissionId,RoleId,IsActive,IsDeleted) values(@Module_DashboardId, @Screen_DashboardPageId, NULL, @AllPermission, @AdminRoleId, @IsActive, @IsDeleted);
Insert into tblRolePermissionDetail (ModuleId,ModuleScreenId,FieldId,PermissionId,RoleId,IsActive,IsDeleted) values(@Module_WashesId, @Screen_WashesListPageId, NULL, @AllPermission, @AdminRoleId, @IsActive, @IsDeleted);
Insert into tblRolePermissionDetail (ModuleId,ModuleScreenId,FieldId,PermissionId,RoleId,IsActive,IsDeleted) values(@Module_WashesId, @Screen_AddWashPageId, NULL, @AllPermission, @AdminRoleId, @IsActive, @IsDeleted);
Insert into tblRolePermissionDetail (ModuleId,ModuleScreenId,FieldId,PermissionId,RoleId,IsActive,IsDeleted) values(@Module_DetailId, @Screen_DetailListPageId, NULL, @AllPermission, @AdminRoleId, @IsActive, @IsDeleted);
Insert into tblRolePermissionDetail (ModuleId,ModuleScreenId,FieldId,PermissionId,RoleId,IsActive,IsDeleted) values(@Module_DetailId, @Screen_AddDetailPageId, NULL, @AllPermission, @AdminRoleId, @IsActive, @IsDeleted);
Insert into tblRolePermissionDetail (ModuleId,ModuleScreenId,FieldId,PermissionId,RoleId,IsActive,IsDeleted) values(@Module_SalesId, @Screen_SalesId, NULL, @AllPermission, @AdminRoleId, @IsActive, @IsDeleted);
Insert into tblRolePermissionDetail (ModuleId,ModuleScreenId,FieldId,PermissionId,RoleId,IsActive,IsDeleted) values(@Module_ReportId, @Screen_EODReportId, NULL, @AllPermission, @AdminRoleId, @IsActive, @IsDeleted);
Insert into tblRolePermissionDetail (ModuleId,ModuleScreenId,FieldId,PermissionId,RoleId,IsActive,IsDeleted) values(@Module_ReportId, @Screen_DailyStatusScreenId, NULL, @AllPermission, @AdminRoleId, @IsActive, @IsDeleted);
Insert into tblRolePermissionDetail (ModuleId,ModuleScreenId,FieldId,PermissionId,RoleId,IsActive,IsDeleted) values(@Module_ReportId, @Screen_DailyTipreportId, NULL, @AllPermission, @AdminRoleId, @IsActive, @IsDeleted);
Insert into tblRolePermissionDetail (ModuleId,ModuleScreenId,FieldId,PermissionId,RoleId,IsActive,IsDeleted) values(@Module_ReportId, @Screen_MonthlyTipreportId, NULL, @AllPermission, @AdminRoleId, @IsActive, @IsDeleted);
Insert into tblRolePermissionDetail (ModuleId,ModuleScreenId,FieldId,PermissionId,RoleId,IsActive,IsDeleted) values(@Module_ReportId, @Screen_MonthlySalesReportId, NULL, @AllPermission, @AdminRoleId, @IsActive, @IsDeleted);
Insert into tblRolePermissionDetail (ModuleId,ModuleScreenId,FieldId,PermissionId,RoleId,IsActive,IsDeleted) values(@Module_ReportId, @Screen_MonthlyCustomerSummaryReportId, NULL, @AllPermission, @AdminRoleId, @IsActive, @IsDeleted);
Insert into tblRolePermissionDetail (ModuleId,ModuleScreenId,FieldId,PermissionId,RoleId,IsActive,IsDeleted) values(@Module_ReportId, @Screen_MonthlyMoneyOwedReportId, NULL, @AllPermission, @AdminRoleId, @IsActive, @IsDeleted);
Insert into tblRolePermissionDetail (ModuleId,ModuleScreenId,FieldId,PermissionId,RoleId,IsActive,IsDeleted) values(@Module_ReportId, @Screen_MonthlyCustomerDetailReportId, NULL, @AllPermission, @AdminRoleId, @IsActive, @IsDeleted);
Insert into tblRolePermissionDetail (ModuleId,ModuleScreenId,FieldId,PermissionId,RoleId,IsActive,IsDeleted) values(@Module_ReportId, @Screen_HourlyWashreportId, NULL, @AllPermission, @AdminRoleId, @IsActive, @IsDeleted);
Insert into tblRolePermissionDetail (ModuleId,ModuleScreenId,FieldId,PermissionId,RoleId,IsActive,IsDeleted) values(@Module_ReportId, @Screen_DailySalesreportId, NULL, @AllPermission, @AdminRoleId, @IsActive, @IsDeleted);
Insert into tblRolePermissionDetail (ModuleId,ModuleScreenId,FieldId,PermissionId,RoleId,IsActive,IsDeleted) values(@Module_AdminId, @Screen_GiftCardsId, NULL, @AllPermission, @AdminRoleId, @IsActive, @IsDeleted);
Insert into tblRolePermissionDetail (ModuleId,ModuleScreenId,FieldId,PermissionId,RoleId,IsActive,IsDeleted) values(@Module_AdminId, @Screen_SchedulesId, NULL, @AllPermission, @AdminRoleId, @IsActive, @IsDeleted);
Insert into tblRolePermissionDetail (ModuleId,ModuleScreenId,FieldId,PermissionId,RoleId,IsActive,IsDeleted) values(@Module_AdminId, @Screen_VehiclesId, NULL, @AllPermission, @AdminRoleId, @IsActive, @IsDeleted);
Insert into tblRolePermissionDetail (ModuleId,ModuleScreenId,FieldId,PermissionId,RoleId,IsActive,IsDeleted) values(@Module_AdminId, @Screen_ClientsId, NULL, @AllPermission, @AdminRoleId, @IsActive, @IsDeleted);
Insert into tblRolePermissionDetail (ModuleId,ModuleScreenId,FieldId,PermissionId,RoleId,IsActive,IsDeleted) values(@Module_AdminId, @Screen_EmployeesId, NULL, @AllPermission, @AdminRoleId, @IsActive, @IsDeleted);
Insert into tblRolePermissionDetail (ModuleId,ModuleScreenId,FieldId,PermissionId,RoleId,IsActive,IsDeleted) values(@Module_AdminId, @Screen_TimeClockMaintenanceId, NULL, @AllPermission, @AdminRoleId, @IsActive, @IsDeleted);
Insert into tblRolePermissionDetail (ModuleId,ModuleScreenId,FieldId,PermissionId,RoleId,IsActive,IsDeleted) values(@Module_AdminId, @Screen_CloseOutRegisterId, NULL, @AllPermission, @AdminRoleId, @IsActive, @IsDeleted);
Insert into tblRolePermissionDetail (ModuleId,ModuleScreenId,FieldId,PermissionId,RoleId,IsActive,IsDeleted) values(@Module_AdminId, @Screen_CashRegisterSetupId, NULL, @AllPermission, @AdminRoleId, @IsActive, @IsDeleted);
Insert into tblRolePermissionDetail (ModuleId,ModuleScreenId,FieldId,PermissionId,RoleId,IsActive,IsDeleted) values(@Module_AdminId, @Screen_ProductSetupId, NULL, @AllPermission, @AdminRoleId, @IsActive, @IsDeleted);
Insert into tblRolePermissionDetail (ModuleId,ModuleScreenId,FieldId,PermissionId,RoleId,IsActive,IsDeleted) values(@Module_AdminId, @Screen_ServiceSetupId, NULL, @AllPermission, @AdminRoleId, @IsActive, @IsDeleted);
Insert into tblRolePermissionDetail (ModuleId,ModuleScreenId,FieldId,PermissionId,RoleId,IsActive,IsDeleted) values(@Module_AdminId, @Screen_SystemSetupId, NULL, @AllPermission, @AdminRoleId, @IsActive, @IsDeleted);
Insert into tblRolePermissionDetail (ModuleId,ModuleScreenId,FieldId,PermissionId,RoleId,IsActive,IsDeleted) values(@Module_AdminId, @Screen_ChecklistSetupId, NULL, @AllPermission, @AdminRoleId, @IsActive, @IsDeleted);
Insert into tblRolePermissionDetail (ModuleId,ModuleScreenId,FieldId,PermissionId,RoleId,IsActive,IsDeleted) values(@Module_AdminId, @Screen_EmployeeHandbookId, NULL, @AllPermission, @AdminRoleId, @IsActive, @IsDeleted);
Insert into tblRolePermissionDetail (ModuleId,ModuleScreenId,FieldId,PermissionId,RoleId,IsActive,IsDeleted) values(@Module_AdminId, @Screen_BonusSetupId, NULL, @AllPermission, @AdminRoleId, @IsActive, @IsDeleted);
Insert into tblRolePermissionDetail (ModuleId,ModuleScreenId,FieldId,PermissionId,RoleId,IsActive,IsDeleted) values(@Module_WhiteLabellingId, @Screen_WhiteLabellingId, NULL, @AllPermission, @AdminRoleId, @IsActive, @IsDeleted);
Insert into tblRolePermissionDetail (ModuleId,ModuleScreenId,FieldId,PermissionId,RoleId,IsActive,IsDeleted) values(@Module_MessengerId, @Screen_MessengerId, NULL, @AllPermission, @AdminRoleId, @IsActive, @IsDeleted);
Insert into tblRolePermissionDetail (ModuleId,ModuleScreenId,FieldId,PermissionId,RoleId,IsActive,IsDeleted) values(@Module_PayRollId, @Screen_PayRollListId, NULL, @AllPermission, @AdminRoleId, @IsActive, @IsDeleted);
Insert into tblRolePermissionDetail (ModuleId,ModuleScreenId,FieldId,PermissionId,RoleId,IsActive,IsDeleted) values(@Module_CheckoutId, @Screen_CheckoutListId, NULL, @AllPermission, @AdminRoleId, @IsActive, @IsDeleted);
Insert into tblRolePermissionDetail (ModuleId,ModuleScreenId,FieldId,PermissionId,RoleId,IsActive,IsDeleted) values(@Module_CustomerHistoryId, @Screen_CustomerHistoryId, NULL, @AllPermission, @AdminRoleId, @IsActive, @IsDeleted);

--MANAGER
Insert into tblRolePermissionDetail (ModuleId,ModuleScreenId,FieldId,PermissionId,RoleId,IsActive,IsDeleted) values(@Module_DashboardId, @Screen_DashboardPageId, NULL, @AllPermission, @ManagerRoleId, @IsActive, @IsDeleted);
Insert into tblRolePermissionDetail (ModuleId,ModuleScreenId,FieldId,PermissionId,RoleId,IsActive,IsDeleted) values(@Module_WashesId, @Screen_WashesListPageId, NULL, @AllPermission, @ManagerRoleId, @IsActive, @IsDeleted);
Insert into tblRolePermissionDetail (ModuleId,ModuleScreenId,FieldId,PermissionId,RoleId,IsActive,IsDeleted) values(@Module_WashesId, @Screen_AddWashPageId, NULL, @AllPermission, @ManagerRoleId, @IsActive, @IsDeleted);
Insert into tblRolePermissionDetail (ModuleId,ModuleScreenId,FieldId,PermissionId,RoleId,IsActive,IsDeleted) values(@Module_DetailId, @Screen_DetailListPageId, NULL, @AllPermission, @ManagerRoleId, @IsActive, @IsDeleted);
Insert into tblRolePermissionDetail (ModuleId,ModuleScreenId,FieldId,PermissionId,RoleId,IsActive,IsDeleted) values(@Module_DetailId, @Screen_AddDetailPageId, NULL, @AllPermission, @ManagerRoleId, @IsActive, @IsDeleted);
Insert into tblRolePermissionDetail (ModuleId,ModuleScreenId,FieldId,PermissionId,RoleId,IsActive,IsDeleted) values(@Module_SalesId, @Screen_SalesId, NULL, @AllPermission, @ManagerRoleId, @IsActive, @IsDeleted);
Insert into tblRolePermissionDetail (ModuleId,ModuleScreenId,FieldId,PermissionId,RoleId,IsActive,IsDeleted) values(@Module_ReportId, @Screen_EODReportId, NULL, @AllPermission, @ManagerRoleId, @IsActive, @IsDeleted);
Insert into tblRolePermissionDetail (ModuleId,ModuleScreenId,FieldId,PermissionId,RoleId,IsActive,IsDeleted) values(@Module_ReportId, @Screen_DailyStatusScreenId, NULL, @AllPermission, @ManagerRoleId, @IsActive, @IsDeleted);
Insert into tblRolePermissionDetail (ModuleId,ModuleScreenId,FieldId,PermissionId,RoleId,IsActive,IsDeleted) values(@Module_ReportId, @Screen_DailyTipreportId, NULL, @AllPermission, @ManagerRoleId, @IsActive, @IsDeleted);
Insert into tblRolePermissionDetail (ModuleId,ModuleScreenId,FieldId,PermissionId,RoleId,IsActive,IsDeleted) values(@Module_ReportId, @Screen_MonthlyTipreportId, NULL, @AllPermission, @ManagerRoleId, @IsActive, @IsDeleted);
Insert into tblRolePermissionDetail (ModuleId,ModuleScreenId,FieldId,PermissionId,RoleId,IsActive,IsDeleted) values(@Module_ReportId, @Screen_MonthlySalesReportId, NULL, @AllPermission, @ManagerRoleId, @IsActive, @IsDeleted);
Insert into tblRolePermissionDetail (ModuleId,ModuleScreenId,FieldId,PermissionId,RoleId,IsActive,IsDeleted) values(@Module_ReportId, @Screen_MonthlyCustomerSummaryReportId, NULL, @AllPermission, @ManagerRoleId, @IsActive, @IsDeleted);
Insert into tblRolePermissionDetail (ModuleId,ModuleScreenId,FieldId,PermissionId,RoleId,IsActive,IsDeleted) values(@Module_ReportId, @Screen_MonthlyMoneyOwedReportId, NULL, @AllPermission, @ManagerRoleId, @IsActive, @IsDeleted);
Insert into tblRolePermissionDetail (ModuleId,ModuleScreenId,FieldId,PermissionId,RoleId,IsActive,IsDeleted) values(@Module_ReportId, @Screen_MonthlyCustomerDetailReportId, NULL, @AllPermission, @ManagerRoleId, @IsActive, @IsDeleted);
Insert into tblRolePermissionDetail (ModuleId,ModuleScreenId,FieldId,PermissionId,RoleId,IsActive,IsDeleted) values(@Module_ReportId, @Screen_HourlyWashreportId, NULL, @AllPermission, @ManagerRoleId, @IsActive, @IsDeleted);
Insert into tblRolePermissionDetail (ModuleId,ModuleScreenId,FieldId,PermissionId,RoleId,IsActive,IsDeleted) values(@Module_ReportId, @Screen_DailySalesreportId, NULL, @AllPermission, @ManagerRoleId, @IsActive, @IsDeleted);
Insert into tblRolePermissionDetail (ModuleId,ModuleScreenId,FieldId,PermissionId,RoleId,IsActive,IsDeleted) values(@Module_WhiteLabellingId, @Screen_WhiteLabellingId, NULL, @AllPermission, @ManagerRoleId, @IsActive, @IsDeleted);
Insert into tblRolePermissionDetail (ModuleId,ModuleScreenId,FieldId,PermissionId,RoleId,IsActive,IsDeleted) values(@Module_MessengerId, @Screen_MessengerId, NULL, @AllPermission, @ManagerRoleId, @IsActive, @IsDeleted);
Insert into tblRolePermissionDetail (ModuleId,ModuleScreenId,FieldId,PermissionId,RoleId,IsActive,IsDeleted) values(@Module_CheckoutId, @Screen_CheckoutListId, NULL, @AllPermission, @ManagerRoleId, @IsActive, @IsDeleted);

--Cashier
Insert into tblRolePermissionDetail (ModuleId,ModuleScreenId,FieldId,PermissionId,RoleId,IsActive,IsDeleted) values(@Module_DashboardId, @Screen_DashboardPageId, NULL, @AllPermission, @CashierRoleId, @IsActive, @IsDeleted);
Insert into tblRolePermissionDetail (ModuleId,ModuleScreenId,FieldId,PermissionId,RoleId,IsActive,IsDeleted) values(@Module_WashesId, @Screen_WashesListPageId, NULL, @AllPermission, @CashierRoleId, @IsActive, @IsDeleted);
Insert into tblRolePermissionDetail (ModuleId,ModuleScreenId,FieldId,PermissionId,RoleId,IsActive,IsDeleted) values(@Module_WashesId, @Screen_AddWashPageId, NULL, @AllPermission, @CashierRoleId, @IsActive, @IsDeleted);
Insert into tblRolePermissionDetail (ModuleId,ModuleScreenId,FieldId,PermissionId,RoleId,IsActive,IsDeleted) values(@Module_DetailId, @Screen_DetailListPageId, NULL, @AllPermission, @CashierRoleId, @IsActive, @IsDeleted);
Insert into tblRolePermissionDetail (ModuleId,ModuleScreenId,FieldId,PermissionId,RoleId,IsActive,IsDeleted) values(@Module_DetailId, @Screen_AddDetailPageId, NULL, @AllPermission, @CashierRoleId, @IsActive, @IsDeleted);
Insert into tblRolePermissionDetail (ModuleId,ModuleScreenId,FieldId,PermissionId,RoleId,IsActive,IsDeleted) values(@Module_SalesId, @Screen_SalesId, NULL, @AllPermission, @CashierRoleId, @IsActive, @IsDeleted);
Insert into tblRolePermissionDetail (ModuleId,ModuleScreenId,FieldId,PermissionId,RoleId,IsActive,IsDeleted) values(@Module_ReportId, @Screen_EODReportId, NULL, @AllPermission, @CashierRoleId, @IsActive, @IsDeleted);
Insert into tblRolePermissionDetail (ModuleId,ModuleScreenId,FieldId,PermissionId,RoleId,IsActive,IsDeleted) values(@Module_ReportId, @Screen_DailyStatusScreenId, NULL, @AllPermission, @CashierRoleId, @IsActive, @IsDeleted);
Insert into tblRolePermissionDetail (ModuleId,ModuleScreenId,FieldId,PermissionId,RoleId,IsActive,IsDeleted) values(@Module_ReportId, @Screen_DailyTipreportId, NULL, @AllPermission, @CashierRoleId, @IsActive, @IsDeleted);
Insert into tblRolePermissionDetail (ModuleId,ModuleScreenId,FieldId,PermissionId,RoleId,IsActive,IsDeleted) values(@Module_ReportId, @Screen_MonthlyTipreportId, NULL, @AllPermission, @CashierRoleId, @IsActive, @IsDeleted);
Insert into tblRolePermissionDetail (ModuleId,ModuleScreenId,FieldId,PermissionId,RoleId,IsActive,IsDeleted) values(@Module_ReportId, @Screen_MonthlySalesReportId, NULL, @AllPermission, @CashierRoleId, @IsActive, @IsDeleted);
Insert into tblRolePermissionDetail (ModuleId,ModuleScreenId,FieldId,PermissionId,RoleId,IsActive,IsDeleted) values(@Module_ReportId, @Screen_MonthlyCustomerSummaryReportId, NULL, @AllPermission, @CashierRoleId, @IsActive, @IsDeleted);
Insert into tblRolePermissionDetail (ModuleId,ModuleScreenId,FieldId,PermissionId,RoleId,IsActive,IsDeleted) values(@Module_ReportId, @Screen_MonthlyMoneyOwedReportId, NULL, @AllPermission, @CashierRoleId, @IsActive, @IsDeleted);
Insert into tblRolePermissionDetail (ModuleId,ModuleScreenId,FieldId,PermissionId,RoleId,IsActive,IsDeleted) values(@Module_ReportId, @Screen_MonthlyCustomerDetailReportId, NULL, @AllPermission, @CashierRoleId, @IsActive, @IsDeleted);
Insert into tblRolePermissionDetail (ModuleId,ModuleScreenId,FieldId,PermissionId,RoleId,IsActive,IsDeleted) values(@Module_ReportId, @Screen_HourlyWashreportId, NULL, @AllPermission, @CashierRoleId, @IsActive, @IsDeleted);
Insert into tblRolePermissionDetail (ModuleId,ModuleScreenId,FieldId,PermissionId,RoleId,IsActive,IsDeleted) values(@Module_ReportId, @Screen_DailySalesreportId, NULL, @AllPermission, @CashierRoleId, @IsActive, @IsDeleted);
Insert into tblRolePermissionDetail (ModuleId,ModuleScreenId,FieldId,PermissionId,RoleId,IsActive,IsDeleted) values(@Module_WhiteLabellingId, @Screen_WhiteLabellingId, NULL, @AllPermission, @CashierRoleId, @IsActive, @IsDeleted);
Insert into tblRolePermissionDetail (ModuleId,ModuleScreenId,FieldId,PermissionId,RoleId,IsActive,IsDeleted) values(@Module_MessengerId, @Screen_MessengerId, NULL, @AllPermission, @CashierRoleId, @IsActive, @IsDeleted);
Insert into tblRolePermissionDetail (ModuleId,ModuleScreenId,FieldId,PermissionId,RoleId,IsActive,IsDeleted) values(@Module_CheckoutId, @Screen_CheckoutListId, NULL, @AllPermission, @CashierRoleId, @IsActive, @IsDeleted);

--Washer
Insert into tblRolePermissionDetail (ModuleId,ModuleScreenId,FieldId,PermissionId,RoleId,IsActive,IsDeleted) values(@Module_WashesId, @Screen_WashesListPageId, NULL, @AllPermission, @WasherRoleId, @IsActive, @IsDeleted);
Insert into tblRolePermissionDetail (ModuleId,ModuleScreenId,FieldId,PermissionId,RoleId,IsActive,IsDeleted) values(@Module_WashesId, @Screen_AddWashPageId, NULL, @AllPermission, @WasherRoleId, @IsActive, @IsDeleted);

--Detailer
Insert into tblRolePermissionDetail (ModuleId,ModuleScreenId,FieldId,PermissionId,RoleId,IsActive,IsDeleted) values(@Module_DetailId, @Screen_DetailListPageId, NULL, @AllPermission, @DetailerRoleId, @IsActive, @IsDeleted);
Insert into tblRolePermissionDetail (ModuleId,ModuleScreenId,FieldId,PermissionId,RoleId,IsActive,IsDeleted) values(@Module_DetailId, @Screen_AddDetailPageId, NULL, @AllPermission, @DetailerRoleId, @IsActive, @IsDeleted);

--Customer
Insert into tblRolePermissionDetail (ModuleId,ModuleScreenId,FieldId,PermissionId,RoleId,IsActive,IsDeleted) values(@Module_CustomerId, @Screen_CustomerDashboardId, NULL, @AllPermission, @ClientRoleId, @IsActive, @IsDeleted);
