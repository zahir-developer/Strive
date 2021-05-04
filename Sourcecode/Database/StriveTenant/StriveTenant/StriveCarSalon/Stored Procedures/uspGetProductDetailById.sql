CREATE PROCEDURE [StriveCarSalon].[uspGetProductDetailById] --[StriveCarSalon].[uspGetProductDetailById] 532
(@ProductId int)
AS
BEGIN

SELECT TOP 1
	prd.ProductId, 
	prd.ProductName, 
	prd.ProductType,
	prd.[FileName],
	prd.[ThumbFileName],
	prd.OriginalFileName,
	prd.LocationId, 
	prd.Size, 
	prd.SizeDescription, 
	prd.Quantity, 
	prd.QuantityDescription,
	prd.Cost, 
	prd.Price,
	prd.IsTaxable, 
	prd.TaxAmount, 
	prd.ThresholdLimit,
	loc.LocationName,
	tbpt.valuedesc as ProductTypeName,
	tbsz.valuedesc as SizeName
FROM [StriveCarSalon].[tblProduct] prd
inner JOIN [StriveCarSalon].[tblLocation] as loc ON (prd.LocationId = loc.LocationId)
LEFT JOIN [StriveCarSalon].[GetTable]('ProductType') tbpt ON (prd.ProductType = tbpt.valueid)
LEFT JOIN [StriveCarSalon].[GetTable]('Size') tbsz ON (prd.Size = tbsz.valueid)
WHERE prd.IsDeleted = 0 AND prd.ProductId = @ProductId

Select ProductVendorId, ProductId, v.VendorId, v.VendorName
from tblProductVendor pv
LEFT JOIN tblVendor v on v.VendorId = pv.VendorId 
where pv.ProductId = @ProductId and pv.IsDeleted = 0

END