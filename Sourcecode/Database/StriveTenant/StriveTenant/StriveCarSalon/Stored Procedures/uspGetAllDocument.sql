--[StriveCarSalon].[uspGetAllDocument]656
CREATE PROCEDURE [StriveCarSalon].[uspGetAllDocument] 
@DocumentType int
AS
BEGIN


SELECT 
DocumentId
,[Filename]
,FilePath
,OriginalFileName
,docSubType.valuedesc as DocumentSubtype
,DocumentName
,r.RoleName
,CONCAT(emp.FirstName, ' ', emp.LastName) as CreatedBy
,doc.CreatedDate
,doc.UpdatedDate
,doc.IsActive
from [tblDocument] doc
LEFT JOIN tblEmployee emp on doc.CreatedBy = emp.EmployeeId
LEFT JOIN GetTable('DocumentSubtype') docSubType on docSubType.valueId = doc.DocumentSubType
LEFT JOIN tblRoleMaster r on doc.RoleId = r.RoleMasterId

WHERE DocumentType=@DocumentType AND (ISNULL(doc.IsDeleted,0) = 0)
ORDER BY 1 DESC

END