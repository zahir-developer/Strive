CREATE Procedure [StriveCarSalon].[uspGetGiftCardBalance]
@GiftCardNumber VARCHAR(20)
as begin

--DECLARE @GiftCardNumber VARCHAR(20) = '456578'

DECLARE @GiftCardId INT;
DECLARE @TotalAmount Decimal;
DECLARE @ExpireDate date;

Select top 1 @GiftCardId = GiftCardId, @TotalAmount = TotalAmount, @ExpireDate = ExpiryDate from StriveCarSalon.tblGiftCard where GiftCardCode = @GiftCardNumber

Select @TotalAmount - SUM(TransactionAmount) AS BalaceAmount, @ExpireDate AS ActiveDate from StriveCarSalon.tblGiftCardHistory
where GiftCardId=@GiftCardId

end
