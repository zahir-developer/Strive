CREATE TABLE [CON].[tblWeatherReport] (
    [locationId]  INT                NULL,
    [Lan]         DECIMAL (18)       NULL,
    [Lon]         DECIMAL (18)       NULL,
    [Timestep]    INT                NULL,
    [UnitSystem]  VARCHAR (10)       NULL,
    [Fields]      VARCHAR (20)       NULL,
    [StartTime]   DATETIMEOFFSET (7) NULL,
    [EndTime]     DATETIMEOFFSET (7) NULL,
    [IsActive]    BIT                NULL,
    [IsDeleted]   BIT                NULL,
    [CreatedBy]   INT                NULL,
    [CreatedDate] DATETIMEOFFSET (7) NULL,
    [UpdatedBy]   INT                NULL,
    [UpdatedDate] DATETIMEOFFSET (7) NULL
);

