

CREATE PROCEDURE [StriveCarSalon].[uspDeleteProduct] (@productId int)
AS
BEGIN

Update [StriveCarSalon].[tblProduct] SET  IsDeleted=1 WHERE ProductId = @productId

END