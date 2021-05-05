﻿

CREATE PROCEDURE [StriveCarSalon].[uspGetItemListByTicketNumber] -- [StriveCarSalon].[uspGetItemListByTicketNumber]'993311' '651284,537631,566450,118839,833659'
@TicketNumber varchar(max),
@LocationId INT = NULL
AS 
--DECLARE @TicketNumber varchar(10)='274997'--'782436'

BEGIN     
DROP TABLE IF EXISTS #JobItemList

SELECT 
	tbljb.JobId,
	tbljb.TicketNumber,
	tblsr.ServiceId,
	tblsr.ServiceName,
	ISNULL(tbljbI.Price,0) Price,
	ISNULL(tbljbI.Quantity,0) Quantity,
	tbljbI.JobItemId,
	tblcv.codevalue AS ServiceType,
	0 AS TaxAmount,
	(ISNULL(tbljbI.Price,0) * ISNULL(tbljbI.Quantity,0)) AS Cost
INTO
	#JobItemList
FROM 
	tblJob tbljb 
LEFT JOIN 
	tblJobItem tbljbI 
ON		tbljb.JobId = tbljbI.JobId  
LEFT JOIN 
	tblService tblsr 
ON		tbljbI.ServiceId = tblsr.ServiceId
LEFT JOIN 
	tblCodeValue tblcv
ON		tblcv.id=tblsr.ServiceType
WHERE 
	 ','+@TicketNumber+',' LIKE '%,'+CONVERT(VARCHAR(50),tbljb.TicketNumber)+',%' AND (tbljb.LocationId = @LocationId OR @LocationId IS NULL)
AND ISNULL(tbljbI.IsDeleted,0)=0 
AND ISNULL(tbljbI.IsActive,1)=1 
AND ISNULL(tbljb.IsDeleted,0)=0 
AND ISNULL(tbljb.IsActive,1)=1

-- Product List
DROP TABLE IF EXISTS #JobProductList

SELECT 
	tbljb.JobId,
	tbljb.TicketNumber,
	tblp.ProductId AS ProductId,
	tblp.ProductName,
	tblCV.CodeValue As ProductTypeName,
	tblp.ProductType as ProductType,
	ISNULL(tbljbP.Price,0) Price,
	ISNULL(tbljbP.Quantity,0) Quantity,
	tbljbP.JobProductItemId,
	tblcv.codevalue AS ServiceType,
	((ISNULL(tblp.TaxAmount,0) ) * ISNULL(tbljbP.Quantity,0)/100) AS TaxAmount,
	(ISNULL(tbljbP.Price,0) * ISNULL(tbljbP.Quantity,0)) AS Cost
INTO
	#JobProductList
FROM 
	tblJob tbljb 
LEFT JOIN 
	tblJobProductItem tbljbP 
ON		tbljb.JobId = tbljbP.JobId  
INNER JOIN
	tblProduct tblp
ON		tblp.ProductId=tbljbP.ProductId
LEFT JOIN 
	tblCodeValue tblCV 
ON		tblP.ProductType = tblcv.id
WHERE 
	 ','+@TicketNumber+',' LIKE '%,'+CONVERT(VARCHAR(50),tbljb.TicketNumber)+',%' AND (tbljb.LocationId = @LocationId OR @LocationId IS NULL)
AND ISNULL(tbljbP.IsDeleted,0)=0 
AND ISNULL(tbljbP.IsActive,1)=1 
AND ISNULL(tbljb.IsDeleted,0)=0 
AND ISNULL(tbljb.IsActive,1)=1


--Item Total
DROP TABLE IF EXISTS #JobTotal

SELECT 
	JIL.TicketNumber,
	SUM(JIL.Cost) Total,
	SUM(JIL.TaxAmount) TaxAmount 
INTO
	#JobTotal
FROM 
	#JobItemList JIL
GROUP BY JIL.TicketNumber

--Total for given tickets
DROP TABLE IF EXISTS #JobTotalForGivenTickets

DECLARE @JobServiceCostForGivenTickets decimal(9,2) = (SELECT 
SUM(JIL.Cost) Total
FROM #JobItemList JIL)
DECLARE @JobTaxCostForGivenTickets decimal(9,2) = (SELECT 
SUM(JIL.TaxAmount) TaxAmount 
FROM #JobItemList JIL)
	
DROP TABLE IF EXISTS #JobProductTotal

SELECT 
	JPL.TicketNumber,
	SUM(JPL.Cost) Total,
	SUM(JPL.TaxAmount) TaxAmount 
INTO
	#JobProductTotal
FROM 
	#JobProductList JPL
GROUP BY JPL.TicketNumber

DECLARE @JobProductCostForGivenTickets decimal(9,2) = (SELECT 
SUM(JIL.Cost) Total
FROM #JobProductList JIL)
DECLARE @JobProductTaxCostForGivenTickets decimal(9,2) = (SELECT 
SUM(JIL.TaxAmount) TaxAmount 
FROM #JobProductList JIL)

DECLARE @Total decimal(9,2)= (ISNULL(@JobServiceCostForGivenTickets,0)+ISNULL(@JobProductCostForGivenTickets,0))
DECLARE @TaxAmount decimal(9,2) = (ISNULL(@JobTaxCostForGivenTickets,0)+ISNULL(@JobProductTaxCostForGivenTickets,0))

DROP TABLE IF EXISTS #PaymentType

Select Cv.id,CodeValue,Category
into #PaymentType
from tblcodevalue cv
join tblCodeCategory CC
ON cc.id=cv.CategoryId
Where CC.[Category]='PaymentType'

DROP TABLE IF EXISTS #Payment_Summary

SELECT 
	--TicketNumber,
	SUM(Cash) AS Cash,
	SUM(Credit) AS Credit,
	SUM(GiftCard)AS GiftCard,
	SUM(CashBack)AS CashBack,
	SUM(Discount) AS Discount,
	SUM(Account) AS Account,
	SUM(Membership) AS Membership,
	SUM(Tips)AS Tips
INTO
	#Payment_Summary
FROM 
(SELECT   
	tbljob.TicketNumber,
	case when tblpt.CodeValue = 'Cash' then SUM(ISNULL(tbljpd.Amount,0)) ELSE 0 end AS Cash,
	case when tblpt.CodeValue = 'Card' then SUM(ISNULL(tbljpd.Amount,0)) ELSE 0 end AS Credit,
	case when tblpt.CodeValue = 'GiftCard' then SUM(ISNULL(tbljpd.Amount,0)) ELSE 0 end AS GiftCard,
	case when tblpt.CodeValue = 'Tips' then SUM(ISNULL(tbljpd.Amount,0)) ELSE 0 end AS Tips,
	SUM(ISNULL(tbljp.Cashback,0)) AS CashBack,
	case when tblpt.CodeValue = 'Discount' then SUM(ISNULL(tbljpd.Amount,0)) ELSE 0 end AS Discount ,
	case when tblpt.CodeValue = 'Account' then SUM(ISNULL(tbljpd.Amount,0)) ELSE 0 end AS Account ,
	case when tblpt.CodeValue = 'Membership' then SUM(ISNULL(tbljpd.Amount,0)) ELSE 0 end AS Membership
FROM
	tblJob tbljob 
LEFT JOIN 
	tblJobPayment tbljp 
ON		tbljob.JobId = tbljp.JobId AND ISNULL(tbljp.IsRollBack,0)=0 
LEFT JOIN 
	tblJobPaymentDetail tbljpd 
ON		tbljp.JobPaymentId = tbljpd.JobPaymentId 
AND		ISNULL(tbljpd.IsDeleted,0)=0 
LEFT JOIN 
	#PaymentType tblpt
on		tbljpd.PaymentType = tblpt.id
WHERE 
	 ','+@TicketNumber+',' LIKE '%,'+CONVERT(VARCHAR(50),tbljob.TicketNumber)+',%'
AND	ISNULL(tbljob.IsDeleted,0)=0 
AND ISNULL(tbljob.IsActive,1)=1 
AND	ISNULL(tbljp.IsDeleted,0)=0 
AND ISNULL(tbljp.IsActive,1)=1 
AND	ISNULL(tbljpd.IsDeleted,0)=0 
AND ISNULL(tbljpd.IsActive,1)=1 
Group by tbljob.TicketNumber,tblpt.CodeValue
) sub
--GROUP BY TicketNumber

-- Summary List
SELECT * FROM #JobItemList ORDER BY JobId
SELECT * FROM #JobProductList ORDER BY JobId

SELECT DISTINCT
	--PS.TicketNumber,
	@Total AS Total, 
	@TaxAmount as TaxAmount,
	(@Total+@TaxAmount) AS GrandTotal,
	Cash,
	Credit,
	GiftCard,
	Discount,
	Tips,
	(Account+Membership) AS Account,
	(Account+Membership+Cash+Credit+GiftCard) AS TotalPaid,
	((@Total+@TaxAmount) - (Account+Membership+Cash+Credit+Discount+GiftCard)) AS BalanceDue,
	CashBack
FROM 
	#Payment_Summary 

--Payment, IsProcessed, IsRollBack
--Select  ISNULL(tbljp.JobPaymentId, 0) as JobPaymentId, ISNULL(tbljp.IsProcessed, 0) as IsProcessed, ISNULL(tbljp.IsRollBack, 0) as IsRollBack
--from tblJobPayment tbljp 
--LEFT JOIN tblJob job on job.JobId = tbljp.JobId
--WHERE  ','+@TicketNumber+',' LIKE '%,'+CONVERT(VARCHAR(50),job.TicketNumber)+',%'
--order by JobPaymentId desc

Select DISTINCT ISNULL(tbljp.JobPaymentId, 0) as JobPaymentId, ISNULL(tbljp.IsProcessed, 0) as IsProcessed, ISNULL(tbljp.IsRollBack, 0) as IsRollBack
from tblJob job 
LEFT JOIN tblJobPayment tbljp on job.JobId = tbljp.JobId
WHERE  ','+@TicketNumber+',' LIKE '%,'+CONVERT(VARCHAR(50),job.TicketNumber)+',%'
order by JobPaymentId desc


--JobId, TicketNumber
Select JobId,TicketNumber from tblJob where ','+@TicketNumber+',' LIKE '%,'+CONVERT(VARCHAR(50),TicketNumber)+',%' and IsActive=1 and ISNULL(IsDeleted,0)=0
END
GO

