CREATE Procedure [StriveCarSalon].[uspGetGiftCardBalance] 
@GiftCardNumber VARCHAR(20)
as begin

--DECLARE @GiftCardNumber VARCHAR(20) = '699080'

DECLARE @GiftCardId INT;
DECLARE @TotalAmount Decimal(18,2);
DECLARE @TransactionAmount Decimal(18,2) = 0;
DECLARE @ActivationDate date;

Select top 1 @GiftCardId = GiftCardId, @TotalAmount = TotalAmount, @ActivationDate = ActivationDate from StriveCarSalon.tblGiftCard where GiftCardCode = @GiftCardNumber

Select @TransactionAmount = SUM(TransactionAmount) from StriveCarSalon.tblGiftCardHistory
where GiftCardId=@GiftCardId

Select @GiftCardId as GiftCardId, ISNULL(@TransactionAmount,0) AS BalanceAmount, @ActivationDate AS ActivationDate


end
