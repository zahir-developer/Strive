
CREATE proc [StriveCarSalon].[uspGetAllDocumentById] 
(@EmployeeId int,
@LocationId int)
as
begin
select tbld.DocumentId,
       tbld.EmployeeId,
       tbld.Filename,
       tbld.Filepath,
       tbld.Password,
       tbld.CreatedDate,
       tbld.ModifiedDate,
       tbld.IsActive

from [StriveCarSalon].[tblDocument]  tbld inner join [StriveCarSalon].[tblEmployeeDetail] tbll
ON(tbld.EmployeeId = tbll.EmployeeId)
WHERE tbld.EmployeeId=@EmployeeId and 
	  tbll.LocationId =@LocationId 
end