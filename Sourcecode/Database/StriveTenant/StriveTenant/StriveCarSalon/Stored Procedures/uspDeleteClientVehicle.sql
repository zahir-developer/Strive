CREATE PROCEDURE [StriveCarSalon].[uspDeleteClientVehicle]
(@VehicleId int)
AS 
BEGIN
    UPDATE [StriveCarSalon].[tblClientVehicle] 
    SET IsActive=0 WHERE VehicleId = @VehicleId
END
