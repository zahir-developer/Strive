
Create PROC  [StriveCarSalon].[uspGetAllEmployee] 
(@LocationId int)
AS
BEGIN

select 
emp.EmployeeId,
emp.firstname,
emp.LastName
from StriveCarSalon.tblEmployee emp
inner join strivecarsalon.tblEmployeeLocation empl on emp.EmployeeId= empl.EmployeeId
where isnull(emp.IsActive,1)=1 and empl.LocationId = @LocationId

END