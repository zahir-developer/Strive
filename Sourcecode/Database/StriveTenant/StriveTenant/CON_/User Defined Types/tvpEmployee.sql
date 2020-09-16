CREATE TYPE [CON].[tvpEmployee] AS TABLE (
    [EmployeeId]        BIGINT       NOT NULL,
    [FirstName]         VARCHAR (50) NULL,
    [MiddleName]        VARCHAR (50) NULL,
    [LastName]          VARCHAR (50) NULL,
    [Gender]            INT          NULL,
    [SSNo]              VARCHAR (50) NULL,
    [MaritalStatus]     INT          NULL,
    [IsCitizen]         BIT          NULL,
    [AlienNo]           VARCHAR (50) NULL,
    [BirthDate]         DATETIME     NULL,
    [ImmigrationStatus] INT          NULL,
    [CreatedDate]       DATETIME     NULL,
    [IsActive]          BIT          NULL);

