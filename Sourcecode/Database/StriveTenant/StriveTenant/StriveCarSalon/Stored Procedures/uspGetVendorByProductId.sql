

-- =============================================
-- Author:		SHALINI
-- Create date: 06-02-2021
-- Description:	Gets the vendor name and phone 
-- =============================================
--[StriveCarSalon].[uspGetVendorByProductId] 488
CREATE PROCEDURE [StriveCarSalon].[uspGetVendorByProductId] 
(@ProductId int)
AS
BEGIN
Select distinct  ProductId, v.VendorId, v.VendorName,venAdd.PhoneNumber
from tblProductVendor pv
INNER JOIN [tblVendorAddress] venAdd on venAdd.VendorId = pv.VendorId	
LEFT JOIN tblVendor v on v.VendorId = pv.VendorId 
where pv.ProductId = @ProductId and pv.IsDeleted = 0
END