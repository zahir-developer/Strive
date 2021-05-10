
CREATE PROCEDURE [StriveCarSalon].[uspDeleteGiftCard]
(
@GiftCardId int
)
AS 
BEGIN


UPDATE [tblGiftCard] SET
  IsDeleted=1 
   WHERE GiftCardId=@GiftCardId	

END