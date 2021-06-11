-- =============================================
-- Author:		Zahir 
-- Create date: 07-Dec-2020
-- Description:	Role Permission access scritp for Customer 
-- =============================================

DECLARE @Module_CustomerId INT = (SELECT TOP 1 ModuleId FROM StriveCarSalon.tblModule WHERE ModuleName = 'Customer')
DECLARE @AllPermission INT = (SELECT TOP 1 PermissionId FROM StriveCarSalon.tblPermission WHERE PermissionName = 'All')
DECLARE @CustomerRoleId INT = (SELECT TOP 1 RoleMasterId FROM StriveCarSalon.tblRoleMaster WHERE RoleName = 'Customer')

--INSERT INTO StriveCarSalon.tblRoleMaster (RoleName,RoleAlias,IsActive,IsDeleted) values('Customer', 'CUS', 1, 0)

DECLARE @ModuleScreenId INT = (SELECT TOP 1 ModuleId FROM StriveCarSalon.tblModuleScreen WHERE ViewName = 'CustomerDashboard')

Select @Module_CustomerId,@CustomerRoleId,@ModuleScreenId  

DECLARE @IsActive INT = 1;
DECLARE @IsDeleted INT = 0;

--Insert into StriveCarSalon.tblRolePermission (ModuleId,ModuleScreenId,FieldId,PermissionId,RoleId,IsActive,IsDeleted) values(@Module_CustomerId, @ModuleScreenId, NULL, @AllPermission, @CustomerRoleId, 1, 0);

--DECLARE @Customer INT = (SELECT TOP 1 ModuleId FROM StriveCarSalon.tblModule WHERE ModuleName = 'Customer')
--insert into StriveCarSalon.tblModuleScreen(ModuleId,ViewName,IsActive,IsDeleted,Description)values(@Customer,'CustomerDashboard',1,0,'Customer Dashboard')
