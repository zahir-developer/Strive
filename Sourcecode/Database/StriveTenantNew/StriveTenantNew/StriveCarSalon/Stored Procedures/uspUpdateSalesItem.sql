-- ====================================================
-- Author:		Benny Johnson M
-- Create date: 07-09-2020
-- Description:	Update the Items
-- ====================================================

CREATE PROC [StriveCarSalon].[uspUpdateSalesItem]
(@JobItemId int,@Quantity int,@Price decimal)
AS
BEGIN

     Update StriveCarSalon.tblJobItem set Quantity = @Quantity, Price = @Price where JobItemId = @JobItemId

END