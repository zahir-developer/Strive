
CREATE PROCEDURE [StriveCarSalon].[uspGetItemListByTicketNumber] --'442491'
@TicketNumber varchar(10)
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
	tbljb.TicketNumber =  @TicketNumber  
AND ISNULL(tbljbI.IsDeleted,0)=0 
AND ISNULL(tbljbI.IsActive,1)=1 
AND ISNULL(tbljb.IsDeleted,0)=0 
AND ISNULL(tbljb.IsActive,1)=1

-- Product List
DROP TABLE IF EXISTS #JobProductList

SELECT 
	tbljb.JobId,
	tbljb.TicketNumber,
	tblCV.Id AS ProductId,
	tblp.ProductName,
	tblCV.CodeValue As ProductTypeName,
	tblp.ProductType as ProductType,
	ISNULL(tbljbP.Price,0) Price,
	ISNULL(tbljbP.Quantity,0) Quantity,
	tbljbP.JobProductItemId,
	tblcv.codevalue AS ServiceType,
	(ISNULL(tblp.TaxAmount,0)* ISNULL(tbljbP.Quantity,0)) AS TaxAmount,
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
	tbljb.TicketNumber =  @TicketNumber  
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

UPDATE #JobTotal SET Total= (ISNULL(JT.Total,0)+ISNULL(JPT.Total,0)),TaxAmount=(ISNULL(JT.TaxAmount,0)+ISNULL(JPT.TaxAmount,0)) FROM #JobTotal JT LEFT JOIN #JobProductTotal JPT ON JT.TicketNumber=JPT.TicketNumber

DROP TABLE IF EXISTS #Payment_Summary

SELECT   
	tbljob.TicketNumber,
	SUM(ISNULL(tbljp.Amount,0)) AS Cash,
	SUM(ISNULL(tblJPCC.Amount,0)) AS Credit,
	SUM(ISNULL(tblGCH.TransactionAmount,0)) AS GiftCard,
	SUM(ISNULL(tbljp.Cashback,0)) AS CashBack,
	SUM(ISNULL(tbljpd.Amount,0)) AS Discount 
INTO
	#Payment_Summary
FROM
	tblJob tbljob 
LEFT JOIN 
	tblJobPayment tbljp 
ON		tbljob.JobId = tbljp.JobId AND ISNULL(tbljp.IsRollBack,0)=0 
LEFT JOIN 
	tblJobPaymentDiscount tbljpd 
ON		tbljp.JobPaymentId = tbljpd.JobPaymentId AND ISNULL(tbljpd.IsDeleted,0)=0 
LEFT JOIN 
	tblJobPaymentCreditCard tblJPCC 
ON		tbljp.JobPaymentId = tblJPCC.JobPaymentId AND ISNULL(tblJPCC.IsDeleted,0)=0 
LEFT JOIN
	tblGiftCardHistory tblGCH
ON		tblGCH.JobPaymentId=tbljp.JobPaymentId AND ISNULL(tblGCH.IsDeleted,0)=0 
WHERE 
	tbljob.TicketNumber = @TicketNumber
AND	ISNULL(tbljob.IsDeleted,0)=0 
AND ISNULL(tbljob.IsActive,1)=1 
AND	ISNULL(tbljp.IsDeleted,0)=0 
AND ISNULL(tbljp.IsActive,1)=1 
--AND	ISNULL(tblJPCC.IsDeleted,0)=0 
--AND ISNULL(tblJPCC.IsActive,1)=1 
--AND	ISNULL(tblGCH.IsDeleted,0)=0 
--AND ISNULL(tblGCH.IsActive,1)=1 
AND	ISNULL(tbljpd.IsDeleted,0)=0 
AND ISNULL(tbljpd.IsActive,1)=1 
GROUP BY tbljob.TicketNumber

-- Summary List
SELECT * FROM #JobItemList
SELECT * FROM #JobProductList

SELECT 
	PS.TicketNumber,
	JT.Total, 
	JT.TaxAmount,
	(JT.Total+JT.TaxAmount) AS GrandTotal,
	Cash,
	Credit,
	GiftCard,
	Discount,
	(Cash+Credit-GiftCard) AS TotalPaid,
	((JT.Total+JT.TaxAmount) - (Cash+Credit+Discount-GiftCard)) AS BalanceDue,
	CashBack
FROM 
	#Payment_Summary PS 
LEFT JOIN 
	#JobTotal JT 
ON JT.TicketNumber=PS.TicketNumber

--Payment, IsProcessed, IsRollBack
Select job.JobId, ISNULL(tbljp.JobPaymentId, 0) as JobPaymentId, ISNULL(tbljp.IsProcessed, 0) as IsProcessed, ISNULL(tbljp.IsRollBack, 0) as IsRollBack
from tblJob job 
LEFT JOIN tblJobPayment tbljp on job.JobId = tbljp.JobId
WHERE job.TicketNumber = @TicketNumber 

END
GO

