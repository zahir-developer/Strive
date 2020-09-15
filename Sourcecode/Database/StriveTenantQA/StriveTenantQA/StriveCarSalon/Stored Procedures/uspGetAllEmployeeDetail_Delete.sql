



CREATE PROC [StriveCarSalon].[uspGetAllEmployeeDetail_Delete]
(@EmployeeName varchar(50)=null)
AS
BEGIN

SELECT 
emp.EmployeeId, 
empdet.EmployeeDetailId,
empdet.EmployeeCode,
emp.FirstName,
emp.LastName,
stuff((SELECT '/'+ phoneNumber FROM strivecarsalon.tblemployeeaddress 
WHERE EmployeeId = emp.EmployeeId
FOR XML PATH('')),1,1,'') as MobileNo,
--CASE WHEN empli.LiabilityId IS NULL THEN 'false' ELSE 'true' END as Collisions,
--CASE WHEN empdoc.EmployeeDocumentId IS NULL THEN 'false' ELSE 'true' END as Documents,
--CASE WHEN empsch.ScheduleId IS NULL THEN 'false' ELSE 'true' END as Schedules,
(SELECT CASE WHEN COUNT(1) > 0 THEN 'true' ELSE 'false' END FROM StriveCarSalon.tblEmployeeLiability WHERE employeeid=emp.employeeid AND (IsDeleted=0 OR IsDeleted IS NULL)) as Collisions,
(SELECT CASE WHEN COUNT(1) > 0 THEN 'true' ELSE 'false' END  FROM StriveCarSalon.tblEmployeeDocument WHERE employeeid=emp.employeeid AND (IsDeleted=0 OR IsDeleted IS NULL)) as Documents,
(SELECT CASE WHEN COUNT(1) > 0 THEN 'true' ELSE 'false' END  FROM StriveCarSalon.tblSchedule WHERE employeeid=emp.employeeid AND (IsDeleted=0 OR IsDeleted IS NULL)) as Schedules,

isnull(emp.IsActive,1) as Status
FROM 
StriveCarSalon.tblEmployee emp 
LEFT JOIN StriveCarSalon.tblEmployeeDetail empdet on emp.EmployeeId = empdet.EmployeeId WHERE empdet.EmployeeDetailId is NOT NULL
--LEFT JOIN StriveCarSalon.tblEmployeeDocument empdoc on emp. EmployeeId = empdoc.EmployeeId
--LEFT JOIN strivecarsalon.tblEmployeeLiability empli on emp.EmployeeId = empli.EmployeeId   
--LEFT JOIN StriveCarSalon.tblSchedule empsch on empsch.EmployeeId = emp.EmployeeId  
AND (emp.IsDeleted=0 OR emp.IsDeleted IS NULL) 
AND (empdet.IsDeleted=0 OR empdet.IsDeleted IS NULL) 
AND
(@EmployeeName is null or emp.FirstName like '%'+@EmployeeName+'%')
OR
(@EmployeeName is null or emp.LastName like '%'+@EmployeeName+'%')
AND (emp.IsDeleted = 0 OR emp.IsDeleted IS NULL)
ORDER BY 1 DESC
END
