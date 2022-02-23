--[StriveCarSalon].[uspGetUserByAuthId] 5
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
EmpDet.AuthId = @AuthId --AND (Emp.IsDeleted=0 OR Emp.IsDeleted IS NULL)


SELECT EmpRo.EmployeeId,EmpRo.RoleId, rm.RoleName AS RoleName
FROM
tblEmployeeRole EmpRo
INNER JOIN tblRoleMaster rm on EmpRo.RoleId=rm.RoleMasterId
LEFT JOIN tblEmployeeDetail EmpDet
ON EmpDet.EmployeeId = EmpRo.EmployeeId 
WHERE
EmpDet.AuthId = @AuthId AND EmpRo.IsDeleted=0

SELECT DISTINCT EmpLo.EmployeeId,EmpLo.LocationId, Lo.LocationName, la.City,tblc.valuedesc AS CityName
FROM
tblEmployeeLocation EmpLo
INNER JOIN
tblLocation Lo 
ON EmpLo.LocationId=Lo.LocationId AND ISNULL(Lo.IsDeleted,0) = 0
INNER JOIN tblEmployeeDetail EmpDet ON EmpDet.EmployeeId = EmpLo.EmployeeId 
LEFT JOIN tbllocationaddress la on Lo.LocationId = la.LocationId
LEFT JOIN [GetTable]('City') tblc ON (la.City = tblc.valueid)
WHERE
EmpDet.AuthId = @AuthId AND EmpLo.IsDeleted=0 ORDER BY LocationId

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
EmpDet.AuthId = @AuthId --AND EmpLoDr.IsActive=1 AND EmpLoDr.IsDeleted=0



DECLARE @EmployeeID INT;
SELECT @EmployeeID = EmployeeId FROM tblEmployeeDetail WHERE AuthId=@AuthId

SELECT 
emp.EmployeeId, 
emp.FirstName,
emp.LastName,
rolper.RoleId,
rolmas.RoleName,
module.ModuleName,
modscrn.ViewName
FROM tblEmployee emp 
left join tblEmployeeRole emprol on emprol.EmployeeId=emp.EmployeeId
left join tblRoleMaster rolmas on emprol.RoleId=rolmas.RoleMasterId
left join TblRolePermissionDetail rolper on rolper.RoleId=emprol.RoleId
left join TblModule module on rolper.ModuleId=module.ModuleId
left join TblModuleScreen modscrn on rolper.ModuleScreenId=modscrn.ModuleScreenId
WHERE emp.EmployeeId =@Employeeid 
--AND  emp.IsActive = 1
--AND emprol.IsActive=1
AND  ISNULL( rolmas.IsActive,1) =1
--AND rolper.IsActive =1
AND module.IsActive =1
AND modscrn.IsActive =1
--AND ISNULL(emp.IsDeleted,0)=0  
AND ISNULL(emprol.IsDeleted,0)=0
AND ISNULL(rolmas.IsDeleted,0)=0
AND ISNULL(rolper.IsDeleted,0)=0
AND ISNULL(modscrn.IsDeleted,0)=0 
AND ISNULL(module.IsDeleted,0)=0

Select 0

end
