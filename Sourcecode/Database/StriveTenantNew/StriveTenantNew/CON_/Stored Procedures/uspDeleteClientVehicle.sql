
CREATE PROCEDURE [CON].[uspDeleteClientVehicle]
(@VehicleId int)
AS 
BEGIN
    UPDATE [CON].[tblClientVehicle] 
    SET IsActive=0 WHERE VehicleId = @VehicleId
END