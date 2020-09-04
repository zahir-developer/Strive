
CREATE procedure [CON].[uspSaveVehicle]	
@tvpClient tvpClient READONLY,
@tvpClientVehicle tvpClientVehicle READONLY
AS 
BEGIN
DECLARE @ClientId int
MERGE  [CON].[tblclient] TRG
USING @tvpClient SRC
ON (TRG.ClientId = SRC.ClientId)
WHEN MATCHED 
THEN

UPDATE SET 

         TRG.FirstName = SRC.FirstName,
		 TRG.MiddleName = SRC.MiddleName,
		 TRG.LastName = SRC.LastName,
		 TRG.Gender = SRC.Gender,
		 TRG.MaritalStatus = SRC.MaritalStatus,
		 TRG.BirthDate = SRC.BirthDate,
		 TRG.Notes = SRC.Notes,
		 TRG.RecNotes = SRC.RecNotes,
		 TRG.Score = SRC.Score,
		 TRG.NoEmail = SRC.NoEmail,
		 TRG.ClientType = SRC.ClientType,
		 TRG.IsActive = SRC.IsActive

WHEN NOT MATCHED  THEN

INSERT (FirstName,
MiddleName,
LastName,
Gender,
MaritalStatus,
BirthDate,
Notes,
RecNotes,
Score,
NoEmail,
ClientType,
IsActive)
VALUES (SRC.FirstName,
SRC.MiddleName,
SRC.LastName,
SRC.Gender,
SRC.MaritalStatus,
SRC.BirthDate,
SRC.Notes,
SRC.RecNotes,
SRC.Score,
SRC.NoEmail,
SRC.ClientType,
SRC.IsActive);
SELECT @ClientId = scope_identity();


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
INSERT (ClientId,
LocationId,
VehicleNumber,
VehicleModel,
VehicleModelNo,
VehicleYear,
VehicleColor,
Upcharge,
Barcode,
Notes)
VALUES(@ClientId,
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