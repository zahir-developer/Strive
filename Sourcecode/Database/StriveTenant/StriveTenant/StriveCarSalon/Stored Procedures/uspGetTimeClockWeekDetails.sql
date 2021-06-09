






CREATE PROCEDURE [StriveCarSalon].[uspGetTimeClockWeekDetails] --136,2056,'2021-01-03','2021-01-09'
@EmployeeId INT,
@LocationId INT,
@StartDate DATETIME,
@EndDate DATETIME
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
  2  |  2020-Sep-16   | Zahir		| Procedure Name changed. Column name changes added. Parameter name changes.
  3  |  2021-MAy-20   | Shalini		| Wash rate changed..taking from employeehourlyrate table
  4  |  2021-June-08  | Vetriselvi  | Added Location filter in Collision  
    
  
-----------------------------------------------------------------------------------------  
*/  
AS  
SET NOCOUNT ON  
  
BEGIN  
-- CollisionCategoryId Fetch  
DECLARE @CollisionAmount DECIMAL(9,2),@CollisionCategoryId INT,@CollisionPaymentId INT  
  
SELECT @CollisionCategoryId=tblCV.id   
FROM   
 tblCodeCategory tblCC   
JOIN   
 tblCodeValue tblCV   
ON  tblcc.id=tblcv.CategoryId  
WHERE tblCC.Category='LiabilityType' AND tblCV.CodeValue='Collision'  
  
SELECT @CollisionPaymentId=tblCV.id   
FROM   
 tblCodeCategory tblCC   
JOIN   
 tblCodeValue tblCV   
ON  tblcc.id=tblcv.CategoryId  
WHERE tblCC.Category='LiabilityDetailType' AND tblCV.CodeValue='Payment'  
  
--Login Details  
DROP TABLE IF EXISTS #TimeClock  
  
SELECT   
   EmployeeId  
 , LocationId  
 , TimeClockId  
 , tblRM.RoleMasterId AS RoleId  
 , DATENAME(DW,EventDate) AS [Day]  
 , EventDate  
 , CONVERT(VARCHAR(8),InTime,108) AS InTime  
 , CONVERT(VARCHAR(8),ISNULL(OutTime,INTIME),108) AS OutTime  
 , tblRM.RoleName  
 , DATEDIFF(HOUR,ISNULL(InTime,OutTime),ISNULL(OutTime,Intime)) AS TotalHours  
 ,CAST(DATEDIFF(MI, ISNULL(InTime,OutTime), ISNULL(OutTime,Intime)) as int) AS TotalHoursInMin   
 ,Replace(CONVERT(VARCHAR(5),DATEADD(minute, DATEDIFF(MI, ISNULL(InTime,OutTime), ISNULL(OutTime,Intime)), 0), 114),':','.')  AS TotH  
INTO  
 #TimeClock  
FROM   
 tblTimeClock tblTC  
 INNER JOIN tblRoleMaster tblRM  
ON  tblRM.RoleMasterId=tblTC.RoleId  
WHERE   
 EmployeeId=@EmployeeId  
AND LocationId=@LocationId   
AND EventDate BETWEEN @StartDate AND @EndDate  
AND  ISNULL(tblTC.IsDeleted,0) = 0  
  
  
-- Detail Amount Sum  
  
/*DROP TABLE IF EXISTS #DetailAmount  
  
SELECT   
   tblJI.EmployeeId  
 , SUM(tblJI.Price) DetailAmount  
INTO  
 #DetailAmount  
FROM   
 tblJobItem tblJI  
INNER JOIN  
 tblJob tblJ  
ON tblJ.JobId=tblJI.JobId  
INNER JOIN   
 tblCodeValue tblCV  
ON  tblCV.id=tblJ.JobType  
WHERE tblJI.EmployeeId = @EmployeeId AND tblJ.LocationId=@LocationId AND tblJ.JobDate BETWEEN @StartDate AND @EndDate AND tblCV.CodeValue='Detail'   
GROUP BY tblJI.EmployeeId  
*/  
  
--Detail Rate  
  
DROP TABLE IF EXISTS #DetailCommission  
  
Select jse.EmployeeId, SUM(jse.CommissionAmount) as CommissionAmount INTO #DetailCommission from tblJobServiceEmployee jse  
INNER JOIN tblTimeClock tc on tc.EmployeeId = jse.EmployeeId  
where (tc.EventDate BETWEEN @StartDate AND @EndDate) AND (jse.CreatedDate BETWEEN @StartDate AND @EndDate)  
GROUP BY jse.EmployeeId  
  
-- Rate Calculation  
DROP TABLE IF EXISTS #Rate  
SELECT   
   tblED.EmployeeId  
 ,ISNULL(ehr.HourlyRate,0)AS WashRate  
 --, ISNULL(tblED.WashRate,0)AS WashRate  
 , tblCV.CodeValue AS [Detail Desc]   
 , ISNULL(tblED.PayRate,0) as DetailRate  
INTO  
 #Rate  
FROM   
 tblEmployeeDetail tblED  
 left join  tblEmployeeHourlyRate ehr on tblED.EmployeeId=ehr.EmployeeId  
LEFT JOIN  
 tblCodeValue tblCV  
ON  tblCV.id=tblED.ComType  
LEFT JOIN  
 tblCodeCategory tblCC  
ON  tblCC.id=tblCV.CategoryId  
WHERE   
 tblED.EmployeeId=@EmployeeId --AND tblCC.Category='DetailCommission'   
 and ehr.LocationId = @LocationId and ehr.IsActive = 1 and ehr.IsDeleted = 0  
-- Rate Summary  
DROP TABLE IF EXISTS #EmployeeRate  
  
SELECT   
 EmployeeId,  
 tbll.LocationId,  
 ISNULL(tbll.WorkHourThreshold,0) WorkHourThreshold,  
 SUM(TotalWashHours) TotalWashHours,  
 SUM(TotalDetailHours ) TotalDetailHours,  
 CASE WHEN SUM(TotalWashHours)>ISNULL(tbll.WorkhourThreshold,0) THEN (SUM(TotalWashHours)-ISNULL(tbll.WorkhourThreshold,0)) ELSE 0   
 END AS OverTimeHours  
INTO  
 #EmployeeRate  
FROM  
(  
SELECT    
 EmployeeId,  
 LocationId,   
 --CASE WHEN RoleName='Wash' THEN ISNULL(TotalHours,0) ELSE 0 END AS TotalWashHours,  
 --CASE WHEN RoleName='Detailer' THEN ISNULL(TotalHours,0) ELSE 0 END AS TotalDetailHours  
 CASE WHEN RoleName='Washer' THEN ISNULL(CAST(TotH AS DECIMAL(18,2)),0) ELSE 0 END AS TotalWashHours,  
 CASE WHEN RoleName='Detailer' THEN ISNULL(CAST(TotH AS DECIMAL(18,2)),0) ELSE 0 END AS TotalDetailHours  
FROM #TimeClock  
) TOLHours  
LEFT JOIN  
 tblLocation tbll  
ON tbll.LocationId=TOLHours.locationId  
GROUP BY EmployeeId,tbll.LocationId,ISNULL(Tbll.WorkhourThreshold,0)  
  
  
--Collision Calculation  
  
SELECT @CollisionAmount =SUM(ISNULL(Amount,'0.00'))   
FROM   
 tblEmployeeLiability tblEL  
JOIN   
 tblEmployeeLiabilityDetail tblELD  
ON  tblEL.LiabilityId=tblELD.LiabilityId  
WHERE   
 tblEL.EmployeeId=@EmployeeId   
--AND tblEL.CreatedDate BETWEEN @StartDate AND @EndDate  
AND TblEL.LiabilityType=@CollisionCategoryId  
AND tblEL.LocationId = @LocationId
--AND tblELD.LiabilityDetailType=@CollisionPaymentId  
  
-- SummaryCalculation  
DROP TABLE IF EXISTS #FinResult  
  
SELECT   
 ER.*,  
 R.WashRate AS WashRate,  
 R.DetailRate AS DetailRate,  
 (ER.TotalWashHours * r.WashRate)  AS [WashAmount],  
 DC.CommissionAmount AS DetailAmount,  
 --(ER.TotalDetaileHours* r.DetailRate) AS [Detail Total],  
 --CASE WHEN R.[Detail Desc]='Hourly Rate' THEN (ER.TotalDetailHours* r.DetailRate)   
 --  WHEN R.[Detail Desc]='Flat Fee' THEN r.DetailRate  
 --  WHEN R.[Detail Desc]='Percentage' THEN ((DA.DetailAmount* r.DetailRate)/100)  
 --  END AS [DetailAmount],  
   
 ((ER.OverTimeHours*1.5)*r.WashRate) AS OverTimePay, ISNULL(@CollisionAmount,'0.00')  AS CollisionAmount  
INTO   
 #FinResult  
FROM   
 #EmployeeRate ER  
LEFT JOIN  
 #Rate R  
ON  R.EmployeeId=ER.EmployeeId  
LEFT JOIN   
 #DetailCommission DC  
ON  DC.EmployeeId=ER.EmployeeId  
  
-- Result  
  
SELECT TimeClockId,RoleId,[Day],EventDate,InTime,OutTime,RoleName,REPLACE(TotH,'.',':') AS 'TotalHours'  
,TotalHoursInMin  FROM #TimeClock  
  
  
SELECT TotalWashHours,TotalDetailHours,OverTimeHours,  
 --CONVERT(NUMERIC(18, 2), TotalWashHours/ 60 + (TotalWashHours% 60) / 100.0) AS TotalWashHours1,  
 --CONVERT(NUMERIC(18, 2), TotalDetailHours/ 60 + (TotalDetailHours% 60) / 100.0) AS TotalDetailHours1,  
 --CONVERT(NUMERIC(18, 2), OverTimeHours/ 60 + (OverTimeHours% 60) / 100.0) AS OverTimeHours1,  
 WorkHourThreshold,WashRate,DetailRate,ISNULL(WashAmount,'0.00')WashAmount,  
 ISNULL(DetailAmount,'0.00')DetailAmount,  
 ISNULL(OverTimePay,'0.00')OverTimePay,  
 ISNULL(CollisionAmount,'0.00')CollisionAmount  
 ,((ISNULL(WashAmount,'0.00')+ISNULL(DetailAmount,'0.00')+ISNULL(OverTimePay,'0.00'))-ISNULL(CollisionAmount,'0.00')) AS GrandTotal  
INTO #Result  
FROM   
 #FinResult  
  
SELECT --TotalWashHours1,TotalDetailHours1,OverTimeHours1,  
Replace(TotalWashHours,'.',':') AS TotalWashHours,REPLACE(TotalDetailHours,'.',':') AS TotalDetailHours,  
 WorkHourThreshold,Replace(OverTimeHours,'.',':')AS OverTimeHours, WashRate,DetailRate,[WashAmount],[DetailAmount],OverTimePay,CollisionAmount,GrandTotal   
 FROM #Result  
END  