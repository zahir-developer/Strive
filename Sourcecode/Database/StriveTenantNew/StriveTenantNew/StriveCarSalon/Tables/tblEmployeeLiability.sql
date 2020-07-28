CREATE TABLE [StriveCarSalon].[tblEmployeeLiability] (
    [LiabilityId]          INT                IDENTITY (1, 1) NOT NULL,
    [EmployeeId]           INT                NULL,
    [LiabilityType]        INT                NOT NULL,
    [LiabilityDescription] VARCHAR (20)       NULL,
    [ProductId]            INT                NULL,
    [Status]               INT                NULL,
    [IsActive]             BIT                NULL,
    [IsDeleted]            BIT                NULL,
    [CreatedBy]            INT                NULL,
    [CreatedDate]          DATETIMEOFFSET (7) NULL,
    [UpdatedBy]            INT                NULL,
    [UpdatedDate]          DATETIMEOFFSET (7) NULL,
    CONSTRAINT [PK_tblEmployeeLiability] PRIMARY KEY CLUSTERED ([LiabilityId] ASC),
    CONSTRAINT [FK_tblEmployeeLiability_tblCodeValue] FOREIGN KEY ([LiabilityType]) REFERENCES [StriveCarSalon].[tblCodeValue] ([id]),
    CONSTRAINT [FK_tblEmployeeLiability_tblEmployee] FOREIGN KEY ([EmployeeId]) REFERENCES [StriveCarSalon].[tblEmployee] ([EmployeeId]),
    CONSTRAINT [FK_tblEmployeeLiability_tblProduct] FOREIGN KEY ([ProductId]) REFERENCES [StriveCarSalon].[tblProduct] ([ProductId])
);






GO


