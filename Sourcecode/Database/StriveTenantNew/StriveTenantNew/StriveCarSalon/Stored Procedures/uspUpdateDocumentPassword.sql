

CREATE proc [StriveCarSalon].[uspUpdateDocumentPassword] 
(@DocumentId int,
@EmployeeId int,
@Password varchar(max))
as
begin
update [StriveCarSalon].[tblDocument] set Password = @Password 
 WHERE DocumentId=@DocumentId
end