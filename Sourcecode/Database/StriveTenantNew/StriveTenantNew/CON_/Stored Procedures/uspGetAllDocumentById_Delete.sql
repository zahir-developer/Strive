




CREATE PROCEDURE [CON].[uspGetAllDocumentById_Delete] 
(@EmployeeId int)
as
begin
select tbld.EmployeeDocumentId,
       tbld.EmployeeId,
       tbld.Filename,
       tbld.Filepath,
       tbld.Password,
       tbld.CreatedDate,
       tbld.IsActive

from [CON].[tblEmployeeDocument]  tbld inner join [CON].[tblEmployeeDetail] tbll
ON(tbld.EmployeeId = tbll.EmployeeId)
WHERE tbld.EmployeeId=@EmployeeId 
and tbld.IsDeleted = 0
end