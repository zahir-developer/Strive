CREATE proc [StriveCarSalon].[uspGetEmailId] --[StriveCarSalon].[uspGetEmailId] 

as 
begin 
	Declare @ManagerId int =(select RoleMasterId from tblRoleMaster where RoleName= 'Manager')
	Declare @OperatorId int =(select RoleMasterId from tblRoleMaster where RoleName= 'Operator')
select rm.rolename,ea.email,ea.EmployeeAddressId
from tblRoleMaster rm
inner join tblemployeerole r on rm.RoleMasterId = r.RoleId
inner join tblemployeeaddress ea  on r.EmployeeId=ea.EmployeeId
where (rm.RoleMasterId =@ManagerId or rm.RoleMasterId =@OperatorId) and ea.email IS NOT NULL and ea.email != ' '
end