CREATE PROCEDURE [StriveCarSalon].[uspGetPayrollList]   -- 2044,'2020-10-18','2020-10-21'-- 2034,'2020-09-27','2020-09-28'
@LocationId INT,
@StartDate DATETIME,
@EndDate DATETIME
/*
-----------------------------------------------------------------------------------------
Author              : Benny
Create date         : 12-OCT-2020
Description         : To Get Employee PayRoll for Last Two Week
FRS					: Payroll
-----------------------------------------------------------------------------------------
*/
as begin
  
DECLARE	@CollisionAmount FLOAT

--Collision,Uniform,Adjusment 

Drop table if exists #Category

SELECT   
         tblEL.EmployeeId as EmployeeId,

	     CASE When CodeValue='Collision' THEN Amount End As Collision,
         CASE When CodeValue='Uniform' THEN Amount End As Uniform,
	     CASE When CodeValue='Adjustment' THEN Amount End As Adjustment
INTO 
    #Category
FROM 
	tblEmployeeLiability tblEL
JOIN 
	tblEmployeeLiabilityDetail tblELD ON tblEL.LiabilityId=tblELD.LiabilityId
	LEFT JOIN
	tblCodeValue tblCV
ON		tblCV.id=tblEL.LiabilityType
LEFT JOIN
	tblCodeCategory tblCC
ON		tblCC.id=tblCV.CategoryId

--WHERE 
--	tblEL.LiabilityDate >= @StartDate AND tblEL.LiabilityDate <= @EndDate

Select EmployeeId,
	   SUM(IsNull(Collision,0))  As Collision,
       SUM(IsNull(Uniform,0))  As Uniform,
	   SUM(IsNull(Adjustment,0))  As Adjustment
	   INTO 
	   #CodeValue
	   from #Category 
	   Group by EmployeeId
    
Select tblemp.EmployeeId,
		       tblemp.FirstName+''+tblemp.LastName as PayeeName,
			   tblTC.LocationId,
			   tblRM.RoleName,
			   CONVERT(VARCHAR(8),InTime,108) AS InTime,
	           CONVERT(VARCHAR(8),ISNULL(OutTime,INTIME),108) AS OutTime,
			   DATEDIFF(HOUR,ISNULL(InTime,OutTime),ISNULL(OutTime,Intime)) AS TotalHours

			   INTO
			       #PayRoll

			   From tblEmployee tblemp
			        Inner join tblTimeClock tblTC ON tblemp.EmployeeId = tblTC.EmployeeId
					Inner join tblRoleMaster tblRM ON tblRM.RoleMasterId=tblTC.RoleId
					Where tblTC.EventDate >=  @StartDate And  tblTC.EventDate <=@EndDate

-- Detail Amount Sum

DROP TABLE IF EXISTS #DetailAmount

SELECT 
	  tblJI.EmployeeId
	, SUM(tblJI.Price) DetailAmount 
	,SUM(tblJI.Commission) as DetailCommission
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
WHERE tblJ.LocationId=@LocationId  AND tblJ.JobDate >= @StartDate AND tblJ.JobDate <= @EndDate AND tblCV.CodeValue='Detail' 

GROUP BY tblJI.EmployeeId,tblJI.Commission

-- #EmployeeHours

DROP TABLE IF EXISTS #EmployeeRate

    SELECT 
	EmployeeId,
	tbll.LocationId,
	PayeeName,
	SUM(TotalWashHours) TotalWashHours,
	SUM(TotalDetailHours) TotalDetailHours,
	CASE WHEN SUM(TotalWashHours)>tbll.WorkhourThreshold THEN (SUM(TotalWashHours)-tbll.WorkhourThreshold) ELSE 0 END AS OverTimeHours
INTO
	#EmployeeHours
FROM
(
SELECT  
	EmployeeId,
	LocationId,
	PayeeName,
	CASE WHEN RoleName='Wash' THEN ISNULL(TotalHours,0) ELSE 0 END AS TotalWashHours,
	CASE WHEN RoleName='Detailer' THEN ISNULL(TotalHours,0) ELSE 0 END AS TotalDetailHours
FROM #PayRoll
) TOLHours
LEFT JOIN
	tblLocation tbll
ON tbll.LocationId=TOLHours.locationId
GROUP BY EmployeeId,tbll.LocationId,Tbll.WorkhourThreshold,PayeeName

--Rate Calculation

SELECT 
	  tblED.EmployeeId
	, tblED.WashRate
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
--WHERE 
--	tblCC.Category='CommisionType'


DROP TABLE IF EXISTS #FinResult

SELECT 
	ER.*,
	--ER.EmployeeId,
	R.WashRate AS WashRate,
	R.DetailRate AS DetailRate,
	(ER.TotalWashHours* r.WashRate)  AS [WashAmount],
	DA.DetailCommission,
	--(ER.TotalDetaileHours* r.DetailRate) AS [Detail Total],
	CASE	WHEN R.[Detail Desc]='Hourly' THEN (ER.TotalDetailHours* r.DetailRate) 
			WHEN R.[Detail Desc]='Flat Fee' THEN r.DetailRate
			WHEN R.[Detail Desc]='Percentage' THEN ((DA.DetailAmount* r.DetailRate)/100)
			END AS [DetailAmount],
	((ER.OverTimeHours*1.5)*r.WashRate) AS OverTimePay, ISNULL(@CollisionAmount,0)  AS CollisionAmount
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


--FinalOutput

Select  FR.EmployeeId,PayeeName,LocationId,IsNull(TotalWashHours,0) as TotalWashHours,
       IsNull(TotalDetailHours,0) as TotalDetailHours,IsNull(OverTimeHours,0) as OverTimeHours,IsNull(WashRate,0) as WashRate,
	   IsNull(DetailRate,0) as DetailRate,IsNull([WashAmount],0) as WashAmount,IsNull([DetailAmount],0) as DetailAmount,IsNull(OverTimePay,0) as OverTimePay,
	   IsNull(CollisionAmount,0) as CollisionAmount,IsNull(DetailCommission,0) as DetailCommission,IsNull(Collision,0) as Collision,IsNull(Uniform,0) as Uniform,
	   IsNull(Adjustment,0) as Adjustment,
       (IsNull([WashAmount],0)+IsNull([OverTimePay],0)+IsNull([DetailCommission],0)) as PayeeTotal
       From #FinResult FR
LEFT JOIN
      #CodeValue CA ON
	     CA.EmployeeId = FR.EmployeeId

END