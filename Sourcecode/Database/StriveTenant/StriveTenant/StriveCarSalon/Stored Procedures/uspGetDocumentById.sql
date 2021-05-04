
CREATE PROCEDURE [StriveCarSalon].[uspGetDocumentById] --[StriveCarSalon].[uspGetDocumentbyid]38
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
from [StriveCarSalon].[tblDocument] doc
LEFT JOIN StriveCarSalon.tblEmployee emp on doc.CreatedBy = emp.EmployeeId
WHERE documentid=@documentId AND (ISNULL(doc.IsDeleted,0) = 0)


END