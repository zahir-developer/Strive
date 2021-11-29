CREATE PROCEDURE [StriveCarSalon].[uspUpdateSalesItem]
(@JobItemId int,@ServiceId int,@Quantity int,@Price decimal(19,2))
AS
BEGIN

     Update tblJobItem set Quantity = @Quantity, Price = @Price ,ServiceId = @ServiceId where JobItemId = @JobItemId

END
