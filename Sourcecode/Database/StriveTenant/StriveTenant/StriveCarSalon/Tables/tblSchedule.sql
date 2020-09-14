CREATE TABLE [StriveCarSalon].[tblSchedule] (
    [ScheduleId]    INT                IDENTITY (1, 1) NOT NULL,
    [EmployeeId]    INT                NULL,
    [LocationId]    INT                NULL,
    [RoleId]        INT                NULL,
    [IsAbscent]     BIT                NULL,
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
    CONSTRAINT [FK_tblSchedule_EmployeeId] FOREIGN KEY ([EmployeeId]) REFERENCES [StriveCarSalon].[tblEmployee] ([EmployeeId]),
    CONSTRAINT [FK_tblSchedule_LocationId] FOREIGN KEY ([LocationId]) REFERENCES [StriveCarSalon].[tblLocation] ([LocationId]),
    CONSTRAINT [FK_tblSchedule_RoleId] FOREIGN KEY ([RoleId]) REFERENCES [StriveCarSalon].[tblRoleMaster] ([RoleMasterId]),
    CONSTRAINT [FK_tblSchedule_ScheduleType] FOREIGN KEY ([ScheduleType]) REFERENCES [StriveCarSalon].[tblCodeValue] ([id])
);

