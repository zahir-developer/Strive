CREATE PROCEDURE [StriveCarSalon].[uspDeleteClient]
    (
     @ClientId int)
AS 
BEGIN
    UPDATE [StriveCarSalon].[tblClient] 
    SET  IsDeleted=1  WHERE ClientId = @ClientId
	UPDATE [StriveCarSalon].[tblClientAddress] 
    SET  IsDeleted=1  WHERE ClientId = @ClientId
END
