CREATE TABLE [StriveCarSalon].[tblWeatherReport] (
    [locationId] INT          NULL,
    [Lan]        DECIMAL (18) NULL,
    [Lon]        DECIMAL (18) NULL,
    [Timestep]   INT          NULL,
    [UnitSystem] VARCHAR (10) NULL,
    [Fields]     VARCHAR (20) NULL,
    [StartTime]  DATETIME     NULL,
    [EndTime]    DATETIME     NULL
);

