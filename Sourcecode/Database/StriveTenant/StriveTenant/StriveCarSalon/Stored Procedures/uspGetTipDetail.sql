--[StriveCarSalon].[uspGetTipDetail] 1,'2021-04-02'
CREATE PROCEDURE [StriveCarSalon].[uspGetTipDetail]
(@locationId INT, @date DATE)
AS
Begin

DECLARE @Tips NVARCHAR(5) = 'Tips' 
Declare @Wash int = (select  valueid from gettable('jobtype') where valuedesc='Wash')

Declare @Detail int = (select  valueid from gettable('jobtype') where valuedesc='Detail')
--DECLARE @LocationId INT = 1

DROP Table IF EXISTS #TotalJobs
Select Top 100 
tblj.LocationId, tblj.JobDate,
tblj.jobid,jt.valueid as JobTypeId, jt.valueid
into #TotalJobs
from [tblJob] tblj 
INNER JOIN GetTable('JobType') jt on(tblj.JobType = jt.valueid) 
INNER JOIN tblJobPayment jp on tblj.JobId = jp.JobId
WHERE tblj.LocationId = @LocationId AND
tblj.JobDate = @date and 
(jt.valueid = @Wash OR jt.valueid = @Detail)

--Select * from tblJobPayment where JobId in (Select JobId from #TotalJobs)


DROP Table IF EXISTS #TotalJobTips
Select
tblj.locationId,
tblj.jobid,
tjpd.Amount as Tips,
tblj.valueid as JobType,
tbljp.JobPaymentId
into #TotalJobTips
from #TotalJobs tblj 
inner join [tblJobPayment] tbljp on(tblj.JobId = tbljp.JobId)
inner join tblJobPaymentDetail tjpd on tjpd.JobPaymentId = tbljp.JobPaymentId 
inner join GetTable('PaymentType') pt on(tjpd.PaymentType = pt.valueid)
--WHERE (pt.valuedesc = @Tips)--tips
--select * from #TotalJobTips
--Finalwashtips

select ISNULL (sum(Tips),0.00) as WashTips from #TotalJobTips tjp where tjp.JobType = @Wash --tjp.JobType ='Wash'

--Detailtips
select 
se.EmployeeId,
te.FirstName,
te.LastName,
ISNULL(sum(tips.Tips),0.00) as DetailerTip
 from #TotalJobTips tips
inner join tbljobitem ji on tips.jobid =ji.jobid
inner join tblJobServiceEmployee se on ji.jobitemid =se.jobitemid
inner join tblemployee te on te.EmployeeId =se.EmployeeId
inner join tbltimeclock tc on tc.EmployeeId =se.EmployeeId
where  tips.JobType =@Detail and tc.Eventdate =@date--'2021-03-26'
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
--inner join tblEmployee E on TC.EmployeeId = E.EmployeeId

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
--sum(tch.LoginTime) as login
-- from #TimeClockHours tch
-- where tch.roleId=3 
-- group by tch.EmployeeId

END