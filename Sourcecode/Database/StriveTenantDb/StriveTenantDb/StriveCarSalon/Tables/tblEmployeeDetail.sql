CREATE TABLE [StriveCarSalon].[tblEmployeeDetail] (
    [EmployeeId]   BIGINT        NOT NULL,
    [EmployeeCode] NVARCHAR (10) NOT NULL,
    [AuthId]       INT           NOT NULL,
    [LocationId]   INT           NOT NULL,
    [PayRate]      NVARCHAR (10) NULL,
    [SickRate]     NVARCHAR (10) NULL,
    [VacRate]      NVARCHAR (10) NULL,
    [ComRate]      NVARCHAR (10) NULL,
    [HiredDate]    DATETIME      NULL,
    [Salary]       NVARCHAR (10) NULL,
    [Tip]          NVARCHAR (10) NULL,
    [LRT]          DATETIME      NULL,
    [Exemptions]   SMALLINT      NULL,
    CONSTRAINT [FK_tblEmployeeDetail_tblEmployee] FOREIGN KEY ([EmployeeId]) REFERENCES [StriveCarSalon].[tblEmployee] ([EmployeeId]),
    CONSTRAINT [FK_tblEmployeeDetail_tblLocation] FOREIGN KEY ([LocationId]) REFERENCES [StriveCarSalon].[tblLocation] ([LocationId])
);

