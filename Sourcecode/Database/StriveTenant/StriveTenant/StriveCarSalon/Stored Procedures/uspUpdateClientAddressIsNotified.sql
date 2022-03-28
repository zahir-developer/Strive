CREATE PROCEDURE [StriveCarSalon].[uspUpdateClientAddressIsNotified]  
@ClientAddressId int,  
@IsNotified bit
as  
Begin  
  
Update tblClientAddress set IsNotified = @IsNotified where ClientAddressId = @ClientAddressId 
  
End