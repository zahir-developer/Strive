
CREATE PROCEDURE [StriveCarSalon].[uspGetProducts] 
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
	tbpt.valuedesc as ProductTypeName
FROM [StriveCarSalon].[tblProduct] prd
LEFT JOIN [StriveCarSalon].[GetTable]('ProductType') tbpt ON prd.ProductType = tbpt.valueid
WHERE isnull(prd.IsDeleted,0)=0
AND
 (@ProductId is null or prd.ProductId = @ProductId)AND
 (@ProductSearch is null or prd.ProductName like '%'+@ProductSearch+'%')
 --or prd. like '%'+@LocationSearch+'%' or tblla.Address2 like '%'+@LocationSearch+'%'
 --or tblla.PhoneNumber like '%'+@LocationSearch+'%'
 --or tblla.Email like '%'+@LocationSearch+'%')

END