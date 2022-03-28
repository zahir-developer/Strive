/*
1  |  2021-July-05  | Vetriselvi  | Removed the hardcode value 
*/
CREATE PROCEDURE [StriveCarSalon].[uspDeleteClientVehicle]
(@VehicleId int)
AS 
BEGIN

UPDATE [tblClientVehicle] SET IsDeleted=1 WHERE VehicleId = @VehicleId

END
