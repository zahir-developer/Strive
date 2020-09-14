




CREATE PROCEDURE [CON].[uspDeleteVendor] (@VendorId int)
AS
BEGIN

Update  [CON].[tblvendor] Set IsDeleted=1 where VendorId=@VendorId

Update [CON].[tblvendorAddress] Set IsDeleted=1 where VendorId=@VendorId

END
