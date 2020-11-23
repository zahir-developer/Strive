
-- =============================================================
-- Author:		Vineeth B
-- Create date: 12-10-2020
-- Description: Update ActualTimeOut and Checkout while Checkout
-- =============================================================
CREATE procedure [StriveCarSalon].[uspUpdateCheckOutDetailForJobId] 
(@JobId  int,@CheckOut bit, @ActualTimeOut datetime)

AS
BEGIN
UPDATE [StriveCarSalon].[tblJob] SET CheckOut=@CheckOut, ActualTimeOut=@ActualTimeOut WHERE JobId=@JobId
END