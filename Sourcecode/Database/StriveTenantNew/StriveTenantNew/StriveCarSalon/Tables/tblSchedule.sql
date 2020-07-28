CREATE TABLE [StriveCarSalon].[tblSchedule] (
    [ScheduleId]    INT                IDENTITY (1, 1) NOT NULL,
    [EmployeeId]    INT                NULL,
    [LocationId]    INT                NULL,
    [RoleId]        INT                NULL,
    [ScheduledDate] DATE               NULL,
    [StartTime]     DATETIMEOFFSET (7) NULL,
    [EndTime]       DATETIMEOFFSET (7) NULL,
    [ScheduleType]  INT                NULL,
    [Comments]      VARCHAR (20)       NULL,
    [IsActive]      BIT                NULL,
    [IsDeleted]     BIT                NULL,
    [CreatedBy]     INT                NULL,
    [CreatedDate]   DATETIMEOFFSET (7) NULL,
    [UpdatedBy]     INT                NULL,
    [UpdatedDate]   DATETIMEOFFSET (7) NULL,
    CONSTRAINT [PK_tblSchedule] PRIMARY KEY CLUSTERED ([ScheduleId] ASC),
    CONSTRAINT [FK_tblSchedule_tblCodeValue] FOREIGN KEY ([ScheduleType]) REFERENCES [StriveCarSalon].[tblCodeValue] ([id]),
    CONSTRAINT [FK_tblSchedule_tblLocation] FOREIGN KEY ([LocationId]) REFERENCES [StriveCarSalon].[tblLocation] ([LocationId]),
    CONSTRAINT [FK_tblSchedule_tblRoleMaster] FOREIGN KEY ([ScheduleId]) REFERENCES [StriveCarSalon].[tblRoleMaster] ([RoleMasterId]),
    CONSTRAINT [FK_tblSchedule_tblSchedule] FOREIGN KEY ([EmployeeId]) REFERENCES [StriveCarSalon].[tblEmployee] ([EmployeeId])
);





