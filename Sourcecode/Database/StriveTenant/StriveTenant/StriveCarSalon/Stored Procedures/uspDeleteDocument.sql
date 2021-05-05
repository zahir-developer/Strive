
CREATE PROCEDURE [StriveCarSalon].[uspDeleteDocument] 
@DocumentType int
AS
begin
UPDATE [tblDocument] SET IsDeleted = 1
WHERE DocumentType=@DocumentType
end