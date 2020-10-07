CREATE PROC [StriveCarSalon].[uspDeleteSalesItemById] --38, 
(@JobItemId int, @IsJobItem bit = 1)
AS
BEGIN
 
 If(@IsJobItem = 1)
Update StriveCarSalon.tblJobItem set  IsDeleted =1 where JobItemId=@JobItemId
ELSE
Update StriveCarSalon.tblJobProductItem set  IsDeleted =1 where JobProductItemId=@JobItemId

End
