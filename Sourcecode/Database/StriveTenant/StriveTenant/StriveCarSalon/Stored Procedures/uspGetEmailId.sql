﻿--[StriveCarSalon].[uspGetEmailId] null
CREATE proc [StriveCarSalon].[uspGetEmailId] 
 @LocationId int =null
as 
begin 
	Declare @ManagerId int =(select RoleMasterId from tblRoleMaster where RoleName= 'Manager')
	Declare @OperatorId int =(select RoleMasterId from tblRoleMaster where RoleName= 'Operator')
select rm.rolename,ea.email,ea.EmployeeAddressId,e.FirstName ,el.locationid
from tblRoleMaster rm
inner join tblemployeerole r on rm.RoleMasterId = r.RoleId
inner join tblemployeeaddress ea  on r.EmployeeId=ea.EmployeeId
inner join tblEmployeeLocation el  on el.EmployeeId=ea.EmployeeId
inner join tblemployee e on r.EmployeeId=e.EmployeeId

where (rm.RoleMasterId =@ManagerId or rm.RoleMasterId =@OperatorId) and ea.email IS NOT NULL and ea.email != ' ' and (el.LocationId=@LocationId or @LocationId Is null)
end