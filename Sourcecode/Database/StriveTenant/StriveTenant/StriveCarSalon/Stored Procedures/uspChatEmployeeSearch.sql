create   procedure [StriveCarSalon].uspChatEmployeeSearch --'Ra'
(@EmployeeName varchar(30))
as 
begin
select * from tblEmployee where FirstName like '%'+@EmployeeName+'%'
end