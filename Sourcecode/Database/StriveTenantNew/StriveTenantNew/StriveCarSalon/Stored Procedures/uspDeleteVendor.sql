


CREATE PROCEDURE [StriveCarSalon].[uspDeleteVendor] (@VendorId int)
AS
BEGIN

Update  [StriveCarSalon].[tblvendor] Set IsDeleted=1 where VendorId=@VendorId

Update [StriveCarSalon].[tblvendorAddress] Set IsDeleted=1 where VendorId=@VendorId

END