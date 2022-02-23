CREATE PROCEDURE [StriveCarSalon].[uspGetWashTimeByLocationId] 
(@LocationId int = null,
@DateTime datetime =null)
AS
--[StriveCarSalon].[uspGetWashTimeByLocationId] 1,'2021-08-24'
-- =============================================
-- 19-08-2021, Vetriselvi - Fixed Average wash time
-- 31-08-2021, Vetriselvi - Fixed Average wash count 
-- =============================================
BEGIN


DECLARE @DefaultWashTime INT = 25;
Declare @WashId INT = (Select valueid from GetTable('JobType') where valuedesc='Wash')
Declare @WashRole INT = (Select RoleMasterId from tblRoleMaster WHERE RoleName='Washer')
DECLARE @CompletedJobStatus INT = (SELECT valueid FROM GetTable('JobStatus') WHERE valuedesc='Completed')


DROP TABLE IF EXISTS #WashRoleCount
SELECT tblL.LocationId, COUNT(DISTINCT EmployeeId) Washer
INTO #WashRoleCount FROM tblTimeClock tblTC Left JOIN
tblLocation tblL ON(tblTC.LocationId = tblL.LocationId) 
--left JOIN tblJob tblJ ON(tblJ.LocationId = tblL.LocationId)
WHERE @DateTime between Cast(tblTC.InTime AS datetime) and Cast(tblTC.OutTime AS datetime)
and 
tblL.IsActive=1 AND ISNULL(tblL.IsDeleted,0)=0 
AND tblTC.IsActive=1 AND ISNULL(tblTC.IsDeleted,0)=0
--AND tblJ.IsActive=1 AND ISNULL(tblJ.IsDeleted,0)=0
AND tblTC.RoleId =@WashRole AND tblTC.EventDate = CONVERT(VARCHAR(10), @DateTime, 111)
--AND tblJ.JobType=@WashId 
GROUP BY tblL.LocationId


DROP TABLE IF EXISTS #CarsCount

Select tbll.LocationId, count(DISTINCT tblj.JobId) Cars into #CarsCount
from tblJob tblj
INNER JOIN tblLocation tbll on tbll.LocationId = tblj.LocationId
INNER join GetTable('JobStatus') GT on GT.valueid = tblj.JobStatus and GT.valuedesc = 'In Progress' 
WHERE ISNULL(tbll.IsActive, 1) = 1 AND ISNULL(tbll.IsDeleted, 0) = 0 AND ISNULL(tblj.IsDeleted, 0) = 0
AND tblj.JobType = @WashId AND tblj.JobDate = CONVERT(VARCHAR, @DateTime, 23)
--AND tblj.JobStatus=@CompletedJobStatus
GROUP by tbll.LocationId
 

DROP TABLE  IF EXISTS #WashTime

(SELECT tbll.LocationId,
CASE
	   WHEN ISNULL(wr.Washer,0) <=3 AND cc.Cars <=1 THEN @DefaultWashTime
	   WHEN ISNULL(wr.Washer,0) <=3 AND cc.Cars > 1 THEN (@DefaultWashTime+(cc.Cars - 1) * 8) + ((cc.Cars+ISNULL(tbllo.OffSet1,0))*ISNULL(tbllo.OffSet1On,0))
	   WHEN ISNULL(wr.Washer,0) <=6 AND cc.Cars <=1 THEN @DefaultWashTime
	   WHEN ISNULL(wr.Washer,0) <=6 AND cc.Cars >1 THEN (@DefaultWashTime+(cc.Cars - 1)*7) + ((cc.Cars+ISNULL(tbllo.OffSet1,0))*ISNULL(tbllo.OffSet1On,0))
	   WHEN ISNULL(wr.Washer,0) <=9 AND cc.Cars <=1 THEN @DefaultWashTime
	   WHEN ISNULL(wr.Washer,0) <=9 AND cc.Cars >1 THEN (@DefaultWashTime+(cc.Cars - 1)*6) + ((cc.Cars+ISNULL(tbllo.OffSet1,0))*ISNULL(tbllo.OffSet1On,0))
	   WHEN ISNULL(wr.Washer,0) <=11 AND cc.Cars <=3 THEN @DefaultWashTime
	   WHEN ISNULL(wr.Washer,0) <=11 AND cc.Cars >3 THEN (@DefaultWashTime+(cc.Cars - 3)*5) + ((cc.Cars+ISNULL(tbllo.OffSet1,0))*ISNULL(tbllo.OffSet1On,0))
	   WHEN ISNULL(wr.Washer,0) >=12 AND ISNULL(wr.Washer,0)<=15 AND cc.Cars <=5 THEN @DefaultWashTime
	   WHEN ISNULL(wr.Washer,0) >=12 AND ISNULL(wr.Washer,0)<=15 AND cc.Cars >5  THEN (@DefaultWashTime+(cc.Cars - 5)*3) + ((cc.Cars+ISNULL(tbllo.OffSet1,0))*ISNULL(tbllo.OffSet1On,0))
	   WHEN ISNULL(wr.Washer,0) >=16 AND ISNULL(wr.Washer,0)<=21 AND cc.Cars <=5 THEN @DefaultWashTime
	   WHEN ISNULL(wr.Washer,0) >=16 AND ISNULL(wr.Washer,0)<=21 AND cc.Cars >5  THEN (@DefaultWashTime+(cc.Cars - 6)*2) + ((cc.Cars+ISNULL(tbllo.OffSet1,0))*ISNULL(tbllo.OffSet1On,0))
	   WHEN ISNULL(wr.Washer,0) >=22 AND ISNULL(wr.Washer,0)<=26 AND cc.Cars <=5 THEN @DefaultWashTime
	   WHEN ISNULL(wr.Washer,0) >=22 AND ISNULL(wr.Washer,0)<=26 AND cc.Cars >5  THEN (@DefaultWashTime+(cc.Cars - 5)*2) + ((cc.Cars+ISNULL(tbllo.OffSet1,0))*ISNULL(tbllo.OffSet1On,0))
	   WHEN ISNULL(wr.Washer,0) >26 AND cc.Cars <=7 THEN @DefaultWashTime
	   WHEN ISNULL(wr.Washer,0) >26 AND cc.Cars >7  THEN (@DefaultWashTime+(cc.Cars - 7)*2) + ((cc.Cars+ISNULL(tbllo.OffSet1,0))*ISNULL(tbllo.OffSet1On,0))
	   WHEN ISNULL(wr.Washer,0) is NULL and cc.Cars is NULL THEN @DefaultWashTime
	   ELSE @DefaultWashTime
	   END AS WashTimeMinutes
	   INTO #WashTime
	   
	   FROM tblLocation tbll
LEFT JOIN #WashRoleCount wr ON(tbll.LocationId = wr.LocationId)
LEFT JOIN #CarsCount cc on tbll.LocationId = cc.LocationId
LEFT JOIN tblLocationOffSet tbllo ON(tbll.LocationId = tbll.LocationId)
WHERE ISNULL(tbll.IsActive,1) = 1 AND
ISNULL(tbll.IsDeleted,0) = 0 --AND ISNULL(tbllo.IsDeleted,0) = 0
)

SELECT distinct
       tbll.LocationId,
	   tbll.LocationType,
	   tbll.LocationName,
	   tbll.LocationDescription,
	   CONVERT(INT, ISNULL(wt.WashTimeMinutes, @DefaultWashTime)) AS WashTimeMinutes,
	   tbll.ColorCode,
	   tbll.IsFranchise,
	   tbll.TaxRate,
	   tbll.SiteUrl,
	   tbll.Currency,
	   tbll.Facebook,
	   tbll.Twitter,
	   tbll.Instagram,
	   tbll.WifiDetail,
	   tbll.WorkhourThreshold,
	   CONVERT(VARCHAR(5),tbll.StartTime,108) AS StartTime,
	   CONVERT(VARCHAR(5),tbll.EndTime,108) AS EndTime
	   

FROM [tblLocation]  tbll
LEFT JOIN #WashTime wt
ON(tbll.LocationId = wt.LocationId )
WHERE (tbll.LocationId = @LocationId OR @LocationId IS NULL) AND
isnull(IsActive,1) = 1 AND
isnull(isDeleted,0) = 0  

END