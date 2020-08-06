CREATE PROCEDURE [StriveCarSalon].[uspGetProducts] 
(@ProductId int = null)
AS
BEGIN

SELECT 
	ProductId, 
	ProductName, 
	ProductCode, 
	ProductDescription, 
	ProductType,
	LocationId, 
	VendorId, 
	Size, 
	SizeDescription, 
	Quantity, 
	QuantityDescription,
	Cost, 
	Price,
	IsTaxable, 
	TaxAmount, 
	IsActive, 
	ThresholdLimit
FROM [StriveCarSalon].[tblProduct] prd
WHERE isnull(prd.IsDeleted,0)=0
AND
 (@ProductId is null or prd.ProductId = @ProductId)

END