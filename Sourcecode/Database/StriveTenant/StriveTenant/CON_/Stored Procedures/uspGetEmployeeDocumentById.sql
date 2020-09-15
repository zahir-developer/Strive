
CREATE PROCEDURE [CON].[uspGetEmployeeDocumentById] 
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
from [CON].[tblEmployeeDocument]
WHERE EmployeeDocumentId=@DocumentId AND (IsDeleted = 0 OR IsDeleted IS NULL)
end
