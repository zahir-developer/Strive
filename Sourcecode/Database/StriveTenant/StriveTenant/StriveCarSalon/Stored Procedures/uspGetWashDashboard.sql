




-- =============================================
-- Author:		Vineeth
-- Create date: 20-08-2020
-- Description:	Dashboard detail for Washes
-- =============================================

---------------------History--------------------
-- =============================================
-- 10-09-2020, Vineeth - Added IsActive condition
--                       and JobType as params
--						 Going to use for Detail 
--                       also

------------------------------------------------
-- =============================================


CREATE proc [StriveCarSalon].[uspGetWashDashboard] --[StriveCarSalon].[uspGetWashDashboard]1,'2021-03-23',121
(@LocationId int, @CurrentDate date, @JobType int)
as
begin 

Declare @WashRole INT = (Select RoleMasterId from tblRoleMaster WHERE RoleName='Washer')
Declare @WashId INT = (Select valueid from GetTable('JobType') where valuedesc='Wash')
Declare @WashServiceId INT = (Select valueid from GetTable('ServiceType') where valuedesc='Washes')
Declare @CompletedJobStatus INT = (Select valueid from GetTable('JobStatus') where valuedesc='Completed')

DROP TABLE IF EXISTS #WashesCount
SELECT 
	tbll.LocationId,COUNT(1) AS WashesCount
	INTO #WashesCount 
	FROM 
	[StriveCarSalon].[tblJob] tblj
	INNER JOIN [StriveCarSalon].GetTable('JobType') jt ON(tblj.JobType=jt.valueid)
	INNER JOIN [StriveCarSalon].tblLocation tbll ON(tblj.LocationId = tbll.LocationId)
	WHERE jt.valuedesc='Wash'
	AND tblj.LocationId=@LocationId
	AND tblj.JobDate=@CurrentDate 
	AND ISNULL(tblj.IsDeleted,0)=0 
	AND tblj.IsActive=1 
	GROUP BY tbll.Locationid

DROP TABLE IF EXISTS #DetailsCount
SELECT  
	tbll.LocationId,COUNT(1) AS DetailsCount
	INTO #DetailsCount 
	FROM [StriveCarSalon].[tblJob] tblj
	INNER JOIN [StriveCarSalon].GetTable('JobType') jt ON(tblj.JobType=jt.valueid)
	INNER JOIN [StriveCarSalon].tblLocation tbll ON(tblj.LocationId = tbll.LocationId)
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
	FROM [StriveCarSalon].[tblJobItem] tblji
	INNER JOIN [StriveCarSalon].[tblJob] tblj ON(tblji.JobId = tblj.JobId)
	INNER JOIN [StriveCarSalon].tblLocation tbll ON(tblj.LocationId = tbll.LocationId)
	WHERE tblj.JobType=@JobType
	AND tblj.LocationId=@LocationId
	AND tblj.JobDate =@CurrentDate
	AND isnull(tblji.IsDeleted,0)=0
	AND tblji.IsActive=1 GROUP BY tbll.LocationId

DROP TABLE  IF EXISTS #TotalCarWashed
(Select tblj.LocationId,Count(*) TotalCarWashed INTO #TotalCarWashed from tblJob tblj inner join tblJobItem tblji on(tblj.JobId =tblji.JobId)
inner join tblService tbls on(tblji.ServiceId=tbls.ServiceId) where tblj.JobType=@WashId and tbls.ServiceType=@WashServiceId
and tblj.JobStatus=@CompletedJobStatus and tblj.IsActive=1 and tblji.IsActive=1 and tbls.IsActive=1 and ISNULL(tblj.IsDeleted,0)=0 and ISNULL(tblji.IsDeleted,0)=0 and 
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
	FROM [StriveCarSalon].[tblJob] tblj
	INNER JOIN [StriveCarSalon].[tblLocation] tbll ON(tblj.LocationId = tbll.LocationId)
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
	FROM [StriveCarSalon].[tblJob] tblj
	inner join [StriveCarSalon].GetTable('JobStatus') js ON(tblj.JobStatus = js.valueid)
	INNER JOIN [StriveCarSalon].tblLocation tbll ON(tblj.LocationId = tbll.LocationId)
	WHERE 
	tblj.LocationId=@LocationId
	AND
	tblj.JobDate=@CurrentDate
	AND 
	tblj.JobType=@JobType
	AND
	js.valuedesc='Completed'
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
AND tblTC.RoleId =@WashRole AND tblTC.EventDate = GETDATE() 
--AND tblJ.JobType=@WashId 
GROUP BY tblL.LocationId


DROP TABLE IF EXISTS #CarsCount

Select tbll.LocationId, count(1) Cars into #CarsCount
from tblJob tblj
INNER JOIN tblLocation tbll on tbll.LocationId = tblj.LocationId
INNER join GetTable('JobStatus') GT on GT.id = tblj.JobStatus and GT.valuedesc = 'In Progress' and tblj.JobType = @WashId
WHERE ISNULL(tbll.IsActive, 1) = 1 AND ISNULL(tbll.IsDeleted, 0) = 0 AND ISNULL(tblj.IsDeleted, 0) = 0
AND tblj.JobType = @WashId AND tblj.JobDate = GETDATE()
GROUP by tbll.LocationId ORDER BY 1 DESC

SELECT 
ISNULL(WC.WashesCount,0) WashesCount,
ISNULL(DC.DetailsCount,0) DetailsCount,
ISNULL(EC.EmployeeCount,0) EmployeeCount,
ISNULL(FC.ForecastedCars,0) ForecastedCars,
ISNULL(cu.[Current],0) [Current],
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
FROM [StriveCarSalon].[tblLocation] tbll 
LEFT JOIN #WashesCount WC ON(tbll.LocationId = WC.LocationId)
LEFT JOIN #DetailsCount DC ON(tbll.LocationId = DC.LocationId)
LEFT JOIN #EmployeeCount EC ON(tbll.LocationId = EC.LocationId)
LEFT JOIN #TotalCarWashed tcw on(tbll.LocationId = tcw.LocationId)
LEFT JOIN #TotalHoursWashed thw on(tbll.LocationId = thw.LocationId)
LEFT JOIN #ForecastedCars fc on(tbll.LocationId = fc.LocationId)
LEFT JOIN #Current cu on(tbll.LocationId = cu.LocationId)
LEFT JOIN #WashRoleCount wr ON(tbll.LocationId = wr.LocationId)

LEFT JOIN #CarsCount cc on (tbll.LocationId = cc.LocationId)
LEFT JOIN tblLocationOffSet tbllo ON(tbll.LocationId = tbllo.LocationId)
WHERE tbll.LocationId =@LocationId

end
