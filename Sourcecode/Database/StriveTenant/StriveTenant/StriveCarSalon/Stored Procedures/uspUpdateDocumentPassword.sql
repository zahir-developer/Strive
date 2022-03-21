CREATE PROCEDURE [StriveCarSalon].[uspUpdateDocumentPassword] 
(@DocumentId int,
@EmployeeId int,
@Password varchar(max))
as
begin
update [tblEmployeeDocument] set [Password] = @Password 
 WHERE EmployeeDocumentId=@DocumentId
end
