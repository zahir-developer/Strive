


Create proc [StriveCarSalon].[uspGetWashDashboard] 
(@LocationId int, @CurrentDate date)
as
begin
SELECT 
	COUNT(1) AS WashesCount
	FROM [StriveCarSalon].[tblJob] tblj
	INNER JOIN [StriveCarSalon].GetTable('ServiceType') st
	ON tblj.JobType = st.valueid
	WHERE tblj.LocationId=@LocationId AND 
	st.valuedesc='Washes' AND
	tblj.JobDate=@CurrentDate AND
    isnull(tblj.IsDeleted,0)=0

SELECT 
	COUNT(1) AS DetailsCount
	FROM [StriveCarSalon].[tblJob] tblj
	INNER JOIN [StriveCarSalon].GetTable('ServiceType') st
	ON tblj.JobType = st.valueid
	WHERE 
	tblj.LocationId=@LocationId AND
	st.valuedesc='Details' AND
	tblj.JobDate=@CurrentDate AND
    isnull(tblj.IsDeleted,0)=0

SELECT 
	COUNT(EmployeeId) AS EmployeeCount
	FROM [StriveCarSalon].[tblSchedule] tbls
	inner join [StriveCarSalon].GetTable('ScheduleType') st
	ON (tbls.ScheduleType = st.valueid)
	WHERE st.valuedesc='Washes'
	AND tbls.LocationId=@LocationId 
	AND tbls.ScheduledDate =@CurrentDate
	AND isnull(tbls.IsDeleted,0)=0

SELECT
    COUNT(VehicleId) AS ForecastedCars
	FROM [StriveCarSalon].[tblJob]
	WHERE 
	LocationId=@LocationId
	AND
	JobDate=@CurrentDate
	AND 
	isnull(IsDeleted,0)=0

SELECT
    COUNT(VehicleId) AS [Current]
	FROM [StriveCarSalon].[tblJob] tblj
	inner join [StriveCarSalon].GetTable('JobStatus') js
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