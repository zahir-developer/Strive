---------------------History---------------------------
-- ====================================================
-- 16-06-2021, Shalini -removed wildcard from Query

------------------------------------------------		 


CREATE PROCEDURE [StriveCarSalon].[uspGetAllVendor]
(@VendorId int = null,@VendorSearch varchar(50) = null)
AS 
BEGIN
SELECT 
Distinct
V.VendorId,
V.VendorName,
V.VendorAlias,
VA.PhoneNumber,
VA.Email,
V.IsActive
FROM  [tblVendor] V
left Join [tblVendorAddress] VA
 On V.VendorId=VA.VendorId
 WHERE V.IsDeleted = 0 AND VA.IsDeleted=0 
  and (@VendorId is null or VA.VendorId= @VendorId)AND
 (@VendorSearch is null or V.VendorName like @VendorSearch+'%'
 or VA.Address1 like @VendorSearch+'%' or VA.Address2 like @VendorSearch+'%'or VA.Email like @VendorSearch+'%'or VA.PhoneNumber like @VendorSearch+'%')

 order by V.VendorName ASC
 --or tblla.PhoneNumber like '%'+@LocationSearch+'%'
 --or tblla.Email like '%'+@LocationSearch+'%')
END


