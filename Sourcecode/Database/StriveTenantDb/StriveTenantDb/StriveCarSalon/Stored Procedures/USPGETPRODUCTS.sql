CREATE PROCEDURE [StriveCarSalon].[USPGETPRODUCTS]
AS
BEGIN

Select ProductId, ProductName, ProductCode, ProductDescription, ProductType,
LocationId, VendorId, Size, SizeDescription, Quantity, QuantityDescription,
Cost, IsTaxable, TaxAmount, IsActive, ThresholdLimit

FROM [StriveCarSalon].[tblProduct]

END