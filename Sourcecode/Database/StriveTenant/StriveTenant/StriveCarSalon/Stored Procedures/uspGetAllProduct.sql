CREATE PROCEDURE [StriveCarSalon].[uspGetAllProduct] 
--(@LocationId int = null)
AS
BEGIN

Select ProductId, ProductName, ProductCode, ProductDescription, ProductType,[FileName],ThumbFileName,
LocationId, VendorId, Size, SizeDescription, Quantity, QuantityDescription,
Cost, IsTaxable, TaxAmount, IsActive, ThresholdLimit

FROM [StriveCarSalon].[tblProduct] WHERE 
--LocationId = @LocationId and 
ISNULL(IsDeleted,0) = 0

END
