
-- =============================================
-- Author:		Vetriselvi
-- Create date: 22-02-2022
-- Description:	update Payment Details 
-- 
-- =============================================
----------History------------
-- =============================================

-- =============================================   
  
CREATE PROCEDURE [StriveCarSalon].[uspUpdatePaymentDetails]
@date DATETIME null,      
@attempts INT,      
@ClientMembershipId INT      
AS      
BEGIN      
 UPDATE tblClientVehicleMembershipDetails      
 SET LastPaymentDate = @date,      
 FailedAttempts = @attempts      
 WHERE ClientMembershipId = @ClientMembershipId    
  
 INSERT INTO tblClientVehicleMembershipPaymentDetails  
 SELECT @ClientMembershipId,@attempts,GETDATE(),1,0,NULL,GETDATE(),NULL,GETDATE()  
END  