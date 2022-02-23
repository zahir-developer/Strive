CREATE PROCEDURE [StriveCarSalon].[uspUpdateProduct] 
 @ProductId int,
 @ProductName nvarchar(60),
 @ProductType int,
 @LocationId int,
 @VendorId int,
 @Size int,
 @SizeDescription nvarchar(10),
 @Quantity float,
 @QuantityDescription nvarchar(10),
 @Cost float,
 @IsTaxable bit,
 @TaxAmount float,
 @IsActive bit,
 @ThresholdLimit int

AS
BEGIN
UPDATE [tblProduct]
   SET [ProductName] = @ProductName
      ,[ProductType] = @ProductType
      ,[LocationId] = @LocationId
      ,[VendorId] = @VendorId
      ,[Size] = @Size
      ,[SizeDescription] = @SizeDescription
      ,[Quantity] = @Quantity
      ,[QuantityDescription] = @QuantityDescription
      ,[Cost] = @Cost
      ,[IsTaxable] = @IsTaxable
      ,[TaxAmount] = @TaxAmount
      ,[IsActive] = @IsActive
      ,[ThresholdLimit] = @ThresholdLimit
 WHERE ProductId = @ProductId

END