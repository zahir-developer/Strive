
CREATE   PROC [StriveCarSalon].[uspGetDetailScheduleStatus] --[StriveCarSalon].[uspGetDetailScheduleStatus] 1,'2021-01'
(@LocationId int,
@Date Varchar(10))
AS
BEGIN
Select 
Distinct tbj.JobDate
from 
StriveCarSalon.tblJob tbj with(nolock)
INNER JOIN StriveCarSalon.GetTable('JobType') tbljt on tbljt.valueid = tbj.JobType
WHERE tbljt.valuedesc='Detail' and tbj.LocationId= @LocationId and Convert( Varchar(7), JobDate) in (@date)
AND isnull(tbj.IsDeleted,0)=0 
Group by tbj.JobId,tbj.JobDate

END