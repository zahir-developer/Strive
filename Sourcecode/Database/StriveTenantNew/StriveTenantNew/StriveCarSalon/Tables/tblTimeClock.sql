CREATE TABLE [StriveCarSalon].[tblTimeClock] (
    [Id]          INT                IDENTITY (1, 1) NOT NULL,
    [UserId]      INT                NOT NULL,
    [LocationId]  INT                NOT NULL,
    [RoleId]      INT                NULL,
    [EventDate]   DATETIMEOFFSET (7) NULL,
    [InTime]      DATETIMEOFFSET (7) NULL,
    [OutTime]     DATETIMEOFFSET (7) NULL,
    [EventType]   INT                NULL,
    [UpdatedFrom] VARCHAR (10)       NULL,
    [Status]      BIT                NOT NULL,
    [Comments]    VARCHAR (20)       NULL,
    [IsActive]    BIT                NULL,
    [IsDeleted]   BIT                NULL,
    [CreatedBy]   INT                NULL,
    [CreatedDate] DATETIMEOFFSET (7) NULL,
    [UpdatedBy]   INT                NULL,
    [UpdatedDate] DATETIMEOFFSET (7) NULL
);

