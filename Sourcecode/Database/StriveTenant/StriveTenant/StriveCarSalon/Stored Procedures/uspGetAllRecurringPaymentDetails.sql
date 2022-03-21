
--use [StriveTenant_UAT_QA(Reverted)]
-- =============================================
-- Author:		Vetriselvi
-- Create date: 22-02-2022
-- Description:	To get Recurring Payment Details
--  --
/*

 --[uspGetAllRecurringPaymentDetails] 2,0,'2022-03-04'  

*/
-- =============================================
----------History------------
-- =============================================
--  06-Mar-2021 - Vetriselvi - Added LastPaymentDate condition
-- =============================================
  
CREATE PROCEDURE [StriveCarSalon].[uspGetAllRecurringPaymentDetails]  
@LocationId INT,  
@FailedAttempts INT,  
@date date  
AS  
BEGIN  
 SELECT   cvmd.ExpiryDate,  
    cvmd.ProfileId,  
    cvmd.AccountId,  
    cvmd.TotalPrice AS Amount,  
    ClientMembershipId,  
    cvmd.LastPaymentDate  
 FROM tblClientVehicleMembershipDetails cvmd  
 JOIN tblClientVehicle cv ON cv.VehicleId = cvmd.ClientVehicleId  
 JOIN tblClient c ON c.ClientId = cv.ClientId  
 WHERE ISNULL(cvmd.FailedAttempts,0) = @FailedAttempts  
   AND ProfileId is not null and AccountId is not null  
   AND cvmd.EndDate IS NULL  
   AND c.LocationId = @LocationId  
   AND (CAST(cvmd.LastPaymentDate AS DATE) < CAST(@date AS DATE) or cvmd.LastPaymentDate is null)  
   
END  
