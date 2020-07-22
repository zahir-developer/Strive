
CREATE PROCEDURE [StriveCarSalon].[uspDeleteLocation]
    (
     @tblLocationId int)
AS 
BEGIN
    UPDATE [StriveCarSalon].[tblLocation] 
    SET IsActive=0 WHERE LocationId = @tblLocationId
END