
CREATE PROCEDURE [CON].[uspGetProducts] 
(@ProductId int = null,@ProductSearch varchar(50)=null)
AS
BEGIN

SELECT 
	prd.ProductId, 
	prd.ProductName, 
	prd.ProductCode, 
	prd.ProductDescription, 
	prd.ProductType,
	prd.[FileName],
	prd.[ThumbFileName],
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
	prd.IsActive, 
	prd.ThresholdLimit,
	loc.LocationName,
	ven.VendorName,
	tbpt.valuedesc as ProductTypeName,
	tbsz.valuedesc as SizeName
FROM [CON].[tblProduct] prd
INNER JOIN [CON].[tblLocation] as loc ON (prd.LocationId = loc.LocationId)
INNER JOIN [CON].[tblVendor] as ven ON (prd.VendorId = ven.VendorId)
LEFT JOIN [CON].[GetTable]('ProductType') tbpt ON (prd.ProductType = tbpt.valueid)
LEFT JOIN [CON].[GetTable]('Size') tbsz ON (prd.Size = tbsz.valueid)

WHERE isnull(prd.IsDeleted,0)=0
AND
 (@ProductId is null or prd.ProductId = @ProductId)AND
 (@ProductSearch is null or prd.ProductName like '%'+@ProductSearch+'%'
  or tbpt.valuedesc like '%'+@ProductSearch+'%'
  or tbsz.valuedesc like '%'+@ProductSearch+'%'
  or loc.LocationName like '%'+@ProductSearch+'%'
  or ven.VendorName like '%'+@ProductSearch+'%')
  
order by ProductId Desc
 

END
