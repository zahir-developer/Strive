﻿CREATE PROCEDURE [StriveCarSalon].[uspGetHourlyWashSalesReport_Old] 
@locationId INT , @fromDate Date, @endDate Date
AS
-- =============================================
-- Author:		Zahir Hussain M
-- Create date: 01-Dec-2020
-- Description:	Returns the Hourly wash report data and Sales data. 
-- EXEC StriveCarSalon.uspGetHourlyWashSalesReport 1, '2021-07-04', '2021-07-11' 
-- =============================================
-- =============================================
-- History
-- =============================================
-- 07-Jul-2021 - Vetriselvi - Included only wash sales in total amount calculation
-- 12-Jul-2021 - Vetriselvi - Included job date in where condition 
-- 12-Jul-2021 - Vetriselvi - Rename TotalPaid to Total
-- 21-Jul-2021 - Vetriselvi - Remove duplicate date in sales calculation
-- =============================================
BEGIN

--Sales Report
Declare @PaymentStatusSuccess INT = (Select valueid from GetTable('PaymentStatus') where valuedesc='Success')

DROP TABLE IF EXISTS #JobItemList


DROP TABLE IF EXISTS #tblJob

Select jobId, JobPaymentId, JobDate, locationId into #tblJob from tblJob
INNER JOIN GetTable('JobType') JT on JT.valueid = JobType and JT.valuedesc = 'Wash'
where JobDate between @fromDate and @endDate and LocationId = @locationId AND ISNULL(IsDeleted,0)=0 
AND ISNULL(IsActive,1)=1

SELECT 
	tbljb.LocationId,
	tbljp.createdDate,
	ISNULL(tbljbI.Price,0) Price,
	ISNULL(tbljbI.Quantity,0) Quantity,
	0 AS TaxAmount,
	(ISNULL(tbljbI.Price,0) * ISNULL(tbljbI.Quantity,0)) AS Cost
INTO
	#JobItemList
FROM 
	#tblJob tbljb 
LEFT JOIN 
	tblJobItem tbljbI 
ON		tbljb.JobId = tbljbI.JobId  
INNER JOIN tblJobPayment tbljp on tbljp.JobPaymentId = tbljb.JobPaymentId
--INNER join GetTable('JobStatus') GT on GT.valueId = tbljb.JobStatus and GT.valuedesc = 'Completed'
INNER JOIN tblService tbls on tbls.serviceId = tbljbI.serviceId 
INNER join GetTable('ServiceType') st on st.valueId = tbls.ServiceType and st.valuedesc = 'Wash Package'
WHERE 
(tbljb.LocationId =  @locationId) AND
ISNULL(tbljbI.IsDeleted,0)=0 
AND ISNULL(tbljbI.IsActive,1)=1 

AND ISNULL(tbljp.IsRollBack,0)!=1
AND tbljp.PaymentStatus = @PaymentStatusSuccess AND tbljp.IsRollBack = 0
order by tbljb.JobId

-- Product List
DROP TABLE IF EXISTS #JobProductList

SELECT
	tbljb.LocationId,
	tbljp.CreatedDate,
	ISNULL(tbljbP.Price,0) Price,
	ISNULL(tbljbP.Quantity,0) Quantity,
	(ISNULL(tblp.TaxAmount,0) * ISNULL(tbljbP.Quantity,0)) AS TaxAmount,
	(ISNULL(tbljbP.Price,0) * ISNULL(tbljbP.Quantity,0)) AS Cost
INTO
	#JobProductList
FROM #tblJob tbljb 
LEFT JOIN tblJobProductItem tbljbP 
ON		tbljb.JobId = tbljbP.JobId  
INNER JOIN
	tblProduct tblp
ON		tblp.ProductId=tbljbP.ProductId
INNER JOIN
	tblJobPayment tbljp
ON		tbljp.JobPaymentId = tbljb.JobPaymentId

WHERE 
tbljb.LocationId = @locationId AND
tbljp.createdDate between @fromDate and @endDate
AND 
ISNULL(tbljbP.IsDeleted,0)=0 
AND ISNULL(tbljbP.IsActive,1)=1 


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
GROUP BY JIL.LocationId,CAST(JIL.CreatedDate AS DATE)

DROP TABLE IF EXISTS #JobProductTotal

SELECT 
	JPL.LocationId,
	JPL.CreatedDate,
	SUM(JPL.Cost) Total,
	SUM(JPL.TaxAmount) TaxAmount 
INTO
	#JobProductTotal
FROM 
	#JobProductList JPL
GROUP BY JPL.LocationId,jpl.CreatedDate

UPDATE #JobTotal SET Total = (ISNULL(JT.Total,0)+ISNULL(JPT.Total,0)),TaxAmount=(ISNULL(JT.TaxAmount,0)+ISNULL(JPT.TaxAmount,0)) FROM #JobTotal JT LEFT JOIN #JobProductTotal JPT ON JT.locationId=JPT.locationId

DROP TABLE IF EXISTS #PaymentType

Select Cv.id,CodeValue,Category
into #PaymentType
from tblcodevalue cv
join tblCodeCategory CC
ON cc.id=cv.CategoryId
Where CC.[Category]='PaymentType'

DROP TABLE IF EXISTS #Payment_Summary

DROP TABLE IF EXISTS #PaymentSalesByType

SELECT   
    tbljob.JobDate,
	tbljob.locationId,
	case when tblpt.CodeValue = 'Cash' then SUM(ISNULL(tbljpd.Amount,0)) ELSE 0 end AS Cash,
	case when tblpt.CodeValue = 'Card' then SUM(ISNULL(tbljpd.Amount,0)) ELSE 0 end AS Credit,
	case when tblpt.CodeValue = 'Tips' then SUM(ISNULL(tbljpd.Amount,0)) ELSE 0 end AS Tips,
	case when tblpt.CodeValue = 'GiftCard' then SUM(ISNULL(tbljpd.Amount,0)) ELSE 0 end AS GiftCard,
	SUM(ISNULL(tbljp.Cashback,0)) AS CashBack,
	case when tblpt.CodeValue = 'Discount' then SUM(ISNULL(tbljpd.Amount,0)) ELSE 0 end AS Discount ,
	case when tblpt.CodeValue = 'Account' then SUM(ISNULL(tbljpd.Amount,0)) ELSE 0 end AS Account ,
	case when tblpt.CodeValue = 'Membership' then SUM(ISNULL(tbljpd.Amount,0)) ELSE 0 end AS Membership
	INTO #PaymentSalesByType
FROM
	#tblJob tbljob 
LEFT JOIN 
	tblJobPayment tbljp 
ON		tbljob.JobPaymentId = tbljp.JobPaymentId AND ISNULL(tbljp.IsRollBack,0)=0 
LEFT JOIN 
	tblJobPaymentDetail tbljpd 
ON		tbljp.JobPaymentId = tbljpd.JobPaymentId 
AND		ISNULL(tbljpd.IsDeleted,0)=0 
LEFT JOIN 
	#PaymentType tblpt
on		tbljpd.PaymentType = tblpt.id
LEFT JOIN 
	tblJobItem tbljbI 
ON		tbljob.JobId = tbljbI.JobId  
INNER JOIN tblService tbls on tbls.serviceId = tbljbI.serviceId 
INNER join GetTable('ServiceType') st on st.valueId = tbls.ServiceType and st.valuedesc = 'Wash Package'
WHERE tbljob.LocationId = @locationId and (tbljp.createdDate between @fromDate and @endDate)
AND	ISNULL(tbljp.IsDeleted,0)=0 
AND ISNULL(tbljp.IsActive,1)=1 
AND	ISNULL(tbljpd.IsDeleted,0)=0 
AND ISNULL(tbljpd.IsActive,1)=1 
AND tbljp.PaymentStatus = @PaymentStatusSuccess
Group by  tbljob.JobDate, tbljob.locationId,tblpt.CodeValue, tbljp.CreatedDate


SELECT 
	LocationId,
	JobDate,
	SUM(Cash) AS Cash,
	SUM(Credit) AS Credit,
	SUM(GiftCard)AS GiftCard,
	SUM(Tips)AS Tips,
	SUM(CashBack)AS CashBack,
	SUM(Discount) AS Discount,
	SUM(Account) AS Account,
	SUM(Membership) AS Membership
INTO
	#Payment_Summary
FROM #PaymentSalesByType
GROUP BY locationId,JobDate

-- Summary List
--SELECT * FROM #JobItemList
--SELECT * FROM #JobProductList


SELECT 
	PS.LocationId,
	PS.JobDate,
	SUM(JT.TaxAmount) TaxAmount,
	SUM(Cash) Cash,
	SUM(Tips) Tips,
	SUM(Credit) Credit,
	SUM(GiftCard) GiftCard,
	SUM(Discount) Discount,
	SUM((Account+Membership)) AS Account,
	SUM((Account+Membership+Cash+Credit+GiftCard)) AS Total
FROM 
	#Payment_Summary PS 
LEFT JOIN 
	#JobTotal JT 
ON JT.LocationId=PS.locationId
GROUP BY PS.LocationId,PS.JobDate --JT.Total, JT.TaxAmount, Cash, Credit, GiftCard, Discount, Account, Membership

----Get All Wash Services
--Select serviceId, serviceName, serviceType as serviceTypeId, st.valuedesc as serviceType from tblService tbls 
--INNER join GetTable('ServiceType') st on st.valueId = tbls.ServiceType and st.valuedesc = 'Washes'
--WHERE ISNULL(IsDeleted, 0) = 0 

--Get Wash count based on location
Select tblj.LocationId, l.LocationName,tblj.JobDate,tbls.ServiceId, TRIM(tbls.serviceName) AS serviceName, count(1) as WashCount from tblJob tblj
INNER JOIN GetTable('JobType') JT on JT.valueid = tblj.JobType and JT.valuedesc = 'Wash'
INNER join GetTable('JobStatus') GT on GT.valueId = tblj.JobStatus and GT.valuedesc = 'Completed'
INNER JOIN tblJobItem ji on ji.JobId = tblj.JobId 
INNER JOIN tblService tbls on tbls.serviceId = ji.serviceId 
INNER join GetTable('ServiceType') st on st.valueId = tbls.ServiceType and st.valuedesc = 'Wash Package'
INNER Join tblLocation l on l.locationId = tblj.locationId
--LEFT JOIN tblTimeClock tc on tc.locationId = l.LocationId
--INNER JOIN tblEmployee e on e.EmployeeId = tc.EmployeeId
WHERE tblj.JobDate between @fromDate and @endDate
AND ISNULL(tblj.IsDeleted, 0) = 0 AND l.LocationId = @locationId

Group by tblj.LocationId, l.LocationName,tblj.JobDate, tbls.ServiceId, tbls.serviceName order by tblj.locationId



Select e.EmployeeId, e.FirstName, e.LastName, tc.EventDate, tc.locationId from tblTimeClock tc 
JOIN tblRoleMaster rm on rm.RoleMasterId = tc.RoleId and rm.RoleName = 'Manager'
JOIN tblEmployee e on e.EmployeeId = tc.EmployeeId
--JOIN #Payment_Summary ps on ps.JobDate = tc.EventDate
WHERE tc.EventDate between @fromDate and @endDate



END