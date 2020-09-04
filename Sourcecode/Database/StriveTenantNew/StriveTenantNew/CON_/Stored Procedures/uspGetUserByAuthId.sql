
CREATE PROCEDURE [CON].[uspGetUserByAuthId]
(@AuthId int)
as
begin
SELECT
Emp.EmployeeId,
EmpDet.EmployeeCode,
Emp.FirstName,
Emp.LastName,
EmpAdd.PhoneNumber,
EmpAdd.Email,
EmpDet.AuthId,
Emp.IsActive
FROM
StriveCarSalon.tblEmployee Emp
LEFT JOIN
StriveCarSalon.tblEmployeeDetail EmpDet ON Emp.EmployeeId = EmpDet.EmployeeId
LEFT JOIN 
StriveCarSalon.tblEmployeeAddress EmpAdd on Emp.EmployeeId = EmpAdd.EmployeeId
WHERE
EmpDet.AuthId = @AuthId AND (Emp.IsDeleted=0 OR Emp.IsDeleted IS NULL)


SELECT EmpRo.EmployeeId,EmpRo.RoleId, Cv.valuedesc AS RoleName
FROM
StriveCarSalon.tblEmployeeRole EmpRo
INNER JOIN
StriveCarSalon.GetTable('EmployeeRole') Cv 
ON EmpRo.RoleId=Cv.valueId
INNER JOIN 
StriveCarSalon.tblEmployeeDetail EmpDet
ON EmpDet.EmployeeId = EmpRo.EmployeeId 
WHERE
EmpDet.AuthId = @AuthId AND EmpRo.IsActive=1 AND EmpRo.IsDeleted=0

SELECT EmpLo.EmployeeId,EmpLo.LocationId, Lo.LocationName
FROM
StriveCarSalon.tblEmployeeLocation EmpLo
INNER JOIN
StriveCarSalon.tblLocation Lo 
ON EmpLo.LocationId=Lo.LocationId
INNER JOIN 
StriveCarSalon.tblEmployeeDetail EmpDet
ON EmpDet.EmployeeId = EmpLo.EmployeeId 
WHERE
EmpDet.AuthId = @AuthId AND EmpLo.IsActive=1 AND EmpLo.IsDeleted=0

SELECT EmpLoDr.drawerid,EmpLoDr.DrawerName, Lo.LocationId
FROM
StriveCarSalon.tblDrawer EmpLoDr
INNER JOIN
StriveCarSalon.tblLocation Lo 
ON EmpLoDr.LocationId=Lo.LocationId
INNER JOIN 
StriveCarSalon.tblEmployeeLocation EmpLo
ON EmpLo.LocationId = EmpLoDr.LocationId
INNER JOIN
StriveCarSalon.tblEmployeeDetail EmpDet
ON EmpDet.EmployeeId = EmpLo.EmployeeId 
WHERE
EmpDet.AuthId = @AuthId AND EmpLoDr.IsActive=1 AND EmpLoDr.IsDeleted=0

--select * from StriveCarSalon.tbldrawer
--select * from StriveCarSalon.tblEmployeeLocation

--select 
--Emp.EmployeeId, Emp.FirstName, Emp.LastName,
--EmpDet.EmployeeDetailId as EmployeeDetail_EmployeeDetailId, EmpDet.Employeecode as EmployeeDetail_Employeecode,
--EmpRole.RoleId as EmployeeRole_EmployeeRoleId,EmpRoleName.CodeValue as EmployeeRole_RoleName, EmpRole.IsActive as EmployeeRole_IsActive, EmpRole.IsDefault as EmployeeRole_IsDefault
--from [CON].tblEmployee as Emp
--Left join [CON].tblEmployeeDetail as EmpDet on Emp.EmployeeId = EmpDet.EmployeeId
--Left join [CON].tblEmployeeRole as EmpRole on Emp.EmployeeId=EmpRole.EmployeeId
--Left join [CON].tblCodeValue as EmpRoleName on EmpRole.RoleId = EmpRoleName.id
--where empDet.AuthId=@AuthId
end