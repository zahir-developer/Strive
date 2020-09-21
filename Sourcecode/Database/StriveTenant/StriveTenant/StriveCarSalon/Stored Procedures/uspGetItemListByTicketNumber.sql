CREATE    PROCEDURE [StriveCarSalon].[uspGetItemListByTicketNumber]
@TicketNumber varchar(10)
AS 
--DECLARE @TicketNumber varchar(10)='658'--'782436'

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

--Item Total
DROP TABLE IF EXISTS #JobTotal

SELECT 
	TicketNumber,
	SUM(Cost) Total,
	SUM(TaxAmount) TaxAmount 
INTO
	#JobTotal
FROM 
	#JobItemList
GROUP BY TicketNumber


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
ON		tbljob.JobId = tbljp.JobId
LEFT JOIN 
	tblJobPaymentDiscount tbljpd 
ON		tbljp.JobPaymentId = tbljpd.JobPaymentId
LEFT JOIN 
	tblJobPaymentCreditCard tblJPCC 
ON		tbljp.JobPaymentId = tblJPCC.JobPaymentId
LEFT JOIN
	tblGiftCardHistory tblGCH
ON		tblGCH.JobPaymentId=tbljp.JobPaymentId
WHERE 
	tbljob.TicketNumber = @TicketNumber
AND	ISNULL(tbljob.IsDeleted,0)=0 
AND ISNULL(tbljob.IsActive,1)=1 
GROUP BY tbljob.TicketNumber

--SUM(JIL.Total) AS Total,
--SUM(ISNULL(JIL.TaxAmount,0)) AS Tax,
SELECT * FROM #JobItemList

SELECT 
	PS.TicketNumber,
	JT.Total, 
	JT.TaxAmount,
	(JT.Total+JT.TaxAmount) AS GrandTotal,
	Cash,
	Credit,
	GiftCard,
	Discount,
	(Cash+Credit+GiftCard) AS TotalPaid,
	((JT.Total+JT.TaxAmount) - (Cash+Credit+GiftCard-Discount+CashBack)) AS BalanceDue,
	CashBack
FROM 
	#Payment_Summary PS 
LEFT JOIN 
	#JobTotal JT 
ON JT.TicketNumber=PS.TicketNumber

END
