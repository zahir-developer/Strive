-- =============================================
-- Author:		Zahir / Shalini
-- Create date: 07-Dec-2020
-- Description:	Role Permission access scripts
-- =============================================
--GREETER
DECLARE @AllPermission INT = (SELECT TOP 1 PermissionId FROM tblPermission WHERE PermissionName = 'All')
DECLARE @AdminRoleId INT = (SELECT TOP 1 RoleMasterId FROM tblRoleMaster WHERE RoleName = 'Admin')
DECLARE @GreeterRoleId INT = (SELECT TOP 1 RoleMasterId FROM tblRoleMaster WHERE RoleName = 'Greet Bay')
DECLARE @ManagerRoleId INT = (SELECT TOP 1 RoleMasterId FROM tblRoleMaster WHERE RoleName = 'Manager')
DECLARE @CashierRoleId INT = (SELECT TOP 1 RoleMasterId FROM tblRoleMaster WHERE RoleName = 'Cashier')
DECLARE @WasherRoleId INT = (SELECT TOP 1 RoleMasterId FROM tblRoleMaster WHERE RoleName = 'Wash')
DECLARE @DetailerRoleId INT = (SELECT TOP 1 RoleMasterId FROM tblRoleMaster WHERE RoleName = 'Detailer')

DECLARE @IsActive INT = 1;
DECLARE @IsDeleted INT = 0;
--modules
DECLARE @Module_ScheduleId INT = (SELECT TOP 1 ModuleId FROM tblModule WHERE ModuleName = 'Schedule')
DECLARE @Module_MessengerId INT = (SELECT TOP 1 ModuleId FROM tblModule WHERE ModuleName = 'Messenger')
DECLARE @Module_ProfileId INT = (SELECT TOP 1 ModuleId FROM tblModule WHERE ModuleName = 'Profile')
DECLARE @Module_TicketsId INT = (SELECT TOP 1 ModuleId FROM tblModule WHERE ModuleName = 'Tickets')
DECLARE @Module_CheckOutId INT = (SELECT TOP 1 ModuleId FROM tblModule WHERE ModuleName = 'Checkout')
DECLARE @Module_CustomerHistoryId INT = (SELECT TOP 1 ModuleId FROM tblModule WHERE ModuleName = 'CustomerHistory')
--sceeen

DECLARE @Screen_MessengerId INT = (SELECT TOP 1 ModuleScreenId FROM tblModuleScreen WHERE ViewName = 'Messenger')
DECLARE @Screen_CheckoutListId INT = (SELECT TOP 1 ModuleScreenId FROM tblModuleScreen WHERE ViewName = 'CheckoutList')
DECLARE @Screen_MyProfileId INT = (SELECT TOP 1 ModuleScreenId FROM tblModuleScreen WHERE ViewName = 'MyProfile')
DECLARE @Screen_MyTicketsId INT = (SELECT TOP 1 ModuleScreenId FROM tblModuleScreen WHERE ViewName = 'MyTickets')
DECLARE @Screen_MyScheduleId INT = (SELECT TOP 1 ModuleScreenId FROM tblModuleScreen WHERE ViewName = 'MySchedule')

DECLARE @Screen_CustomerHistoryId INT = (SELECT TOP 1 ModuleScreenId FROM tblModuleScreen WHERE ViewName = 'CustomerHistory')
Insert into tblRolePermission (ModuleId,ModuleScreenId,FieldId,PermissionId,RoleId,IsActive,IsDeleted) values(@Module_ScheduleId, @Screen_MyScheduleId, NULL, @AllPermission, @GreeterRoleId, @IsActive, @IsDeleted);
Insert into tblRolePermission (ModuleId,ModuleScreenId,FieldId,PermissionId,RoleId,IsActive,IsDeleted) values(@Module_ProfileId, @Screen_MyProfileId, NULL, @AllPermission, @GreeterRoleId, @IsActive, @IsDeleted);
Insert into tblRolePermission (ModuleId,ModuleScreenId,FieldId,PermissionId,RoleId,IsActive,IsDeleted) values(@Module_MessengerId, @Screen_MessengerId, NULL, @AllPermission, @GreeterRoleId, @IsActive, @IsDeleted);
Insert into tblRolePermission (ModuleId,ModuleScreenId,FieldId,PermissionId,RoleId,IsActive,IsDeleted) values(@Module_CheckOutId, @Screen_CheckoutListId, NULL, @AllPermission, @GreeterRoleId, @IsActive, @IsDeleted);
Insert into tblRolePermission (ModuleId,ModuleScreenId,FieldId,PermissionId,RoleId,IsActive,IsDeleted) values(@Module_ScheduleId, @Screen_MyScheduleId, NULL, @AllPermission, @AdminRoleId, @IsActive, @IsDeleted);
Insert into tblRolePermission (ModuleId,ModuleScreenId,FieldId,PermissionId,RoleId,IsActive,IsDeleted) values(@Module_ProfileId, @Screen_MyProfileId, NULL, @AllPermission, @AdminRoleId, @IsActive, @IsDeleted);
Insert into tblRolePermission (ModuleId,ModuleScreenId,FieldId,PermissionId,RoleId,IsActive,IsDeleted) values(@Module_TicketsId, @Screen_MyTicketsId, NULL, @AllPermission, @AdminRoleId, @IsActive, @IsDeleted);
Insert into tblRolePermission (ModuleId,ModuleScreenId,FieldId,PermissionId,RoleId,IsActive,IsDeleted) values(@Module_ScheduleId, @Screen_MyScheduleId, NULL, @AllPermission, @ManagerRoleId, @IsActive, @IsDeleted);
Insert into tblRolePermission (ModuleId,ModuleScreenId,FieldId,PermissionId,RoleId,IsActive,IsDeleted) values(@Module_ProfileId, @Screen_MyProfileId, NULL, @AllPermission, @ManagerRoleId, @IsActive, @IsDeleted);
Insert into tblRolePermission (ModuleId,ModuleScreenId,FieldId,PermissionId,RoleId,IsActive,IsDeleted) values(@Module_ScheduleId, @Screen_MyScheduleId, NULL, @AllPermission, @CashierRoleId, @IsActive, @IsDeleted);
Insert into tblRolePermission (ModuleId,ModuleScreenId,FieldId,PermissionId,RoleId,IsActive,IsDeleted) values(@Module_ProfileId, @Screen_MyProfileId, NULL, @AllPermission, @CashierRoleId, @IsActive, @IsDeleted);
Insert into tblRolePermission (ModuleId,ModuleScreenId,FieldId,PermissionId,RoleId,IsActive,IsDeleted) values(@Module_ScheduleId, @Screen_MyScheduleId, NULL, @AllPermission, @WasherRoleId, @IsActive, @IsDeleted);
Insert into tblRolePermission (ModuleId,ModuleScreenId,FieldId,PermissionId,RoleId,IsActive,IsDeleted) values(@Module_ProfileId, @Screen_MyProfileId, NULL, @AllPermission, @WasherRoleId, @IsActive, @IsDeleted);
Insert into tblRolePermission (ModuleId,ModuleScreenId,FieldId,PermissionId,RoleId,IsActive,IsDeleted) values(@Module_TicketsId, @Screen_MyTicketsId, NULL, @AllPermission, @WasherRoleId, @IsActive, @IsDeleted);
Insert into tblRolePermission (ModuleId,ModuleScreenId,FieldId,PermissionId,RoleId,IsActive,IsDeleted) values(@Module_MessengerId, @Screen_MessengerId, NULL, @AllPermission, @WasherRoleId, @IsActive, @IsDeleted);
Insert into tblRolePermission (ModuleId,ModuleScreenId,FieldId,PermissionId,RoleId,IsActive,IsDeleted) values(@Module_ScheduleId, @Screen_MyScheduleId, NULL, @AllPermission, @DetailerRoleId, @IsActive, @IsDeleted);
Insert into tblRolePermission (ModuleId,ModuleScreenId,FieldId,PermissionId,RoleId,IsActive,IsDeleted) values(@Module_ProfileId, @Screen_MyProfileId, NULL, @AllPermission, @DetailerRoleId, @IsActive, @IsDeleted);
Insert into tblRolePermission (ModuleId,ModuleScreenId,FieldId,PermissionId,RoleId,IsActive,IsDeleted) values(@Module_MessengerId, @Screen_MessengerId, NULL, @AllPermission, @DetailerRoleId, @IsActive, @IsDeleted);
Insert into tblRolePermission (ModuleId,ModuleScreenId,FieldId,PermissionId,RoleId,IsActive,IsDeleted) values(@Module_TicketsId, @Screen_MyTicketsId, NULL, @AllPermission, @DetailerRoleId, @IsActive, @IsDeleted);


