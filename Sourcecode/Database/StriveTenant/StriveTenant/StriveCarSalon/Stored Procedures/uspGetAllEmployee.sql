
CREATE PROCEDURE  [StriveCarSalon].[uspGetAllEmployee] 
(@LocationId int)
AS
BEGIN

select 
emp.EmployeeId,
emp.firstname,
emp.LastName
from tblEmployee emp
inner join tblEmployeeLocation empl on emp.EmployeeId= empl.EmployeeId
where isnull(emp.IsActive,1)=1 and empl.LocationId = @LocationId

END