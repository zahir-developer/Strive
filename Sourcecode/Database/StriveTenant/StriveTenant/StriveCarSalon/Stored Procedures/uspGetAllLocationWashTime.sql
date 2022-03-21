
--[StriveCarSalon].[uspGetAllLocationWashTime] 0,'2022-01-07 20:56:00.000'  
  
-- =============================================================  
-- Author:         Zahir Hussain   
-- Created date:   2020-03-18  
-- Description:    Returns all locations with washtime  
-- Example: [StriveCarSalon].[uspGetAllLocationWashTime] 0,'2022-01-07 20:56:00.000'  
-- =============================================================  
  
-- =============================================================  
----------------------------History-----------------------------  
-- =============================================================  
-- 06-09-2021, Zahir - Location store open/closed status added.  
-- 09-09-2021, Vetriselvi - Updated average wash time.    
-- 14-09-2021, Zahir - Fixed duplicate Issue.  
--      - Added, location Address, Email, Phone  
-- 01-10-2021, Vetriselvi - Updated average wash time.    
-- 07-10-2021, Vetriselvi - Updated average wash time. When no washer clocked in or store is closed wash time is 0    
-- 15-10-2021, Vetriselvi - Updated average wash time. store should be closed on the time updated   
-- 18-10-2021, Vetriselvi - Fixed - if store is closed avg time should be zero   
-- 29-10-2021, Vetriselvi - Open the store only on the current time is greater than store in time  
-- 15-11-2021, Vetriselvi - Washer count as in dashboard  
-- 16-11-2021, Vetriselvi - Added new case to handle the case if the store is closed and still have time left then show  wash time  
-- 29-11-2021, Vetriselvi - Included Off set in wash time calculation  
-- 01-12-2021, Vetriselvi - Fixed avg wash time included offset  
-- 22-12-2021, Vetriselvi - Fixed avg wash time bug  
-- 17-01-2022, Vetriselvi - Fixed avg wash time bug  
  
----------------------------------------------------------------  
-- =============================================================  
  
CREATE PROCEDURE [StriveCarSalon].[uspGetAllLocationWashTime]  
 (@LocationId int = NULL, @Date DATETIME = NULL)  
AS   
BEGIN  
  
Declare @WashId INT = (Select valueid from GetTable('JobType') where valuedesc='Wash')  
  
Declare @WashRole INT = (Select RoleMasterId from tblRoleMaster WHERE RoleName='Washer')  
  
Declare @CashRegisterType INT = (Select top 1 valueid from GetTable('CashRegisterType') where valuedesc='CashIn')  
  
DROP TABLE IF EXISTS #WashRoleCount  
--SELECT tblL.LocationId, COUNT(1) Washer  
--INTO #WashRoleCount FROM tblTimeClock tblTC Left JOIN  
--tblLocation tblL ON(tblTC.LocationId = tblL.LocationId)   
----left JOIN tblJob tblJ ON(tblJ.LocationId = tblL.LocationId)  
--WHERE tblL.IsActive=1 AND ISNULL(tblL.IsDeleted,0)=0   
--AND tblTC.IsActive=1 AND ISNULL(tblTC.IsDeleted,0)=0  
----AND tblJ.IsActive=1 AND ISNULL(tblJ.IsDeleted,0)=0  
--AND tblTC.RoleId =@WashRole AND tblTC.EventDate = GETDATE()   
----AND tblJ.JobType=@WashId   
--GROUP BY tblL.LocationId  
  
SELECT tblL.LocationId, COUNT(DISTINCT EmployeeId) Washer  
INTO #WashRoleCount FROM tblTimeClock tblTC   
JOIN tblLocation tblL ON (tblTC.LocationId = tblL.LocationId)   
WHERE  --Cast(tblTC.InTime AS datetime) >= @FromDate and Cast(tblTC.OutTime AS datetime) <=@ToDate and   
 tblTC.IsActive=1 AND ISNULL(tblTC.IsDeleted,0)=0  
AND tblTC.RoleId =@WashRole AND tblTC.EventDate= CAST( @Date AS DATE)  
--AND (( @CurrentDate BETWEEN tblTC.InTime AND tblTC.OutTime ) or  
--and  @Date  >= tblTC.InTime and (tblTC.OutTime IS NULL or tblTC.OutTime <= @Date )  
 AND tblTC.EventDate>=Cast(@Date AS date) AND (tblTC.EventDate<=Cast(@Date AS date) or tblTC.EventDate is null)  
GROUP BY tblL.LocationId  
  
DROP TABLE IF EXISTS #CarsCount  
  
Select tblj.LocationId, count(DISTINCT tblj.JobId) CarCount into #CarsCount  
from tblJob tblj  
JOIN tblLocation tblL ON (tblj.LocationId = tblL.LocationId)   
INNER join GetTable('JobStatus') GT on GT.valueid = tblj.JobStatus and GT.valuedesc = 'In Progress'   
WHERE ISNULL(tblj.IsDeleted, 0) = 0  
AND tblj.JobType = @WashId AND tblj.JobDate =CAST( @Date AS DATE)  
GROUP by tblj.LocationId  
   
   
--Location Open/Closed status  
DROP TABLE IF EXISTS #StoreStatus  
  
Select cr.locationId, cr.CashRegisterId,   
cr.StoreTimeIn,  
cr.StoreTimeOut,  
cv.CodeValue as StoreStatus into #StoreStatus  
from tblCashRegister cr  
LEFT JOIN tblCodeValue cv on cv.id = cr.StoreOpenCloseStatus   
WHERE cr.CashRegisterDate = CAST( @Date AS DATE) and cr.CashRegisterType = @CashRegisterType  
and ISNULL(cr.IsDeleted,0) = 0  
  
DROP TABLE  IF EXISTS #WashTime  
  
(SELECT tbll.LocationId,  
CASE  
    WHEN ISNULL(wt.Washer,0) = 0 THEN 0  
    WHEN ISNULL(wt.Washer,0) <=3 AND car.CarCount <=1 THEN 25  
    WHEN ISNULL(wt.Washer,0) <=3 AND car.CarCount >1 THEN (25+(car.CarCount - 1)*8) + (((car.CarCount - 1)*ISNULL(tbllo.OffSet1,0))*ISNULL(tbllo.OffSet1On,0))  
    WHEN ISNULL(wt.Washer,0) <=6 AND car.CarCount <=1 THEN 25  
    WHEN ISNULL(wt.Washer,0) <=6 AND car.CarCount >1 THEN (25+(car.CarCount - 1)*7) + (((car.CarCount - 1)*ISNULL(tbllo.OffSet1,0))*ISNULL(tbllo.OffSet1On,0))  
    WHEN ISNULL(wt.Washer,0) <=9 AND car.CarCount <=1 THEN 25  
    WHEN ISNULL(wt.Washer,0) <=9 AND car.CarCount >1 THEN (25+(car.CarCount - 1)*6) + (((car.CarCount - 1)*ISNULL(tbllo.OffSet1,0))*ISNULL(tbllo.OffSet1On,0))  
    WHEN ISNULL(wt.Washer,0) <=11 AND car.CarCount <=3 THEN 25  
    WHEN ISNULL(wt.Washer,0) <=11 AND car.CarCount >3 THEN (25+(car.CarCount - 3)*5) + (((car.CarCount - 3)*ISNULL(tbllo.OffSet1,0))*ISNULL(tbllo.OffSet1On,0))  
    WHEN ISNULL(wt.Washer,0) >=12 AND ISNULL(wt.Washer,0)<=15 AND car.CarCount <=5 THEN 25  
    WHEN ISNULL(wt.Washer,0) >=12 AND ISNULL(wt.Washer,0)<=15 AND car.CarCount >5  THEN (25+(car.CarCount - 5)*3) + (((car.CarCount - 5)*ISNULL(tbllo.OffSet1,0))*ISNULL(tbllo.OffSet1On,0))  
    WHEN ISNULL(wt.Washer,0) >=16 AND ISNULL(wt.Washer,0)<=21 AND car.CarCount <=5 THEN 25  
    WHEN ISNULL(wt.Washer,0) >=16 AND ISNULL(wt.Washer,0)<=21 AND car.CarCount >5  THEN (25+(car.CarCount - 5)*2) + (((car.CarCount - 5)*ISNULL(tbllo.OffSet1,0))*ISNULL(tbllo.OffSet1On,0))  
    WHEN ISNULL(wt.Washer,0) >=22 AND ISNULL(wt.Washer,0)<=26 AND car.CarCount <=5 THEN 25  
    WHEN ISNULL(wt.Washer,0) >=22 AND ISNULL(wt.Washer,0)<=26 AND car.CarCount >5  THEN (25+(car.CarCount - 5)*2) + (((car.CarCount - 5)*ISNULL(tbllo.OffSet1,0))*ISNULL(tbllo.OffSet1On,0))  
    WHEN ISNULL(wt.Washer,0) >26 AND car.CarCount <=7 THEN 25  
    WHEN ISNULL(wt.Washer,0) >26 AND car.CarCount >7  THEN (25+(car.CarCount - 7)*2) + (((car.CarCount - 7)*ISNULL(tbllo.OffSet1,0))*ISNULL(tbllo.OffSet1On,0))  
    ELSE 25  
END AS WashTimeMinutes,wt.Washer  
INTO #WashTime  
FROM tblLocation tbll  
LEFT JOIN #WashRoleCount wt ON (tbll.LocationId = wt.LocationId)  
LEFT JOIN #CarsCount car on tbll.LocationId = car.LocationId  
LEFT JOIN tblLocationOffSet tbllo ON (tbll.LocationId = tbllo.LocationId)  
WHERE ISNULL(tbllo.IsActive,1) = 1 AND  
ISNULL(tbllo.IsDeleted,0) = 0 --AND ISNULL(tbllo.IsDeleted,0) = 0  
)  
  
  
 DROP TABLE  IF EXISTS #WashServices  
(   
SELECT distinct tbllo.LocationId, SUM(  
CASE   
 WHEN tbls.ServiceName ='Wash Upcharges (A)' THEN ( 0 + ISNULL(tbllo.OffSetA,0))*ISNULL(tbllo.OffSet1On,0)  
 WHEN tbls.ServiceName ='Wash Upcharges (B)' THEN ( 1 + ISNULL(tbllo.OffSetB,0))*ISNULL(tbllo.OffSet1On,0)  
 WHEN tbls.ServiceName ='Wash Upcharges (C)' THEN ( 2 + ISNULL(tbllo.OffSetC,0))*ISNULL(tbllo.OffSet1On,0)  
 WHEN tbls.ServiceName ='Wash Upcharges (D)' THEN ( 3 + ISNULL(tbllo.OffSetD,0))*ISNULL(tbllo.OffSet1On,0)  
 WHEN tbls.ServiceName ='Wash Upcharges (E)' THEN ( 4 + ISNULL(tbllo.OffSetE,0))*ISNULL(tbllo.OffSet1On,0)  
 WHEN tbls.ServiceName ='Wash Upcharges (F)' THEN ( 4 + ISNULL(tbllo.OffSetF,0))*ISNULL(tbllo.OffSet1On,0)  
 ELSE 0 END) AS WashTimeMinutes  
 INTO #WashServices  
FROM tblJob tblj inner join tblJobItem tblji on(tblj.JobId = tblji.JobId)   
inner join tblService tblS on(tblji.ServiceId = tbls.ServiceId)  
inner join GetTable('ServiceType') serviceType on serviceType.valueid = tbls.ServiceType  
LEFT JOIN tblLocationOffSet tbllo ON(tblj.LocationId = tbllo.LocationId) and tbllo.IsActive = 1 AND  
 tbllo.isDeleted = 0  
LEFT JOIN #StoreStatus ss ON ss.LocationId = tblj.LocationId  
INNER join GetTable('JobStatus') GT on GT.valueid = tblj.JobStatus and GT.valuedesc = 'In Progress' and tblj.JobType = @WashId  
AND tblj.JobDate = Cast(@Date AS date) AND ISNULL(tblj.IsDeleted,0) = 0  
WHERE serviceType.valuedesc like '%Upcharge%' AND ISNULL(tblji.IsDeleted,0) = 0  
GROUP BY tbllo.LocationId  
 )  
  
  
   
 DROP TABLE  IF EXISTS #AvgWashTime  
(SELECT wt.LocationId AS LocationId,  
ISNULL(wt.WashTimeMinutes,0) + ISNULL(ws.WashTimeMinutes,0) AS WashTimeMinutes  ,Washer
    INTO #AvgWashTime  
    FROM  #WashTime wt  
LEFT JOIN #WashServices ws  ON wt.LocationId = ws.LocationId  
LEFT JOIN tblLocationOffSet tbllo ON(ws.LocationId = tbllo.LocationId) and tbllo.IsActive = 1 AND  
tbllo.isDeleted = 0    
 )  
  
--select * from #StoreStatus  
SELECT DISTINCT  
       tbll.LocationId,  
    tbll.LocationName,  
    STUFF((SELECT Distinct ', ' + le.EmailAddress    
    FROM [tblLocationEmail] le  
 WHERE le.LocationId = tbll.LocationId and IsDeleted = 0  
    FOR XML PATH('')  
 ), 1, 2, '')  AS Email,  
    la.Address1,  
    la.Address2,  
    la.PhoneNumber,  
    CASE WHEN ISNULL(wt.Washer,0) = 0 THEN 0
	WHEN (ss.StoreStatus = 'Open'  AND ss.StoreTimeIn <= @Date) THEN  wt.WashTimeMinutes  
    WHEN ((ss.StoreStatus = 'Closed' OR ss.StoreStatus = 'Closed for Weather') AND ss.StoreTimeOut <= @Date) THEN 0    
     WHEN ((ss.StoreStatus = 'Closed' OR ss.StoreStatus = 'Closed for Weather') AND ss.StoreTimeOut >= @Date) THEN wt.WashTimeMinutes  
    WHEN ss.StoreStatus IS NULL AND ss.StoreTimeOut IS NULL THEN 0  
      ELSE 0 END   
    WashTimeMinutes,  
     CASE WHEN (ss.StoreStatus = 'Open'  AND ss.StoreTimeIn <= @Date) THEN ss.StoreStatus   
  WHEN ((ss.StoreStatus = 'Closed' OR ss.StoreStatus = 'Closed for Weather') AND ss.StoreTimeOut <= @Date) THEN ss.StoreStatus  
  WHEN ((ss.StoreStatus = 'Closed' OR ss.StoreStatus = 'Closed for Weather') AND ss.StoreTimeOut >= @Date) THEN 'Open'  
    WHEN ss.StoreStatus IS NULL AND ss.StoreTimeOut IS NULL THEN  'Closed'  
  ELSE 'Closed' END StoreStatus,  
    ss.StoreTimeIn,  
    ss.StoreTimeOut,  
    la.Latitude,  
    la.Longitude  
FROM [tblLocation]  tbll  
LEFT JOIN #AvgWashTime wt  
ON(tbll.LocationId = wt.LocationId)  
LEFT JOIN #StoreStatus ss on tbll.LocationId = ss.LocationId  
LEFT JOIN tblLocationAddress la ON(tbll.LocationId = la.LocationId)  
WHERE (tbll.LocationId = @locationId OR @locationId = 0 OR @LocationId is NULL) AND  
isnull(tbll.IsActive,1) = 1 AND  
isnull(tbll.isDeleted,0) = 0    
  
END