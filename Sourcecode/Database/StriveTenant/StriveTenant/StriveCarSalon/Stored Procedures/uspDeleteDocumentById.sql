
CREATE PROCEDURE [StriveCarSalon].[uspDeleteDocumentById] 
@DocumentId int
AS
begin
UPDATE [tblDocument] SET IsDeleted = 1
WHERE DocumentId=@DocumentId
end