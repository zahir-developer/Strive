
CREATE PROCEDURE [CON].[uspUpdateBarcodeByVehicleId]
(@Barcode nvarchar(100), @VehicleId int)
AS
BEGIN
update [CON].[tblClientVehicle] set Barcode=@Barcode where VehicleId=@VehicleId;
END