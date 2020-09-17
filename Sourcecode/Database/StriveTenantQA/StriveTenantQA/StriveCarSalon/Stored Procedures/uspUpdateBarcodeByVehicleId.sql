
create proc [StriveCarSalon].[uspUpdateBarcodeByVehicleId]
(@Barcode nvarchar(100), @VehicleId int)
AS
BEGIN
update [StriveCarSalon].[tblClientVehicle] set Barcode=@Barcode where VehicleId=@VehicleId;
END
