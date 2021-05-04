CREATE TABLE [StriveCarSalon].[tblPayrollEmployee] (
    [PayrollEmployeeId] INT                IDENTITY (1, 1) NOT NULL,
    [EmployeeId]        INT                NULL,
    [PayRollProcessId]  INT                NULL,
    [Adjustment]        DECIMAL (18)       NULL,
    [IsActive]          BIT                NULL,
    [IsDeleted]         BIT                NULL,
    [CreatedBy]         INT                NULL,
    [CreatedDate]       DATETIMEOFFSET (7) NULL,
    [UpdatedBy]         INT                NULL,
    [UpdatedDate]       DATETIMEOFFSET (7) NULL,
    CONSTRAINT [PK_tblPayrollEmployee] PRIMARY KEY CLUSTERED ([PayrollEmployeeId] ASC),
    CONSTRAINT [FK_tblPayrollEmployee_EmployeeId] FOREIGN KEY ([EmployeeId]) REFERENCES [StriveCarSalon].[tblEmployee] ([EmployeeId]),
    CONSTRAINT [FK_tblPayrollEmployee_PayrollProcessId] FOREIGN KEY ([PayRollProcessId]) REFERENCES [StriveCarSalon].[tblPayrollProcess] ([PayrollProcessId])
);

