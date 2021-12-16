-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- [StriveCarSalon].[uspGetEODSalesReport] 0
/*
2021-06-03 - Vetriselvi Gift card value should show only todays gift card purchased or used amount ( showing yesterday's data)
Total paid Paid shows wrong amount value and if we change date it shows same value. please refer screenshots
*/
-- =============================================
CREATE PROCEDURE [StriveCarSalon].[uspGetEODSalesReport] 
	@LocationId INT,
	@FromDate datetime = NULL,
	@EndDate datetime = NULL
AS
BEGIN
	--Select * from StriveCarSalon.tblLocation 


Declare @PaymentStatusSuccess INT = (Select valueid from GetTable('PaymentStatus') where valuedesc='Success')

DROP TABLE IF EXISTS #JobItemList

SELECT 
	--tbljb.JobId,
	tbljb.LocationId,
	--tblsr.ServiceId,
	--tblsr.ServiceName,
	ISNULL(tbljbI.Price,0) Price,
	ISNULL(tbljbI.Quantity,0) Quantity,
	--tbljbI.JobItemId,
	--tblcv.codevalue AS ServiceType,
	0 AS TaxAmount,
	(ISNULL(tbljbI.Price,0) * ISNULL(tbljbI.Quantity,0)) AS Cost
INTO
	#JobItemList
FROM 
	tblJob tbljb 
LEFT JOIN 
	tblJobItem tbljbI 
ON		tbljb.JobId = tbljbI.JobId  

WHERE 
	tbljb.LocationId =  @locationId  and (tbljb.JobDate between @FromDate and @EndDate )
AND ISNULL(tbljbI.IsDeleted,0)=0 
AND ISNULL(tbljbI.IsActive,1)=1 
AND ISNULL(tbljb.IsDeleted,0)=0 
AND ISNULL(tbljb.IsActive,1)=1


-- Product List
DROP TABLE IF EXISTS #JobProductList

SELECT 
	tbljb.LocationId,
	--tbljb.JobId,
	--tbljb.TicketNumber,
	--tblCV.Id AS ProductId,
	--tblp.ProductName,
	--tblCV.CodeValue As ProductTypeName,
	--tblp.ProductType as ProductType,
	ISNULL(tbljbP.Price,0) Price,
	ISNULL(tbljbP.Quantity,0) Quantity,
	--tbljbP.JobProductItemId,
	--tblcv.codevalue AS ServiceType,
	(ISNULL(tblp.TaxAmount,0) * ISNULL(tbljbP.Quantity,0)) AS TaxAmount,
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

WHERE 
tbljb.LocationId = @locationId  
AND 
ISNULL(tbljbP.IsDeleted,0)=0 
AND ISNULL(tbljbP.IsActive,1)=1 
AND ISNULL(tbljb.IsDeleted,0)=0 
AND ISNULL(tbljb.IsActive,1)=1


--Item Total
DROP TABLE IF EXISTS #JobTotal

SELECT 
	JIL.LocationId,
	SUM(JIL.Cost) Total,
	SUM(JIL.TaxAmount) TaxAmount 
INTO
	#JobTotal
FROM 
	#JobItemList JIL
GROUP BY JIL.LocationId

DROP TABLE IF EXISTS #JobProductTotal

SELECT 
	
	JPL.LocationId,
	SUM(JPL.Cost) Total,
	SUM(JPL.TaxAmount) TaxAmount 
INTO
	#JobProductTotal
FROM 
	#JobProductList JPL
GROUP BY JPL.LocationId

UPDATE #JobTotal SET Total = (ISNULL(JT.Total,0)+ISNULL(JPT.Total,0)),TaxAmount=(ISNULL(JT.TaxAmount,0)+ISNULL(JPT.TaxAmount,0)) FROM #JobTotal JT LEFT JOIN #JobProductTotal JPT ON JT.locationId=JPT.locationId

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
	SUM(CashBack)AS CashBack,
	SUM(Discount) AS Discount,
	SUM(Account) AS Account,
	SUM(Membership) AS Membership
INTO
	#Payment_Summary
FROM 
(SELECT   
	tbljob.LocationId,
	case when tblpt.CodeValue = 'Cash' then SUM(ISNULL(tbljpd.Amount,0)) ELSE 0 end AS Cash,
	case when tblpt.CodeValue = 'Credit' then SUM(ISNULL(tbljpd.Amount,0)) ELSE 0 end AS Credit,
	case when tblpt.CodeValue = 'GiftCard' then SUM(ISNULL(tbljpd.Amount,0)) ELSE 0 end AS GiftCard,
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
tbljob.LocationId = @locationId
AND	
ISNULL(tbljob.IsDeleted,0)=0 
AND ISNULL(tbljob.IsActive,1)=1 
AND	ISNULL(tbljp.IsDeleted,0)=0 
AND ISNULL(tbljp.IsActive,1)=1 
AND	ISNULL(tbljpd.IsDeleted,0)=0 
AND ISNULL(tbljpd.IsActive,1)=1 
AND tbljp.PaymentStatus = @PaymentStatusSuccess
AND (CONVERT(date, tbljp.CreatedDate)  between @FromDate and @EndDate)	
Group by tbljob.locationId, tblpt.CodeValue
) sub
GROUP BY locationId

-- Summary List
--SELECT * FROM #JobItemList
--SELECT * FROM #JobProductList

SELECT 
	PS.LocationId,
	JT.Total, 
	JT.TaxAmount,
	(JT.Total+JT.TaxAmount) AS GrandTotal,
	Cash,
	Credit,
	GiftCard,
	Discount,
	(Account+Membership) AS Account,
	(Account+Membership+Cash+Credit+GiftCard) AS TotalPaid,
	((JT.Total+JT.TaxAmount) - (Account+Membership+Cash+Credit+Discount+GiftCard)) AS BalanceDue,
	CashBack
FROM 
	#Payment_Summary PS 
LEFT JOIN 
	#JobTotal JT 
ON JT.LocationId=PS.locationId
END