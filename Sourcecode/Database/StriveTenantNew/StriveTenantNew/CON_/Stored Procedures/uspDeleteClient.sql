
CREATE PROCEDURE [CON].[uspDeleteClient]
    (
     @ClientId int)
AS 
BEGIN
    UPDATE [CON].[tblClient] 
    SET  IsDeleted=1  WHERE ClientId = @ClientId
	UPDATE [CON].[tblClientAddress] 
    SET  IsDeleted=1  WHERE ClientId = @ClientId
END