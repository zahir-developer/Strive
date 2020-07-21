CREATE PROCEDURE [StriveCarSalon].[uspDeleteProduct] (@productId int)
AS
BEGIN

Update [StriveCarSalon].[tblProduct] SET IsActive = 0 WHERE ProductId = @productId

END