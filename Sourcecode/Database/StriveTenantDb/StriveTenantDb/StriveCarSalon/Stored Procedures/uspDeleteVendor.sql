
CREATE PROCEDURE [StriveCarSalon].[uspDeleteVendor] (@VendorId int)
AS
BEGIN

Update  [StriveCarSalon].[tblvendor] Set IsActive=0 where VendorId=@VendorId

Update [StriveCarSalon].[tblvendorAddress] Set IsActive=0 where RelationshipId=@VendorId

--DELETE FROM [StriveCarSalon].[tblProduct]
-- WHERE ProductId = @VendorId

END