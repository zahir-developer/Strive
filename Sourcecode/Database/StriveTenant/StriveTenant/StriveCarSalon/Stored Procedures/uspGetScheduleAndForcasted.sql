--[StriveCarSalon].[uspGetScheduleAndForcasted]1,null,'2021-05-01','2021-05-23'
CREATE PROCEDURE [StriveCarSalon].[uspGetScheduleAndForcasted] 
@LocationId int,
@EmployeeId int = NULL,
@ScheduledStartDate Date = NULL,--lastweek
@ScheduledEndDate Date = NULL--today
--@lastMonth VARCHAR(10),
--@lastThirdMonth  VARCHAR(10)
AS
BEGIN  
DROP TABLE IF EXISTS #Schedule

IF @EmployeeId is null
BEGIN

SELECT
	 tblsc.ScheduleId,
	 tblsc.EmployeeId,
	 ISNULL(tblsc.IsAbscent, 0) as IsEmployeeAbscent,
	 tblemp.FirstName +''+tblemp.LastName as EmployeeName,
	 tblsc.LocationId,
	 tblloc.LocationName,
	 tblLoc.ColorCode,
	 tblsc.RoleId,
	 tblsc.ScheduledDate,
	 tblsc.StartTime,
	 tblsc.EndTime,
	 tblsc.ScheduleType,
	 tblsc.Comments,
	 tbler.valuedesc as EmployeeRole,
	 tblsc.IsDeleted INTO #Schedule
FROM tblSchedule as tblsc 
left JOIN [tblEmployee] tblemp ON (tblsc.EmployeeId = tblemp.EmployeeId)
INNER JOIN [tblLocation] tblloc ON (tblsc.LocationId = tblloc.LocationId)
LEFT JOIN [GetTable]('EmployeeRole') tbler ON  (tblsc.RoleId = tbler.valueid)
WHERE 
(ISNULL(tblloc.IsActive,1) = 1 AND tblloc.IsDeleted = 0) AND
(ISNULL(tblsc.IsDeleted,0)=0 AND ISNULL(tblsc.IsActive,1) = 1) AND 
((tblsc.LocationId =@LocationId OR @LocationId is null)
AND
(ScheduledDate BETWEEN @ScheduledStartDate AND @ScheduledendDate) OR (@ScheduledStartDate IS NULL AND @ScheduledendDate IS NULL))

select SUM(CONVERT(DECIMAL(6,2),(DATEDIFF(MINUTE,StartTime, EndTime))))/60 as Totalhours ,ScheduledDate
,count(distinct EmployeeId) as TotalEmployees
from #Schedule s
Group by s.ScheduledDate

select count(distinct EmployeeId) as TotalEmployees ,ScheduledDate
from #Schedule s
Group by s.ScheduledDate

--Forcasted employee hours and cars

Declare @imax int,@imin int
DROP TABLE IF EXISTS #TempDate
DROP TABLE IF EXISTS #tempdata

Create table #tempdata (
id int identity(1,1),
today date,
Lastweek date,
LastMonth date,
LastThreeMonth date);

WITH DateRange(DateData) AS 
(
    SELECT @ScheduledStartDate as Date
    UNION ALL
    SELECT DATEADD(d,1,DateData)
    FROM DateRange 
    WHERE DateData < @ScheduledendDate
)

insert into #tempdata
Select DateData,DATEADD(Day,-7,DateData)As LastWeek,DATEADD(Month,-1,DateData)as LastMonth,DATEADD(Month,-3,DateData)as LastthreeMonth FROM DateRange
--SELECT * FROM #TEMPDATA


select @imin =min(id),@imax =MAX(id) from #tempdata


while @imax >= @imin
begin
declare @Forcasted table
(date date ,
ForcastedEmployeeHours decimal(19,2),
ForcastedCars decimal(19,2),
RainPrecipitation decimal(19,2))

Declare @today varchar(10);
Declare @lastWeek  VARCHAR(10)  ;
declare @lastMonth VARCHAR(10);
declare @lastThirdMonth  VARCHAR(10);

select @today=today,@lastWeek=Lastweek,@lastMonth=LastMonth,@lastThirdMonth=LastThreeMonth from #tempdata where id=@imin
--select @today,@lastWeek,@lastMonth,@lastThirdMonth

insert into @Forcasted
Exec uspGetForcastedCarsEmployeeHours 1,@today,@lastWeek,@lastMonth,@lastThirdMonth
set @imin =@imin+1;
end
select * from @Forcasted

END
End