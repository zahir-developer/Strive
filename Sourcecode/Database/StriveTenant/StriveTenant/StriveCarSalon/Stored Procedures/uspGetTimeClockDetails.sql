
CREATE   PROCEDURE [StriveCarSalon].[uspGetTimeClockDetails]
@EmployeeId INT,
@LocationId INT,
@FromDate DATETIME,
@ToDate DATETIME
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

-----------------------------------------------------------------------------------------
*/
AS
SET NOCOUNT ON

BEGIN
-- CollisionCategoryId Fetch
DECLARE	@CollisionAmount INT,@CollisionCategoryId INT,@CollisionPaymentId INT

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
JOIN
	tblRoleMaster tblRM
ON		tblRM.RoleMasterId=tblTC.RoleId
WHERE 
	EmployeeId=@EmployeeId 
AND LocationId=@LocationId 
AND EventDate BETWEEN @FromDate AND @ToDate 

-- Rate Calculation
DROP TABLE IF EXISTS #Rate

SELECT 
	  EmployeeId
	, WashRate
	, DetailRate 
INTO
	#Rate
FROM 
	tblEmployeeDetail 
WHERE 
	EmployeeId=@EmployeeId

-- Rate Summary
DROP TABLE IF EXISTS #EmployeeRate

SELECT 
	EmployeeId,
	tbll.LocationId,
	SUM(TotalWashHours) TotalWashHours,
	SUM(TotalDetaileHours) TotalDetaileHours,
	CASE WHEN SUM(TotalWashHours)>tbll.WorkhourThreshold THEN (SUM(TotalWashHours)-tbll.WorkhourThreshold) ELSE 0 END AS OverTimeHours
INTO
	#EmployeeRate
FROM
(
SELECT  
	EmployeeId,
	LocationId,
	CASE WHEN RoleName='Wash' THEN ISNULL(TotalHours,0) ELSE 0 END AS TotalWashHours,
	CASE WHEN RoleName='Detailer' THEN ISNULL(TotalHours,0) ELSE 0 END AS TotalDetaileHours
FROM #TimeClock
) TOLHours
LEFT JOIN
	tblLocation tbll
ON tbll.LocationId=TOLHours.locationId
GROUP BY EmployeeId,tbll.LocationId,Tbll.WorkhourThreshold


--Collision Calculation

SELECT @CollisionAmount=SUM(Amount) 
FROM 
	tblEmployeeLiability tblEL
JOIN 
	tblEmployeeLiabilityDetail tblELD
ON		tblEL.LiabilityId=tblELD.LiabilityId
WHERE 
	tblEL.EmployeeId=@EmployeeId 
AND tblEL.CreatedDate BETWEEN @FromDate AND @ToDate
AND TblEL.LiabilityType=@CollisionCategoryId
AND tblELD.LiabilityDetailType=@CollisionPaymentId

-- Result

SELECT [Day],EventDate,InTime,OutTime,RoleName,TotH AS 'TotH(HH:MM)'  FROM #TimeClock

SELECT 
	ER.*,
	R.WashRate AS WashRate,
	R.DetailRate AS DetailRate,
	((ER.TotalWashHours* r.WashRate) +(ER.TotalDetaileHours* r.DetailRate)) AS [TOTAL],
	((ER.OverTimeHours*1.5)*r.WashRate) AS OverTimePay, @CollisionAmount  AS CollisionAMT
FROM 
	#EmployeeRate ER
LEFT JOIN
	#Rate R
ON		R.EmployeeId=ER.EmployeeId

END