---------------------History---------------------------
-- ====================================================
-- 11-jun-2021, Vetriselvi - Credit card tips showing wrongly  
-- 16-Sep-2021, Vetriselvi - Added detial tips too
-------------------------------------------------------

 --[StriveCarSalon].[uspGetTipDetail] 1,'2021-12-29'
CREATE PROCEDURE [StriveCarSalon].[uspGetTipDetail]
@locationId INT, @date DATE
AS
Begin


DECLARE @WashTips DECIMAL(18,2);

DECLARE @DetailTips DECIMAL(18,2);

Declare @Wash int = (select  valueid from gettable('jobtype') where valuedesc='Wash')

Declare @Detail int = (select  valueid from gettable('jobtype') where valuedesc='Detail')
Declare @Tips int = (select  valueid from gettable('PaymentType') where valuedesc='Tips')
DECLARE @CompletedJobStatus INT = (SELECT valueid FROM GetTable('JobStatus') WHERE valuedesc='Completed')
DECLARE @CompletedPaymentStatus INT = (SELECT valueid FROM GetTable('PaymentStatus') WHERE valuedesc='Success')
--DECLARE @LocationId INT = 1

DROP Table IF EXISTS #TotalJobs
Select Top 100 
tblj.LocationId, tblj.JobDate,
tblj.jobid,jt.valueid as JobTypeId, jt.valueid,
tblj.JobPaymentId
into #TotalJobs
from [tblJob] tblj 
INNER JOIN GetTable('JobType') jt on(tblj.JobType = jt.valueid) 
INNER JOIN tblJobPayment jp on tblj.JobPaymentId = jp.JobPaymentId
WHERE tblj.LocationId = @LocationId AND
CONVERT(DATE,jp.CreatedDate) = @date and 
(jt.valueid = @Wash OR jt.valueid = @Detail)
and ISNULL(jp.IsRollBack,0) ! = 1
AND jp.PaymentStatus=@CompletedPaymentStatus
AND tblj.JobStatus=@CompletedJobStatus

--Select * from tblJobPayment where JobId in (Select JobId from #TotalJobs)
--Select * from #TotalJobs

DROP Table IF EXISTS #TotalJobTips
Select
tblj.locationId,
tblj.jobid,
tjpd.Amount as Tips,
tblj.valueid as JobType,
tbljp.JobPaymentId
into #TotalJobTips
from #TotalJobs tblj 
inner join [tblJobPayment] tbljp on(tblj.JobPaymentId = tbljp.JobPaymentId)
inner join tblJobPaymentDetail tjpd on tjpd.JobPaymentId = tbljp.JobPaymentId AND tjpd.PaymentType = @Tips
--inner join GetTable('PaymentType') pt on(tjpd.PaymentType = pt.valueid)
--WHERE (pt.valuedesc = @Tips)--tips
--select * from #TotalJobTips
--Finalwashtips


DROP Table IF EXISTS #TotalTips
select distinct JobPaymentId,Tips,JobType
INTO #TotalTips
FROM #TotalJobTips

select @WashTips = ISNULL (sum(Tips),0.00) from #TotalTips tjp where tjp.JobType = @Wash --tjp.JobType ='Wash'

select @DetailTips = ISNULL (sum(Tips),0.00) from #TotalTips tjp where tjp.JobType = @Detail --tjp.JobType ='Wash'

Select @WashTips as WashTips, @DetailTips as DetailTips

--Detailtips
select 
se.EmployeeId,
te.FirstName,
te.LastName,
SUM(ISNULL(se.CommissionAmount,0)) as DetailerTip
from #TotalJobs jobs
inner join tbljobitem ji on jobs.jobid =ji.jobid
inner join tblJobServiceEmployee se on ji.jobitemid =se.jobitemid
inner join tblemployee te on te.EmployeeId =se.EmployeeId
--inner join tbltimeclock tc on tc.EmployeeId =se.EmployeeId
--JOIN tblRoleMaster tblRM  ON  tblRM.RoleMasterId=tc.RoleId and RoleName = 'Detailer'
where  jobs.JobTypeId = @Detail --and tc.Eventdate =@date--'2021-03-26'
group by se.EmployeeId,te.FirstName,
te.LastName


-------------------future implementation-----------------
----employee washhours and detail hours
--DROP Table IF EXISTS #TimeClockHours
--select 
--E.EmployeeId,
--TC.RoleId,--name
--	DateDiff(HOUR,InTime,ISNULL(OutTime,InTime)) LoginTime into #TimeClockHours
--from tblTimeClock TC 

----role (washer detailer)
--where TC.EventDate ='2021-01-20' and TC.Roleid in (3,6)
----select * from #TimeClockHours
----total 
--select tch.RoleId ,
--sum(tch.LoginTime) as login
-- from #TimeClockHours tch
 
-- group by tch.RoleId
-- --washer time
-- select tch.EmployeeId ,
--sum(tch.LoginTime) as login
-- from #TimeClockHours tch
-- where tch.roleId=6
-- group by tch.EmployeeId

-- --detailer time
--  select tch.EmployeeId ,
-- from #TimeClockHours tch
-- where tch.roleId=3 
-- group by tch.EmployeeId

END