CREATE PROCEDURE [StriveCarSalon].[uspGetEmployeePayCheck] 
@EmployeeId INT,
@Month INT,
@Year INT
AS
BEGIN

--DECLARE @WorkhourThreshold DECIMAL(4,2);

Declare @WashId INT = (Select valueid from GetTable('JobType') where valuedesc='Wash')
Declare @WashServiceId INT = (Select valueid from GetTable('ServiceType') where valuedesc='Wash Package')
Declare @CompletedJobStatus INT = (Select valueid from GetTable('JobStatus') where valuedesc='Completed')

DROP TABLE IF EXISTS #Location

	SELECT LocationId,WorkhourThreshold
	INTO #Location
	FROM	tbllocation WHERE IsActive = 1

DECLARE @OverTimeWashRate DECIMAL(9,2) = 1.5;



DROP TABLE IF EXISTS #PayRoll
Select tblemp.EmployeeId,
	tblemp.FirstName+' '+tblemp.LastName as PayeeName,
	tblTC.LocationId,
	tblRM.RoleName,
	CONVERT(VARCHAR(8),InTime,108) AS InTime,
	CONVERT(VARCHAR(8),ISNULL(OutTime,INTIME),108) AS OutTime,
	DATEDIFF(HOUR,ISNULL(InTime,OutTime),ISNULL(OutTime,Intime)) AS TotalHours
	,CAST(DATEDIFF(MI, ISNULL(InTime,OutTime), ISNULL(OutTime,Intime)) as int) AS TotalHoursInMin 
	,Replace(CONVERT(VARCHAR(5),DATEADD(minute, DATEDIFF(MI, ISNULL(InTime,OutTime), ISNULL(OutTime,Intime)), 0), 114),':','.')  AS TotH
INTO  #PayRoll
From tblEmployee tblemp
	Inner join tblTimeClock tblTC ON tblemp.EmployeeId = tblTC.EmployeeId AND tblTC.IsActive = 1 AND ISNULL(tblTC.IsDeleted,0)=0
	Inner join tblRoleMaster tblRM ON tblRM.RoleMasterId=tblTC.RoleId
	Where  DATEPART(YEAR,tblTC.EventDate )=@Year 
	and DATEPART(MONTH,tblTC.EventDate )=@Month 
	And tblTC.LocationId IN (SELECT LocationId FROM #Location)
	
DROP TABLE IF EXISTS #DetailAmount
SELECT 
	  tblJSE.EmployeeId
	, SUM(tblJI.Price) DetailAmount 
	,SUM(ISNULL(tblJSE.CommissionAmount,0)) as DetailCommission
INTO
	#DetailAmount
FROM 
	tblJobItem tblJI
INNER JOIN
	tblJob tblJ
ON	tblJ.JobId=tblJI.JobId
INNER JOIN 
    tblJobServiceEmployee tblJSE
ON  tblJSE.JobItemId = tblJI.JobItemId AND tblJSE.IsActive=1 AND ISNULL(tblJSE.IsDeleted,0)=0
INNER JOIN 
	tblCodeValue tblCV
ON		tblCV.id=tblJ.JobType
WHERE tblJ.LocationId IN (SELECT LocationId FROM #Location)  AND  DATEPART(YEAR,tblJ.JobDate )=@Year 
	and DATEPART(MONTH,tblJ.JobDate )=@Month  AND tblCV.CodeValue='Detail' 
GROUP BY tblJSE.EmployeeId,tblJI.Commission

-- #EmployeeHours

DROP TABLE IF EXISTS #EmployeeRate

    SELECT 
	EmployeeId,
	tbll.LocationId,
	PayeeName,
	SUM(TotalWashHours) TotalWashesHours,
	SUM(TotalDetailHours) TotalDetailHours,
	CASE WHEN SUM(TotalWashHours)>TOLHours.WorkhourThreshold THEN (SUM(TotalWashHours) - (SUM(TotalWashHours)-TOLHours.WorkhourThreshold)) ELSE SUM(TotalWashHours)  
		END TotalWashHours,
	CASE WHEN SUM(TotalWashHours)>TOLHours.WorkhourThreshold THEN (SUM(TotalWashHours)-TOLHours.WorkhourThreshold) ELSE 0 
	END AS OverTimeHours
	,TOLHours.WorkhourThreshold
INTO
	#EmployeeHours
FROM
(
SELECT  
	EmployeeId,
	pr.LocationId,
	PayeeName,
	l.WorkhourThreshold,
	CASE WHEN RoleName='Washer' THEN ISNULL(CAST(TotH AS DECIMAL(18,2)),0) ELSE 0 END AS TotalWashHours,
	CASE WHEN RoleName='Detailer' THEN ISNULL(CAST(TotH AS DECIMAL(18,2)),0) ELSE 0 END AS TotalDetailHours
FROM #PayRoll pr
LEFT JOIN #Location l on l.LocationId = pr.LocationId
) TOLHours
LEFT JOIN
	tblLocation tbll
ON tbll.LocationId=TOLHours.locationId
GROUP BY EmployeeId,tbll.LocationId,Tbll.WorkhourThreshold,PayeeName,TOLHours.WorkhourThreshold

--Rate Calculation

SELECT 
	  tblED.EmployeeId
	--, tblED.WashRate
	,ehr.HourlyRate as WashRate
	,tblED.Tip
	, tblCV.CodeValue AS [Detail Desc] 
	, tblED.PayRate AS [DetailRate]
INTO
	#Rate
FROM 
	tblEmployeeDetail tblED
LEFT JOIN
	tblCodeValue tblCV
ON		tblCV.id=tblED.ComType
LEFT JOIN
	tblCodeCategory tblCC
ON		tblCC.id=tblCV.CategoryId
LEFT JOIN tblEmployeeHourlyRate ehr on ehr.EmployeeId = tblED.EmployeeId
where  ehr.LocationId IN (SELECT LocationId FROM #Location) and ehr.IsActive = 1 and ehr.IsDeleted = 0


DROP TABLE IF EXISTS #DetailCommission

Select jse.EmployeeId, SUM(jse.CommissionAmount) as CommissionAmount 
INTO #DetailCommission 
from tblJobServiceEmployee jse
INNER JOIN tblTimeClock tc on tc.EmployeeId = jse.EmployeeId
where DATEPART(YEAR,tc.EventDate )=@Year 
	and DATEPART(MONTH,tc.EventDate)=@Month
	 AND DATEPART(YEAR,jse.CreatedDate)=@Year 
	and DATEPART(MONTH,jse.CreatedDate)=@Month
GROUP BY jse.EmployeeId


DROP TABLE IF EXISTS #FinResult

SELECT 
	ER.*,
	R.WashRate AS WashRate,
	R.DetailRate AS DetailRate,
	CASE WHEN ER.TotalWashHours < 40 THEN (ER.TotalWashHours* r.WashRate) ELSE (WorkhourThreshold * r.WashRate) END AS [WashAmount],
	DA.DetailCommission,
	R.Tip,
	
	DC.CommissionAmount AS DetailAmount,
	CASE   WHEN ER.OverTimeHours > 0 THEN (@OverTimeWashRate * R.WashRate * ER.OverTimeHours) ELSE 0 END AS OverTimePay
INTO 
	#FinResult
FROM 
	#EmployeeHours ER
LEFT JOIN
	#Rate R
ON		R.EmployeeId=ER.EmployeeId
LEFT JOIN 
	#DetailAmount DA
ON		DA.EmployeeId=ER.EmployeeId
LEFT JOIN 
	#DetailCommission DC
ON		DC.EmployeeId=ER.EmployeeId


--Bonus

DECLARE @BonusId INT =(SELECT BonusId FROM tblBonus WHERE BonusMonth=@Month AND
BonusYear=@Year AND LocationId IN (SELECT LocationId FROM #Location) AND IsActive=1 AND ISNULL(IsDeleted,0)=0)

DROP TABLE IF EXISTS #tblBonusRange
SELECT 
BonusRangeId
,BonusId
,Min
,Max
,BonusAmount
,Total
INTO #tblBonusRange
FROM tblBonusRange
WHERE BonusId =@BonusId
AND IsActive = 1 AND ISNULL(IsDeleted,0)=0

DROP TABLE IF EXISTS #WashCount
SELECT 
	tbll.LocationId,tbll.LocationName,COUNT(*) WashCount INTO #WashCount
	FROM tbljob tblj 
	INNER JOIN tblLocation tbll ON(tblj.LocationId = tbll.LocationId)
	INNER JOIN tblJobItem tblji ON(tblj.JobId=tblji.JobId)
	INNER JOIN tblService tbls ON(tblji.ServiceId = tbls.ServiceId)
	WHERE tblj.JobType=@WashId
	AND tbls.ServiceType=@WashServiceId
	AND tblj.JobStatus=@CompletedJobStatus
	and DATEPART(YEAR,tblj.JobDate )=@Year 
	and DATEPART(MONTH,tblj.JobDate )=@Month 
	AND tblj.LocationId IN (SELECT LocationId FROM #Location)
	AND tblj.IsActive=1 AND tblji.IsActive=1 AND tbls.IsActive=1 AND tbll.IsActive=1
	AND ISNULL(tblj.IsDeleted,0)=0 AND ISNULL(tblji.IsDeleted,0)=0 AND ISNULL(tbls.IsDeleted,0)=0
	AND ISNULL(tbll.IsDeleted,0)=0
	GROUP BY tbll.LocationId,tbll.LocationName

DROP TABLE IF EXISTS #EmpBonus
Select DISTINCT FR.EmployeeId,PayeeName,FR.LocationId,
IsNull(TotalWashHours,0) as TotalWashHours,
       IsNull(TotalDetailHours,0) as TotalDetailHours,
	   IsNull(WashRate,0) as WashRate,
	   IsNull(DetailRate,0) as DetailRate,
	   IsNull([WashAmount],0) as WashAmount,
	   IsNull([DetailAmount],0) as DetailAmount,
	   IsNull(OverTimePay,0) as OverTimePay,
	   IsNull(DetailCommission,0) as DetailCommission,
       (ISNULL([WashAmount],0)+IsNull([OverTimePay],0)+IsNull([DetailCommission],0)) as PayeeTotal,
	   WC.WashCount,
	   CASE WHEN (WC.WashCount >= BR.MIN AND WC.WashCount <= BR.MAX) then IsNull(TotalWashHours,0) * BR.BonusAmount ELSE 0 END as Bonus
	   ,CASE WHEN (WC.WashCount >= BR.MIN AND WC.WashCount <= BR.MAX) then BR.BonusAmount ELSE 0 END as BonusAmount
	   
	   INTO #EmpBonus
       From #FinResult FR
	    JOIN #tblBonusRange BR ON 1 = 1
	    JOIN #WashCount WC ON WC.LocationId = FR.LocationId


		SELECT EmployeeId,PayeeName,LocationId,WashRate,
		TotalWashHours,
       TotalDetailHours,
	   WashAmount,
	   OverTimePay,
	   DetailCommission,
       --(ISNULL([WashAmount],0)+IsNull([OverTimePay],0)+IsNull([DetailCommission],0)) as PayeeTotal,
	   WashCount,
		SUM(Bonus) Bonus,
		SUM(BonusAmount) BonusAmount, (SUM(Bonus) + WashAmount + DetailCommission) AS NetPay
		INTO #FinalResult
		FROM #EmpBonus
		GROUP BY EmployeeId,PayeeName,LocationId,
		TotalWashHours,
       TotalDetailHours,
	   WashAmount,
	   DetailAmount,
	   OverTimePay,
	   DetailCommission,
	   WashCount,WashRate

	   SELECT * FROM #FinalResult where EmployeeId = @EmployeeId

	   SELECT LocationId,
	   COUNT(DISTINCT EmployeeId) AS EmployeeCount,
	   sum(TotalWashHours) AS TotalWashHours,
		sum(Bonus) BonusAmt, (SUM(TotalWashHours) / sum(Bonus)) AS BonusPerHour
		FROM #FinalResult
		group by LocationId
END