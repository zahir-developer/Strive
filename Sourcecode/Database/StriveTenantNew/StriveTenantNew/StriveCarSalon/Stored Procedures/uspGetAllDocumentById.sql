



CREATE proc [StriveCarSalon].[uspGetAllDocumentById] 
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

from [StriveCarSalon].[tblEmployeeDocument]  tbld inner join [StriveCarSalon].[tblEmployeeDetail] tbll
ON(tbld.EmployeeId = tbll.EmployeeId)
WHERE tbld.EmployeeId=@EmployeeId 
and tbld.IsDeleted = 0
end