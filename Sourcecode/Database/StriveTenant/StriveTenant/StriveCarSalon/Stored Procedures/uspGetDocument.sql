--[StriveCarSalon].[uspGetDocument]661
CREATE PROCEDURE [StriveCarSalon].[uspGetDocument] 
@DocumentType int
AS
BEGIN
SELECT 
TOP 1
DocumentId
,[Filename]
,FilePath
,OriginalFileName
,DocumentName
,CONCAT(emp.FirstName, ' ', emp.LastName) as CreatedBy
,doc.CreatedDate
,doc.UpdatedDate
,doc.IsActive
from [tblDocument] doc
LEFT JOIN tblEmployee emp on doc.CreatedBy = emp.EmployeeId
WHERE DocumentType=@DocumentType AND (ISNULL(doc.IsDeleted,0) = 0)

END