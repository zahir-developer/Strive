CREATE TABLE [StriveCarSalon].[tblSchedule] (
    [Id]            INT                IDENTITY (1, 1) NOT NULL,
    [UserId]        INT                NULL,
    [LocationId]    INT                NULL,
    [RoleId]        INT                NULL,
    [ScheduledDate] DATETIMEOFFSET (7) NULL,
    [StartTime]     DATETIMEOFFSET (7) NULL,
    [EndTime]       DATETIMEOFFSET (7) NULL,
    [ScheduleType]  INT                NULL,
    [Comments]      VARCHAR (20)       NULL,
    [IsActive]      BIT                NULL,
    [IsDeleted]     BIT                NULL,
    [CreatedBy]     INT                NULL,
    [CreatedDate]   DATETIMEOFFSET (7) NULL,
    [UpdatedBy]     INT                NULL,
    [UpdatedDate]   DATETIMEOFFSET (7) NULL
);

