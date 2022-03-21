
CREATE PROCEDURE [StriveCarSalon].[uspGetLocationWeather]
@date Datetime,
@locationId INT
AS
BEGIN
	SELECT	HighTemperature,
			RainProbability
	FROM	[StriveCarSalon].[tblLocationWeather]
	WHERE	LocationId = @locationId AND WeatherDate = cast(@date as date)
END