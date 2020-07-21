


CREATE procedure [StriveCarSalon].[uspSaveVendor]
@tvpVendor tvpVendor READONLY,
@tvpVendorAddress tvpVendorAddress READONLY
AS 
BEGIN
DECLARE @InsertedVendorId INT

MERGE  [StriveCarSalon].[tblVendor] TRG
USING @tvpVendor SRC
ON (TRG.VendorId = SRC.VendorId)
WHEN MATCHED 
THEN

UPDATE SET TRG.VIN=SRC.VIN, TRG.VendorName=SRC.VendorName, TRG.VendorAlias=SRC.VendorAlias

WHEN NOT MATCHED  THEN

INSERT (VIN, VendorName, VendorAlias,IsActive)
 VALUES (SRC.VIN, SRC.VendorName, SRC.VendorAlias,SRC.IsActive);
 SELECT @InsertedVendorId = scope_identity();


MERGE  [StriveCarSalon].[tblVendorAddress] TRG
USING @tvpVendorAddress SRC
ON (TRG.AddressId = SRC.VendorAddressId)
WHEN MATCHED 
THEN

UPDATE SET TRG.RelationshipId = SRC.RelationshipId, 
TRG.Address1 = SRC.Address1, TRG.Address2 = SRC.Address2, TRG.PhoneNumber = SRC.PhoneNumber,
TRG.PhoneNumber2 = SRC.PhoneNumber2, TRG.Email = SRC.Email, TRG.City = SRC.City, TRG.State = SRC.State,
TRG.Zip = SRC.Zip, TRG.Fax = SRC.Fax, TRG.IsActive = SRC.IsActive 

WHEN NOT MATCHED  THEN

INSERT (RelationshipId, Address1, Address2,PhoneNumber,PhoneNumber2,Email,City,State,Zip,Fax,IsActive)
VALUES (@InsertedVendorId, SRC.Address1, SRC.Address2,SRC.PhoneNumber,SRC.PhoneNumber2,SRC.Email,SRC.City,SRC.State,SRC.ZIP,SRC.FAX,SRC.IsActive);

END