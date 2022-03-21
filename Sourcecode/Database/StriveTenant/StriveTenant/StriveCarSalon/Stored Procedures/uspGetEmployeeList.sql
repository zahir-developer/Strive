



CREATE PROCEDURE [StriveCarSalon].[uspGetEmployeeList]
AS
BEGIN

SELECT 
emp.EmployeeId, 
empdet.EmployeeDetailId,
empdet.EmployeeCode,
emp.FirstName,
emp.LastName,
isnull(emp.IsActive,1) as Status
FROM 
StriveCarSalon.tblEmployee emp 
LEFT JOIN StriveCarSalon.tblEmployeeDetail empdet on emp.EmployeeId = empdet.EmployeeId WHERE empdet.EmployeeDetailId is NOT NULL
AND (emp.IsDeleted=0 OR emp.IsDeleted IS NULL) 
AND (empdet.IsDeleted=0 OR empdet.IsDeleted IS NULL) 
AND (emp.IsDeleted = 0 OR emp.IsDeleted IS NULL)
ORDER BY 1 DESC

Select EmployeeRoleId, EmployeeId, RoleId from StriveCarSalon.tblEmployeeRole empr where (empr.IsDeleted = 0 OR empr.IsDeleted IS NULL) AND ISNULL(IsActive,1) = 1  AND empr.IsActive = 1

END
