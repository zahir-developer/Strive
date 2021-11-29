-- =============================================
-- Author:		Zahir / Shalini
-- Create date: 07-Dec-2020
-- Description:	Role Permission access scripts
-- =============================================
-----MANAGERS
DECLARE @AllPermission INT = (SELECT TOP 1 PermissionId FROM tblPermission WHERE PermissionName = 'All')
DECLARE @ManagerRoleId INT = (SELECT TOP 1 RoleMasterId FROM tblRoleMaster WHERE RoleName = 'Manager')

DECLARE @IsDeActive INT = 1;
DECLARE @IsDeleted INT = 0;


--Modules

DECLARE @Module_ReportId INT = (SELECT TOP 1 ModuleId FROM tblModule WHERE ModuleName = 'Report')
--View

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



--field

DECLARE @Field_StoreLocationId INT = (SELECT TOP 1 FieldId FROM tblField WHERE ViewName = 'DailySalesreport')

Insert into tblRolePermission (ModuleId,ModuleScreenId,FieldId,PermissionId,RoleId,IsActive,IsDeleted) values(@Module_ReportId, @Screen_EODReportId, @Field_StoreLocationId, @AllPermission, @ManagerRoleId, @IsDeActive, @IsDeleted);
Insert into tblRolePermission (ModuleId,ModuleScreenId,FieldId,PermissionId,RoleId,IsActive,IsDeleted) values(@Module_ReportId, @Screen_DailyStatusScreenId, @Field_StoreLocationId, @AllPermission, @ManagerRoleId, @IsDeActive, @IsDeleted);
Insert into tblRolePermission (ModuleId,ModuleScreenId,FieldId,PermissionId,RoleId,IsActive,IsDeleted) values(@Module_ReportId, @Screen_DailyTipreportId, @Field_StoreLocationId, @AllPermission, @ManagerRoleId, @IsDeActive, @IsDeleted);
Insert into tblRolePermission (ModuleId,ModuleScreenId,FieldId,PermissionId,RoleId,IsActive,IsDeleted) values(@Module_ReportId, @Screen_MonthlyTipreportId, @Field_StoreLocationId, @AllPermission, @ManagerRoleId, @IsDeActive, @IsDeleted);
Insert into tblRolePermission (ModuleId,ModuleScreenId,FieldId,PermissionId,RoleId,IsActive,IsDeleted) values(@Module_ReportId, @Screen_MonthlySalesReportId, @Field_StoreLocationId, @AllPermission, @ManagerRoleId, @IsDeActive, @IsDeleted);
Insert into tblRolePermission (ModuleId,ModuleScreenId,FieldId,PermissionId,RoleId,IsActive,IsDeleted) values(@Module_ReportId, @Screen_MonthlyCustomerSummaryReportId, @Field_StoreLocationId, @AllPermission, @ManagerRoleId, @IsDeActive, @IsDeleted);
Insert into tblRolePermission (ModuleId,ModuleScreenId,FieldId,PermissionId,RoleId,IsActive,IsDeleted) values(@Module_ReportId, @Screen_MonthlyMoneyOwedReportId, @Field_StoreLocationId, @AllPermission, @ManagerRoleId, @IsDeActive, @IsDeleted);
Insert into tblRolePermission (ModuleId,ModuleScreenId,FieldId,PermissionId,RoleId,IsActive,IsDeleted) values(@Module_ReportId, @Screen_MonthlyCustomerDetailReportId, @Field_StoreLocationId, @AllPermission, @ManagerRoleId, @IsDeActive, @IsDeleted);
Insert into tblRolePermission (ModuleId,ModuleScreenId,FieldId,PermissionId,RoleId,IsActive,IsDeleted) values(@Module_ReportId, @Screen_HourlyWashreportId, @Field_StoreLocationId, @AllPermission, @ManagerRoleId, @IsDeActive, @IsDeleted);
Insert into tblRolePermission (ModuleId,ModuleScreenId,FieldId,PermissionId,RoleId,IsActive,IsDeleted) values(@Module_ReportId, @Screen_DailySalesreportId, @Field_StoreLocationId, @AllPermission, @ManagerRoleId, @IsDeActive, @IsDeleted);


DECLARE @Screen_CustomerHistoryId INT = (SELECT TOP 1 ModuleScreenId FROM tblModuleScreen WHERE ViewName = 'CustomerHistory')

DECLARE @AdminRoleId INT = (SELECT TOP 1 RoleMasterId FROM tblRoleMaster WHERE RoleName = 'Admin')

Insert into tblRolePermission (ModuleId,ModuleScreenId,FieldId,PermissionId,RoleId,IsActive,IsDeleted) values(@Module_CustomerHistoryId, @Screen_CustomerHistoryId, NULL, @AllPermission, @AdminRoleId, 1, 0);

