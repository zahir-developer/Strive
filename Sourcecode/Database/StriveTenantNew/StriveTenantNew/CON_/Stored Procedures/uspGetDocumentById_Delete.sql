
CREATE PROCEDURE [CON].[uspGetDocumentById_Delete] 
@EmployeeDocumentId int
AS
begin
select 
EmployeeDocumentId
,EmployeeId
,[Filename]
,FileType
,IsPasswordProtected
,IsActive
from [CON].[tblEmployeeDocument]
WHERE EmployeeDocumentId=@EmployeeDocumentId AND (IsDeleted = 0 OR IsDeleted IS NULL)
end