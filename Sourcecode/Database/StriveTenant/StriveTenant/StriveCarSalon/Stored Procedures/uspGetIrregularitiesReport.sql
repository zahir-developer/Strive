CREATE PROCEDURE [StriveCarSalon].[uspGetIrregularitiesReport] 
@locationId INT , @fromDate Date, @endDate Date
AS
-- =============================================
-- Author:		Vetriselvi
-- Create date: 11-Jul-202
-- Description:	Returns the 1 Irregularities report data 
-- EXEC StriveCarSalon.uspGetIrregularitiesReport 1, '2021-07-18', '2021-07-23' 
-- =============================================
-- =============================================
----------History------------
-- =============================================
-- 16-12-2021, Vetriselvi - In Coupon applied discount filter
-- =============================================

BEGIN


--Sales Report
Declare @PaymentStatusSuccess INT = (Select valueid from GetTable('PaymentStatus') where valuedesc='Success')
Declare @AdditionalServices INT = (Select valueid from GetTable('ServiceType') where valuedesc='Additional Services')
Declare @WashPackage INT = (Select valueid from GetTable('ServiceType') where valuedesc='Wash Package')
DECLARE @CashRegisterTypeId INT = (  
Select CV.id from tblCodeCategory CC  
JOIN tblCodeValue CV on CV.CategoryId = CC.id  
WHERE CV.CodeValue = 'CLOSEOUT')  

DROP TABLE IF EXISTS #tblService

SELECT ServiceId,ServiceType
INTO #tblService
FROM tblService tbls 
INNER JOIN GetTable('ServiceType') tblcv on tbls.ServiceType = tblcv.valueid
--WHERE LocationId = @locationId
	
DROP TABLE IF EXISTS #PaymentType

Select Cv.id,CodeValue,Category
into #PaymentType
from tblcodevalue cv
join tblCodeCategory CC ON cc.id=cv.CategoryId
Where CC.[Category]='PaymentType'

DROP TABLE IF EXISTS #tblEmployee
Select e.FirstName, e.LastName,tc.EventDate
INTO #tblEmployee
from tblTimeClock tc 
JOIN tblRoleMaster rm on rm.RoleMasterId = tc.RoleId and rm.RoleName = 'Manager'
JOIN tblEmployee e on e.EmployeeId = tc.EmployeeId
WHERE tc.EventDate between @fromDate and @endDate
AND  tc.locationId = @locationId


DROP TABLE IF EXISTS #tblJob  
SELECT JobId, JobPaymentId,JobType,JobDate,TicketNumber,VehicleId,Make,Model,Color,TimeIn
INTO #tblJob
FROM	tblJob
WHERE LocationId =  @locationId AND
	JobDate BETWEEN @fromDate and @endDate
	AND	ISNULL(tbljob.IsDeleted,0)=0 
	AND ISNULL(tbljob.IsActive,1)=1 

--Deleted jobs list
DROP TABLE IF EXISTS #tblJobDel
SELECT JobId, JobPaymentId,JobType,JobDate,TicketNumber,VehicleId,Make,Model,Color,TimeIn
INTO #tblJobDel
FROM	tblJob
WHERE LocationId =  @locationId AND
	JobDate BETWEEN @fromDate and @endDate
	AND	ISNULL(tbljob.IsDeleted,0)=1 
	AND ISNULL(tbljob.IsActive,1)=1 

DROP TABLE IF EXISTS #PaymentSalesByType

SELECT   
    tbljob.JobId,
    CONVERT(VARCHAR(10), tbljp.CreatedDate,120) as PaymentDate,
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
AND tbljpd.PaymentType IN (SELECT id from #PaymentType where CodeValue='Discount') --Selecting only discount
AND		ISNULL(tbljpd.IsDeleted,0)=0 
LEFT JOIN 
	#PaymentType tblpt
on		tbljpd.PaymentType = tblpt.id
LEFT JOIN 
	tblJobItem tbljbI 
ON		tbljob.JobId = tbljbI.JobId  
INNER JOIN GetTable('JobType') JT on JT.valueid = tbljob.JobType and JT.valuedesc = 'Wash'
INNER JOIN tblService tbls on tbls.serviceId = tbljbI.serviceId 
INNER join GetTable('ServiceType') st on st.valueId = tbls.ServiceType and st.valuedesc = 'Wash Package'
WHERE ISNULL(tbljp.IsDeleted,0)=0 
AND ISNULL(tbljp.IsActive,1)=1 
AND	ISNULL(tbljpd.IsDeleted,0)=0 
AND ISNULL(tbljpd.IsActive,1)=1 
AND tbljp.PaymentStatus = @PaymentStatusSuccess
Group by  tbljob.JobId,tblpt.CodeValue, tbljp.CreatedDate

--Payment sales by type for deleted tickets
DROP TABLE IF EXISTS #PaymentSalesByTypeDel

SELECT   
    tbljob.JobId,
    CONVERT(VARCHAR(10), tbljp.CreatedDate,120) as PaymentDate,
	case when tblpt.CodeValue = 'Cash' then SUM(ISNULL(tbljpd.Amount,0)) ELSE 0 end AS Cash,
	case when tblpt.CodeValue = 'Card' then SUM(ISNULL(tbljpd.Amount,0)) ELSE 0 end AS Credit,
	case when tblpt.CodeValue = 'Tips' then SUM(ISNULL(tbljpd.Amount,0)) ELSE 0 end AS Tips,
	case when tblpt.CodeValue = 'GiftCard' then SUM(ISNULL(tbljpd.Amount,0)) ELSE 0 end AS GiftCard,
	SUM(ISNULL(tbljp.Cashback,0)) AS CashBack,
	case when tblpt.CodeValue = 'Discount' then SUM(ISNULL(tbljpd.Amount,0)) ELSE 0 end AS Discount ,
	case when tblpt.CodeValue = 'Account' then SUM(ISNULL(tbljpd.Amount,0)) ELSE 0 end AS Account ,
	case when tblpt.CodeValue = 'Membership' then SUM(ISNULL(tbljpd.Amount,0)) ELSE 0 end AS Membership
	INTO #PaymentSalesByTypeDel
FROM
	#tblJobDel tbljob 
LEFT JOIN 
	tblJobPayment tbljp 
ON		tbljob.JobPaymentId = tbljp.JobPaymentId AND ISNULL(tbljp.IsRollBack,0)=0 
LEFT JOIN 
	tblJobPaymentDetail tbljpd 
ON		tbljp.JobPaymentId = tbljpd.JobPaymentId
AND tbljpd.PaymentType IN (SELECT id from #PaymentType where CodeValue='Discount') --Selecting only discount
AND		ISNULL(tbljpd.IsDeleted,0)=0 
LEFT JOIN 
	#PaymentType tblpt
on		tbljpd.PaymentType = tblpt.id
LEFT JOIN 
	tblJobItem tbljbI 
ON		tbljob.JobId = tbljbI.JobId  
INNER JOIN GetTable('JobType') JT on JT.valueid = tbljob.JobType and (JT.valuedesc = 'Wash' OR JT.valuedesc='Detail')
INNER JOIN tblService tbls on tbls.serviceId = tbljbI.serviceId 
INNER join GetTable('ServiceType') st on st.valueId = tbls.ServiceType and (st.valuedesc = 'Wash Package' OR st.valuedesc='Detail Package')
WHERE ISNULL(tbljp.IsDeleted,0)=0 
AND ISNULL(tbljp.IsActive,1)=1 
AND	ISNULL(tbljpd.IsDeleted,0)=0 
AND ISNULL(tbljpd.IsActive,1)=1 
AND tbljp.PaymentStatus = @PaymentStatusSuccess
Group by  tbljob.JobId,tblpt.CodeValue, tbljp.CreatedDate

DROP TABLE IF EXISTS #tblJobItem 
 SELECT sum(ISNULL(tblji.Price,0) * ISNULL(tblji.Quantity,0)) SaleAmt,
 tbljb.JobId,
 case when tbls.ServiceType = @AdditionalServices then 1 ELSE 0 end AS extra
 --count(ServiceId) as ServiceId
 INTO #tblJobItem
 FROM #tblJob tbljb
 LEFT JOIN tblJobItem tblji on tbljb.JobId = tblji.JobId 
 LEFT JOIN #tblService tbls on tblji.ServiceId = tbls.ServiceId
 GROUP BY tbljb.JobId ,tbls.ServiceType,
 tblji.Price,tblji.Quantity

 --Job items of deleted ticket
 DROP TABLE IF EXISTS #tblJobItemDel
 SELECT sum(ISNULL(tblji.Price,0) * ISNULL(tblji.Quantity,0)) SaleAmt,
 tbljb.JobId,
 case when tbls.ServiceType = @AdditionalServices then 1 ELSE 0 end AS extra
 --count(ServiceId) as ServiceId
 INTO #tblJobItemDel
 FROM #tblJobDel tbljb
 LEFT JOIN tblJobItem tblji on tbljb.JobId = tblji.JobId 
 LEFT JOIN #tblService tbls on tblji.ServiceId = tbls.ServiceId
 GROUP BY tbljb.JobId ,tbls.ServiceType,
 tblji.Price,tblji.Quantity
 
--Vehicles with no info
	
DROP TABLE IF EXISTS #VehiclesInfo  
	SELECT te.FirstName + ' ' + te.LastName As Manager,
	tbljb.JobDate,
	ISNULL(tblclv.Barcode,'') Barcode,
	FORMAT(tbljb.TimeIn,'HH:mm') TimeIn,
	tbljb.TicketNumber,
	model.ModelValue AS Model,
	make.MakeValue As Make,
	cvCo.valuedesc as Color,
	tps.Discount,
	STUFF((SELECT ','+ CodeValue --FROM #PaymentType
	from tblJobPaymentDetail tbljpd
	JOIN #PaymentType tblpt on	tbljpd.PaymentType = tblpt.id 
	 where tbljp.JobPaymentId = tbljpd.JobPaymentId AND ISNULL(tbljpd.IsDeleted,0)=0 
	FOR XML PATH('')),1,1,'') as PaidBy,
	--tblpt.CodeValue AS PaidBy,
	SUM(SaleAmt) SaleAmt,
	SUM(tblji.extra) as ExtraServices
	INTO #VehiclesInfo
	FROM #tblJob tbljb
	LEFT JOIN	tblJobPayment tbljp  WITH(NOLOCK) ON tbljb.JobPaymentId = tbljp.JobPaymentId AND tbljp.IsProcessed=1 
		AND ISNULL(tbljp.IsRollBack,0)=0 AND tbljp.IsActive = 1 AND ISNULL(tbljp.IsDeleted,0)=0 
	INNER JOIN GetTable('JobType') JT on JT.valueid = tbljb.JobType and JT.valuedesc = 'Wash'
	LEFT JOIN tblClientVehicle tblclv on tbljb.VehicleId = tblclv.VehicleId
	LEFT JOIN #tblJobItem tblji on tbljb.JobId = tblji.JobId --and tblji.ServiceId = @WashPackage
	--LEFT JOIN #tblService tbls on tblji.ServiceId = tbls.ServiceId
	Left join tblVehicleMake make on tbljb.Make=make.MakeId
	Left join tblvehicleModel model on tbljb.Model= model.ModelId
	LEFT JOIN GetTable('VehicleColor') cvCo ON tbljb.Color = cvCo.valueid
	LEFT JOIN #PaymentSalesByType tps ON tbljb.JobId = tps.JobId
	LEFT JOIN #tblEmployee te ON tbljb.JobDate = CONVERT(DATE,te.EventDate)
	LEFT JOIN tblJobPaymentDetail tbljpd ON tbljp.JobPaymentId = tbljpd.JobPaymentId AND ISNULL(tbljpd.IsDeleted,0)=0 
	LEFT JOIN #PaymentType tblpt on	tbljpd.PaymentType = tblpt.id 
	where  tbljp.PaymentStatus = @PaymentStatusSuccess 
	AND ISNULL(tblclv.Barcode,'None/UNK') = 'None/UNK' 
	GROUP BY te.FirstName, te.LastName,tbljb.JobDate,
	tblclv.Barcode,tbljb.TimeIn,tbljb.TicketNumber,model.ModelValue,make.MakeValue,cvCo.valuedesc,
	--tbls.ServiceId, 
	tps.Discount--, tblc.FirstName, tblc.LastName 
	,tblpt.CodeValue,PaymentType, tblpt.id ,tbljpd.JobPaymentId,tbljpd.IsDeleted,tbljp.JobPaymentId


	SELECT DISTINCT *
	FROM #VehiclesInfo

--Missing Ticket info
DROP TABLE IF EXISTS #MissingTicket  
	SELECT te.FirstName + ' ' + te.LastName As Manager,
	tbljb.JobDate,
	ISNULL(tblclv.Barcode,'') Barcode,
	FORMAT(tbljb.TimeIn,'HH:mm') TimeIn,
	tbljb.TicketNumber,
	model.ModelValue AS Model,
	make.MakeValue As Make,
	cvCo.valuedesc as Color,
	tps.Discount,
	--tbljpd.Amount,
	STUFF((SELECT ','+ CodeValue --FROM #PaymentType
	from tblJobPaymentDetail tbljpd
	JOIN #PaymentType tblpt on	tbljpd.PaymentType = tblpt.id 
	 where tbljp.JobPaymentId = tbljpd.JobPaymentId AND ISNULL(tbljpd.IsDeleted,0)=0 
	FOR XML PATH('')),1,1,'') as PaidBy,
	CONVERT(DECIMAL(7,2), SUM(tblji.SaleAmt)) SaleAmt,
	SUM(tblji.extra) as ExtraServices
	INTO #MissingTicket
	FROM tblJob tbljb
	LEFT JOIN	tblJobPayment tbljp  WITH(NOLOCK) ON tbljb.JobPaymentId = tbljp.JobPaymentId AND tbljp.IsProcessed=1 
		AND ISNULL(tbljp.IsRollBack,0)=0 AND tbljp.IsActive = 1 AND ISNULL(tbljp.IsDeleted,0)=0 
	INNER JOIN GetTable('JobType') JT on JT.valueid = tbljb.JobType and (JT.valuedesc = 'Wash' OR JT.valuedesc='Detail')
	LEFT JOIN tblClientVehicle tblclv on tbljb.VehicleId = tblclv.VehicleId
	LEFT JOIN #tblJobItemDel tblji on tbljb.JobId = tblji.JobId --and tblji.ServiceId = @WashPackage
	--LEFT JOIN #tblService tbls on tblji.ServiceId = tbls.ServiceId
	Left join tblVehicleMake make on tbljb.Make=make.MakeId
	Left join tblvehicleModel model on tbljb.Model= model.ModelId
	LEFT JOIN GetTable('VehicleColor') cvCo ON tbljb.Color = cvCo.valueid
	LEFT JOIN #PaymentSalesByTypeDel tps ON tbljb.JobId = tps.JobId
	--INNER JOIN tblJobPaymentDetail tbljpd ON tbljb.JobPaymentId = tbljpd.JobPaymentId AND tbljpd.PaymentType=(select ID from #PaymentType where CodeValue = 'Discount' )
	LEFT JOIN #tblEmployee te ON tbljb.JobDate = CONVERT(DATE,te.EventDate)
	where  	
	ISNULL(tbljb.IsDeleted,0)=1  AND
	tbljb.LocationId =  @locationId AND
	tbljb.JobDate BETWEEN @fromDate and @endDate
	--AND tbljp.PaymentStatus = @PaymentStatusSuccess 
	GROUP BY te.FirstName, te.LastName,tbljb.JobDate,
	tblclv.Barcode,tbljb.TimeIn,tbljb.TicketNumber,model.ModelValue,make.MakeValue,cvCo.valuedesc,
	--tbls.ServiceId, 
	tps.Discount--,PaymentType, tblpt.id ,tbljpd.JobPaymentId,tbljpd.IsDeleted
	,tbljp.JobPaymentId

	SELECT DISTINCT * from #MissingTicket
--Price Change/Modification 

--Coupon
DROP TABLE IF EXISTS #Coupon
SELECT te.FirstName + ' ' + te.LastName As Manager,
	tbljb.JobDate,
	ISNULL(tblclv.Barcode,'') Barcode,
	FORMAT(tbljb.TimeIn,'HH:mm') TimeIn,
	tbljb.TicketNumber,
	model.ModelValue AS Model,
	make.MakeValue As Make,
	cvCo.valuedesc as Color,
	tps.Discount,
	STUFF((SELECT ','+ CodeValue --FROM #PaymentType
	from tblJobPaymentDetail tbljpd
	JOIN #PaymentType tblpt on	tbljpd.PaymentType = tblpt.id 
	 where tbljp.JobPaymentId = tbljpd.JobPaymentId AND ISNULL(tbljpd.IsDeleted,0)=0 
	FOR XML PATH('')),1,1,'') as PaidBy,
	SUM(SaleAmt) SaleAmt,
	SUM(tblji.extra) as ExtraServices
	INTO #Coupon
	FROM #tblJob tbljb
	LEFT JOIN	tblJobPayment tbljp  WITH(NOLOCK) ON tbljb.JobPaymentId = tbljp.JobPaymentId AND tbljp.IsProcessed=1 
		AND ISNULL(tbljp.IsRollBack,0)=0 AND tbljp.IsActive = 1 AND ISNULL(tbljp.IsDeleted,0)=0 
	INNER JOIN GetTable('JobType') JT on JT.valueid = tbljb.JobType and JT.valuedesc = 'Wash'
	LEFT JOIN tblClientVehicle tblclv on tbljb.VehicleId = tblclv.VehicleId
	LEFT JOIN #tblJobItem tblji on tbljb.JobId = tblji.JobId --and tblji.ServiceId = @WashPackage
	--LEFT JOIN #tblService tbls on tblji.ServiceId = tbls.ServiceId
	Left join tblVehicleMake make on tbljb.Make=make.MakeId
	Left join tblvehicleModel model on tbljb.Model= model.ModelId
	LEFT JOIN GetTable('VehicleColor') cvCo ON tbljb.Color = cvCo.valueid
	LEFT JOIN #PaymentSalesByType tps ON tbljb.JobId = tps.JobId
	LEFT JOIN #tblEmployee te ON tbljb.JobDate = CONVERT(DATE,te.EventDate)
	LEFT JOIN tblJobPaymentDetail tbljpd ON tbljp.JobPaymentId = tbljpd.JobPaymentId AND ISNULL(tbljpd.IsDeleted,0)=0 
	LEFT JOIN #PaymentType tblpt on	tbljpd.PaymentType = tblpt.id 
	where  tbljp.PaymentStatus = @PaymentStatusSuccess AND tblpt.CodeValue like '%Discount%'
	GROUP BY te.FirstName, te.LastName,tbljb.JobDate,
	tblclv.Barcode,tbljb.TimeIn,tbljb.TicketNumber,model.ModelValue,make.MakeValue,cvCo.valuedesc,
	--tbls.ServiceId, 
	tps.Discount,tblpt.CodeValue,PaymentType, tblpt.id ,tbljpd.JobPaymentId,tbljpd.IsDeleted,tbljp.JobPaymentId

	SELECT DISTINCT * FROM #Coupon
-- Deposit Off
DROP TABLE IF EXISTS #tblCashRegister
Select  
CR.CashRegisterId ,
CR.CashRegisterDate
INTO #tblCashRegister
FROM   
tblCashRegister CR   
WHERE  
CR.LocationId = @LocationId AND  
CR.CashRegisterType = @CashRegisterTypeId AND  
CR.CashRegisterDate between @fromDate AND  @endDate AND  
isnull(CR.isDeleted,0) = 0    

DROP TABLE IF EXISTS #tblCashRegisterCoins
SELECT   (CRC.Pennies/ 100.00 + (5 * CRC.Nickels ) /100.00 + (10 * CRC.Dimes )/100.00 +  ( 25 * CRC.Quarters) /100.00 + ( 50 * CRC.HalfDollars )/100.00) as TotalCoins,
CR.CashRegisterDate
INTO #tblCashRegisterCoins
FROM  
[tblCashRegisterCoins] CRC  
INNER JOIN  #tblCashRegister CR  ON CRC.cashRegisterId = CR.CashRegisterId  
 
 DROP TABLE IF EXISTS #tblCashRegisterRolls
SELECT  ((50 * CRR.Pennies) / 100.00 + ((5 * CRR.Nickels)  * 40) / 100.00 +  ((10 * CRR.Dimes)) * 50 / 100.00 + ((25 * CRR.Quarters)) * 40 / 100.00 ) as TotalRolls,
CR.CashRegisterDate
INTO #tblCashRegisterRolls
FROM  
[tblCashRegisterRolls] CRR  
INNER JOIN  #tblCashRegister CR  ON CRR.cashRegisterId = CR.CashRegisterId  
  
  DROP TABLE IF EXISTS #tblCashRegisterBills
SELECT  (CRB.[s1] + ( 5 * CRB.[s5]) + (10 * CRB.[s10]) + (20 * CRB.[s20]) + (50 * CRB.[s50]) + (100 * CRB.[s100] )) as TotalBills,
CR.CashRegisterDate
INTO #tblCashRegisterBills
FROM  [tblCashRegisterBills] CRB  
INNER JOIN  #tblCashRegister CR  ON CRB.cashRegisterId = CR.CashRegisterId  


DROP TABLE IF EXISTS #DepositOff
SELECT te.FirstName + ' ' + te.LastName As Manager,
	tbljb.JobDate,
	SUM(isnull(tps.Cash,0)) TotalCash
	--(TotalCoins + TotalRolls + TotalBills) as ClosedCash
	--TotalCoins, TotalRolls ,TotalBills
	INTO #DepositOff
	FROM #tblJob tbljb
	
	LEFT JOIN #PaymentSalesByType tps ON tbljb.JobId = tps.JobId
	LEFT JOIN #tblEmployee te ON tbljb.JobDate = CONVERT(DATE,te.EventDate)
	GROUP BY tbljb.JobDate,te.FirstName, te.LastName
	--,TotalCoins , TotalRolls , TotalBills
	--tps.Discount
	SELECT Manager,JobDate,TotalCash - (isnull(TotalCoins,0) + isnull(TotalRolls,0) + isnull(TotalBills,0)) AS 'Difference'
	FROM #DepositOff df
	LEFT JOIN	#tblCashRegisterCoins coin   ON coin.CashRegisterDate = df.JobDate
	LEFT JOIN	#tblCashRegisterRolls roll   ON roll.CashRegisterDate = df.JobDate
	LEFT JOIN	#tblCashRegisterBills bill   ON bill.CashRegisterDate = df.JobDate
	WHERE TotalCash - (isnull(TotalCoins,0) + isnull(TotalRolls,0) + isnull(TotalBills,0)) != 0
END