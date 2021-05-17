--[StriveCarSalon].[uspGetWashTimeByLocationId] 2,'2021-03-03'
CREATE PROC [StriveCarSalon].[uspGetWashTimeByLocationId] 
(@LocationId int = null,
@DateTime datetime =null)
AS
BEGIN

Declare @WashId INT = (Select valueid from GetTable('JobType') where valuedesc='Wash')
Declare @WashRole INT = (Select RoleMasterId from tblRoleMaster WHERE RoleName='Washer')


DROP TABLE IF EXISTS #WashRoleCount
SELECT tblL.LocationId, COUNT(1) Washer
INTO #WashRoleCount FROM tblTimeClock tblTC Left JOIN
tblLocation tblL ON(tblTC.LocationId = tblL.LocationId) 
--left JOIN tblJob tblJ ON(tblJ.LocationId = tblL.LocationId)
WHERE tblTC.InTime <= @DateTime
AND tblTC.OutTime is NULL and 
tblL.IsActive=1 AND ISNULL(tblL.IsDeleted,0)=0 
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
GROUP by tbll.LocationId
 

DROP TABLE  IF EXISTS #WashTime

(SELECT tbll.LocationId,
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
	   wt.WashTimeMinutes,
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