CREATE PROCEDURE [StriveCarSalon].[uspGetProductById] --[StriveCarSalon].[uspGetProductById] 505
(@ProductId int = null)
AS
BEGIN

SELECT 
	prd.ProductId, 
	prd.ProductName, 
	prd.ProductType,
	prd.[FileName],
	prd.[ThumbFileName],
	prd.OriginalFileName,
	prd.LocationId, 
	prd.VendorId, 
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
	ven.VendorName,
	tbpt.valuedesc as ProductTypeName,
	tbsz.valuedesc as SizeName
FROM [StriveCarSalon].[tblProduct] prd
inner JOIN [StriveCarSalon].[tblLocation] as loc ON (prd.LocationId = loc.LocationId)
LEFT JOIN [StriveCarSalon].[tblVendor] as ven ON (prd.VendorId = ven.VendorId)
LEFT JOIN [StriveCarSalon].[GetTable]('ProductType') tbpt ON (prd.ProductType = tbpt.valueid)
LEFT JOIN [StriveCarSalon].[GetTable]('Size') tbsz ON (prd.Size = tbsz.valueid)

WHERE isnull(prd.IsDeleted,0)=0
AND
 (prd.ProductId = @ProductId)
 Order by ProductId DESC
END