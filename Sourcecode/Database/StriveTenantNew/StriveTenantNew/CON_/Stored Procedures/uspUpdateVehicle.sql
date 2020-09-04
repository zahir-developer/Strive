
CREATE procedure [CON].[uspUpdateVehicle]	
@tvpClientVehicle tvpClientVehicle READONLY
AS 
BEGIN
MERGE  [CON].[tblClientVehicle] TRG
USING @tvpClientVehicle SRC
ON (TRG.VehicleId = SRC.VehicleId)
WHEN MATCHED 
THEN

UPDATE SET

      TRG.ClientId = SRC.ClientId,
	  TRG.LocationId = SRC.LocationId,
	  TRG.VehicleNumber = SRC.VehicleNumber,
	  TRG.VehicleModel = SRC.VehicleModel,
	  TRG.VehicleModelNo = SRC.VehicleModelNo,
	  TRG.VehicleYear = SRC.VehicleYear,
	  TRG.VehicleColor = SRC.VehicleColor,
	  TRG.Upcharge = SRC.Upcharge,
	  TRG.Barcode = SRC.Barcode,
	  TRG.Notes = SRC.Notes,
	  TRG.CreatedDate = SRC.CreatedDate


WHEN NOT MATCHED  THEN
INSERT (
LocationId,
VehicleNumber,
VehicleModel,
VehicleModelNo,
VehicleYear,
VehicleColor,
Upcharge,
Barcode,
Notes)
VALUES(
SRC.LocationId,
SRC.VehicleNumber,
SRC.VehicleModel,
SRC.VehicleModelNo,
SRC.VehicleYear,
SRC.VehicleColor,
SRC.Upcharge,
SRC.Barcode,
SRC.Notes);

  

END