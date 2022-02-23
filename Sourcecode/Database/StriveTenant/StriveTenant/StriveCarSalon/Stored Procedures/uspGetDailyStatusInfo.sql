-- =============================================
-- Author:		Zahir Hussain 
-- Create date: 03-12-2020
-- Description:	Gets the Daily Status - Detail Info and Wash Info 
 --[StriveCarSalon].[uspGetDailyStatusInfo] 1,'2021-10-13 22:58'
-- =============================================
-------------history-----------------
-- =============================================
-- 1  Shalini 2021-05-20  - Wash rate taken from employeehourlyrate table
-- 2  Shalini 2021-05-31  - Datediff modifed from hours to minutes
-- 3  Shalini 2021-06-03  - Round of wsash hours to 2 decimal points
-- 4  Vetriselvi 2021-06-11 - Calculating only hourly , not for min 
-- 5  Vetriselvi 2021-06-13  - Applied Location id for calculating wash count
-- 6  Vetriselvi 2021-09-29  - Fixed timeclock issue and was expense employeecount issue
-- 7  Vetriselvi 2021-10-06  - Removed the dynamic calculation in time calcuation
-- =============================================

CREATE PROCEDURE [StriveCarSalon].[uspGetDailyStatusInfo]
(@LocationId int = null,@Date Datetime)
AS
BEGIN

select L.LocationId,J.TicketNumber,SE.EmployeeId As EmployeeId, CONCAT(E.FirstName + ' ', E.LastName) as EmployeeName , SUM(ISNULL(SE.CommissionAmount,0)) as Commission
from tblLocation L
join tblJob J on J.LocationId = L.LocationId
left join tblJobItem JI on J.JobId = JI.JobId
--left join tblJobDetail  JS on JS.JobId = J.JobId
left join tblJobServiceEmployee SE on SE.JobItemId = ji.JobItemId
INNER join tblEmployee E on E.EmployeeId = SE.EmployeeId
left join GetTable('JobStatus') GT on GT.id = JobStatus and GT.valuedesc = 'Completed'
where L.LocationId = @LocationId and JobDate =  CAST(@Date AS DATE)  and j.isDeleted = 0--and Js.SalesRep is not null
group by L.LocationId,J.TicketNumber,SE.EmployeeId,E.FirstName, E.LastName


DROP Table IF EXISTS #tempWash
Select distinct tc.TimeClockId, e.EmployeeId,InTime, isnull(OutTime,@Date) OutTime,
CASE WHEN ISNULL(OutTime,'') = '' THEN  
	  CAST(Replace(CONVERT(VARCHAR(5),
	 CASE WHEN 
		CAST(InTime AS DATE) = CAST(@Date AS DATE) 
		 THEN DATEADD(minute, DATEDIFF(MI, InTime, ISNULL(OutTime,@Date)), 0)
		 ELSE 0
	 END
	 , 114),':','.') AS DECIMAL(9,2))
	  --DATEDIFF(MI, ISNULL(InTime,OutTime), ISNULL(OutTime,GETDATE()))
	
 ELSE CAST(Replace(CONVERT(VARCHAR(5),DATEADD(minute, DATEDIFF(MI, InTime, ISNULL(OutTime,InTime)), 0), 114),':','.')  AS DECIMAL(9,2))
 END 
	
--Cast(SUM(ISNULL(DateDiff(MINUTE,InTime,OutTime)/60.0, 0) * ISNULL(ehr.HourlyRate,0))as numeric(18,2)) 
--+ Cast(SUM((ISNULL(DateDiff(MINUTE,InTime,OutTime)%60.0, 0))/100 * ISNULL(ehr.HourlyRate,0))as numeric(18,2))

as WashExpense,
ISNULL(ehr.HourlyRate,0) HourlyRate
INTO #tempWash
from tblTimeClock tc 
INNER JOIN tblEmployee e on e.EmployeeId = tc.EmployeeId
INNER JOIN tblRoleMaster r on r.RoleMasterId = tc.RoleId and r.RoleName = 'Washer'
LEFT JOIN tblEmployeeDetail ed on e.EmployeeId = ed.EmployeeId 
left join  tblEmployeeHourlyRate ehr on e.EmployeeId=ehr.EmployeeId
where tc.EventDate = CAST(@Date AS DATE)  AND tc.LocationId =@LocationId
and tc.IsActive = 1 and tc.IsDeleted != 1
--GROUP BY tc.TimeClockId,e.EmployeeId,ehr.HourlyRate

--select * from #tempWash
SELECT COUNT(DISTINCT EmployeeId) AS WashEmployeeCount
, SUM( CAST(WashExpense AS DECIMAL(9,2)) * HourlyRate) WashExpense
FROM #tempWash
--GROUP BY WashExpense
 

END