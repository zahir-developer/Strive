﻿CREATE PROC [StriveCarSalon].[uspUpdateSalesItem]
(@JobItemId int,@ServiceId int,@Quantity int,@Price decimal)
AS
BEGIN

     Update tblJobItem set Quantity = @Quantity, Price = @Price ,ServiceId = @ServiceId where JobItemId = @JobItemId

END
