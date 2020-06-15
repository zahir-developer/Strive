
create proc [StriveLimoSalon].[uspGetEmployee]
as
begin
select EmployeeId, FirstName, LastName, [Role] from [StriveLimoSalon].tblEmployee
end