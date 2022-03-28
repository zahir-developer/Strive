
CREATE PROCEDURE [StriveCarSalon].[uspGetUserByAuthId_New] 
(@AuthId int)
as
begin


-- =============================================
-- Author:		Naveen
-- Sample: [StriveCarSalon].[uspGetUserByAuthId] 5
-- =============================================
-- 2021-07-02, Zahir, Query Optimization 

SET NOCOUNT ON;

DROP TABLE IF EXISTS #EmployeeDetail

SELECT
Emp.EmployeeId,
EmpDet.EmployeeCode,
Emp.FirstName,
Emp.LastName,
EmpDet.AuthId into #EmployeeDetail
FROM
tblEmployee (NOLOCK) Emp 
INNER JOIN tblEmployeeDetail (NOLOCK) EmpDet ON Emp.EmployeeId = EmpDet.EmployeeId
WHERE
EmpDet.AuthId = @AuthId AND Emp.IsDeleted=0

Select * from #EmployeeDetail

DROP TABLE IF EXISTS #EmployeeRole

SELECT EmpRo.EmployeeId,EmpRo.RoleId, rm.RoleName AS RoleName into #EmployeeRole
FROM #EmployeeDetail ed
INNER JOIN tblEmployeeRole (NOLOCK) EmpRo on ed.EmployeeId = EmpRo.EmployeeId
INNER JOIN tblRoleMaster (NOLOCK) rm on EmpRo.RoleId=rm.RoleMasterId
WHERE EmpRo.IsDeleted=0

Select * from #EmployeeRole


DROP TABLE IF EXISTS #EmployeeLocation

SELECT DISTINCT EmpLo.EmployeeId, EmpLo.LocationId, Lo.LocationName, la.City,tblc.valuedesc AS CityName into #EmployeeLocation
FROM #EmployeeDetail ed
JOIN tblEmployeeLocation (NOLOCK) EmpLo on ed.EmployeeId = EmpLo.EmployeeId
INNER JOIN tblLocation (NOLOCK) Lo ON EmpLo.LocationId=Lo.LocationId
INNER JOIN tbllocationaddress (NOLOCK) la on Lo.LocationId = la.LocationId
LEFT JOIN [GetTable]('City') tblc ON (la.City = tblc.valueid)
WHERE EmpLo.IsDeleted=0

Select * from #EmployeeLocation

SELECT EmpLoDr.drawerid, EmpLoDr.DrawerName, EmpLo.LocationId
FROM #EmployeeDetail ed
INNER JOIN #EmployeeLocation (NOLOCK) EmpLo ON EmpLo.EmployeeId = ed.EmployeeId
INNER JOIN tblDrawer EmpLoDr (NOLOCK) on EmpLo.LocationId = EmpLoDr.LocationId

WHERE EmpLoDr.IsActive=1 AND EmpLoDr.IsDeleted=0

DECLARE @EmployeeID INT;
SELECT @EmployeeID = EmployeeId FROM tblEmployeeDetail WHERE AuthId=@AuthId

SELECT 
emp.EmployeeId, 
emp.FirstName,
emp.LastName,
rolper.RoleId,
empRol.RoleName,
module.ModuleName,
modscrn.ViewName
FROM #EmployeeDetail emp 
INNER join #EmployeeRole (NOLOCK) empRol on empRol.EmployeeId=emp.EmployeeId
INNER join tblRolePermissionDetail (NOLOCK) rolper on rolper.RoleId=emprol.RoleId
INNER join tblModule (NOLOCK) module on rolper.ModuleId=module.ModuleId
INNER join tblModuleScreen (NOLOCK) modscrn on rolper.ModuleScreenId=modscrn.ModuleScreenId
--AND rolper.IsActive =1
AND module.IsActive =1
AND modscrn.IsActive =1
AND ISNULL(rolper.IsDeleted,0)=0
AND ISNULL(modscrn.IsDeleted,0)=0 
AND ISNULL(module.IsDeleted,0)=0

end