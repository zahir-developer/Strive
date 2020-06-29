CREATE TABLE [StriveCarSalon].[tblEmployee] (
    [EmployeeId]        BIGINT       IDENTITY (1, 1) NOT NULL,
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
    CONSTRAINT [PK_tblEmployee1] PRIMARY KEY CLUSTERED ([EmployeeId] ASC)
);



