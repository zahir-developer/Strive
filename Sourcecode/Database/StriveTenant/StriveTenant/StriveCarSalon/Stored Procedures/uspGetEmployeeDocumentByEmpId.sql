CREATE PROCEDURE [StriveCarSalon].[uspGetEmployeeDocumentByEmpId] 
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
,CreatedDate
from [StriveCarSalon].[tblEmployeeDocument]
WHERE EmployeeId=@EmployeeId AND (IsDeleted = 0 OR IsDeleted IS NULL)
end
