--[StriveCarSalon].[uspGetTimeClockWeekDetails] 1137,1,'2021-08-29','2021-09-04'
    
CREATE PROCEDURE [StriveCarSalon].[uspGetTimeClockWeekDetails]   
@EmployeeId INT,  
@LocationId INT,  
@StartDate DATETIME,  
@EndDate DATETIME  
/*  
-----------------------------------------------------------------------------------------  
Author              : Lenin  
Create date         : 14-SEP-2020  
Description         : To Get TimeClock Details for an Employee for a Week  
FRS     : TimeClock Maintainance  
-----------------------------------------------------------------------------------------  
 Rev | Date Modified | Developer | Change Summary  
-----------------------------------------------------------------------------------------  
  1  |  2020-Sep-01   | Lenin  | Added RollBack for errored transaction   
  2  |  2020-Sep-16   | Zahir  | Procedure Name changed. Column name changes added. Parameter name changes.  
  3  |  2021-MAy-20   | Shalini  | Wash rate changed..taking from employeehourlyrate table  
  4  |  2021-June-08  | Vetriselvi  | Added Location filter in Collision    
  5  |  2021-June-22  | Vetriselvi  | Added Location filter in detail commission 
  6  |  2021-June-22  | Vetriselvi  | Set default WorkHourThreshold as 40 if WorkHourThreshold is empty
  7  |  2021-August-17  | Vetriselvi  | Added the condition that only active employee details are obtained
  8  |  2021-Aug-26  | Vetriselvi  | Detail commision should be applied for the date detailer is allocated
  9  |  2021-Aug-30  | Vetriselvi  | Detail commision remove duplicate records, ignored deleted recors
  10 |  2021-Sep-02  | Vetriselvi  | FixedDetail commision same as in payroll
    
  
-----------------------------------------------------------------------------------------  
*/  
AS  
SET NOCOUNT ON  
  
BEGIN  
-- CollisionCategoryId Fetch  
DECLARE @CollisionAmount DECIMAL(9,2),@CollisionCategoryId INT,@CollisionPaymentId INT  ,@WorkHourThreshold DECIMAL(9,2),@RoleId INT

SELECT @RoleId = RoleMasterId FROM tblRoleMaster WHERE RoleName = 'Detailer'

SELECT @WorkhourThreshold = ISNULL(WorkhourThreshold,40) FROM tblLocation
WHERE LocationId = @LocationId
  
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
   tblTC.EmployeeId  
 , LocationId  
 , TimeClockId  
 , tblRM.RoleMasterId AS RoleId  
 , DATENAME(DW,EventDate) AS [Day]  
 , EventDate  
 , CONVERT(VARCHAR(8),InTime,108) AS InTime  
 , CONVERT(VARCHAR(8),OutTime,108) AS OutTime  
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
JOIN tblEmployee tblE ON tblE.EmployeeId = tblTC.EmployeeId
WHERE   
 tblTC.EmployeeId=@EmployeeId  
AND LocationId=@LocationId   
AND EventDate BETWEEN @StartDate AND @EndDate  
AND  ISNULL(tblTC.IsDeleted,0) = 0  
AND  ISNULL(tblE.IsDeleted,0) = 0  
  
  
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

  /*
Select jse.EmployeeId, SUM(jse.CommissionAmount) as CommissionAmount INTO #DetailCommission from tblJobServiceEmployee jse  
INNER JOIN tblTimeClock tc on tc.EmployeeId = jse.EmployeeId   
JOIN tblRoleMaster tblRM  ON  tblRM.RoleMasterId=tc.RoleId 
--where (CONVERT(date, tc.EventDate) BETWEEN '2021-06-20' AND '2021-06-26') AND (CONVERT(date,jse.CreatedDate) BETWEEN '2021-06-20' AND '2021-06-26')  
--and jse.EmployeeId = 1057 and  tc.EmployeeId = 1057 and RoleName = 'Detailer' and LocationId = 3
where (tc.EventDate BETWEEN @StartDate AND @EndDate) AND (jse.CreatedDate BETWEEN @StartDate AND @EndDate)  
and RoleName = 'Detailer' and LocationId = @LocationId
and tc.EventDate = convert(varchar, jse.CreatedDate, 23)	
and tc.IsDeleted != 1
GROUP BY jse.EmployeeId  */

DROP TABLE IF EXISTS #DetailAmount
SELECT DISTINCT JobServiceEmployeeId,
	  tblJSE.EmployeeId,tblJSE.CommissionAmount
	--,SUM(ISNULL(tblJSE.CommissionAmount,0)) as CommissionAmount
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
JOIN tblTimeClock TC ON tblJSE.EmployeeId = TC.EmployeeId AND  TC.EventDate between  @StartDate  AND @EndDate and tc.EmployeeId = @EmployeeId  and tc.RoleId = @RoleId
WHERE tblJ.LocationId=@LocationId  AND tblJ.JobDate >= @StartDate AND tblJ.JobDate <= @EndDate AND tblCV.CodeValue='Detail' 
and tblj.IsDeleted = 0 and tblJSE.EmployeeId = @EmployeeId 

  
  SELECT 
	  EmployeeId,SUM(ISNULL(CommissionAmount,0)) as CommissionAmount
INTO
	#DetailCommission
FROM #DetailAmount
GROUP BY EmployeeId

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
JOIN tblEmployee tblE ON tblE.EmployeeId = tblED.EmployeeId  
WHERE   
 tblED.EmployeeId=@EmployeeId --AND tblCC.Category='DetailCommission'   
 and ehr.LocationId = @LocationId and ehr.IsActive = 1 and ehr.IsDeleted = 0   
AND  ISNULL(tblE.IsDeleted,0) = 0  

-- Rate Summary  
DROP TABLE IF EXISTS #EmployeeRate  
  
SELECT   
 EmployeeId,  
 @LocationId LocationId,  
 @WorkHourThreshold WorkHourThreshold,  
 SUM(TotalWashHours) TotalWashHours,  
 SUM(TotalDetailHours ) TotalDetailHours,  
 CASE WHEN SUM(TotalWashHours)>@WorkHourThreshold THEN (SUM(TotalWashHours)-@WorkHourThreshold) ELSE 0   
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
GROUP BY EmployeeId
  
  
--Collision Calculation  
  
SELECT @CollisionAmount =SUM(ISNULL(Amount,'0.00'))   
FROM   
 tblEmployeeLiability tblEL  
JOIN   
 tblEmployeeLiabilityDetail tblELD  
ON  tblEL.LiabilityId=tblELD.LiabilityId  
JOIN tblEmployee tblE ON tblE.EmployeeId = tblEL.EmployeeId  
WHERE   
 tblEL.EmployeeId=@EmployeeId   
--AND tblEL.CreatedDate BETWEEN @StartDate AND @EndDate  
AND TblEL.LiabilityType=@CollisionCategoryId  
AND tblEL.LocationId = @LocationId
AND  ISNULL(tblE.IsDeleted,0) = 0  
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