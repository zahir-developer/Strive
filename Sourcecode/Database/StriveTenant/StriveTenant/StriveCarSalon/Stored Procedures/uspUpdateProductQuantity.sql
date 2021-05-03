CREATE PROCEDURE [StriveCarSalon].[uspUpdateProductQuantity]--[StriveCarSalon].[uspUpdateProductQuantity] 1,470
 (
 @Quantity int,
 @ProductId int)
AS
BEGIN
UPDATE [tblProduct]
   SET       [Quantity] = Quantity - (@Quantity)
 WHERE ProductId = @ProductId

END