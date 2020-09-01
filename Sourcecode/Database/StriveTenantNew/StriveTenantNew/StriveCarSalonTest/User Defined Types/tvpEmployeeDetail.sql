CREATE TYPE [StriveCarSalonTest].[tvpEmployeeDetail] AS TABLE (
    [EmployeeDetailId] BIGINT        NOT NULL,
    [EmployeeId]       BIGINT        NOT NULL,
    [EmployeeCode]     NVARCHAR (20) NOT NULL,
    [AuthId]           INT           NOT NULL,
    [LocationId]       INT           NOT NULL,
    [PayRate]          NVARCHAR (20) NULL,
    [SickRate]         NVARCHAR (20) NULL,
    [VacRate]          NVARCHAR (20) NULL,
    [ComRate]          NVARCHAR (20) NULL,
    [HiredDate]        DATETIME      NULL,
    [Salary]           NVARCHAR (20) NULL,
    [Tip]              NVARCHAR (20) NULL,
    [LRT]              DATETIME      NULL,
    [Exemptions]       SMALLINT      NULL,
    [IsActive]         BIT           NULL);

