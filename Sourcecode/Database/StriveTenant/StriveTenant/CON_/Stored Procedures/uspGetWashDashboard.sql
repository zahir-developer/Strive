



CREATE PROCEDURE [CON].[uspGetWashDashboard] 
(@LocationId int, @CurrentDate date)
as
begin
SELECT 
	COUNT(1) AS WashesCount
	FROM [CON].[tblJob] tblj
	INNER JOIN [CON].GetTable('ServiceType') st
	ON tblj.JobType = st.valueid
	WHERE tblj.LocationId=@LocationId AND 
	st.valuedesc='Washes' AND
	tblj.JobDate=@CurrentDate AND
    isnull(tblj.IsDeleted,0)=0

SELECT 
	COUNT(1) AS DetailsCount
	FROM [CON].[tblJob] tblj
	INNER JOIN [CON].GetTable('ServiceType') st
	ON tblj.JobType = st.valueid
	WHERE 
	tblj.LocationId=@LocationId AND
	st.valuedesc='Details' AND
	tblj.JobDate=@CurrentDate AND
    isnull(tblj.IsDeleted,0)=0

SELECT 
	COUNT(EmployeeId) AS EmployeeCount
	FROM [CON].[tblSchedule] tbls
	inner join [CON].GetTable('ScheduleType') st
	ON (tbls.ScheduleType = st.valueid)
	WHERE st.valuedesc='Washes'
	AND tbls.LocationId=@LocationId 
	AND tbls.ScheduledDate =@CurrentDate
	AND isnull(tbls.IsDeleted,0)=0

SELECT
    COUNT(VehicleId) AS ForecastedCars
	FROM [CON].[tblJob]
	WHERE 
	LocationId=@LocationId
	AND
	JobDate=@CurrentDate
	AND 
	isnull(IsDeleted,0)=0

SELECT
    COUNT(VehicleId) AS [Current]
	FROM [CON].[tblJob] tblj
	inner join [CON].GetTable('JobStatus') js
	ON(tblj.JobStatus = js.valueid)
	WHERE 
	tblj.LocationId=@LocationId
	AND
	tblj.JobDate=@CurrentDate
	AND 
	js.valuedesc='Completed'
	AND
	isnull(tblj.IsDeleted,0)=0


SELECT
    convert(varchar(8),Cast(DateAdd(ms, AVG(CAST(DateDiff( ms, '00:00:00', cast(ActualTimeOut as time)) AS BIGINT)), '00:00:00' ) as Time ))
	AS AverageWashTime from StriveCarSalon.tblJob 
	where 
	LocationId=@LocationId AND 
	JobDate=@CurrentDate AND
	isnull(IsDeleted,0)=0

end
