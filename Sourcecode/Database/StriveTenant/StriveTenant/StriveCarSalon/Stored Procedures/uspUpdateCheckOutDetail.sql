
-- =============================================================
-- Author:		Vineeth B
-- Create date: 12-10-2020
-- Description: Update ActualTimeOut and Checkout while Checkout
-- =============================================================


-- =============================================================
----------------------------History-----------------------------
-- 02-Apr-2021 - Zahir - Changed ActualTimout with CheckoutTime
-- =============================================================
CREATE procedure [StriveCarSalon].[uspUpdateCheckOutDetail] 
(@JobId  int,@CheckOut bit, @CheckOutTime datetime)

AS
BEGIN
UPDATE [tblJob] SET CheckOut=@CheckOut, CheckOutTime = @CheckOutTime WHERE JobId=@JobId
END