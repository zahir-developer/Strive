



CREATE PROCEDURE [CON].[uspDeleteServiceById]
    (
     @tblServiceId int)
AS 
BEGIN
    UPDATE [CON].[tblService] 
    SET IsDeleted=1 WHERE ServiceId = @tblServiceId
END
