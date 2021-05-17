--[StriveCarSalon].[uspUpdateProductQuantity] 1,470
CREATE PROCEDURE [StriveCarSalon].[uspUpdateProductQuantity]
 (
 @Quantity int,
 @ProductId int)
AS
BEGIN
UPDATE [tblProduct]
   SET       [Quantity] = Quantity - (@Quantity)
 WHERE ProductId = @ProductId

END