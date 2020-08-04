CREATE proc [StriveCarSalon].[uspGetDocumentByEmpId] 
(@DocumentId int,
@EmployeeId int)
as
begin
select DocumentId,
       EmployeeId,
       Filename,
       Filepath,
       Password,
       CreatedDate,
       ModifiedDate,
       IsActive

from [StriveCarSalon].[tblDocument] 
WHERE DocumentId=@DocumentId and 
	  EmployeeId =@EmployeeId
end