
CREATE PROCEDURE [StriveCarSalon].[uspGetProduct] (@productId int)
AS
BEGIN

Select TOP 1 ProductId, ProductName, ProductCode, ProductDescription, ProductType,
LocationId, VendorId, Size, SizeDescription, Quantity, QuantityDescription,
Cost, IsTaxable, TaxAmount, IsActive, ThresholdLimit

FROM [StriveCarSalon].[tblProduct] WHERE ProductId = @productId
AND IsDeleted=0

END
