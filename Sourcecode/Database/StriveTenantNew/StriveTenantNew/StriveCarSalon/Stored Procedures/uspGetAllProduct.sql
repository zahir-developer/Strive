CREATE PROCEDURE [StriveCarSalon].[uspGetAllProduct] 
--(@LocationId int = null)
AS
BEGIN

Select ProductId, ProductName, ProductCode, ProductDescription, ProductType,
LocationId, VendorId, Size, SizeDescription, Quantity, QuantityDescription,
Cost, IsTaxable, TaxAmount, IsActive, ThresholdLimit

FROM [StriveCarSalon].[tblProduct] WHERE 
--LocationId = @LocationId and 
IsActive = 1

END