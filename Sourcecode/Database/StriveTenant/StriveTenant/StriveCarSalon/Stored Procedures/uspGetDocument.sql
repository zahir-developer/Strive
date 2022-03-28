--[StriveCarSalon].[uspGetDocument]661
CREATE PROCEDURE [StriveCarSalon].[uspGetDocument] 
@DocumentType int,
@DocumentSubType int = null
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
WHERE DocumentType=@DocumentType AND (DocumentSubType =@DocumentSubType OR (@DocumentSubType IS NULL OR @DocumentSubType = 0)) AND (ISNULL(doc.IsDeleted,0) = 0)

END