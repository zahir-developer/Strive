CREATE PROC [StriveCarSalon].[uspDeleteSalesItemById]
(@JobId int)
AS
BEGIN
 
       Update StriveCarSalon.tblJob set  IsDeleted =1 where JobId=@JobId

End