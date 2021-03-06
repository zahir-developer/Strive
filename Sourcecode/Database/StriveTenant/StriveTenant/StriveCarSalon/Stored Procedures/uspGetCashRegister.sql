---------------------History--------------------  
-- =============================================  
-- 19-05-2021, Shalini - Added totalamount column 
--03-06-2021, Shalini -Added credit card amount.  
------------------------------------------------  
--[StriveCarSalon].[uspGetCashRegister] 1,'CLOSEIN','2021-06-03'  
------------------------------------------------  
  
CREATE PROCEDURE [StriveCarSalon].[uspGetCashRegister]   
(  
@LocationId int,  
@CashRegisterType varchar(10),   
@CashRegisterDate varchar(10)  
)  
  
AS  
BEGIN  
  
DECLARE @CashRegisterTypeId INT = (  
Select CV.id from tblCodeCategory CC  
JOIN tblCodeValue CV on CV.CategoryId = CC.id  
WHERE CV.CodeValue = @CashRegisterType)  
  
DECLARE @DrawerID INT;  
SELECT @DrawerID = DrawerId FROM [tblDrawer] WHERE LocationId=@LocationId  
  
Select  
CR.CashRegisterId ,  
CR.CashRegisterType,  
CR.LocationId ,  
CR.DrawerId,  
CR.CashRegisterDate ,  
CR.StoreTimeIn,  
CR.StoreTimeOut,  
CR.StoreOpenCloseStatus ,  
CR.Tips,  
CR.TotalAmount  
FROM   
tblCashRegister CR   
WHERE  
CR.LocationId = @LocationId AND  
CR.CashRegisterType = @CashRegisterTypeId AND  
CR.CashRegisterDate = @CashRegisterDate AND  
--CR.DrawerId = @DrawerID AND  
isnull(CR.isDeleted,0) = 0    
  
SELECT   
CRC.CashRegCoinId,  
CRC.CashRegisterId,  
CRC.Pennies ,  
CRC.Nickels ,  
CRC.Dimes ,  
CRC.Quarters ,  
CRC.HalfDollars  
FROM  
[tblCashRegisterCoins] CRC  
INNER JOIN  
tblCashRegister CR   
ON CRC.cashRegisterId = CR.CashRegisterId  
WHERE  
CR.LocationId = @LocationId AND  
CR.CashRegisterType = @CashRegisterTypeId AND  
CR.CashRegisterDate = @CashRegisterDate AND  
--CR.DrawerId = @DrawerID AND  
isnull(CR.isDeleted,0) = 0    
  
  
SELECT  
CRR.CashRegRollId,  
CRR.CashRegisterId,  
CRR.Pennies,  
CRR.Nickels,  
CRR.Dimes,  
CRR.Quarters,  
CRR.HalfDollars  
FROM  
[tblCashRegisterRolls] CRR  
INNER JOIN  
tblCashRegister CR   
ON CRR.cashRegisterId = CR.CashRegisterId  
WHERE  
CR.LocationId = @LocationId AND  
CR.CashRegisterType = @CashRegisterTypeId AND  
CR.CashRegisterDate = @CashRegisterDate AND  
--CR.DrawerId = @DrawerID AND  
isnull(CR.isDeleted,0) = 0    
  
SELECT  
CRB.CashRegBillId,  
CRB.CashRegisterId,  
CRB.[s1],  
CRB.[s5],  
CRB.[s10],  
CRB.[s20],  
CRB.[s50],  
CRB.[s100]  
FROM  
[tblCashRegisterBills] CRB  
INNER JOIN  
tblCashRegister CR   
ON CRB.cashRegisterId = CR.CashRegisterId  
WHERE  
CR.LocationId = @LocationId AND  
CR.CashRegisterType = @CashRegisterTypeId AND  
CR.CashRegisterDate = @CashRegisterDate AND  
--CR.DrawerId = @DrawerID AND  
isnull(CR.isDeleted,0) = 0    
  
  
SELECT  
CRO.CashRegOtherId,  
CRO.CashRegisterId,  
CRO.CreditCard1,  
CRO.CreditCard2,  
CRO.CreditCard3,  
CRO.Checks,  
CRO.Payouts  
FROM  
[tblCashRegisterOthers] CRO  
INNER JOIN  
tblCashRegister CR   
ON CRO.cashRegisterId = CR.CashRegisterId  
WHERE  
CR.LocationId = @LocationId AND  
CR.CashRegisterType = @CashRegisterTypeId AND  
CR.CashRegisterDate = @CashRegisterDate AND  
--CR.DrawerId = @DrawerID AND  
isnull(CR.isDeleted,0) = 0    
  
SELECT   
WP.Weather,  
WP.RainProbability,  
WP.PredictedBusiness,  
WP.TargetBusiness  
FROM [tblWeatherPrediction] WP  
INNER JOIN  
tblCashRegister CR   
ON WP.LocationId=CR.LocationId  
WHERE  
WP.LocationId =@LocationId AND  
WP.CreatedDate=@CashRegisterDate  
  
Declare @WashId INT = (Select valueid from GetTable('JobType') where valuedesc='Wash')  
Declare @WashServiceId INT = (Select valueid from GetTable('ServiceType') where valuedesc='Wash Package')  
Declare @CompletedJobStatus INT = (Select valueid from GetTable('JobStatus') where valuedesc='Completed')  
SELECT   
 tbll.LocationId,tbll.LocationName,COUNT(*) WashCount  
 FROM tbljob tblj   
 INNER JOIN tblLocation tbll ON(tblj.LocationId = tbll.LocationId)  
 INNER JOIN tblJobItem tblji ON(tblj.JobId=tblji.JobId)  
 INNER JOIN tblService tbls ON(tblji.ServiceId = tbls.ServiceId)  
 WHERE tblj.JobType=@WashId  
 AND tbls.ServiceType=@WashServiceId  
 AND tblj.JobStatus=@CompletedJobStatus  
 AND tblj.LocationId=@LocationId  
 GROUP BY tbll.LocationId,tbll.LocationName   

 SELECT	
	SUM(Credit) AS CreditCardAmount

FROM 
(SELECT  
	 SUM(ISNULL(tbljpd.Amount,0)) AS Credit
FROM	tblJob tbljob 
LEFT JOIN 	tblJobPayment tbljp ON		tbljob.JobId = tbljp.JobId AND ISNULL(tbljp.IsRollBack,0)=0 
LEFT JOIN 	tblJobPaymentDetail tbljpd ON		tbljp.JobPaymentId = tbljpd.JobPaymentId 
AND		ISNULL(tbljpd.IsDeleted,0)=0 
LEFT JOIN GetTable('Paymenttype') gt on tbljpd.PaymentType = gt.valueid 

WHERE tbljob.jobdate =@CashRegisterDate and gt.Valuedesc='Card'
and ISNULL(tbljob.IsDeleted,0)=0 
AND ISNULL(tbljob.IsActive,1)=1 
AND	ISNULL(tbljp.IsDeleted,0)=0 
AND ISNULL(tbljp.IsActive,1)=1 
AND	ISNULL(tbljpd.IsDeleted,0)=0 
AND ISNULL(tbljpd.IsActive,1)=1 
Group by tbljob.TicketNumber
) sub
  
END

