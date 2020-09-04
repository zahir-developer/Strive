
CREATE PROCEDURE [CON].[uspGetAllProduct] 
--(@LocationId int = null)
AS
BEGIN

Select ProductId, ProductName, ProductCode, ProductDescription, ProductType,[FileName],ThumbFileName,
LocationId, VendorId, Size, SizeDescription, Quantity, QuantityDescription,
Cost, IsTaxable, TaxAmount, IsActive, ThresholdLimit

FROM [CON].[tblProduct] WHERE 
--LocationId = @LocationId and 
IsDeleted = 0

END