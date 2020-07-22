CREATE proc [StriveCarSalon].[uspGetUserByAuthId]
(@AuthId int)
as
begin
select 
Emp.EmployeeId, Emp.FirstName, Emp.LastName,
EmpDet.EmployeeDetailId as EmployeeDetail_EmployeeDetailId, EmpDet.Employeecode as EmployeeDetail_Employeecode,
EmpRole.RoleId as EmployeeRole_EmployeeRoleId,EmpRoleName.CodeValue as EmployeeRole_RoleName, EmpRole.IsActive as EmployeeRole_IsActive, EmpRole.IsDefault as EmployeeRole_IsDefault
from [StriveCarSalon].tblEmployee as Emp
Left join [StriveCarSalon].tblEmployeeDetail as EmpDet on Emp.EmployeeId = EmpDet.EmployeeId
Left join [StriveCarSalon].tblEmployeeRole as EmpRole on Emp.EmployeeId=EmpRole.EmployeeId
Left join [StriveCarSalon].tblCodeValue as EmpRoleName on EmpRole.RoleId = EmpRoleName.id
where empDet.AuthId=@AuthId
end