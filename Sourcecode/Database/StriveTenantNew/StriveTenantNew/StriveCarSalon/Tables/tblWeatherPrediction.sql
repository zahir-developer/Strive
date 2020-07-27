CREATE TABLE [StriveCarSalon].[tblWeatherPrediction] (
    [WeatherId]         INT                IDENTITY (1, 1) NOT NULL,
    [LocationId]        INT                NULL,
    [Weather]           VARCHAR (10)       NULL,
    [RainProbability]   VARCHAR (10)       NULL,
    [PredictedBusiness] VARCHAR (10)       NULL,
    [TargetBusiness]    VARCHAR (10)       NULL,
    [IsActive]          BIT                NULL,
    [IsDeleted]         BIT                NULL,
    [CreatedBy]         INT                NULL,
    [CreatedDate]       DATETIMEOFFSET (7) NULL,
    [UpdatedBy]         INT                NULL,
    [UpdatedDate]       DATETIMEOFFSET (7) NULL,
    CONSTRAINT [PK_tblWeatherPrediction] PRIMARY KEY CLUSTERED ([WeatherId] ASC),
    CONSTRAINT [FK_tblWeatherPrediction_tblLocation] FOREIGN KEY ([LocationId]) REFERENCES [StriveCarSalon].[tblLocation] ([LocationId])
);



