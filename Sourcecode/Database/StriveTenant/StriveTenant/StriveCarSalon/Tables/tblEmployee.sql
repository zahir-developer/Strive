CREATE TABLE [StriveCarSalon].[tblEmployee] (
    [EmployeeId]        INT                                                              IDENTITY (1, 1) NOT NULL,
    [FirstName]         VARCHAR (50)                                                     NULL,
    [MiddleName]        VARCHAR (50)                                                     NULL,
    [LastName]          VARCHAR (50)                                                     NULL,
    [Gender]            INT                                                              NULL,
    [SSNo]              VARCHAR (50) MASKED WITH (FUNCTION = 'partial(2, "XXXXXXX", 0)') NULL,
    [MaritalStatus]     INT                                                              NULL,
    [IsCitizen]         BIT                                                              NULL,
    [AlienNo]           VARCHAR (50)                                                     NULL,
    [BirthDate]         DATETIME                                                         NULL,
    [ImmigrationStatus] INT                                                              NULL,
    [IsActive]          BIT                                                              NULL,
    [IsDeleted]         BIT                                                              NULL,
    [CreatedBy]         INT                                                              NULL,
    [CreatedDate]       DATETIMEOFFSET (7)                                               NULL,
    [UpdatedBy]         INT                                                              NULL,
    [UpdatedDate]       DATETIMEOFFSET (7)                                               NULL,
    [WorkPermit]        DATETIME                                                         NULL,
    [UserId]            INT                                                              NULL,
    [Tips]              BIT                                                              NULL,
    [Token]             VARCHAR (500)                                                    NULL,
    CONSTRAINT [PK_tblEmployee] PRIMARY KEY CLUSTERED ([EmployeeId] ASC)
);







