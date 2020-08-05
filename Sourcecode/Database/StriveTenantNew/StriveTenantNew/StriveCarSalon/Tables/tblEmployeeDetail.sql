CREATE TABLE [StriveCarSalon].[tblEmployeeDetail] (
    [EmployeeDetailId] INT                IDENTITY (1, 1) NOT NULL,
    [EmployeeId]       INT                NULL,
    [EmployeeCode]     VARCHAR (10)       NULL,
    [AuthId]           INT                NOT NULL,
    [LocationId]       INT                NOT NULL,
    [PayRate]          VARCHAR (10)       NULL,
    [SickRate]         VARCHAR (10)       NULL,
    [VacRate]          VARCHAR (10)       NULL,
    [ComRate]          VARCHAR (10)       NULL,
    [HiredDate]        DATETIMEOFFSET (7) NULL,
    [Salary]           VARCHAR (10)       NULL,
    [Tip]              VARCHAR (10)       NULL,
    [LRT]              DATETIME           NULL,
    [Exemptions]       SMALLINT           NULL,
    [IsActive]         BIT                NULL,
    [IsDeleted]        BIT                NULL,
    [CreatedBy]        INT                NULL,
    [CreatedDate]      DATETIMEOFFSET (7) NULL,
    [UpdatedBy]        INT                NULL,
    [UpdatedDate]      DATETIMEOFFSET (7) NULL,
    CONSTRAINT [PK_tblEmployeeDetail] PRIMARY KEY CLUSTERED ([EmployeeDetailId] ASC),
    CONSTRAINT [FK_tblEmployeeDetail_EmployeeId] FOREIGN KEY ([EmployeeId]) REFERENCES [StriveCarSalon].[tblEmployee] ([EmployeeId]),
    CONSTRAINT [FK_tblEmployeeDetail_LocationId] FOREIGN KEY ([LocationId]) REFERENCES [StriveCarSalon].[tblLocation] ([LocationId])
);








GO
CREATE NONCLUSTERED INDEX [IX_tblEmployeeDetail_EmployeeId]
    ON [StriveCarSalon].[tblEmployeeDetail]([EmployeeId] ASC);

