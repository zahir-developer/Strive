cREATE PROCEDURE [StriveCarSalon].[uspUpdateEmployeeAddressIsNotified]  
@EmployeeAddressId int,  
@IsNotified bit
as  
Begin  
  
Update tblEmployeeAddress set IsNotified = @IsNotified where EmployeeAddressId = @EmployeeAddressId 
  
End