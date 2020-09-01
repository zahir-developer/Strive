
CREATE PROCEDURE [CON].[uspDeleteEmployeeDocumentById] 
@DocumentId int
AS
begin
UPDATE [CON].[tblEmployeeDocument] SET IsDeleted = 1
WHERE EmployeeDocumentId=@DocumentId
end