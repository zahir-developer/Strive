CREATE PROC [StriveCarSalon].[uspGetEmployeeList]
AS
BEGIN

SELECT 
emp.EmployeeId, 
empdet.EmployeeCode,
emp.FirstName,
emp.LastName,
stuff((SELECT '/'+ phoneNumber FROM strivecarsalon.tblemployeeaddress 
WHERE EmployeeId = emp.EmployeeId
FOR XML PATH('')),1,1,'') as MobileNo,
(SELECT COUNT(1) FROM StriveCarSalon.tblEmployeeLiability WHERE employeeid=emp.employeeid) as Collisions,
(SELECT COUNT(1) FROM StriveCarSalon.tblEmployeeDocument WHERE employeeid=emp.employeeid) as Documents,
0 as Schedules,
isnull(emp.IsActive,1) as Status
FROM 
StriveCarSalon.tblEmployee emp 
LEFT JOIN strivecarsalon.tblEmployeeLiability empli on emp.EmployeeId = empli.EmployeeId   
LEFT JOIN StriveCarSalon.tblEmployeeDetail empdet on emp.EmployeeId = empdet.EmployeeId
LEFT JOIN StriveCarSalon.tblEmployeeLiability emplic on emp.EmployeeId = emplic.EmployeeId
LEFT JOIN StriveCarSalon.tblEmployeeDocument empdoc on emp.EmployeeId = empdoc.EmployeeId

END