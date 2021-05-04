-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [StriveCarSalon].GetPastWeatherInfo
	(
@LocationId int,
@Date date
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
w.WashCount
FROM [StriveCarSalon].[tblWeatherPrediction] WP
inner join #WashHours w on WP.LocationId=w.LocationId
WHERE
WP.LocationId =@LocationId AND wp.CreatedDate=@Date

END