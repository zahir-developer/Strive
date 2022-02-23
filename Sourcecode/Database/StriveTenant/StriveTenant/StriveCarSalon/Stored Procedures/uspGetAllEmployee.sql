
CREATE PROCEDURE  [StriveCarSalon].[uspGetAllEmployee] 
(@LocationId int)
AS
BEGIN


IF @LocationId = 0 
BEGIN
Select distinct
emp.EmployeeId,
emp.firstname,
emp.LastName
from tblEmployee emp
where isnull(emp.IsActive,1)=1
AND emp.IsDeleted = 0 order by emp.FirstName
END

IF @LocationId > 0 
BEGIN
Select distinct
emp.EmployeeId,
emp.firstname,
emp.LastName
from tblEmployee emp
inner join tblEmployeeLocation empl on emp.EmployeeId= empl.EmployeeId
where isnull(emp.IsActive,1)=1 and empl.LocationId = @LocationId
AND emp.IsDeleted = 0 order by emp.FirstName
END

END