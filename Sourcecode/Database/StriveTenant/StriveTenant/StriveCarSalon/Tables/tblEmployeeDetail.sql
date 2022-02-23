CREATE TABLE [StriveCarSalon].[tblEmployeeDetail] (
    [EmployeeDetailId] INT                IDENTITY (1, 1) NOT NULL,
    [EmployeeId]       INT                NULL,
    [EmployeeCode]     VARCHAR (10)       NULL,
    [AuthId]           INT                NOT NULL,
    [HiredDate]        DATE               NULL,
    [Salary]           VARCHAR (10)       NULL,
    [Tip]              VARCHAR (10)       NULL,
    [LRT]              DATETIME           NULL,
    [Exemptions]       SMALLINT           NULL,
    [PayRate]          DECIMAL (19, 4)    NULL,
    [WashRate]         DECIMAL (19, 4)    NULL,
    [DetailRate]       DECIMAL (19, 4)    NULL,
    [SickRate]         DECIMAL (19, 4)    NULL,
    [VacRate]          DECIMAL (19, 4)    NULL,
    [ComRate]          DECIMAL (19, 4)    NULL,
    [IsActive]         BIT                NULL,
    [IsDeleted]        BIT                NULL,
    [CreatedBy]        INT                NULL,
    [CreatedDate]      DATETIMEOFFSET (7) NULL,
    [UpdatedBy]        INT                NULL,
    [UpdatedDate]      DATETIMEOFFSET (7) NULL,
    [ComType]          INT                NULL,
    [IsSalary]         BIT                DEFAULT ('FALSE') NULL,
    CONSTRAINT [PK_tblEmployeeDetail] PRIMARY KEY CLUSTERED ([EmployeeDetailId] ASC),
    CONSTRAINT [FK_tblEmployeeDetail_EmployeeId] FOREIGN KEY ([EmployeeId]) REFERENCES [StriveCarSalon].[tblEmployee] ([EmployeeId])
);






GO
CREATE NONCLUSTERED INDEX [IX_tblEmployeeDetail_EmployeeId]
    ON [StriveCarSalon].[tblEmployeeDetail]([EmployeeId] ASC);

