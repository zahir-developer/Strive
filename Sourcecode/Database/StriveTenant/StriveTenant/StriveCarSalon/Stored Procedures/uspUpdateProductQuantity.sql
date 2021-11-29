--[StriveCarSalon].[uspUpdateProductQuantity] 5,578
CREATE PROCEDURE [StriveCarSalon].[uspUpdateProductQuantity]
 (
 @Quantity int,
 @ProductId int)
AS
BEGIN
UPDATE [tblProduct]
   SET       [Quantity] =  @Quantity
 WHERE ProductId = @ProductId

END