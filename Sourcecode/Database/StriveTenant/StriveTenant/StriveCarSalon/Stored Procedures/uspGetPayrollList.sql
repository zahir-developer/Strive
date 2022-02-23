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
Sample: [StriveCarSalon].[uspGetPayrollList]  1,'2022-02-06','2022-02-16'

-----------------------------------------------------------------------------------------

03-06-2021 | Vetriselvi OverTime Pay calculation and excluded overtime hours in total wash hours
2021-June-08  | Vetriselvi  | Added Location filter in Collision  
2021-Aug-26  | Vetriselvi  | Detail commision should be applied for the date detailer is allocated 
2021-Sep-07  | Vetriselvi  | Included tip amount
2021-Sep-29  | Vetriselvi  | Fixed bug in Tip Amount
2021-Oct-06  | Vetriselvi  | Fixed bug in Bonus
2021-Oct-20  | Vetriselvi  | Fixed bug in Cash Tip Amount
2021-Nov-02  | Vetriselvi  | Fixed arithmetic overflow error 
2021-Nov-10  | Vetriselvi  | hours should be in decimal format
2021-Nov-24  | Vetriselvi  | Bonus should be calculated to match the bonus settings and should be calculated at EOM
2021-Nov-30  | Vetriselvi  | Bonus range should be between between Min and Max
2022-Jan-05  | Zahir  | Employee clockout status added.
*/

as begin
  
DECLARE	@CollisionAmount DECIMAL(18,2)
DECLARE @HoursLimit INT = 40;
DECLARE @OverTimeWashRate DECIMAL(18,2) = 1.5;

DECLARE @CompletedJobStatus INT = (SELECT valueid FROM GetTable('JobStatus') WHERE valuedesc='Completed')
DECLARE @CompletedPaymentStatus INT = (SELECT valueid FROM GetTable('PaymentStatus') WHERE valuedesc='Success')
Declare @WashId INT = (Select valueid from GetTable('JobType') where valuedesc='Wash')
Declare @WashServiceId INT = (Select valueid from GetTable('ServiceType') where valuedesc='Wash Package')

Declare @Detail int = (select  valueid from gettable('jobtype') where valuedesc='Detail')
--Collision,Uniform,Adjusment 
Declare @noOfWk int = (select datediff(ww,@StartDate,@EndDate))

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
WHERE tblEL.LocationId = @LocationId
--WHERE tblEL.LiabilityDate BETWEEN @StartDate AND @EndDate

Drop table if exists #EmployeeCollision

Select EmployeeId, 
STUFF((SELECT  ', ' + Notes from #Category c
	WHERE c.EmployeeId = ct.EmployeeId
    FOR XML PATH('')
	), 1, 2, '')  AS Notes INTO #EmployeeCollision from #Category ct where Notes IS NOT NULL

Drop table if exists #CodeValue
Select EmployeeId,
	   --Notes,
	   SUM(IsNull(Collision,0))  As Collision,
       SUM(IsNull(Uniform,0))  As Uniform,
	   SUM(IsNull(Adjustment,0))  As Adjustment
	   INTO 
	   #CodeValue
	   from #Category 
	   GROUP BY EmployeeId
	   --,Notes


	   
DROP TABLE IF EXISTS #EmpNotClockedOut

Select DISTINCT tblemp.EmployeeId into #EmpNotClockedOut From tblEmployee tblemp
Inner join tblTimeClock tblTC ON tblemp.EmployeeId = tblTC.EmployeeId 
--AND tblTC.IsActive = 1 
AND ISNULL(tblTC.IsDeleted,0)=0
Inner join tblRoleMaster tblRM ON tblRM.RoleMasterId=tblTC.RoleId
Where tblTC.EventDate >=  @StartDate And  tblTC.EventDate <=@EndDate And tblTC.LocationId=@LocationId AND tbltc.OutTime is NULL


DROP TABLE IF EXISTS #PayRoll   
Select tblemp.EmployeeId,
		       tblemp.FirstName+' '+tblemp.LastName as PayeeName,
			   tblTC.LocationId,
			   tblRM.RoleName,
			   CONVERT(VARCHAR(8),InTime,108) AS InTime,
	           CONVERT(VARCHAR(8),ISNULL(OutTime,INTIME),108) AS OutTime,
			   DATEDIFF(HOUR,ISNULL(InTime,OutTime),ISNULL(OutTime,Intime)) AS TotalHours
			   ,CAST(DATEDIFF(MI, ISNULL(InTime,OutTime), ISNULL(OutTime,Intime)) as int) AS TotalHoursInMin 
		,CASE WHEN ISNULL(OutTime,'') = '' THEN 
	 '0'
 ELSE Replace(CONVERT(VARCHAR(5),DATEADD(minute, DATEDIFF(MI, ISNULL(InTime,OutTime), ISNULL(OutTime,InTime)), 0), 114),':','.')  
 END 
	AS TotH
	,tblTC.EventDate
			   INTO
			       #PayRoll

			   From tblEmployee tblemp
			        Inner join tblTimeClock tblTC ON tblemp.EmployeeId = tblTC.EmployeeId 
					--AND tblTC.IsActive = 1 
					AND ISNULL(tblTC.IsDeleted,0)=0
					Inner join tblRoleMaster tblRM ON tblRM.RoleMasterId=tblTC.RoleId
					Where tblTC.EventDate >=  @StartDate And  tblTC.EventDate <=@EndDate And tblTC.LocationId=@LocationId

					
DROP TABLE IF EXISTS #tempDates
;with cte as
(
  select @StartDate StartDate, 
    DATEADD(wk, DATEDIFF(wk, 0, @StartDate), 6) EndDate
  union all
  select dateadd(ww, 1, StartDate),
    dateadd(ww, 1, EndDate)
  from cte
  where dateadd(ww, 1, StartDate)<  @EndDate
)
SELECT * INTO #tempDates
FROM cte
	
DROP TABLE IF EXISTS #TempSalary
select DISTINCT p.EmployeeId,p.LocationId,t.StartDate ,ISNULL(er.HourlyRate,0) Salary
INTO #TempSalary
from #PayRoll p
join #tempDates t on p.EventDate between t.StartDate and t.EndDate
LEFT JOIN [tblEmployeeHourlyRate] er ON p.EmployeeId = er.EmployeeId AND p.LocationId = er.LocationId
join tblEmployeeDetail ed ON p.EmployeeId = ed.EmployeeId  
WHERE p.RoleName!='Detailer' AND p.RoleName!='Washer' 
AND ISNULL(ed.IsSalary,0) = 1
AND ISNULL(er.IsActive,0) = 1
AND ISNULL(er.IsDeleted,0) = 0
AND p.TotalHoursInMin > 0


DROP TABLE IF EXISTS #TotalSalary
SELECT EmployeeId,LocationId,SUM(Salary) Salary
INTO #TotalSalary
FROM #TempSalary
GROUP BY EmployeeId,LocationId

-- Detail Amount Sum

DROP TABLE IF EXISTS #DetailAmount
DROP TABLE IF EXISTS #FinalDetailAmount
SELECT DISTINCT
	  tblJSE.EmployeeId,tblJ.JobId
	, tblJI.Price DetailAmount 
	,ISNULL(tblJSE.CommissionAmount,0) as DetailCommission
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
JOIN tblTimeClock tc on tc.EmployeeId = tblJSE.EmployeeId  
JOIN tblRoleMaster tblRM  ON  tblRM.RoleMasterId=tc.RoleId and RoleName = 'Detailer'
WHERE tblJ.LocationId=@LocationId  AND tblJ.JobDate >= @StartDate AND tblJ.JobDate <= @EndDate AND tblCV.CodeValue='Detail' 
and tblj.IsDeleted = 0
and  (tc.EventDate BETWEEN @StartDate AND @EndDate) --AND (tblJSE.CreatedDate BETWEEN @StartDate AND @EndDate)
--GROUP BY tblJSE.EmployeeId

SELECT EmployeeId,SUM(DetailAmount) DetailAmount 
	,SUM(DetailCommission) as DetailCommission
INTO #FinalDetailAmount
FROM #DetailAmount
GROUP BY EmployeeId

-- #EmployeeHours


DROP TABLE IF EXISTS #EmpHour 
DROP TABLE IF EXISTS #EmployeeHours 

    SELECT 
	EmployeeId,
	tbll.LocationId,
	PayeeName,
	SUM(TotalWashHours) TotalWashesHours,	
	SUM(TotalDetailHours) TotalDetailHours	,	
	SUM(TotalOtherHours) TotalOtherHours	
INTO	#EmpHour
FROM
(
SELECT  
	EmployeeId,
	LocationId,
	PayeeName,
	CASE WHEN RoleName='Washer' THEN ISNULL(TotalHoursInMin,0) ELSE 0 END AS TotalWashHours,
	CASE WHEN RoleName='Detailer' THEN ISNULL(TotalHoursInMin,0) ELSE 0 END AS TotalDetailHours,
	CASE WHEN (RoleName!='Detailer' AND RoleName!='Washer' ) THEN ISNULL(TotalHoursInMin,0) ELSE 0 END AS TotalOtherHours
FROM #PayRoll
) TOLHours
LEFT JOIN
	tblLocation tbll
ON tbll.LocationId=TOLHours.locationId
GROUP BY EmployeeId,tbll.LocationId,Tbll.WorkhourThreshold,PayeeName

 SELECT 
	EmployeeId,
	LocationId,
	PayeeName,
	 TotalWashesHours,
	
	 TotalDetailHours,
	
		CASE WHEN TotalHours>@HoursLimit THEN (TotalHours - (TotalHours-@HoursLimit)) ELSE TotalHours  
		END TotalWashHours,
	CASE WHEN TotalHours>@HoursLimit THEN (TotalHours-@HoursLimit) ELSE 0 
	END AS OverTimeHours
	,TotalOtherHrs
INTO
	#EmployeeHours
FROM (
select EmployeeId,
	LocationId,
	PayeeName,
	 TotalWashesHours,
	CAST(TotalDetailHours/60.00 AS DECIMAL(18,2))
--	 CAST( CAST(TotalDetailHours/60 AS VARCHAR(10))+ '.'+ CASE WHEN TotalDetailHours%60 >=10 THEN CAST(TotalDetailHours%60 AS VARCHAR(10))
--ELSE CAST(FORMAT((TotalDetailHours%60), 'd2') AS VARCHAR(10)) END AS DECIMAL(18,2))
TotalDetailHours ,
--	CAST( CAST(TotalWashesHours/60 AS VARCHAR(10))+ '.'+ CASE WHEN TotalWashesHours%60 >=10 THEN CAST(TotalWashesHours%60 AS VARCHAR(10))
--ELSE CAST(FORMAT((TotalWashesHours%60), 'd2') AS VARCHAR(10)) END AS DECIMAL(18,2))
CAST(TotalWashesHours/60.00 AS DECIMAL(18,2))
AS TotalHours
,CAST(TotalOtherHours/60.00 AS DECIMAL(18,2)) AS TotalOtherHrs
	 from #EmpHour

) A

--Rate Calculation
DROP TABLE IF EXISTS #Rate
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
 and ISNULL(tblED.IsSalary,0) = 0
--WHERE 
--	tblCC.Category='CommisionType'


DROP TABLE IF EXISTS #DetailCommission

Select jse.EmployeeId, SUM(jse.CommissionAmount) as CommissionAmount INTO #DetailCommission from tblJobServiceEmployee jse
INNER JOIN tblTimeClock tc on tc.EmployeeId = jse.EmployeeId  
JOIN tblRoleMaster tblRM  ON  tblRM.RoleMasterId=tc.RoleId and RoleName = 'Detailer'
where (tc.EventDate BETWEEN @StartDate AND @EndDate) 
--AND (jse.CreatedDate BETWEEN @StartDate AND @EndDate)
--and tc.EventDate = convert(varchar, jse.CreatedDate, 23)	
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
	EC.Notes,
	ER.TotalOtherHrs * r.WashRate AS OtherAmount
INTO 
	#FinResult
FROM 
	#EmployeeHours ER
LEFT JOIN
	#Rate R
ON		R.EmployeeId=ER.EmployeeId
LEFT JOIN 
	#FinalDetailAmount DA
ON		DA.EmployeeId=ER.EmployeeId
LEFT JOIN 
	#EmployeeCollision EC 
ON	    EC.EmployeeId = ER.EmployeeId
LEFT JOIN 
	#DetailCommission DC
ON		DC.EmployeeId=ER.EmployeeId


--Bonus
DROP TABLE IF EXISTS #tblBonus
CREATE TABLE #tblBonus(id INT IDENTITY(1,1),BonusId INT,BonusMonth INT,BonusYear INT,NoOfBadReviews INT,
BadReviewDeductionAmount DECIMAL(18,2),
NoOfCollisions INT,
CollisionDeductionAmount DECIMAL(18,2))
INSERT INTO #tblBonus
SELECT BonusId ,BonusMonth,BonusYear,NoOfBadReviews,
BadReviewDeductionAmount,
NoOfCollisions,
CollisionDeductionAmount
FROM tblBonus WHERE 
BonusMonth=DATEPART(MONTH,@StartDate ) AND
BonusYear=DATEPART(YEAR,@StartDate ) 
AND LocationId = @LocationId AND IsActive=1 AND ISNULL(IsDeleted,0)=0

--select * from #tblBonus

DROP TABLE IF EXISTS #tblBonusRange
SELECT BonusRangeId
		,BonusId
		,Min
		,Max
		,BonusAmount
		,Total
INTO #tblBonusRange
FROM tblBonusRange
WHERE BonusId in (SELECT BonusId FROM #tblBonus)
AND IsActive = 1 AND ISNULL(IsDeleted,0)=0
--select * from #tblBonusRange

DROP TABLE IF EXISTS #EmpBonus
CREATE TABLE #EmpBonus (EmployeeId INT,Bonus DECIMAL(18,2))

 IF (  EOMONTH(@StartDate) between @StartDate and @EndDate)
   BEGIN

  	DECLARE @BonusId INT, @Month INT, @Year INT , @BonusPerHr DECIMAL(18,6),@TotalHrsLogged DECIMAL(18,2),@Bonus DECIMAL(18,2)
	
	SELECT @BonusId = BonusId,
	@Month = BonusMonth,
	@Year = BonusYear
	FROM #tblBonus 
  
 DROP TABLE IF EXISTS #FullPayRoll   
	Select tblemp.EmployeeId,
		tblemp.FirstName+' '+tblemp.LastName as PayeeName,
		tblTC.LocationId,
		tblRM.RoleName,
		CONVERT(VARCHAR(8),InTime,108) AS InTime,
	    CONVERT(VARCHAR(8),ISNULL(OutTime,INTIME),108) AS OutTime,
		DATEDIFF(HOUR,ISNULL(InTime,OutTime),ISNULL(OutTime,Intime)) AS TotalHours
		,CAST(DATEDIFF(MI, ISNULL(InTime,OutTime), ISNULL(OutTime,Intime)) as int) AS TotalHoursInMin 
		,CASE WHEN ISNULL(OutTime,'') = '' THEN  '0'
		ELSE Replace(CONVERT(VARCHAR(5),DATEADD(minute, DATEDIFF(MI, ISNULL(InTime,OutTime), ISNULL(OutTime,InTime)), 0), 114),':','.')  END AS TotH
		,tblTC.EventDate
		INTO #FullPayRoll
	From tblEmployee tblemp
	Inner join tblTimeClock tblTC ON tblemp.EmployeeId = tblTC.EmployeeId 
	--AND tblTC.IsActive = 1 
	AND ISNULL(tblTC.IsDeleted,0)=0
	Inner join tblRoleMaster tblRM ON tblRM.RoleMasterId=tblTC.RoleId
	Where 				
	DATEPART(MONTH, tblTC.EventDate ) = @Month
	AND DATEPART(YEAR, tblTC.EventDate ) = @Year
	And tblTC.LocationId=@LocationId
   

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
	--AND tblj.JobDate >= @StartDate AND tblj.JobDate <= @EndDate 
	AND tblj.LocationId = @LocationId
	AND tblj.IsActive=1 AND tblji.IsActive=1 AND tbls.IsActive=1 AND tbll.IsActive=1
	AND ISNULL(tblj.IsDeleted,0)=0 AND ISNULL(tblji.IsDeleted,0)=0 AND ISNULL(tbls.IsDeleted,0)=0
	AND ISNULL(tbll.IsDeleted,0)=0
	GROUP BY tbll.LocationId,tbll.LocationName

DROP TABLE IF EXISTS #EmpHours 

    SELECT 
	EmployeeId,
	tbll.LocationId,
	PayeeName,
	CAST((SUM(TotalWashHours)/60.00) AS DECIMAL(18,2)) TotalWashesHours
INTO
	#EmpHours
FROM
(
SELECT  
	EmployeeId,
	LocationId,
	PayeeName,
	CASE WHEN RoleName='Washer' THEN TotalHoursInMin ELSE 0 END AS TotalWashHours
FROM #FullPayRoll
WHERE DATEPART(MONTH,EventDate ) = @Month
AND DATEPART(YEAR,EventDate ) = @Year
) TOLHours
LEFT JOIN
	tblLocation tbll
ON tbll.LocationId=TOLHours.locationId
GROUP BY EmployeeId,tbll.LocationId,Tbll.WorkhourThreshold,PayeeName
select @TotalHrsLogged = SUM(ISNULL(TotalWashesHours,0)) from #EmpHours 

--
SELECT @Bonus = CASE WHEN (WC.WashCount >= BR.MIN AND WC.WashCount <= BR.MAX) then
ISNULL(BR.BonusAmount,0) - ((ISNULL(B.NoOfBadReviews,0) * ISNULL(B.BadReviewDeductionAmount,0)) + (ISNULL(B.NoOfCollisions,0) * ISNULL(B.CollisionDeductionAmount,0)))
 WHEN (WC.WashCount >= BR.MIN AND BR.MAX <= WC.WashCount ) then  BR.BonusAmount
ELSE 0
END 
From #tblBonusRange BR 
JOIN #tblBonus B ON B.BonusId = BR.BonusId
--join #EmpHours eh ON 1 =1
JOIN #WashCount WC ON WC.LocationId = @LocationId
where B.BonusId = @BonusId
and  (WC.WashCount >= BR.MIN AND WC.WashCount <= BR.MAX)

--select * from #WashCount

select @BonusPerHr = CASE WHEN @TotalHrsLogged > 0 THEN @Bonus / @TotalHrsLogged ELSE 0 END

INSERT INTO #EmpBonus
select EmployeeId, TotalWashesHours * @BonusPerHr 
FROM #EmpHours
--SELECT * FROM #EmpBonus
--	SET @StartBonus = @StartBonus + 1
--END
END

DROP TABLE IF EXISTS #FinalBonus
SELECT eb.EmployeeId,SUM(isnull(Bonus,0)) Bonus
INTO #FinalBonus 
FROM #EmpBonus eb
join tblEmployee tblemp on tblemp.EmployeeId = eb.EmployeeId
GROUP BY eb.EmployeeId
--select * from #FinalBonus

DROP TABLE IF EXISTS #TipsAmount
CREATE TABLE #TipsAmount(ID INT IDENTITY(1,1),LocationId INT,TipsAmount DECIMAL(18,2),JobDate DATETIME,WashType Varchar(20))
insert into #TipsAmount
SELECT j.LocationId,SUM(ISNULL(pd.Amount,0)) TipsAmount,j.JobDate,jt.valuedesc
--into #TipsAmount
From tblJob j 
join tblJobPayment p on j.JobPaymentId = p.JobPaymentId
JOIN tblJobPaymentDetail pd on pd.JobPaymentId = p.JobPaymentId
join tblCodeValue cv on cv.id = pd.PaymentType
INNER JOIN GetTable('JobType') jt on(j.JobType = jt.valueid) 
WHERE cv.CodeValue = 'Tips'
AND p.PaymentStatus=@CompletedPaymentStatus
AND j.JobStatus=@CompletedJobStatus
AND (CONVERT(DATE,p.CreatedDate)>=@StartDate AND CONVERT(DATE,p.CreatedDate)<=@EndDate)
AND j.IsDeleted = 0 AND j.LocationId = @LocationId
and ISNULL(p.IsRollBack,0) ! = 1
AND  (jt.valueid = @WashId OR jt.valueid = @Detail)
GROUP BY j.LocationId,j.JobDate,jt.valuedesc

--select * from #TipsAmount
DROP TABLE IF EXISTS #EmpLogged
;WITH Hours_Data
AS (
select 
E.EmployeeId,
TC.EventDate,

CASE WHEN ISNULL(OutTime,'') = '' THEN '0'
	 --CASE WHEN GETDATE() <= InTime THEN '0'
	 --ELSE 
	 -- --CAST(CONVERT(decimal(9,2), DateDiff(MINUTE,InTime, ISNULL(OutTime,GETDATE())))/60 as DECIMAL(9,2)) 
	 --		 CASE WHEN CAST(CONVERT(decimal(9,2), DateDiff(MINUTE,InTime, ISNULL(OutTime,GETDATE())))/60 as DECIMAL(9,2))  <= 24 
		--		THEN  CAST(CONVERT(decimal(9,2), DateDiff(MINUTE,InTime, ISNULL(OutTime,GETDATE())))/60 as DECIMAL(9,2))  else 24 end 
	 --END 
 ELSE CAST(CONVERT(decimal(18,2), DateDiff(MINUTE,InTime,OutTime))/60.00 as DECIMAL(18,2)) 
 END LoginTime
,RoleName
from tblTimeClock TC 
inner join tblEmployee E on TC.EmployeeId = E.EmployeeId
Inner join tblRoleMaster tblRM ON tblRM.RoleMasterId=TC.RoleId
where LocationId = @LocationId and EventDate >=@StartDate AND EventDate <=@EndDate  AND Tc.Isdeleted=0
--AND RoleName = 'Washer'
)

SELECT EmployeeId,EventDate, SUM(ISNULL(Logintime,0)) AS HoursPerDay  ,RoleName
INTO #EmpLogged
FROM Hours_Data
where Logintime >0 
and RoleName in ('Washer','Detailer')
Group by EmployeeId,EventDate,RoleName

--SELECT * FROM #EmpLogged
--order by EventDate

DECLARE @Start INT = 1,@Count INT 

DROP TABLE IF EXISTS #EmpTips 
create table #EmpTips(EmployeeId int,EventDate date,CashTip DECIMAL(18,2),WashTip  DECIMAL(18,2),DetailTip  DECIMAL(18,2),CardTip  DECIMAL(18,2))

DECLARE @CashRegisterTypeId INT = (  
Select CV.id from tblCodeCategory CC  
JOIN tblCodeValue CV on CV.CategoryId = CC.id  
WHERE CV.CodeValue = 'CLOSEOUT') 


INSERT INTO #TipsAmount
SELECT  LocationId,SUM(ISNULL(Tips,0) ),CAST(CashRegisterDate AS DATE),'CashTips'
FROM tblCashRegister
WHERE CashRegisterDate BETWEEN @StartDate AND @EndDate
--CAST(CashRegisterDate AS DATE) = CAST(@Date AS DATE) 
AND	CashRegisterType = @CashRegisterTypeId AND  
ISNULL(isDeleted,0) = 0 
and LocationId = @LocationId
AND ISNULL(Tips,0) >0
GROUP BY LocationId,CashRegisterDate


SELECT @Count = COUNT(1)
FROM #TipsAmount
 
 
WHILE(@Start <= @Count)
BEGIN

	DECLARE @Date DATETIME,@TotalHours DECIMAL(18,2),@RatePerHr DECIMAL(18,6),@TotalTip DECIMAL(18,2),@Type VARCHAR(20), @CashTips DECIMAL(18,2)
	
	declare @tip DECIMAL(18,6)
	SELECT @Date = JobDate,@TotalTip = isnull(TipsAmount,0),@Type =WashType
	FROM	#TipsAmount
	WHERE 	id = @Start

	SELECT @CashTips = TipsAmount
	FROM	#TipsAmount
	WHERE JobDate = @Date AND WashType = 'CashTips'
	
	IF(@Type = 'Wash')
	BEGIN
		SELECT @TotalHours = SUM(HoursPerDay)
		FROM	#EmpLogged
		WHERE EventDate = @Date
			AND RoleName = 'Washer'

			--select @Date,@CashRegisterTypeId
		
		--print @CashTips 
		--print @Date
		--print @CashRegisterTypeId
		if(@TotalHours >0)
		begin
		SELECT @RatePerHr = @TotalTip / @TotalHours
		end
		else
		begin
		SELECT @RatePerHr = 0
		end
		--select @Date,@TotalTip,@TotalHours,@CashTips,@RatePerHr
		
			
			declare @washCount INT

			select @washCount = COUNT(DISTINCT EmployeeId) FROM #EmpLogged WHERE EventDate = @Date AND RoleName = 'Washer'
			--select @washCount
			--select * FROM #EmpLogged
			
			--print @tip 
		IF(@washCount > 1)
		BEGIN
			insert into #EmpTips
			SELECT EmployeeId,EventDate,0,CAST(@RatePerHr * HoursPerDay  AS DECIMAL(18,2)),0,0
			FROM	#EmpLogged
			WHERE EventDate = @Date AND  RoleName = 'Washer'
		END
		ELSE
		BEGIN
			insert into #EmpTips
			SELECT EmployeeId,EventDate,0,CAST(@TotalTip   AS DECIMAL(18,2)),0,0
			FROM	#EmpLogged
			WHERE EventDate = @Date AND  RoleName = 'Washer'
		END
	
	END
	ELSE IF(@Type = 'Detail')
	BEGIN
		SELECT @TotalHours = 0,@RatePerHr = 0
		SELECT @TotalHours = SUM(HoursPerDay)
		FROM	#EmpLogged
		WHERE EventDate = @Date
			AND RoleName = 'Detailer'

		if(@TotalHours >0)
		begin
			SELECT @RatePerHr = @TotalTip / @TotalHours
		end
		else
		begin
			SELECT @RatePerHr = 0
		end
			--SELECT @RatePerHr,@TotalTip , @TotalHours
		IF((SELECT COUNT(DISTINCT EmployeeId) FROM #EmpLogged WHERE EventDate = @Date AND RoleName = 'Detailer') > 1)
		BEGIN
			insert into #EmpTips
			SELECT EmployeeId,EventDate,0,0,CAST(@RatePerHr * HoursPerDay  AS DECIMAL(18,2)),0
			FROM	#EmpLogged
			WHERE EventDate = @Date  AND RoleName = 'Detailer'
		END
		ELSE
		BEGIN
			insert into #EmpTips
			SELECT EmployeeId,EventDate,0,0,CAST(@TotalTip  AS DECIMAL(18,2)),0
			FROM	#EmpLogged
			WHERE EventDate = @Date AND RoleName = 'Detailer'
		END
	
	END
	ELSE IF(@Type = 'CashTips')
	BEGIN
		SELECT @TotalHours = SUM(HoursPerDay)
		FROM	#EmpLogged
		WHERE EventDate = @Date
			AND RoleName = 'Washer'

				
			declare @washerCount INT

			select @washerCount = COUNT(DISTINCT EmployeeId) FROM #EmpLogged WHERE EventDate = @Date AND RoleName = 'Washer'
			--select @washCount
			--select * FROM #EmpLogged
			if(@washCount>0)
			begin
			set @tip = @CashTips / @TotalHours
			
			end
			else
			begin
			set @tip = 0
			end
		
		IF(@washerCount > 1)
		BEGIN
			insert into #EmpTips
			SELECT EmployeeId,EventDate,CAST(@Tip * HoursPerDay  AS DECIMAL(18,2)),0,0,0
			FROM	#EmpLogged
			WHERE EventDate = @Date AND  RoleName = 'Washer'
		END
		ELSE
		BEGIN
			insert into #EmpTips
			SELECT EmployeeId,EventDate,@CashTips,0,0,0
			FROM	#EmpLogged
			WHERE EventDate = @Date AND  RoleName = 'Washer'
		END
	
	END

	SET @Start = @Start + 1
END

update #EmpTips
set CardTip = WashTip + DetailTip 

DROP TABLE IF EXISTS #FinalTip
SELECT EmployeeId,SUM(ISNULL(CashTip,0)) CashTip
,SUM(ISNULL( CardTip,0)) CardTip
,SUM(ISNULL(WashTip,0)) WashTip
,SUM(ISNULL(DetailTip ,0)) DetailTip
INTO #FinalTip
FROM #EmpTips
GROUP BY EmployeeId

DROP TABLE IF EXISTS #tblEmployee
SELECT DISTINCT EmployeeId, FirstName+' '+LastName as PayeeName
INTO #tblEmployee
FROM tblEmployee
WHERE EmployeeId in (select EmployeeId from #FinResult)
OR
EmployeeId in (select EmployeeId from #FinalBonus)

--FinalOutput
Select DISTINCT tble.EmployeeId,tble.PayeeName,@LocationId LocationId,
FR.Notes,
--CASE WHEN SUM(ISNULL(FR.TotalWashHours,0))>@HoursLimit THEN @HoursLimit ELSE SUM(FR.TotalWashHours) END AS TotalWashHours,
IsNull(TotalWashHours,0) +  IsNull(TotalOtherHrs,0) as TotalWashHours,
       IsNull(TotalDetailHours,0) as TotalDetailHours,
	   IsNull(OverTimeHours,0) as OverTimeHours,
	   CASE WHEN IsNull(TotalOtherHrs,0) > 0 THEN IsNull(WashRate,0)  + IsNull((ts.Salary/TotalOtherHrs),0) ELSE IsNull(WashRate,0) END as WashRate,
	   IsNull(DetailRate,0) as DetailRate,
	    IsNull([WashAmount],0) + IsNull(FR.OtherAmount,0) + IsNull(ts.Salary,0)  as WashAmount,
	   IsNull([DetailAmount],0) as DetailAmount,IsNull(OverTimePay,0) as OverTimePay,
	   IsNull(CollisionAmount,0.00) as CollisionAmount,IsNull(DetailCommission,0) as DetailCommission,IsNull(CA.Collision,0) as Collision,IsNull(Uniform,0) as Uniform,
	   IsNull(Adjustment,0) as Adjustment,
       ((ISNULL([WashAmount],0)+IsNull([OverTimePay],0)+IsNull([DetailCommission],0)--+[Tip] //has to add but its in varchar
	   +ISNULL([Adjustment],0))--+ISNULL([Uniform],0)
	   -ISNULL([Collision],0) + ISNULL(ft.CashTip,0) + ISNULL(ft.CardTip,0) + ISNULL(fb.Bonus,0)
	   + ISNULL(ts.Salary,0) + IsNull(FR.OtherAmount,0) 
	   ) as PayeeTotal
	   --,ISNULL(ft.Tip,0) Tip
		, ISNULL(CashTip,0)CashTip, ISNULL(CardTip,0)CardTip,(ISNULL(CashTip,0)+ISNULL(WashTip,0))WashTip,ISNULL(DetailTip,0)DetailTip
	   ,ISNULL(fb.Bonus,0) Bonus
	   ,CASE 
	   WHEN ISNULL(empClock.employeeId, 0) > 0 THEN 0
	   ELSE ISNULL(empClock.employeeId, 1) END as IsClockedOut
	   --,ISNULL(ts.Salary, 0) Salary
	  , TotalOtherHrs
       From #tblEmployee tble
	   LEFT JOIN #FinResult FR ON FR.EmployeeId = tble.EmployeeId
LEFT JOIN
      #CodeValue CA ON
	     CA.EmployeeId = FR.EmployeeId
LEFT JOIN #FinalTip ft on ft.EmployeeId = FR.EmployeeId
LEFT JOIN #FinalBonus fb on fb.EmployeeId = tble.EmployeeId
LEFT JOIN #EmpNotClockedOut empClock on empClock.EmployeeId = tble.EmployeeId
LEFT JOIN #TotalSalary ts on ts.EmployeeId = tble.EmployeeId

END
