--[StriveCarSalon].[uspGetDetailScheduleStatus] 1,'2021-01'
CREATE   PROC [StriveCarSalon].[uspGetDetailScheduleStatus] 
(@LocationId int,
@Date Varchar(10))
AS
BEGIN
Select 
Distinct tbj.JobDate
from 
tblJob tbj with(nolock)
INNER JOIN GetTable('JobType') tbljt on tbljt.valueid = tbj.JobType
WHERE tbljt.valuedesc='Detail' and tbj.LocationId= @LocationId and Convert( Varchar(7), JobDate) in (@date)
AND isnull(tbj.IsDeleted,0)=0 
Group by tbj.JobId,tbj.JobDate

END