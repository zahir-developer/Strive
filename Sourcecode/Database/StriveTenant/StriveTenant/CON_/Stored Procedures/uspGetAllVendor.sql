CREATE PROCEDURE [CON].[uspGetAllVendor]
(@VendorId int = null,@VendorSearch varchar(50) = null)
AS 
BEGIN
SELECT 
V.VendorId
,V.VIN
,V.VendorName
,V.VendorAlias
,V.IsActive
,VA.VendorAddressId	
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
	
FROM  [CON].[tblVendor] V
Inner Join [CON].[tblVendorAddress] VA
 On V.VendorId=VA.VendorId
 WHERE V.IsDeleted = 0 AND VA.IsDeleted=0 
  and (@VendorId is null or VA.VendorId= @VendorId)AND
 (@VendorSearch is null or V.VendorName like '%'+@VendorSearch+'%'
 or VA.Address1 like '%'+@VendorSearch+'%' or VA.Address2 like '%'+@VendorSearch+'%' or VA.Email like '%'+@VendorSearch+'%')
 order by VendorId desc
 --or tblla.PhoneNumber like '%'+@LocationSearch+'%'
 --or tblla.Email like '%'+@LocationSearch+'%')
 

END


