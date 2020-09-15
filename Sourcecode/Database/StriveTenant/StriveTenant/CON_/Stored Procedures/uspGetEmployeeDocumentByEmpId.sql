
CREATE PROCEDURE [CON].[uspGetEmployeeDocumentByEmpId] 
(@EmployeeId int)
AS
begin
select 
EmployeeDocumentId
,EmployeeId
,[Filename]
,Filepath
,FileType
,IsPasswordProtected
,Comments
,IsActive
from [CON].[tblEmployeeDocument]
WHERE EmployeeId=@EmployeeId AND (IsDeleted = 0 OR IsDeleted IS NULL)
end
