CREATE TABLE [StriveCarSalon].[tblBaySchedule] (
    [BayScheduleID]   INT                IDENTITY (1, 1) NOT NULL,
    [BayId]           INT                NOT NULL,
    [JobId]           INT                NULL,
    [ScheduleDate]    DATETIME           NULL,
    [ScheduleInTime]  TIME (7)           NULL,
    [ScheduleOutTime] TIME (7)           NULL,
    [IsActive]        BIT                NULL,
    [IsDeleted]       BIT                NULL,
    [CreatedBy]       INT                NULL,
    [CreatedDate]     DATETIMEOFFSET (7) NULL,
    [UpdatedBy]       INT                NULL,
    [UpdatedDate]     DATETIMEOFFSET (7) NULL,
    CONSTRAINT [PK_tblBaySchedule_BayScheduleID] PRIMARY KEY CLUSTERED ([BayScheduleID] ASC),
    CONSTRAINT [FK_tblBaySchedule_BayId] FOREIGN KEY ([BayId]) REFERENCES [StriveCarSalon].[tblBay] ([BayId]),
    CONSTRAINT [FK_tblBaySchedule_JobId] FOREIGN KEY ([JobId]) REFERENCES [StriveCarSalon].[tblJob] ([JobId])
);

