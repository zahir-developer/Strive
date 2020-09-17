

-- =============================================
-- Author:		Vineeth
-- Create date: 20-08-2020
-- Description:	Dashboard detail for Washes
-- =============================================

---------------------History--------------------
-- =============================================
-- 10-09-2020, Vineeth - Added IsActive condition
--                       and JobType as params
--						 Going to use for Detail 
--                       also

------------------------------------------------
-- =============================================


CREATE proc [StriveCarSalon].[uspGetWashDashboard] 
(@LocationId int, @CurrentDate date, @JobType int)
as
begin 
SELECT 
	COUNT(1) AS WashesCount
	FROM [StriveCarSalon].[tblJob] tblj
	INNER JOIN [StriveCarSalon].GetTable('JobType') jt 
	ON(tblj.JobType=jt.valueid)
	WHERE jt.valuedesc='Wash'
	AND tblj.LocationId=@LocationId
	AND tblj.JobDate=@CurrentDate AND
    isnull(tblj.IsDeleted,0)=0 AND
	tblj.IsActive=1

SELECT  
	COUNT(1) AS DetailsCount
	FROM [StriveCarSalon].[tblJob] tblj
	INNER JOIN [StriveCarSalon].GetTable('JobType') jt 
	ON(tblj.JobType=jt.valueid)
	WHERE jt.valuedesc='Detail' AND
	tblj.LocationId=@LocationId AND
	tblj.JobDate=@CurrentDate AND
    isnull(tblj.IsDeleted,0)=0 AND
	tblj.IsActive=1

SELECT
	COUNT(distinct  tblji.EmployeeId) AS EmployeeCount
	FROM [StriveCarSalon].[tblJobItem] tblji
	inner join [StriveCarSalon].[tblJob] tblj 
	ON(tblji.JobId = tblj.JobId)
	WHERE tblj.JobType=@JobType
	AND tblj.LocationId=@LocationId
	AND tblj.JobDate =@CurrentDate
	AND isnull(tblji.IsDeleted,0)=0
	AND tblji.IsActive=1

SELECT
    COUNT(distinct VehicleId) AS ForecastedCars
	FROM [StriveCarSalon].[tblJob]
	WHERE 
	JobType=@JobType
	AND
	LocationId=@LocationId
	AND
	JobDate=@CurrentDate
	AND 
	isnull(IsDeleted,0)=0
	AND
	IsActive=1

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
	tblj.JobType=@JobType
	AND
	js.valuedesc='Completed'
	AND
	isnull(tblj.IsDeleted,0)=0
	AND
	tblj.IsActive=1


SELECT
    convert(varchar(8),Cast(DateAdd(ms, AVG(CAST(DateDiff( ms, '00:00:00', cast(ActualTimeOut as time)) AS BIGINT)), '00:00:00' ) as Time ))
	AS AverageWashTime from StriveCarSalon.tblJob 
	where 
	LocationId=@LocationId AND 
	JobDate=@CurrentDate AND
	isnull(IsDeleted,0)=0 AND
	IsActive=1

end
