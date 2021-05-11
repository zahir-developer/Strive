
CREATE PROCEDURE [StriveCarSalon].[uspGetDocumentById] 
@documentId int
AS
BEGIN

SELECT 
DocumentId
,[Filename]
,FilePath
,OriginalFileName
,DocumentName
,CONCAT(emp.FirstName, ' ', emp.LastName) as CreatedBy
,doc.CreatedDate
,doc.UpdatedDate
,doc.IsActive
,er.RoleMasterId as RoleId
from [tblDocument] doc
LEFT JOIN tblEmployee emp on doc.CreatedBy = emp.EmployeeId
left join tblRoleMaster er on doc.RoleId   = er.RoleMasterId
WHERE documentid=@documentId AND (ISNULL(doc.IsDeleted,0) = 0)


END