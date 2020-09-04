
CREATE procedure [CON].[uspGiftCardChangeStatus]
(@GiftCardId int,
 @IsActive int)
AS 
BEGIN
UPDATE [CON].[tblGiftCard] SET IsActive= @IsActive WHERE GiftCardId=@GiftCardId;
UPDATE [CON].[tblGiftCardHistory] SET IsActive= @IsActive WHERE GiftCardId=@GiftCardId;
END