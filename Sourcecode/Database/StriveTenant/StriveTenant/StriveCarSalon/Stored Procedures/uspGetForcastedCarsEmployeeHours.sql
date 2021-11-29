--Exec uspGetForcastedCarsEmployeeHours 1,'2021-05-07','2021-04-30','2021-04-07','2021-02-07'
-- [StriveCarSalon].[uspGetForcastedCarsEmployeeHours] 1,'2021-05-01','2021-04-24','2021-04-01','2021-01-01'
CREATE PROCEDURE [StriveCarSalon].[uspGetForcastedCarsEmployeeHours] 
(
@LocationId int,
@date date,
@lastweek VARCHAR(10),
@lastMonth VARCHAR(10),
@lastThirdMonth  VARCHAR(10)
)
AS
BEGIN

DROP TABLE IF EXISTS #WashHours

Select count(1) as WashCount,j.JobDate, j.locationId into #WashHours from tblJob j 
INNER JOIN GetTable('JobType') JT on JT.valueid = j.JobType
--INNER join GetTable('JobStatus') GT on GT.valueId = j.JobStatus and GT.valuedesc = 'Completed'
WHERE j.JobDate in(@date,@lastweek,@lastMonth,@lastThirdMonth) and j.Locationid=@LocationId
GROUP BY JobDate, j.LocationId

DROP TABLE IF EXISTS #WashTime
SELECT 
WP.Weather,
WP.RainProbability,
convert (decimal(18,2),(ISNULL(a.WashCount,0)*1.5)) AS WashTimeMinutes,
a.JobDate ,
WP.CreatedDate into #WashTime
FROM [tblWeatherPrediction] WP
LEFT join #WashHours a on CONVERT(VARCHAR(10), wp.CreatedDate, 120) = a.JobDate
WHERE
WP.LocationId =@LocationId AND CONVERT(VARCHAR(10), wp.CreatedDate, 120) in (@date,@lastweek,@lastMonth,@lastThirdMonth) 
and wp.Weather IS NOT NULL AND WP.RainProbability IS nOT NULL 
ORDER BY CreatedDate DESC

DECLARE @AvgCount INT = (Select count(1) from #WashTime WHERE WashTimeMinutes >0 )

DECLARE @Normal DECIMAL(18,2) = (Select SUM(CONVERT(DECIMAL(18,2),WashTimeMinutes))/nullif(@AvgCount,0) from #WashTime)

DECLARE @Today_RainPrecipitation int = (select top 1 RainProbability from #WashTime where CONVERT(VARCHAR(10), CreatedDate, 120)  =@date)


DEclare @Formula decimal(18,2) =(select top(1) Formula from tblForcastedRainPercentageMaster fr 
where @Today_RainPrecipitation between fr.PrecipitationRangeFrom and fr.PrecipitationRangeTo)

select @date, isnull(Round(@Normal * @Formula,0),0) as ForcastedEmployeeHours
,isnull(Round((@Normal * @Formula) / 1.25,0),0) as ForcastedCars ,ISNULL(@Today_RainPrecipitation,0) as RainPrecipitation

END