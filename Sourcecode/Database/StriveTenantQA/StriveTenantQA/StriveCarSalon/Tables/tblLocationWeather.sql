CREATE TABLE [StriveCarSalon].[tblLocationWeather] (
    [LocationWeatherId] INT           IDENTITY (1, 1) NOT NULL,
    [LocationId]        INT           NULL,
    [LocationApiId]     VARCHAR (100) NULL,
    [HighTemperature]   VARCHAR (10)  NULL,
    [LowTemperature]    VARCHAR (10)  NULL,
    [RainProbability]   VARCHAR (10)  NULL,
    [WeatherDate]       DATE          NULL,
    [CreatedDate]       DATETIME      NULL,
    CONSTRAINT [PK_tblLocationWeather] PRIMARY KEY CLUSTERED ([LocationWeatherId] ASC),
    CONSTRAINT [FK_tblLocationWeather_tblLocation] FOREIGN KEY ([LocationId]) REFERENCES [StriveCarSalon].[tblLocation] ([LocationId])
);

