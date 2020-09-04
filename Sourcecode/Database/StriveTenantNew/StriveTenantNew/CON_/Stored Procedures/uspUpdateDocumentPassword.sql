


CREATE PROCEDURE [CON].[uspUpdateDocumentPassword] 
(@DocumentId int,
@EmployeeId int,
@Password varchar(max))
as
begin
update [CON].[tblDocument] set Password = @Password 
 WHERE DocumentId=@DocumentId
end