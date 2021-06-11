Create PROCEDURE [StriveCarSalon].[uspUpdateQuantity]
 (
 @Quantity int,
 @ProductId int)
AS
BEGIN
UPDATE [tblProduct]
   SET       [Quantity] = @Quantity
 WHERE ProductId = @ProductId

END