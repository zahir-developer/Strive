CREATE TABLE [StriveCarSalon].[tblWeatherPrediction] (
    [WeatherId]         INT           IDENTITY (1, 1) NOT NULL,
    [LocationId]        INT           NULL,
    [Weather]           NVARCHAR (10) NULL,
    [RainProbability]   NVARCHAR (10) NULL,
    [PredictedBusiness] NVARCHAR (10) NULL,
    [TargetBusiness]    NVARCHAR (10) NULL,
    [CreatedDate]       DATETIME      NULL,
    CONSTRAINT [PK_tblWeatherPrediction] PRIMARY KEY CLUSTERED ([WeatherId] ASC),
    CONSTRAINT [FK_tblWeatherPrediction_tblLocation] FOREIGN KEY ([LocationId]) REFERENCES [StriveCarSalon].[tblLocation] ([LocationId])
);

