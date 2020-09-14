
CREATE PROCEDURE [StriveCarSalon].[uspSaveClient]
@tvpClient tvpClient READONLY,
@tvpClientAddress tvpClientAddress READONLY
--@tvpClientMembershipDetails tvpClientMembershipDetails READONLY,
--@tvpClientVehicle tvpClientVehicle READONLY

AS 
BEGIN
DECLARE @ClientAddressId int ,
        @ClientMembershipId int ,
        @ClientVehicleId int,
		@ClientId int


---Client--
MERGE  [StriveCarSalon].[tblClient] TRG
USING @tvpClient SRC
ON (TRG.ClientId = SRC.ClientId)
WHEN MATCHED 
THEN

UPDATE SET  TRG.FirstName=SRC.FirstName, TRG.MiddleName=SRC.MiddleName, TRG.LastName=SRC.LastName, 
      TRG.Gender=SRC.Gender, TRG.MaritalStatus = SRC.MaritalStatus, TRG.BirthDate=SRC.BirthDate, TRG.CreatedDate=SRC.CreatedDate,TRG.IsActive=SRC.IsActive,
	  TRG.Notes=SRC.Notes, TRG.RecNotes=SRC.RecNotes, TRG.Score=SRC.Score,TRG.NoEmail=SRC.NoEmail,TRG.ClientType=SRC.ClientType
WHEN NOT MATCHED  THEN

INSERT (FirstName, MiddleName,LastName,Gender,MaritalStatus,BirthDate,CreatedDate,IsActive,Notes,RecNotes,Score,NoEmail,ClientType)
 VALUES (FirstName, MiddleName,LastName,Gender,MaritalStatus,BirthDate,CreatedDate,IsActive,Notes,RecNotes,Score,NoEmail,ClientType);
 SELECT @ClientId = scope_identity();

--ClientAddress--

MERGE  [StriveCarSalon].[tblClientAddress] TRG
USING @tvpClientAddress SRC
ON (TRG.ClientAddressId = SRC.AddressId)
WHEN MATCHED 
THEN

UPDATE SET TRG.ClientId=@ClientId, TRG.Address1=SRC.Address1, TRG.Address2=SRC.Address2, TRG.PhoneNumber=SRC.PhoneNumber,
    TRG.Email=SRC.Email, TRG.City= SRC.City,TRG.State=SRC.State, TRG.Country=SRC.Country, TRG.Zip=SRC.Zip, TRG.IsActive=SRC.IsActive

WHEN NOT MATCHED THEN

INSERT ( ClientId, Address1,Address2,PhoneNumber,Email,City,State,Country,Zip,IsActive)
 VALUES ( @ClientId, SRC.Address1,SRC.Address2,SRC.PhoneNumber,SRC.Email,SRC.City,SRC.State,SRC.Country,SRC.Zip,SRC.IsActive);
 SELECT @ClientAddressId = scope_identity();


 ---ClientMembershipDetails---
--MERGE  [StriveCarSalon].[tblClientMembershipDetails] TRG
--USING @tvpClientMembershipDetails SRC
--ON (TRG.ClientMembershipId = SRC.ClientMembershipId)
--WHEN MATCHED 
--THEN
--UPDATE SET TRG.ClientId=@ClientId, TRG.LocationId=SRC.LocationId, TRG.MembershipId=SRC.MembershipId, 
--    TRG.StartDate=SRC.StartDate, TRG.EndDate= SRC.EndDate,TRG.Status=SRC.Status,TRG.Notes=SRC.Notes,TRG.CreatedDate=SRC.CreatedDate
--WHEN NOT MATCHED  THEN
--INSERT (ClientId, LocationId,MembershipId,StartDate,EndDate,Status,Notes,CreatedDate)
-- VALUES (@ClientId, SRC.LocationId,SRC.MembershipId,SRC.StartDate,SRC.EndDate,SRC.Status,SRC.Notes,SRC.CreatedDate);
-- SELECT @ClientMembershipId = scope_identity();

---ClientVehicle--
--MERGE  [StriveCarSalon].[tblClientVehicle] TRG
--USING @tvpClientVehicle SRC
--ON (TRG.VehicleId = SRC.VehicleId)
--WHEN MATCHED 
--THEN
--UPDATE SET TRG.[ClientId]=@ClientId, TRG.[LocationId]=SRC.[LocationId], TRG.[VehicleNumber]=SRC.[VehicleNumber], 
--    TRG.[VehicleMake]=SRC.[VehicleMake],TRG.[VehicleModel]=SRC.[VehicleModel],TRG.[VehicleModelNo]= SRC.[VehicleModelNo],TRG.[VehicleYear]=SRC.[VehicleYear],
--	TRG.[VehicleColor]=SRC.[VehicleColor],TRG.[Upcharge]=TRG.[Upcharge],TRG.[Barcode]=SRC.[Barcode],TRG.[Notes]=SRC.[Notes],
--	TRG.[CreatedDate]=SRC.[CreatedDate]
--    WHEN NOT MATCHED  THEN

--INSERT ( ClientId, LocationId,VehicleNumber,VehicleMake,VehicleModel,VehicleModelNo,VehicleYear,VehicleColor,Upcharge,Barcode,Notes)
-- VALUES ( @ClientId, LocationId,VehicleNumber,VehicleMake,VehicleModel,VehicleModelNo,VehicleYear,VehicleColor,Upcharge,Barcode,Notes);
-- SELECT @ClientVehicleId = scope_identity();

END
