CREATE PROCEDURE [StriveCarSalon].[uspGetVendorById]
(@VendorId NVARCHAR(50))
AS 
BEGIN
SELECT 
DISTINCT
V.VendorId
,V.VIN
,V.VendorName
,V.VendorAlias
,V.IsActive
,VA.Address1	 
,VA.Address2	
,VA.PhoneNumber	 
,VA.PhoneNumber2
,VA.Email	 
,VA.City	 
,VA.State	 
,VA.Country 
,VA.Zip	 
,VA.Fax	
,VA.IsActive
,websiteAddress	 
	
FROM  [tblVendor] V
Inner Join [tblVendorAddress] VA
 On V.VendorId=VA.VendorId
 WHERE V.IsDeleted = 0 AND VA.IsDeleted=0 
and VA.VendorId IN (Select ID from SPLIT(@VendorId))
 
END
