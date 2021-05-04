

CREATE proc [StriveCarSalon].[uspUpdateDocumentPassword] 
(@DocumentId int,
@EmployeeId int,
@Password varchar(max))
as
begin
update [StriveCarSalon].[tblEmployeeDocument] set Password = @Password 
 WHERE EmployeeDocumentId=@DocumentId
end
