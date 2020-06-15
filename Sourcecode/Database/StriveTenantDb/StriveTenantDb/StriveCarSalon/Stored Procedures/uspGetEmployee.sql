create proc [StriveCarSalon].[uspGetEmployee]
as
begin
select EmployeeId, FirstName, LastName, [Role] from [StriveCarSalon].tblEmployee
end