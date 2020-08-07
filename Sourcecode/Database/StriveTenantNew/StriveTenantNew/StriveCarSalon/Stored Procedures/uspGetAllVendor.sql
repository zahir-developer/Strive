




CREATE PROCEDURE [StriveCarSalon].[uspGetAllVendor]
AS 
BEGIN
SELECT 
V.VendorId
,VIN
,VendorName
,VendorAlias
,v.IsActive
,VendorAddressId	 AS 	VendorAddress_VendorAddressId
,Address1	 AS 	VendorAddress_Address1
,Address2	 AS 	VendorAddress_Address2
,PhoneNumber	 AS 	VendorAddress_PhoneNumber
,PhoneNumber2	 AS 	VendorAddress_PhoneNumber2
,Email	 AS 	VendorAddress_Email
,City	 AS 	VendorAddress_City
,State	 AS 	VendorAddress_State
,Country AS     VendorAddress_Country
,Zip	 AS 	VendorAddress_Zip
,Fax	 AS 	VendorAddress_Fax
,VA.IsActive	 AS 	VendorAddress_IsActive
	
FROM  [StriveCarSalon].[tblVendor] V
Inner Join [StriveCarSalon].[tblVendorAddress] VA
			 On V.VendorId=VA.VendorId
			 WHERE V.IsDeleted = 0 AND VA.IsDeleted=0 
END