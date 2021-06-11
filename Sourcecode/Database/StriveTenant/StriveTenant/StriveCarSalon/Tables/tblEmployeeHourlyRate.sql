CREATE TABLE [StriveCarSalon].[tblEmployeeHourlyRate] (
    [EmployeeHourlyRateId] INT                IDENTITY (1, 1) NOT NULL,
    [EmployeeId]           INT                NULL,
    [RoleId]               INT                NULL,
    [LocationId]           INT                NULL,
    [HourlyRate]           DECIMAL (18, 2)    NULL,
    [IsActive]             BIT                NULL,
    [IsDeleted]            BIT                NULL,
    [CreatedBy]            INT                NULL,
    [CreatedDate]          DATETIMEOFFSET (7) NULL,
    [UpdatedBy]            INT                NULL,
    [UpdatedDate]          DATETIMEOFFSET (7) NULL,
    CONSTRAINT [PK_tblEmployeeHourlyRate] PRIMARY KEY CLUSTERED ([EmployeeHourlyRateId] ASC),
    CONSTRAINT [FK_tblEmployeeHourlyRate_EmployeeId] FOREIGN KEY ([EmployeeId]) REFERENCES [StriveCarSalon].[tblEmployee] ([EmployeeId]),
    CONSTRAINT [FK_tblEmployeeHourlyRate_LocationId] FOREIGN KEY ([LocationId]) REFERENCES [StriveCarSalon].[tblLocation] ([LocationId]),
    CONSTRAINT [FK_tblEmployeeHourlyRate_RoleId] FOREIGN KEY ([RoleId]) REFERENCES [StriveCarSalon].[tblRoleMaster] ([RoleMasterId])
);



