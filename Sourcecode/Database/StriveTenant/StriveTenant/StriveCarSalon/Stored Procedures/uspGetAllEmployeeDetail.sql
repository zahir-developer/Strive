-- =============================================
-- Author:		Naveen
-- Create date: 15-07-2020
-- Description:	Retrieves All Employee details
-- =============================================

---------------------History--------------------
-- =============================================
-- <Modified Date>, <Author> - <Description>
-- 29-07-2020, Zahir - Fixed duplicate issue & Modified logic for getting count of Document/Liability/Schedules


------------------------------------------------
-- =============================================

CREATE PROC [StriveCarSalon].[uspGetAllEmployeeDetail]
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

(SELECT CASE WHEN COUNT(1) > 0 THEN 'true' ELSE 'false' END FROM StriveCarSalon.tblEmployeeLiability WHERE employeeid=emp.employeeid AND (IsDeleted=0 OR IsDeleted IS NULL)) as Collisions,
(SELECT CASE WHEN COUNT(1) > 0 THEN 'true' ELSE 'false' END  FROM StriveCarSalon.tblEmployeeDocument WHERE employeeid=emp.employeeid AND (IsDeleted=0 OR IsDeleted IS NULL)) as Documents,
(SELECT CASE WHEN COUNT(1) > 0 THEN 'true' ELSE 'false' END  FROM StriveCarSalon.tblSchedule WHERE employeeid=emp.employeeid AND (IsDeleted=0 OR IsDeleted IS NULL)) as Schedules,

isnull(emp.IsActive,1) as Status
FROM 
StriveCarSalon.tblEmployee emp 
LEFT JOIN StriveCarSalon.tblEmployeeDetail empdet on emp.EmployeeId = empdet.EmployeeId WHERE empdet.EmployeeDetailId is NOT NULL 
AND (emp.IsDeleted=0 OR emp.IsDeleted IS NULL) 
AND ((emp.FirstName like '%'+@EmployeeName+'%')
OR (emp.LastName like '%'+@EmployeeName+'%')
OR @EmployeeName is null OR @EmployeeName = ' ')
AND (emp.IsDeleted = 0 OR emp.IsDeleted IS NULL)
ORDER BY 1 DESC
END
