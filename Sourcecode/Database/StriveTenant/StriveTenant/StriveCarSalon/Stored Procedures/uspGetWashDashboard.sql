

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


CREATE proc [StriveCarSalon].[uspGetWashDashboard] --2056,'2021-01-03',121
(
@LocationId int, @CurrentDate date, @JobType int,
@lastweek VARCHAR(10)= NULL,
@lastMonth VARCHAR(10) =NULL,
@lastThirdMonth  VARCHAR(10)= NULL)
as
begin 

Declare @WashRole INT = (Select RoleMasterId from tblRoleMaster WHERE RoleName='Wash')
Declare @WashId INT = (Select valueid from GetTable('JobType') where valuedesc='Wash')
Declare @WashServiceId INT = (Select valueid from GetTable('ServiceType') where valuedesc='Wash Package')
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
	isnull(tblj.IsDeleted,0)=0
	AND
	tblj.IsActive=1 
	GROUP BY tbll.LocationId

	DROP TABLE IF EXISTS #WashRoleCount
    SELECT tblL.LocationId,COUNT(tblTC.EmployeeId) Washer,COUNT(tblJ.JobId) CarCount
    INTO #WashRoleCount FROM tblTimeClock tblTC INNER JOIN
    tblLocation tblL ON(tblTC.LocationId = tblL.LocationId) 
    INNER JOIN tblJob tblJ ON(tblJ.LocationId = tblL.LocationId)
    WHERE tblL.IsActive=1 AND ISNULL(tblL.IsDeleted,0)=0 
    AND tblTC.IsActive=1 AND ISNULL(tblTC.IsDeleted,0)=0
    AND tblJ.IsActive=1 AND ISNULL(tblJ.IsDeleted,0)=0
    AND tblTC.RoleId =@WashRole  
	AND tblj.JobDate=@CurrentDate
    AND tblJ.JobType=@WashId 
	AND tblL.LocationId =@LocationId GROUP BY tblL.LocationId

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
	   WHEN wt.Washer <=3 AND wt.CarCount <=1 THEN 25
	   WHEN wt.Washer <=3 AND wt.CarCount >1 THEN (25+(wt.CarCount - 1)*8) + ((wt.CarCount+ISNULL(tbllo.OffSet1,0))*ISNULL(tbllo.OffSet1On,0))
	   WHEN wt.Washer <=6 AND wt.CarCount <=1 THEN 25
	   WHEN wt.Washer <=6 AND wt.CarCount >1 THEN (25+(wt.CarCount - 1)*7) + ((wt.CarCount+ISNULL(tbllo.OffSet1,0))*ISNULL(tbllo.OffSet1On,0))
	   WHEN wt.Washer <=9 AND wt.CarCount <=1 THEN 25
	   WHEN wt.Washer <=9 AND wt.CarCount >1 THEN (25+(wt.CarCount - 1)*6) + ((wt.CarCount+ISNULL(tbllo.OffSet1,0))*ISNULL(tbllo.OffSet1On,0))
	   WHEN wt.Washer <=11 AND wt.CarCount <=3 THEN 25
	   WHEN wt.Washer <=11 AND wt.CarCount >3 THEN (25+(wt.CarCount - 3)*5) + ((wt.CarCount+ISNULL(tbllo.OffSet1,0))*ISNULL(tbllo.OffSet1On,0))
	   WHEN wt.Washer >=12 AND wt.Washer<=15 AND wt.CarCount <=5 THEN 25
	   WHEN wt.Washer >=12 AND wt.Washer<=15 AND wt.CarCount >5  THEN (25+(wt.CarCount - 5)*3) + ((wt.CarCount+ISNULL(tbllo.OffSet1,0))*ISNULL(tbllo.OffSet1On,0))
	   WHEN wt.Washer >=16 AND wt.Washer<=21 AND wt.CarCount <=5 THEN 25
	   WHEN wt.Washer >=16 AND wt.Washer<=21 AND wt.CarCount >5  THEN (25+(wt.CarCount - 6)*2) + ((wt.CarCount+ISNULL(tbllo.OffSet1,0))*ISNULL(tbllo.OffSet1On,0))
	   WHEN wt.Washer >=22 AND wt.Washer<=26 AND wt.CarCount <=5 THEN 25
	   WHEN wt.Washer >=22 AND wt.Washer<=26 AND wt.CarCount >5  THEN (25+(wt.CarCount - 5)*2) + ((wt.CarCount+ISNULL(tbllo.OffSet1,0))*ISNULL(tbllo.OffSet1On,0))
	   WHEN wt.Washer >26 AND wt.CarCount <=7 THEN 25
	   WHEN wt.Washer >26 AND wt.CarCount >7  THEN (25+(wt.CarCount - 7)*2) + ((wt.CarCount+ISNULL(tbllo.OffSet1,0))*ISNULL(tbllo.OffSet1On,0))
	   ELSE 0
	   END AS AverageWashTime
FROM [StriveCarSalon].[tblLocation] tbll 
LEFT JOIN #WashesCount WC ON(tbll.LocationId = WC.LocationId)
LEFT JOIN #DetailsCount DC ON(tbll.LocationId = DC.LocationId)
LEFT JOIN #EmployeeCount EC ON(tbll.LocationId = EC.LocationId)
LEFT JOIN #TotalCarWashed tcw on(tbll.LocationId = tcw.LocationId)
LEFT JOIN #TotalHoursWashed thw on(tbll.LocationId = thw.LocationId)
LEFT JOIN #ForecastedCars fc on(tbll.LocationId = fc.LocationId)
LEFT JOIN #Current cu on(tbll.LocationId = cu.LocationId)
LEFT JOIN #WashRoleCount wt ON(tbll.LocationId = wt.LocationId)
LEFT JOIN tblLocationOffSet tbllo ON(tbll.LocationId = tbllo.LocationId)
WHERE tbll.LocationId =@LocationId

end
