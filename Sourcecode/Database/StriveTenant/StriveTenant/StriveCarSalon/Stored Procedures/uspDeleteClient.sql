CREATE PROCEDURE [StriveCarSalon].[uspDeleteClient]
    (
     @ClientId int)
AS 
BEGIN
    UPDATE [tblClient] 
    SET  IsDeleted=1  WHERE ClientId = @ClientId
	UPDATE [tblClientAddress] 
    SET  IsDeleted=1  WHERE ClientId = @ClientId
END
