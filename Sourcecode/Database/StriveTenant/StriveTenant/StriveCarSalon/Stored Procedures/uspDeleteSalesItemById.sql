CREATE PROCEDURE [StriveCarSalon].[uspDeleteSalesItemById] 
(@JobItemId int, @IsJobItem bit = 1)
AS
BEGIN
 
 If(@IsJobItem = 1)
Update tblJobItem set  IsDeleted =1 where JobItemId=@JobItemId
ELSE   
Update tblJobProductItem set  IsDeleted =1 where JobProductItemId=@JobItemId

End
