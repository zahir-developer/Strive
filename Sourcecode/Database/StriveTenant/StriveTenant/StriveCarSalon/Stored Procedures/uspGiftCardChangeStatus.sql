CREATE procedure [StriveCarSalon].[uspGiftCardChangeStatus]
(@GiftCardId int,
 @IsActive int)
AS 
BEGIN
UPDATE [StriveCarSalon].[tblGiftCard] SET IsActive= @IsActive WHERE GiftCardId=@GiftCardId;
UPDATE [StriveCarSalon].[tblGiftCardHistory] SET IsActive= @IsActive WHERE GiftCardId=@GiftCardId;
END
