CREATE TABLE [StriveCarSalon].[tblForcastedRainPercentageMaster] (
    [ForecastRainPercentageId] INT             NOT NULL,
    [Percentage]               DECIMAL (18, 2) NULL,
    [Formula]                  DECIMAL (18, 2) NULL,
    [PrecipitationRangeFrom]   DECIMAL (18, 2) NULL,
    [PrecipitationRangeTo]     DECIMAL (18, 2) NULL,
    CONSTRAINT [PK_ForcastedRainPercentage] PRIMARY KEY CLUSTERED ([ForecastRainPercentageId] ASC)
);



