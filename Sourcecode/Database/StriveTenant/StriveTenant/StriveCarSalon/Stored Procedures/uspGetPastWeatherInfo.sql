-- =============================================
-- Author:		Shalini
-- Create date: To get past week and month weather prediction
 --[StriveCarSalon].[uspGetPastWeatherInfo] 1,'2021-05-13', '2021-05-18', '2021-04-25','2021-02-25'
-- =============================================
CREATE PROCEDURE [StriveCarSalon].[uspGetPastWeatherInfo]
	(
@LocationId int,
@date date,
----@today VARCHAR(10),
@lastweek VARCHAR(10),
@lastMonth VARCHAR(10),
@lastThirdMonth  VARCHAR(10)
)



AS
BEGIN

DROP TABLE IF EXISTS #WashHours

Select count(1) as WashCount,LocationId,j.JobDate into #WashHours from tblJob j 
INNER JOIN GetTable('JobType') JT on JT.valueid = j.JobType
INNER join GetTable('JobStatus') GT on GT.valueId = j.JobStatus and GT.valuedesc = 'Completed'
WHERE j.JobDate IN (@date,@lastweek,@lastMonth,@lastThirdMonth) 
GROUP BY LocationId,j.JobDate

SELECT 
WP.WeatherId,
WP.Weather,
WP.RainProbability,
WP.PredictedBusiness,
WP.TargetBusiness,
WP.LocationId ,
WP.CreatedDate,
w.WashCount
FROM [tblWeatherPrediction] WP
LEFT join #WashHours w on WP.LocationId=w.LocationId AND CONVERT(VARCHAR(10), wp.CreatedDate, 120) = w.JobDate
WHERE
WP.LocationId =@LocationId AND CONVERT(VARCHAR(10), wp.CreatedDate, 120) in (@date,@lastweek,@lastMonth,@lastThirdMonth) 

ORDER BY 1 DESC


END