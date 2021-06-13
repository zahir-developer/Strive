--[StriveCarSalon].[uspGetUserByAuthId]2499
CREATE PROCEDURE [StriveCarSalon].[uspGetUserByAuthId] 
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
Emp.IsActive,
ISNULL(ClientDet.ClientId,0) ClientId,
ISNULL(ClientDet.AuthId,0) ClientAuthId
FROM
tblEmployee Emp
LEFT JOIN
tblEmployeeDetail EmpDet ON Emp.EmployeeId = EmpDet.EmployeeId
LEFT JOIN
tblClient ClientDet ON ClientDet.AuthId = @AuthId
LEFT JOIN 
tblEmployeeAddress EmpAdd on Emp.EmployeeId = EmpAdd.EmployeeId
WHERE
EmpDet.AuthId = @AuthId AND (Emp.IsDeleted=0 OR Emp.IsDeleted IS NULL)


SELECT EmpRo.EmployeeId,EmpRo.RoleId, rm.RoleName AS RoleName
FROM
tblEmployeeRole EmpRo
INNER JOIN tblRoleMaster rm on EmpRo.RoleId=rm.RoleMasterId
INNER JOIN tblEmployeeDetail EmpDet
ON EmpDet.EmployeeId = EmpRo.EmployeeId 
WHERE
EmpDet.AuthId = @AuthId AND EmpRo.IsActive=1 AND EmpRo.IsDeleted=0

SELECT DISTINCT EmpLo.EmployeeId,EmpLo.LocationId, Lo.LocationName, la.City,tblc.valuedesc AS CityName
FROM
tblEmployeeLocation EmpLo
INNER JOIN
tblLocation Lo 
ON EmpLo.LocationId=Lo.LocationId
INNER JOIN tblEmployeeDetail EmpDet ON EmpDet.EmployeeId = EmpLo.EmployeeId 
INNER JOIN tbllocationaddress la on Lo.LocationId =la.LocationId
LEFT JOIN [GetTable]('City') tblc ON (la.City = tblc.valueid)
WHERE
EmpDet.AuthId = @AuthId AND EmpLo.IsActive=1 AND EmpLo.IsDeleted=0

SELECT EmpLoDr.drawerid,EmpLoDr.DrawerName, Lo.LocationId
FROM
tblDrawer EmpLoDr
INNER JOIN
tblLocation Lo 
ON EmpLoDr.LocationId=Lo.LocationId
INNER JOIN 
tblEmployeeLocation EmpLo
ON EmpLo.LocationId = EmpLoDr.LocationId
INNER JOIN
tblEmployeeDetail EmpDet
ON EmpDet.EmployeeId = EmpLo.EmployeeId 
WHERE
EmpDet.AuthId = @AuthId AND EmpLoDr.IsActive=1 AND EmpLoDr.IsDeleted=0



DECLARE @EmployeeID INT;
SELECT @EmployeeID = EmployeeId FROM tblEmployeeDetail WHERE AuthId=@AuthId

SELECT 
emp.EmployeeId, 
emp.FirstName,
emp.LastName,
rolper.RoleId,
rolmas.RoleName,
module.ModuleName,
modscrn.ViewName,
fld.FieldName
FROM tblEmployee emp 
left join tblEmployeeRole emprol on emprol.EmployeeId=emp.EmployeeId
left join tblRoleMaster rolmas on emprol.RoleId=rolmas.RoleMasterId
left join TblRolePermission rolper on rolper.RoleId=emprol.RoleId
left join TblModule module on rolper.ModuleId=module.ModuleId
left join TblModuleScreen modscrn on rolper.ModuleScreenId=modscrn.ModuleScreenId
left join TblField fld  on rolper.FieldId=fld.FieldId AND fld.IsActive =1
WHERE emp.EmployeeId =@Employeeid 
AND  emp.IsActive =1
AND emprol.IsActive=1
AND  ISNULL( rolmas.IsActive,1) =1
AND rolper.IsActive =1
AND module.IsActive =1
AND modscrn.IsActive =1
AND ISNULL(emp.IsDeleted,0)=0  
AND ISNULL(emprol.IsDeleted,0)=0
AND ISNULL(rolmas.IsDeleted,0)=0
AND ISNULL(rolper.IsDeleted,0)=0
AND ISNULL(modscrn.IsDeleted,0)=0 
AND ISNULL(fld.IsDeleted,0)=0
AND ISNULL(module.IsDeleted,0)=0

--select * from StriveCarSalon.tbldrawer
--select * from StriveCarSalon.tblEmployeeLocation

--select 
--Emp.EmployeeId, Emp.FirstName, Emp.LastName,
--EmpDet.EmployeeDetailId as EmployeeDetail_EmployeeDetailId, EmpDet.Employeecode as EmployeeDetail_Employeecode,
--EmpRole.RoleId as EmployeeRole_EmployeeRoleId,EmpRoleName.CodeValue as EmployeeRole_RoleName, EmpRole.IsActive as EmployeeRole_IsActive, EmpRole.IsDefault as EmployeeRole_IsDefault
--from [StriveCarSalon].tblEmployee as Emp
--Left join [StriveCarSalon].tblEmployeeDetail as EmpDet on Emp.EmployeeId = EmpDet.EmployeeId
--Left join [StriveCarSalon].tblEmployeeRole as EmpRole on Emp.EmployeeId=EmpRole.EmployeeId
--Left join [StriveCarSalon].tblCodeValue as EmpRoleName on EmpRole.RoleId = EmpRoleName.id
--where empDet.AuthId=@AuthId
end
