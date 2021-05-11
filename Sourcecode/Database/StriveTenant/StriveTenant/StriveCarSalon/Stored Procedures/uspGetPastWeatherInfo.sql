﻿-- =============================================
-- Author:		Shalini
-- Create date: To get past week and month weather prediction
 --[StriveCarSalon].[uspGetPastWeatherInfo] 2034,'2020-12-08', '2020-11-03', '2020-12-26','2020-09-03'
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

Select count(1) as WashCount,LocationId into #WashHours from tblJob j 
INNER JOIN GetTable('JobType') JT on JT.valueid = j.JobType
INNER join GetTable('JobStatus') GT on GT.valueId = j.JobStatus and GT.valuedesc = 'Completed'
WHERE j.JobDate = @date
GROUP BY LocationId

SELECT 
WP.Weather,
WP.RainProbability,
WP.PredictedBusiness,
WP.TargetBusiness,
WP.LocationId ,
WP.CreatedDate,
w.WashCount
FROM [tblWeatherPrediction] WP
LEFT join #WashHours w on WP.LocationId=w.LocationId
WHERE
WP.LocationId =@LocationId AND CONVERT(VARCHAR(10), wp.CreatedDate, 120) in (@date,@lastweek,@lastMonth,@lastThirdMonth) 

ORDER BY 1 DESC


END