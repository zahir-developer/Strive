CREATE PROC [StriveCarSalon].[uspDeleteSalesItemById]
(@JobItemId int)
AS
BEGIN
 
       Update StriveCarSalon.tblJobItem set  IsDeleted =1 where JobItemId=@JobItemId

End
