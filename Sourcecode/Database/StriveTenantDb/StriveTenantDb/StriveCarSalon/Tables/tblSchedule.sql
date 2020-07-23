CREATE TABLE [StriveCarSalon].[tblSchedule] (
    [Id]            BIGINT        IDENTITY (1, 1) NOT NULL,
    [UserId]        INT           NULL,
    [LocationId]    INT           NULL,
    [RoleId]        INT           NULL,
    [ScheduledDate] DATETIME      NULL,
    [StartTime]     DATETIME      NULL,
    [EndTime]       DATETIME      NULL,
    [ScheduleType]  INT           NULL,
    [IsActive]      BIT           NULL,
    [IsDeleted]     BIT           NULL,
    [CreatedBy]     INT           NULL,
    [CreatedDate]   INT           NULL,
    [Comments]      NVARCHAR (20) NULL,
    CONSTRAINT [PK_tblSchedule] PRIMARY KEY CLUSTERED ([Id] ASC)
);

