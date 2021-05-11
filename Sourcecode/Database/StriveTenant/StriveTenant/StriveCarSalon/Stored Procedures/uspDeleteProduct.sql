CREATE PROCEDURE [StriveCarSalon].[uspDeleteProduct] (@ProductId int)
AS
BEGIN

	update tblProduct set IsDeleted= 1 where ProductId = @ProductId

END