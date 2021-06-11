---------------------History--------------------
-- =============================================

-- 24-05-2021, shalini added tblbayschedule (bug fix)
------------------------------------------------
--[StriveCarSalon].[uspGetDetailScheduleStatus] 1,'2021-05'
CREATE PROCEDURE [StriveCarSalon].[uspGetDetailScheduleStatus] 
(@LocationId int,
@Date Varchar(10))
AS
BEGIN
Select 
Distinct tbj.JobDate
from 
tblJob tbj with(nolock)
INNER JOIN GetTable('JobType') tbljt on tbljt.valueid = tbj.JobType
inner join tblBaySchedule tbs on tbj.JobId =tbs.JobId
WHERE tbljt.valuedesc='Detail' and tbj.LocationId= @LocationId and Convert( Varchar(7), JobDate) in (@date)
AND isnull(tbj.IsDeleted,0)=0 
Group by tbj.JobId,tbj.JobDate

END