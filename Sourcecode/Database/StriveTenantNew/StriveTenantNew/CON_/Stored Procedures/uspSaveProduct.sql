


CREATE procedure [CON].[uspSaveProduct]	
@tvpProduct tvpProduct READONLY
AS 
BEGIN
DECLARE @InsertedProductId INT

MERGE  [CON].[tblProduct] TRG
USING @tvpProduct SRC
ON (TRG.ProductId = SRC.ProductId)
WHEN MATCHED 
THEN

UPDATE SET 
 TRG.ProductName = SRC.ProductName
,TRG.ProductType = SRC.ProductType
,TRG.LocationId = SRC.LocationId
,TRG.VendorId = SRC.VendorId
,TRG.Size = SRC.Size
,TRG.SizeDescription = SRC.SizeDescription
,TRG.Quantity = SRC.Quantity
,TRG.QuantityDescription = SRC.QuantityDescription
,TRG.Cost = SRC.Cost
,TRG.IsTaxable = SRC.IsTaxable
,TRG.TaxAmount = SRC.TaxAmount
,TRG.IsActive = SRC.IsActive
,TRG.ThresholdLimit = SRC.ThresholdLimit


WHEN NOT MATCHED  THEN

INSERT (ProductName
,ProductCode
,ProductDescription
,ProductType
,LocationId
,VendorId
,Size
,SizeDescription
,Quantity
,QuantityDescription
,Cost
,IsTaxable
,TaxAmount
,IsActive
,ThresholdLimit)
 VALUES (
 SRC.ProductName
,SRC.ProductCode
,SRC.ProductDescription
,SRC.ProductType
,SRC.LocationId
,SRC.VendorId
,SRC.Size
,SRC.SizeDescription
,SRC.Quantity
,SRC.QuantityDescription
,SRC.Cost
,SRC.IsTaxable
,SRC.TaxAmount
,SRC.IsActive
,SRC.ThresholdLimit);
 SELECT @InsertedProductId = scope_identity();


END