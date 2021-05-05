CREATE PROCEDURE [StriveCarSalon].[uspGetEmployeeDocumentById] 
@DocumentId int
AS
begin
select 
EmployeeDocumentId
,EmployeeId
,[Filename]
,FileType
,[Password]
,IsPasswordProtected
,IsActive
from [tblEmployeeDocument]
WHERE EmployeeDocumentId=@DocumentId AND (IsDeleted = 0 OR IsDeleted IS NULL)
end
