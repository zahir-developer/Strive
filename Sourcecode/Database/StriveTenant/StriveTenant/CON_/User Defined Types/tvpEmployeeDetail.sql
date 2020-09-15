CREATE TYPE [CON].[tvpEmployeeDetail] AS TABLE (
    [EmployeeDetailId] BIGINT        NOT NULL,
    [EmployeeId]       BIGINT        NOT NULL,
    [EmployeeCode]     NVARCHAR (10) NOT NULL,
    [AuthId]           INT           NOT NULL,
    [LocationId]       INT           NOT NULL,
    [PayRate]          NVARCHAR (10) NULL,
    [SickRate]         NVARCHAR (10) NULL,
    [VacRate]          NVARCHAR (10) NULL,
    [ComRate]          NVARCHAR (10) NULL,
    [HiredDate]        DATETIME      NULL,
    [Salary]           NVARCHAR (10) NULL,
    [Tip]              NVARCHAR (10) NULL,
    [LRT]              DATETIME      NULL,
    [Exemptions]       SMALLINT      NULL,
    [IsActive]         BIT           NULL);

