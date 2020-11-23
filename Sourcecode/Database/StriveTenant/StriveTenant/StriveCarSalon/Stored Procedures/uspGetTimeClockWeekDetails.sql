
CREATE PROCEDURE [StriveCarSalon].[uspGetTimeClockWeekDetails]
@EmployeeId INT,
@LocationId INT,
@StartDate DATETIME,
@EndDate DATETIME
/*
-----------------------------------------------------------------------------------------
Author              : Lenin
Create date         : 14-SEP-2020
Description         : To Get TimeClock Details for an Employee for a Week
FRS					: TimeClock Maintainance
-----------------------------------------------------------------------------------------
 Rev | Date Modified | Developer	| Change Summary
-----------------------------------------------------------------------------------------
  1  |  2020-Sep-01   | Lenin		| Added RollBack for errored transaction 
  2  |  2020-Sep-16   | Zahir		| Procedure Name changed. Column name changes added. Parameter name changes.


-----------------------------------------------------------------------------------------
*/
AS
SET NOCOUNT ON

BEGIN
-- CollisionCategoryId Fetch
DECLARE	@CollisionAmount FLOAT,@CollisionCategoryId INT,@CollisionPaymentId INT

SELECT @CollisionCategoryId=tblCV.id 
FROM 
	tblCodeCategory tblCC 
JOIN 
	tblCodeValue tblCV 
ON		tblcc.id=tblcv.CategoryId
WHERE tblCC.Category='LiabilityType' AND tblCV.CodeValue='Collision'

SELECT @CollisionPaymentId=tblCV.id 
FROM 
	tblCodeCategory tblCC 
JOIN 
	tblCodeValue tblCV 
ON		tblcc.id=tblcv.CategoryId
WHERE tblCC.Category='LiabilityDetailType' AND tblCV.CodeValue='Payment'

--Login Details
DROP TABLE IF EXISTS #TimeClock

SELECT 
	  EmployeeId
	, LocationId
	, TimeClockId
	, tblRM.RoleMasterId AS RoleId
	, DATENAME(DW,EventDate) AS [Day]
	, EventDate
	, CONVERT(VARCHAR(8),InTime,108) AS InTime
	, CONVERT(VARCHAR(8),ISNULL(OutTime,INTIME),108) AS OutTime
	, tblRM.RoleName
	, DATEDIFF(HOUR,ISNULL(InTime,OutTime),ISNULL(OutTime,Intime)) AS TotalHours
	,CONVERT(VARCHAR(8),DATEADD(minute, DATEDIFF(MI, ISNULL(InTime,OutTime), ISNULL(OutTime,Intime)), 0), 114) AS TotH
INTO
	#TimeClock
FROM 
	tblTimeClock tblTC
	INNER JOIN	tblRoleMaster tblRM
ON		tblRM.RoleMasterId=tblTC.RoleId
WHERE 
	EmployeeId=@EmployeeId
AND LocationId=@LocationId 
AND EventDate BETWEEN @StartDate AND @EndDate
AND  ISNULL(tblTC.IsDeleted,0) = 0


-- Detail Amount Sum
DROP TABLE IF EXISTS #DetailAmount

SELECT 
	  tblJI.EmployeeId
	, SUM(tblJI.Price) DetailAmount
INTO
	#DetailAmount
FROM 
	tblJobItem tblJI
INNER JOIN
	tblJob tblJ
ON	tblJ.JobId=tblJI.JobId
INNER JOIN 
	tblCodeValue tblCV
ON		tblCV.id=tblJ.JobType
WHERE tblJI.EmployeeId = @EmployeeId AND tblJ.LocationId=@LocationId AND tblJ.JobDate BETWEEN @StartDate AND @EndDate AND tblCV.CodeValue='Detail' 
GROUP BY tblJI.EmployeeId

-- Rate Calculation
DROP TABLE IF EXISTS #Rate

SELECT 
	  tblED.EmployeeId
	, tblED.WashRate
	, tblCV.CodeValue AS [Detail Desc] 
	, tblED.ComRate as DetailRate
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
WHERE 
	tblED.EmployeeId=@EmployeeId AND tblCC.Category='DetailCommission' 

-- Rate Summary
DROP TABLE IF EXISTS #EmployeeRate

SELECT 
	EmployeeId,
	tbll.LocationId,
	SUM(TotalWashHours) TotalWashHours,
	SUM(TotalDetailHours) TotalDetailHours,
	CASE WHEN SUM(TotalWashHours)>tbll.WorkhourThreshold THEN (SUM(TotalWashHours)-tbll.WorkhourThreshold) ELSE 0 END AS OverTimeHours
INTO
	#EmployeeRate
FROM
(
SELECT  
	EmployeeId,
	LocationId,
	CASE WHEN RoleName='Wash' THEN ISNULL(TotalHours,0) ELSE 0 END AS TotalWashHours,
	CASE WHEN RoleName='Detailer' THEN ISNULL(TotalHours,0) ELSE 0 END AS TotalDetailHours
FROM #TimeClock
) TOLHours
LEFT JOIN
	tblLocation tbll
ON tbll.LocationId=TOLHours.locationId
GROUP BY EmployeeId,tbll.LocationId,Tbll.WorkhourThreshold


--Collision Calculation

SELECT @CollisionAmount=SUM(ISNULL(Amount,0)) 
FROM 
	tblEmployeeLiability tblEL
JOIN 
	tblEmployeeLiabilityDetail tblELD
ON		tblEL.LiabilityId=tblELD.LiabilityId
WHERE 
	tblEL.EmployeeId=@EmployeeId 
AND tblEL.CreatedDate BETWEEN @StartDate AND @EndDate
AND TblEL.LiabilityType=@CollisionCategoryId
--AND tblELD.LiabilityDetailType=@CollisionPaymentId

-- SummaryCalculation
DROP TABLE IF EXISTS #FinResult

SELECT 
	ER.*,
	R.WashRate AS WashRate,
	R.DetailRate AS DetailRate,
	(ER.TotalWashHours* r.WashRate)  AS [WashAmount],
	--(ER.TotalDetaileHours* r.DetailRate) AS [Detail Total],
	CASE	WHEN R.[Detail Desc]='Hourly Rate' THEN (ER.TotalDetailHours* r.DetailRate) 
			WHEN R.[Detail Desc]='Flat Fee' THEN r.DetailRate
			WHEN R.[Detail Desc]='Percentage' THEN ((DA.DetailAmount* r.DetailRate)/100)
			END AS [DetailAmount],
	((ER.OverTimeHours*1.5)*r.WashRate) AS OverTimePay, ISNULL(@CollisionAmount,0)  AS CollisionAmount
INTO 
	#FinResult
FROM 
	#EmployeeRate ER
LEFT JOIN
	#Rate R
ON		R.EmployeeId=ER.EmployeeId
LEFT JOIN 
	#DetailAmount DA
ON		DA.EmployeeId=ER.EmployeeId

-- Result

SELECT TimeClockId,RoleId,[Day],EventDate,InTime,OutTime,RoleName,TotH AS 'TotalHours'  FROM #TimeClock

SELECT 
	TotalWashHours,TotalDetailHours,OverTimeHours,WashRate,DetailRate,[WashAmount],[DetailAmount],OverTimePay,CollisionAmount
	,(([WashAmount]+[DetailAmount]+OverTimePay)-CollisionAmount) AS GrandTotal
FROM 
	#FinResult
END