--[StriveCarSalon].[uspGetEmailId] 1 
CREATE PROCEDURE [StriveCarSalon].[uspGetEmailId]
 @LocationId varchar(max) =null
as 
begin 
	Declare @ManagerId int =(select RoleMasterId from tblRoleMaster where RoleName= 'Manager')
	Declare @OperatorId int =(select RoleMasterId from tblRoleMaster where RoleName= 'Operator')
select distinct rm.rolename,ea.email,e.FirstName, e.LastName
from tblRoleMaster rm
inner join tblemployeerole r on rm.RoleMasterId = r.RoleId
inner join tblemployeeaddress ea  on r.EmployeeId=ea.EmployeeId
inner join tblEmployeeLocation el  on el.EmployeeId=ea.EmployeeId
inner join tblemployee e on r.EmployeeId=e.EmployeeId

where (rm.RoleMasterId =@ManagerId or rm.RoleMasterId =@OperatorId) and ea.email IS NOT NULL and (ea.email != ' ' and ea.email like '%@%.%') and (el.LocationId in (select Id from split(@LocationId)) or @LocationId Is null)

end