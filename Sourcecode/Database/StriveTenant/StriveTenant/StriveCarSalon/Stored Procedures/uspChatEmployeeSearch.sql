CREATE   procedure [StriveCarSalon].[uspChatEmployeeSearch]
(@EmployeeName varchar(30))
as 
begin
select * from tblEmployee where FirstName like '%'+@EmployeeName+'%'
end