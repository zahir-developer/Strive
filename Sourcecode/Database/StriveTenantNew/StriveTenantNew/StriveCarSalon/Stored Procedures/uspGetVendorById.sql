


CREATE PROC [StriveCarSalon].[uspGetVendorById] 
(@VendorId int)
AS
BEGIN
SELECT tblVendor.VendorId,
tblVendor.VIN,
tblVendor.VendorName,
tblVendor.VendorAlias,
tblVendor.IsActive,


tblVenaddress.AddressId      AS VendorAddress_VendorAddressId,
tblVenaddress.RelationshipId            AS VendorAddress_RelationshipId,
tblVenaddress.Address1          AS VendorAddress_Address1,
tblVenaddress.Address2                AS VendorAddress_Address2,
tblVenaddress.PhoneNumber               AS VendorAddress_PhoneNumber,
tblVenaddress.PhoneNumber2              AS VendorAddress_PhoneNumber2,
tblVenaddress.Email               AS VendorAddress_Email,
tblVenaddress.City               AS VendorAddress_City,
tblVenaddress.State              AS VendorAddress_State,
tblVenaddress.Country         AS VendorAddress_Country,
tblVenaddress.Zip                AS VendorAddress_Zip,
tblVenaddress.Fax                AS VendorAddress_Fax,
tblVenaddress.IsActive           AS VendorAddress_IsActive
from  [StriveCarSalon].[tblVendor] tblVendor LEFT JOIN 
 [StriveCarSalon].[tblVendorAddress] tblVenaddress ON (tblVendor.VendorId = tblVenaddress.RelationshipId)

WHERE tblVendor.VendorId=@VendorId and tblVendor.IsActive=1
END