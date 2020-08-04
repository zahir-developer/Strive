CREATE TABLE [StriveCarSalon].[tblTimeClock] (
    [TimeClockId] INT                IDENTITY (1, 1) NOT NULL,
    [EmployeeId]  INT                NOT NULL,
    [LocationId]  INT                NOT NULL,
    [RoleId]      INT                NULL,
    [EventDate]   DATE               NULL,
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
    [UpdatedDate] DATETIMEOFFSET (7) NULL,
    CONSTRAINT [PK_tblTimeClock] PRIMARY KEY CLUSTERED ([TimeClockId] ASC),
    CONSTRAINT [FK_tblTimeClock_EmployeeId] FOREIGN KEY ([EmployeeId]) REFERENCES [StriveCarSalon].[tblEmployee] ([EmployeeId]),
    CONSTRAINT [FK_tblTimeClock_EventType] FOREIGN KEY ([EventType]) REFERENCES [StriveCarSalon].[tblCodeValue] ([id]),
    CONSTRAINT [FK_tblTimeClock_LocationId] FOREIGN KEY ([LocationId]) REFERENCES [StriveCarSalon].[tblLocation] ([LocationId]),
    CONSTRAINT [FK_tblTimeClock_RoleId] FOREIGN KEY ([RoleId]) REFERENCES [StriveCarSalon].[tblRoleMaster] ([RoleMasterId])
);








GO
CREATE NONCLUSTERED INDEX [IX_tblTimeClock_EmployeeId]
    ON [StriveCarSalon].[tblTimeClock]([EmployeeId] ASC);

