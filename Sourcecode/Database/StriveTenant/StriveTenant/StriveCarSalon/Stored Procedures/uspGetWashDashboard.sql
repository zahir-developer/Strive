-- =============================================
-- Author:		Vineeth
-- Create date: 20-08-2020
-- Description:	Dashboard detail for Washes
-- =============================================

---------------------History--------------------
-- =============================================
-- 10-09-2020, Vineeth - Added IsActive condition and JobType as params
-- 12-05-2021, Shalini - Added isdeleted condition and removed group by from #JobWashes
-- 19-05-2021, Shalini - Added jobtype filter to #JobWashes
------------------------------------------------
--[StriveCarSalon].[uspGetWashDashboard]20,'2021-03-18',57,'2021-03-11','2021-02-18' ,'2017-11-16' 
--[StriveCarSalon].[uspGetWashDashboard]1,'2021-05-01',57,'2021-03-11','2021-02-18' ,'2017-11-16' 
--[StriveCarSalon].[uspGetWashDashboard]1,'2021-05-13',57,'2021-03-11','2021-02-18' ,'2017-11-16' 
-- =============================================

CREATE PROCEDURE [StriveCarSalon].[uspGetWashDashboard] 
(@LocationId int, @CurrentDate date, @JobType int,@lastweek VARCHAR(10)= NULL,
@lastMonth VARCHAR(10) =NULL,
@lastThirdMonth  VARCHAR(10)= NULL)
as
begin 

Declare @WashRole INT = (Select RoleMasterId from tblRoleMaster WHERE RoleName='Washer')
Declare @WashId INT = (Select valueid from GetTable('JobType') where valuedesc='Wash')
Declare @WashServiceId INT = (Select valueid from GetTable('ServiceType') where valuedesc='Wash package')
Declare @CompletedJobStatus INT = (Select valueid from GetTable('JobStatus') where valuedesc='Completed')
Declare @DetailerRole int =(Select RoleMasterId from tblRoleMaster WHERE RoleName='Detailer')

DROP TABLE IF EXISTS #JobWashes
Select j.JobDate, j.locationId into #JobWashes from tblJob j 
INNER JOIN GetTable('JobType') JT on JT.valueid = j.JobType
--INNER join GetTable('JobStatus') GT on GT.valueId = j.JobStatus and GT.valuedesc = 'Completed'
WHERE j.JobDate in(@CurrentDate,@lastweek,@lastMonth,@lastThirdMonth) and j.Locationid=@LocationId and j.JobType=@WashId
and j.IsActive=1 and isnull(j.IsDeleted,0)=0
--GROUP BY JobDate, j.LocationId

DROP TABLE IF EXISTS #WashesCount
SELECT 
	w.LocationId,COUNT(1) AS WashesCount
	INTO #WashesCount 
	FROM #JobWashes w	
	where w.JobDate=@CurrentDate
	
	GROUP BY w.Locationid

DROP TABLE IF EXISTS #DetailsCount
SELECT  
	tbll.LocationId,COUNT(1) AS DetailsCount
	INTO #DetailsCount 
	FROM [tblJob] tblj
	INNER JOIN GetTable('JobType') jt ON(tblj.JobType=jt.valueid)
	INNER JOIN tblLocation tbll ON(tblj.LocationId = tbll.LocationId)
	WHERE jt.valuedesc='Detail' AND
	tblj.LocationId=@LocationId AND
	tblj.JobDate=@CurrentDate AND
    isnull(tblj.IsDeleted,0)=0 AND
	tblj.IsActive=1 
	GROUP BY tbll.LocationId

DROP TABLE IF EXISTS #EmployeeCount
SELECT
	tbll.LocationId,COUNT(distinct  tblji.EmployeeId) AS EmployeeCount
	INTO #EmployeeCount 
	FROM [tblJobItem] tblji
	INNER JOIN [tblJob] tblj ON(tblji.JobId = tblj.JobId)
	INNER JOIN tblLocation tbll ON(tblj.LocationId = tbll.LocationId)
	WHERE tblj.JobType=@JobType
	AND tblj.LocationId=@LocationId
	AND tblj.JobDate =@CurrentDate
	AND isnull(tblji.IsDeleted,0)=0
	AND tblji.IsActive=1 GROUP BY tbll.LocationId

DROP TABLE  IF EXISTS #TotalCarWashed
(Select tblj.LocationId,Count(*) TotalCarWashed INTO #TotalCarWashed from tblJob tblj inner join tblJobItem tblji on(tblj.JobId =tblji.JobId)
inner join tblService tbls on(tblji.ServiceId=tbls.ServiceId) where tblj.JobType=@WashId and tbls.ServiceType=@WashServiceId
and tblj.JobStatus=@CompletedJobStatus
 and tblj.IsActive=1 and tblji.IsActive=1 and tbls.IsActive=1 and ISNULL(tblj.IsDeleted,0)=0 and ISNULL(tblji.IsDeleted,0)=0 and 
ISNULL(tbls.IsDeleted,0)=0 
AND (tblj.JobDate=@CurrentDate) 
AND tblj.LocationId=@LocationId 
GROUP BY tblj.LocationId)


DROP TABLE  IF EXISTS #TotalHoursWashed
(Select tblj.LocationId,SUM(CAST((CAST(DATEDIFF(MINUTE, TimeIn,ActualTimeOut)AS decimal(9,2))/60) AS decimal(9,2))) TotalHoursWashed
INTO #TotalHoursWashed from tblJob tblj inner join tblJobItem tblji on(tblj.JobId =tblji.JobId)
inner join tblService tbls on(tblji.ServiceId=tbls.ServiceId) where tblj.JobType=@WashId and tbls.ServiceType=@WashServiceId
and tblj.IsActive=1 and tblji.IsActive=1 and tbls.IsActive=1 and ISNULL(tblj.IsDeleted,0)=0 and ISNULL(tblji.IsDeleted,0)=0 and 
ISNULL(tbls.IsDeleted,0)=0
AND tblj.JobDate=@CurrentDate 
AND tblj.LocationId=@LocationId 
GROUP BY tblj.LocationId)

DROP TABLE IF EXISTS #ForecastedCars
SELECT
    tbll.LocationId,COUNT(distinct VehicleId) AS ForecastedCars
	INTO #ForecastedCars 
	FROM [tblJob] tblj
	INNER JOIN [tblLocation] tbll ON(tblj.LocationId = tbll.LocationId)
	WHERE 
	tblj.JobType=@JobType
	AND
	tblj.LocationId=@LocationId
	AND
	JobDate=@CurrentDate
	AND 
	isnull(tblj.IsDeleted,0)=0
	AND
	tblj.IsActive=1 GROUP BY tbll.LocationId

DROP TABLE IF EXISTS #Current
SELECT
    tbll.LocationId,COUNT(VehicleId) AS [Current]
	INTO #Current 
	FROM [tblJob] tblj
	inner join GetTable('JobStatus') js ON(tblj.JobStatus = js.valueid)
	INNER JOIN tblLocation tbll ON(tblj.LocationId = tbll.LocationId)
	WHERE 
	tblj.LocationId=@LocationId
	AND
	tblj.JobDate=@CurrentDate
	AND 
	tblj.JobType=@JobType
	--AND	js.valuedesc='Completed'
	AND
	isnull(tblj.IsDeleted,0)=0
	AND
	tblj.IsActive=1 
	GROUP BY tbll.LocationId
	
DROP TABLE IF EXISTS #WashRoleCount
SELECT tblL.LocationId, COUNT(1) Washer
INTO #WashRoleCount FROM tblTimeClock tblTC Left JOIN
tblLocation tblL ON(tblTC.LocationId = tblL.LocationId) 
--left JOIN tblJob tblJ ON(tblJ.LocationId = tblL.LocationId)
WHERE tblL.IsActive=1 AND ISNULL(tblL.IsDeleted,0)=0 
AND tblTC.IsActive=1 AND ISNULL(tblTC.IsDeleted,0)=0
--AND tblJ.IsActive=1 AND ISNULL(tblJ.IsDeleted,0)=0
AND tblTC.RoleId =@WashRole AND tblTC.EventDate =@CurrentDate-- GETDATE() 
--AND tblJ.JobType=@WashId 
GROUP BY tblL.LocationId

	
DROP TABLE IF EXISTS #DetailRoleCount
SELECT tblL.LocationId, COUNT(1) Detailer
INTO #DetailRoleCount FROM tblTimeClock tblTC Left JOIN
tblLocation tblL ON(tblTC.LocationId = tblL.LocationId) 
--left JOIN tblJob tblJ ON(tblJ.LocationId = tblL.LocationId)
WHERE tblL.IsActive=1 AND ISNULL(tblL.IsDeleted,0)=0 
AND tblTC.IsActive=1 AND ISNULL(tblTC.IsDeleted,0)=0
--AND tblJ.IsActive=1 AND ISNULL(tblJ.IsDeleted,0)=0
AND tblTC.RoleId =@DetailerRole AND tblTC.EventDate =@CurrentDate-- GETDATE() 
--AND tblJ.JobType=@WashId 
GROUP BY tblL.LocationId


DROP TABLE IF EXISTS #CarsCount

Select tbll.LocationId, count(1) Cars into #CarsCount
from tblJob tblj
INNER JOIN tblLocation tbll on tbll.LocationId = tblj.LocationId
INNER join GetTable('JobStatus') GT on GT.valueid = tblj.JobStatus and GT.valuedesc = 'In Progress' and tblj.JobType = @WashId
WHERE ISNULL(tbll.IsActive, 1) = 1 AND ISNULL(tbll.IsDeleted, 0) = 0 AND ISNULL(tblj.IsDeleted, 0) = 0
AND tblj.JobType = @WashId AND tblj.JobDate = @CurrentDate-- GETDATE()
GROUP by tbll.LocationId ORDER BY 1 DESC

--Forcasted Cars and Employeehours
DROP TABLE IF EXISTS #WashTime
DROP TABLE IF EXISTS #Forcasted
SELECT 
WP.Weather,
WP.RainProbability,
convert (decimal(18,2),(ISNULL(wc.WashesCount,0)*1.5)) AS WashTimeMinutes,
a.JobDate ,
WP.LocationId,
WP.CreatedDate into #WashTime
FROM [tblWeatherPrediction] WP
LEFT join #JobWashes a on CONVERT(VARCHAR(10), wp.CreatedDate, 120) = a.JobDate
Left Join #washesCount wc on wp.LocationId =wc.LocationId
WHERE
WP.LocationId =@LocationId AND CONVERT(VARCHAR(10), wp.CreatedDate, 120) in (@CurrentDate,@lastweek,@lastMonth,@lastThirdMonth) 
and wp.Weather IS NOT NULL AND WP.RainProbability IS nOT NULL 
ORDER BY CreatedDate DESC
DECLARE @AvgCount INT = (Select count(1) from #WashTime WHERE WashTimeMinutes >0 )
DECLARE @Normal DECIMAL(18,2) = (Select SUM(CONVERT(DECIMAL(18,2),WashTimeMinutes))/@AvgCount from #WashTime)
DECLARE @Today_RainPrecipitation int = (select top 1 RainProbability from #WashTime where CONVERT(VARCHAR(10), CreatedDate, 120)  =@CurrentDate)
DECLARE @Loc int = (select top 1 LocationId from #WashTime)
DEclare @Formula decimal(18,2) =(select top (1)Formula from tblForcastedRainPercentageMaster fr 
where @Today_RainPrecipitation between fr.PrecipitationRangeFrom and fr.PrecipitationRangeTo)
select @AvgCount as Average,@Normal as Normal, Round(@Normal * @Formula,0) as ForcastedemployeeHours,@Loc as LocationId
,ISNULL(Round((@Normal * @Formula) / 1.25,0),0) as ForcastedCars ,@Today_RainPrecipitation as RainPrecipitation into #Forcasted


SELECT 
ISNULL(WC.WashesCount,0) WashesCount,
ISNULL(DC.DetailsCount,0) DetailsCount,
ISNULL(EC.EmployeeCount,0) EmployeeCount,
IsNull(wr.Washer,0) as WasherCount,
IsNull(dr.Detailer,0) as DetailerCount,
--ISNULL(FC.ForecastedCars,0) ForecastedCars,
ISNULL(cu.[Current],0) [Current],

ISNULL(f.ForcastedCars,0) ForecastedCars,
ISNULL(f.ForcastedemployeeHours,0) ForecastedEmployeeHours,
CASE 
	       WHEN thw.TotalHoursWashed='0.00' THEN ISNULL(tcw.TotalCarWashed,0)
		   WHEN thw.TotalHoursWashed!='0.00' THEN CAST((ISNULL(tcw.TotalCarWashed,0)/thw.TotalHoursWashed)as decimal(9,2))
		   END AS Score,
	CASE
	   WHEN wr.Washer <=3 AND cc.Cars <=1 THEN 25
	   WHEN wr.Washer <=3 AND cc.Cars > 1 THEN (25+(cc.Cars - 1) * 8) + ((cc.Cars+tbllo.OffSet1)*tbllo.OffSet1On)
	   WHEN wr.Washer <=6 AND cc.Cars <=1 THEN 25
	   WHEN wr.Washer <=6 AND cc.Cars >1 THEN (25+(cc.Cars - 1)*7) + ((cc.Cars+tbllo.OffSet1)*tbllo.OffSet1On)
	   WHEN wr.Washer <=9 AND cc.Cars <=1 THEN 25
	   WHEN wr.Washer <=9 AND cc.Cars >1 THEN (25+(cc.Cars - 1)*6) + ((cc.Cars+tbllo.OffSet1)*tbllo.OffSet1On)
	   WHEN wr.Washer <=11 AND cc.Cars <=3 THEN 25
	   WHEN wr.Washer <=11 AND cc.Cars >3 THEN (25+(cc.Cars - 3)*5) + ((cc.Cars+tbllo.OffSet1)*tbllo.OffSet1On)
	   WHEN wr.Washer >=12 AND wr.Washer<=15 AND cc.Cars <=5 THEN 25
	   WHEN wr.Washer >=12 AND wr.Washer<=15 AND cc.Cars >5  THEN (25+(cc.Cars - 5)*3) + ((cc.Cars+tbllo.OffSet1)*tbllo.OffSet1On)
	   WHEN wr.Washer >=16 AND wr.Washer<=21 AND cc.Cars <=5 THEN 25
	   WHEN wr.Washer >=16 AND wr.Washer<=21 AND cc.Cars >5  THEN (25+(cc.Cars - 6)*2) + ((cc.Cars+tbllo.OffSet1)*tbllo.OffSet1On)
	   WHEN wr.Washer >=22 AND wr.Washer<=26 AND cc.Cars <=5 THEN 25
	   WHEN wr.Washer >=22 AND wr.Washer<=26 AND cc.Cars >5  THEN (25+(cc.Cars - 5)*2) + ((cc.Cars+tbllo.OffSet1)*tbllo.OffSet1On)
	   WHEN wr.Washer >26 AND cc.Cars <=7 THEN 25
	   WHEN wr.Washer >26 AND cc.Cars >7  THEN (25+(cc.Cars - 7)*2) + ((cc.Cars+tbllo.OffSet1)*tbllo.OffSet1On)
	   WHEN wr.Washer is NULL and cc.Cars is NULL THEN 25
	   END AS  AverageWashTime
FROM [tblLocation] tbll 
LEFT JOIN #WashesCount WC ON(tbll.LocationId = WC.LocationId)
LEFT JOIN #DetailsCount DC ON(tbll.LocationId = DC.LocationId)
LEFT JOIN #EmployeeCount EC ON(tbll.LocationId = EC.LocationId)
LEFT JOIN #TotalCarWashed tcw on(tbll.LocationId = tcw.LocationId)
LEFT JOIN #TotalHoursWashed thw on(tbll.LocationId = thw.LocationId)
LEFT JOIN #ForecastedCars fc on(tbll.LocationId = fc.LocationId)
LEFT JOIN #Current cu on(tbll.LocationId = cu.LocationId)
LEFT JOIN #WashRoleCount wr ON(tbll.LocationId = wr.LocationId)

LEFT JOIN #DetailRoleCount dr ON(tbll.LocationId = dr.LocationId)
LEFT JOIN #Forcasted f on(tbll.LocationId = f.LocationId)
LEFT JOIN #CarsCount cc on (tbll.LocationId = cc.LocationId)
LEFT JOIN tblLocationOffSet tbllo ON(tbll.LocationId = tbllo.LocationId)
WHERE tbll.LocationId =@LocationId

end
