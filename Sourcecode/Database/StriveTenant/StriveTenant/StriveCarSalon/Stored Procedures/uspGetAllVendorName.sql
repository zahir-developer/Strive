CREATE PROCEDURE [StriveCarSalon].[uspGetAllVendorName] 

AS 
BEGIN

	select 
	v.VendorId,
	v.VendorName,
	v.IsActive,
	v.IsDeleted
	 from tblVendor v
	 where v.IsActive = 1 
	 and ISNULL(v.IsDeleted,0) = 0 

end