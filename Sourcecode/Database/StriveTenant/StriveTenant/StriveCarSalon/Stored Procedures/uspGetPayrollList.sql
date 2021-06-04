

CREATE PROCEDURE [StriveCarSalon].[uspGetPayrollList] 
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
--[StriveCarSalon].[uspGetPayrollList] 20,'2021-04-08','2021-04-23'
03-06-2021 - Vetriselvi OverTime Pay calculation and excluded overtime hours in total wash hours
*/
as begin
  
DECLARE	@CollisionAmount DECIMAL(9,2)
DECLARE @HoursLimit INT = 40;
DECLARE @OverTimeWashRate DECIMAL(9,2) = 1.5;

--Collision,Uniform,Adjusment 

Drop table if exists #Category

SELECT   
         tblEL.EmployeeId as EmployeeId,
		 tblEl.LiabilityDescription as Notes,
	     CASE When CodeValue='Collision' THEN Amount End As Collision,
         CASE When CodeValue='Uniform' THEN Amount End As Uniform,
	     CASE When CodeValue='Adjustment' THEN Amount End As Adjustment
INTO     #Category 
FROM 	tblEmployeeLiability tblEL 
JOIN 
	tblEmployeeLiabilityDetail tblELD ON tblEL.LiabilityId=tblELD.LiabilityId AND tblEL.IsActive=1 AND ISNULL(tblEL.IsDeleted,0)=0
	LEFT JOIN	tblCodeValue tblCV ON		tblCV.id=tblEL.LiabilityType
    LEFT JOIN	tblCodeCategory tblCC ON		tblCC.id=tblCV.CategoryId
--WHERE tblEL.LiabilityDate BETWEEN @StartDate AND @EndDate


Select EmployeeId, 
STUFF((SELECT  ', ' + Notes from #Category c
	WHERE c.EmployeeId = ct.EmployeeId
    FOR XML PATH('')
	), 1, 2, '')  AS Notes INTO #EmployeeCollision from #Category ct where Notes IS NOT NULL


Select EmployeeId,
	   --Notes,
	   SUM(IsNull(Collision,0))  As Collision,
       SUM(IsNull(Uniform,0))  As Uniform,
	   SUM(IsNull(Adjustment,0))  As Adjustment
	   INTO 
	   #CodeValue
	   from #Category 
	   Group by EmployeeId
	   --,Notes
    
Select tblemp.EmployeeId,
		       tblemp.FirstName+' '+tblemp.LastName as PayeeName,
			   tblTC.LocationId,
			   tblRM.RoleName,
			   CONVERT(VARCHAR(8),InTime,108) AS InTime,
	           CONVERT(VARCHAR(8),ISNULL(OutTime,INTIME),108) AS OutTime,
			   DATEDIFF(HOUR,ISNULL(InTime,OutTime),ISNULL(OutTime,Intime)) AS TotalHours
			   ,CAST(DATEDIFF(MI, ISNULL(InTime,OutTime), ISNULL(OutTime,Intime)) as int) AS TotalHoursInMin 
	,Replace(CONVERT(VARCHAR(5),DATEADD(minute, DATEDIFF(MI, ISNULL(InTime,OutTime), ISNULL(OutTime,Intime)), 0), 114),':','.')  AS TotH
			   INTO
			       #PayRoll

			   From tblEmployee tblemp
			        Inner join tblTimeClock tblTC ON tblemp.EmployeeId = tblTC.EmployeeId AND tblTC.IsActive = 1 AND ISNULL(tblTC.IsDeleted,0)=0
					Inner join tblRoleMaster tblRM ON tblRM.RoleMasterId=tblTC.RoleId
					Where tblTC.EventDate >=  @StartDate And  tblTC.EventDate <=@EndDate And tblTC.LocationId=@LocationId
					
-- Detail Amount Sum

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
WHERE tblJ.LocationId=@LocationId  AND tblJ.JobDate >= @StartDate AND tblJ.JobDate <= @EndDate AND tblCV.CodeValue='Detail' 

GROUP BY tblJSE.EmployeeId,tblJI.Commission

-- #EmployeeHours

DROP TABLE IF EXISTS #EmployeeRate

    SELECT 
	EmployeeId,
	tbll.LocationId,
	PayeeName,
	SUM(TotalWashHours) TotalWashesHours,
	--CASE WHEN SUM(TotalWashHours)>@HoursLimit THEN @HoursLimit ELSE SUM(TotalWashHours) END AS TotalWashHours,
	SUM(TotalDetailHours) TotalDetailHours,
	--CASE WHEN ((SUM(TotalWashHours) + SUM(TotalDetailHours))>@HoursLimit AND SUM(TotalWashHours)>((SUM(TotalWashHours) + SUM(TotalDetailHours))/2)AND SUM(TotalDetailHours)<((SUM(TotalWashHours) + SUM(TotalDetailHours))/2)) THEN SUM(TotalDetailHours) ELSE 0 END AS OverTimeHours
		CASE WHEN SUM(TotalWashHours)>@HoursLimit THEN (SUM(TotalWashHours) - (SUM(TotalWashHours)-@HoursLimit)) ELSE SUM(TotalWashHours)  
		END TotalWashHours,
	CASE WHEN SUM(TotalWashHours)>@HoursLimit THEN (SUM(TotalWashHours)-@HoursLimit) ELSE 0 
	END AS OverTimeHours

INTO
	#EmployeeHours
FROM
(
SELECT  
	EmployeeId,
	LocationId,
	PayeeName,
	--CASE WHEN RoleName='Washer' THEN ISNULL(TotalHours,0) ELSE 0 END AS TotalWashHours,
	--CASE WHEN RoleName='Detailer' THEN ISNULL(TotalHours,0) ELSE 0 END AS TotalDetailHours
	CASE WHEN RoleName='Washer' THEN ISNULL(CAST(TotH AS DECIMAL(18,2)),0) ELSE 0 END AS TotalWashHours,
	CASE WHEN RoleName='Detailer' THEN ISNULL(CAST(TotH AS DECIMAL(18,2)),0) ELSE 0 END AS TotalDetailHours
FROM #PayRoll
) TOLHours
LEFT JOIN
	tblLocation tbll
ON tbll.LocationId=TOLHours.locationId
GROUP BY EmployeeId,tbll.LocationId,Tbll.WorkhourThreshold,PayeeName

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
where  ehr.LocationId = @LocationId and ehr.IsActive = 1 and ehr.IsDeleted = 0
--WHERE 
--	tblCC.Category='CommisionType'


DROP TABLE IF EXISTS #DetailCommission

Select jse.EmployeeId, SUM(jse.CommissionAmount) as CommissionAmount INTO #DetailCommission from tblJobServiceEmployee jse
INNER JOIN tblTimeClock tc on tc.EmployeeId = jse.EmployeeId
where (tc.EventDate BETWEEN @StartDate AND @EndDate) AND (jse.CreatedDate BETWEEN @StartDate AND @EndDate)
GROUP BY jse.EmployeeId


DROP TABLE IF EXISTS #FinResult

SELECT 
	ER.*,
	--ER.EmployeeId,
	R.WashRate AS WashRate,
	R.DetailRate AS DetailRate,
	CASE WHEN ER.TotalWashHours < 40 THEN (ER.TotalWashHours* r.WashRate) ELSE (@HoursLimit * r.WashRate) END AS [WashAmount],
	DA.DetailCommission,
	R.Tip,
	--(ER.TotalDetaileHours* r.DetailRate) AS [Detail Total],
	--CASE	WHEN R.[Detail Desc]='Hourly' THEN (ER.TotalDetailHours* r.DetailRate) 
	--		WHEN R.[Detail Desc]='Flat Fee' THEN r.DetailRate
	--		WHEN R.[Detail Desc]='Percentage' THEN ((DA.DetailAmount* r.DetailRate)/100)
	--		END AS [DetailAmount],
	DC.CommissionAmount AS DetailAmount,
	CASE   WHEN ER.OverTimeHours > 0 THEN (@OverTimeWashRate * R.WashRate * ER.OverTimeHours) ELSE 0 END AS OverTimePay, 
	--WHEN ER.TotalWashesHours > @HoursLimit THEN CAST(((ER.TotalWashesHours - @HoursLimit) * @OverTimeWashRate) AS decimal(9,2)) ELSE 0 END AS OverTimePay,
	ISNULL(@CollisionAmount,0.00)  AS CollisionAmount,
	EC.Notes
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
	#EmployeeCollision EC 
ON	    EC.EmployeeId = ER.EmployeeId
LEFT JOIN 
	#DetailCommission DC
ON		DC.EmployeeId=ER.EmployeeId


--FinalOutput

Select DISTINCT FR.EmployeeId,PayeeName,LocationId,
FR.Notes,
--CASE WHEN SUM(ISNULL(FR.TotalWashHours,0))>@HoursLimit THEN @HoursLimit ELSE SUM(FR.TotalWashHours) END AS TotalWashHours,
IsNull(TotalWashHours,0) as TotalWashHours,
       IsNull(TotalDetailHours,0) as TotalDetailHours,IsNull(OverTimeHours,0) as OverTimeHours,IsNull(WashRate,0) as WashRate,
	   IsNull(DetailRate,0) as DetailRate,IsNull([WashAmount],0) as WashAmount,IsNull([DetailAmount],0) as DetailAmount,IsNull(OverTimePay,0) as OverTimePay,
	   IsNull(CollisionAmount,0.00) as CollisionAmount,IsNull(DetailCommission,0) as DetailCommission,IsNull(CA.Collision,0) as Collision,IsNull(Uniform,0) as Uniform,
	   IsNull(Adjustment,0) as Adjustment,
       ((ISNULL([WashAmount],0)+IsNull([OverTimePay],0)+IsNull([DetailCommission],0)--+[Tip] //has to add but its in varchar
	   +ISNULL([Adjustment],0))--+ISNULL([Uniform],0)
	   -ISNULL([Collision],0)) as PayeeTotal
       From #FinResult FR
LEFT JOIN
      #CodeValue CA ON
	     CA.EmployeeId = FR.EmployeeId

END