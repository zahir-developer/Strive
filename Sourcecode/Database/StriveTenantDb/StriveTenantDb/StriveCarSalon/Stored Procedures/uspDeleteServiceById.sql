
CREATE PROCEDURE [StriveCarSalon].[uspDeleteServiceById]
    (
     @tblServiceId int)
AS 
BEGIN
    UPDATE [StriveCarSalon].[tblService] 
    SET IsActive=0 WHERE ServiceId = @tblServiceId
END