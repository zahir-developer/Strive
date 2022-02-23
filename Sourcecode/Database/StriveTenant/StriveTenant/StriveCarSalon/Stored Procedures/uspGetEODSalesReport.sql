-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- [StriveCarSalon].[uspGetEODSalesReport] 1,'2021-10-27','2021-10-27'
-- =============================================
-------------history-----------------
-- =============================================
-- 1  Vetriselvi 2021-06-13  -Sales, Grand total and total paid - not showing right values not calculating/displaying credit card amount , cash back amount and Tax shows wrongly
-- 1  Vetriselvi 2021-07-14  -Sales tab not showing wrong data - not showing any sales now
							--Giftcard details shows wrongly
							--Charge card showing zero
--   Vetriselvi 2021-07-23  -Optimized the SP 
							-- Fixed Tax and cash ayment calculation
--   Vetriselvi 2021-07-27  -Total sales should return total payed amount
--   Vetriselvi 2021-10-06  -Fixed Cashback duplicate issue
--   Vetriselvi 2021-10-07  - Fixed Cashback duplicate issue
--   Vetriselvi 2021-10-27  - 960 Changed the date filter from job date to payment date
--   Juki		2021-11-18	- Fixed duplication of cash, GC, Credicard amount when doing mixed payment
--   JUki		2022-02-16  - Added total tip amount
-- =============================================

CREATE PROCEDURE [StriveCarSalon].[uspGetEODSalesReport] 
	@LocationId INT,
	@FromDate datetime = NULL,
	@EndDate datetime = NULL
AS
BEGIN
	--Select * from StriveCarSalon.tblLocation 

DECLARE @CashBack DECIMAL(9,2)
Declare @PaymentStatusSuccess INT = (Select valueid from GetTable('PaymentStatus') where valuedesc='Success')


DROP TABLE IF EXISTS #tblJobPayment
Select JobPaymentId, PaymentStatus,Cashback INTO #tblJobPayment from tblJobPayment jp
WHERE jp.IsActive=1 
and ISNULL(jp.IsDeleted,0)=0 
and ISNULL(jp.IsRollBack,0) ! = 1
AND (CAST(jp.CreatedDate AS DATE) >=@FromDate AND CAST(jp.CreatedDate AS DATE) <=@EndDate) 
--and jp.JobPaymentId in (select JobPaymentId from #tblJob)

DROP TABLE IF EXISTS #tblJob
Select jobId, LocationId, JobType, JobStatus, JobPaymentId, VehicleId INTO #tblJob from tblJob tblj
where tblj.JobPaymentId in (select JobPaymentId from #tblJobPayment)
AND ISNULL(tblj.IsDeleted,0)=0 AND tblj.LocationId = @LocationId

DROP TABLE IF EXISTS #tblJobItem
Select tblji.JobId, tblji.JobItemId, tblji.Price, ServiceId,Quantity INTO #tblJobItem from tblJobItem tblji 
INNER JOIN #tblJob tblj on tblj.JobId = tblji.JobId
WHERE tblji.IsActive=1 AND ISNULL(tblji.IsDeleted,0)=0


DROP TABLE IF EXISTS #tblService
Select ServiceId, ServiceType INTO #tblService from tblService tbls
WHERE tbls.IsActive=1 and ISNULL(tbls.IsDeleted,0)=0 



select @CashBack = SUM(ISNULL(Cashback,0)) 
from #tblJobPayment

DROP TABLE IF EXISTS #JobItemList

SELECT   
 tbljb.JobId,  
 tbljb.LocationId,  
 --tblsr.ServiceId,  
 --tblsr.ServiceName,  
 ISNULL(tbljbI.Price,0) Price,
 --ISNULL(tbljbI.Quantity,0) Quantity,  
 tbljbI.JobItemId,  
 tblcv.codevalue AS ServiceType,  
 0 AS TaxAmount,
 (ISNULL(tbljbI.Price,0) * ISNULL(tbljbI.Quantity,0)) AS Cost  
INTO  
 #JobItemList  
FROM   
 #tblJob tbljb   
LEFT JOIN   
 #tblJobItem tbljbI   
ON  tbljb.JobId = tbljbI.JobId    
LEFT JOIN   
 #tblService tblsr   
ON  tbljbI.ServiceId = tblsr.ServiceId  
LEFT JOIN   
 tblCodeValue tblcv  
ON  tblcv.id=tblsr.ServiceType  
 JOIN 
	#tblJobPayment tbljp 
ON		tbljb.JobPaymentId = tbljp.JobPaymentId 
AND tbljp.PaymentStatus = @PaymentStatusSuccess
WHERE (tbljb.LocationId = @LocationId )  


-- Product List
DROP TABLE IF EXISTS #JobProductList

SELECT   
 tbljb.JobId,  
 tbljb.LocationId,  
 tblp.ProductId AS ProductId,  
 tblp.ProductName,  
 tblCV.CodeValue As ProductTypeName,  
 tblp.ProductType as ProductType,  
 ISNULL(tbljbP.Price,0) Price,  
 ISNULL(tbljbP.Quantity,0) Quantity,  
 tbljbP.JobProductItemId,  
 tblcv.codevalue AS ServiceType,  
 (ISNULL(tbljbP.Price,0) * ISNULL(tbljbP.Quantity,0))*((ISNULL(tblp.TaxAmount,0)/100)) as TaxAmount,
-- ((ISNULL(tbljbP.Quantity,0)) * ISNULL(tblp.TaxAmount,0) /100) AS TaxAmount,  
 (ISNULL(tbljbP.Price,0) * ISNULL(tbljbP.Quantity,0)) AS Cost  
INTO  
 #JobProductList  
FROM   
 #tblJob tbljb   
LEFT JOIN   
 tblJobProductItem tbljbP   
ON  tbljb.JobId = tbljbP.JobId    
INNER JOIN  
 tblProduct tblp  
ON  tblp.ProductId=tbljbP.ProductId  
LEFT JOIN   
 tblCodeValue tblCV   
ON  tblP.ProductType = tblcv.id  
JOIN 
	#tblJobPayment tbljp 
ON		tbljb.JobPaymentId = tbljp.JobPaymentId 
AND tbljp.PaymentStatus = @PaymentStatusSuccess
WHERE  (tbljb.LocationId = @LocationId )  
AND ISNULL(tbljbP.IsDeleted,0)=0   
AND ISNULL(tbljbP.IsActive,1)=1   


--Item Total

DECLARE @JobServiceCost decimal(9,2) = (SELECT   
SUM(JIL.Cost) Total  
FROM #JobItemList JIL)  

DECLARE @JobProductCost decimal(9,2) = (SELECT   
SUM(JIL.Cost) Total  
FROM #JobProductList JIL)  
DECLARE @JobProductTaxCost decimal(9,2) = (SELECT   
SUM(JIL.TaxAmount) TaxAmount   
FROM #JobProductList JIL) 

DECLARE @Total decimal(9,2)= (ISNULL(@JobServiceCost,0)+ISNULL(@JobProductCost,0))  

DROP TABLE IF EXISTS #PaymentType

Select Cv.id,CodeValue,Category
into #PaymentType
from tblcodevalue cv
join tblCodeCategory CC
ON cc.id=cv.CategoryId
Where CC.[Category]='PaymentType'

DROP TABLE IF EXISTS #Payment_Summary

SELECT 
	LocationId,
	SUM(Cash) AS Cash,
	SUM(Credit) AS Credit,
	SUM(GiftCard)AS GiftCard,
	SUM(Discount) AS Discount,
	SUM(Account) AS Account,
	SUM(Membership) AS Membership,
	SUM(Tips) AS Tips
INTO
	#Payment_Summary
FROM 
(SELECT   
	tbljob.LocationId,
	case when tblpt.CodeValue = 'Cash' then SUM(ISNULL(tbljpd.Amount,0)) ELSE 0 end AS Cash,
	case when tblpt.CodeValue = 'Card' then SUM(ISNULL(tbljpd.Amount,0)) ELSE 0 end AS Credit,
	case when tblpt.CodeValue = 'GiftCard' then SUM(ISNULL(tbljpd.Amount,0)) ELSE 0 end AS GiftCard,
	case when tblpt.CodeValue = 'Discount' then SUM(ISNULL(tbljpd.Amount,0)) ELSE 0 end AS Discount ,
	case when tblpt.CodeValue = 'Account' then SUM(ISNULL(tbljpd.Amount,0)) ELSE 0 end AS Account ,
	case when tblpt.CodeValue = 'Membership' then SUM(ISNULL(tbljpd.Amount,0)) ELSE 0 end AS Membership,
	case when tblpt.CodeValue = 'Tips' then SUM(ISNULL(tbljpd.Amount,0)) ELSE 0 end AS Tips
FROM
	(SELECT DISTINCT JobPaymentID, LocationID from #tbljob) tbljob
	--#tblJob tbljob 
JOIN
	#tblJobPayment tbljp 
ON		tbljob.JobPaymentId = tbljp.JobPaymentId
JOIN 
	tblJobPaymentDetail tbljpd 
ON		tbljp.JobPaymentId = tbljpd.JobPaymentId 
AND		ISNULL(tbljpd.IsDeleted,0)=0 
JOIN 
	#PaymentType tblpt
on		tbljpd.PaymentType = tblpt.id
WHERE ISNULL(tbljpd.IsDeleted,0)=0 
AND ISNULL(tbljpd.IsActive,1)=1 
AND tbljp.PaymentStatus = @PaymentStatusSuccess
Group by tbljob.locationId, tblpt.CodeValue
) sub
GROUP BY locationId

-- Summary List

SELECT 
	LocationId,
	@Total AS Total,   
	@JobProductTaxCost as TaxAmount,  
	(@Total+@JobProductTaxCost) AS GrandTotal,  
	Cash,
	Credit,
	GiftCard,
	Discount,
	(Account+Membership) AS Account,
	(Account+Membership+Cash+Credit+GiftCard) AS TotalPaid,
	((@Total+@JobProductTaxCost) - (Account+Membership+Cash+Credit+Discount+GiftCard)) AS BalanceDue,
	@CashBack AS CashBack,
	Tips
FROM #Payment_Summary 

END