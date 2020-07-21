

CREATE procedure [StriveCarSalon].[uspSaveLocation]
@tvpLocation tvpLocation READONLY,
@tvpLocationAddress tvpLocationAddress READONLY
AS 
BEGIN
--tbllocation
DECLARE @LocationId int
MERGE  [StriveCarSalon].[tblLocation] TRG
USING @tvpLocation SRC
ON (TRG.LocationId = SRC.LocationId)
WHEN MATCHED 
THEN

UPDATE SET TRG.LocationType = SRC.LocationType, TRG.LocationName = SRC.LocationName, 
TRG.LocationDescription = SRC.LocationDescription, TRG.IsFranchise = SRC.IsFranchise, TRG.IsActive = SRC.IsFranchise,
TRG.TaxRate = SRC.TaxRate, TRG.SiteUrl = SRC.SiteUrl, TRG.Currency = SRC.Currency, TRG.Facebook = SRC.Facebook,
TRG.Twitter = SRC.Twitter, TRG.Instagram = SRC.Instagram, TRG.WifiDetail = SRC.WifiDetail, TRG.WorkhourThreshold = SRC.WorkhourThreshold

WHEN NOT MATCHED  THEN

INSERT (LocationType, LocationName, LocationDescription,IsFranchise,IsActive,TaxRate,SiteUrl,Currency,Facebook,Twitter,Instagram,WifiDetail,WorkhourThreshold)
VALUES (SRC.LocationType, SRC.LocationName, SRC.LocationDescription,SRC.IsFranchise,SRC.IsActive,SRC.TaxRate,SRC.SiteUrl,SRC.Currency,SRC.Facebook,SRC.Twitter,SRC.Instagram,SRC.WifiDetail,SRC.WorkhourThreshold);
SELECT @LocationId = scope_identity();
--tblLocationAddress
MERGE  [StriveCarSalon].[tblLocationAddress] TRG
USING @tvpLocationAddress SRC
ON (TRG.AddressId = SRC.LocationAddressId)
WHEN MATCHED 
THEN

UPDATE SET TRG.RelationshipId = SRC.RelationshipId, TRG.Address1 = SRC.Address1, 
TRG.Address2 = SRC.Address2, TRG.PhoneNumber = SRC.PhoneNumber, TRG.PhoneNumber2 = SRC.PhoneNumber2,
TRG.Email = SRC.Email, TRG.City = SRC.City, TRG.State = SRC.State, TRG.Zip = SRC.Zip,
TRG.IsActive = SRC.IsActive, TRG.Country = SRC.Country

WHEN NOT MATCHED  THEN

INSERT (RelationshipId, Address1, Address2,PhoneNumber,PhoneNumber2,Email,City,State,Zip,IsActive,Country)
VALUES (@LocationId, SRC.Address1, SRC.Address2,SRC.PhoneNumber,SRC.PhoneNumber2,SRC.Email,SRC.City,SRC.State,SRC.Zip,SRC.IsActive,SRC.Country);
END