
CREATE PROCEDURE [CON].[uspDeleteProduct] (@ProductId int)
AS
BEGIN

	update StriveCarSalon.tblProduct set IsDeleted= 1 where ProductId = @ProductId

END
