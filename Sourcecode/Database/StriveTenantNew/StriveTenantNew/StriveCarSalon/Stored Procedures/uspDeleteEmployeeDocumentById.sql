CREATE PROCEDURE [StriveCarSalon].[uspDeleteEmployeeDocumentById] 
@DocumentId int
AS
begin
UPDATE [StriveCarSalon].[tblEmployeeDocument] SET IsDeleted = 1
WHERE EmployeeDocumentId=@DocumentId
end