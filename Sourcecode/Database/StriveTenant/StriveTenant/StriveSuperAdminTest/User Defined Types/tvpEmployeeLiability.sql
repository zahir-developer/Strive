CREATE TYPE [StriveSuperAdminTest].[tvpEmployeeLiability] AS TABLE (
    [LiabilityId]          BIGINT         NOT NULL,
    [EmployeeId]           BIGINT         NOT NULL,
    [LiabilityType]        INT            NULL,
    [LiabilityDescription] NVARCHAR (400) NULL,
    [ProductId]            INT            NULL,
    [Status]               INT            NULL,
    [CreatedDate]          DATETIME       NOT NULL,
    [IsActive]             BIT            NULL);

