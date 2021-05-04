
Create PROCEDURE [StriveCarSalon].[uspDeleteDocumentById] 
@DocumentId int
AS
begin
UPDATE [StriveCarSalon].[tblDocument] SET IsDeleted = 1
WHERE DocumentId=@DocumentId
end