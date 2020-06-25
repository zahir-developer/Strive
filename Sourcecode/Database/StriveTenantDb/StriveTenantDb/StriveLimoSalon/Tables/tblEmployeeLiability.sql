CREATE TABLE [StriveLimoSalon].[tblEmployeeLiability] (
    [LiabilityId]          BIGINT         IDENTITY (1, 1) NOT NULL,
    [EmployeeId]           BIGINT         NOT NULL,
    [LiabilityType]        INT            NOT NULL,
    [LiabilityDescription] NVARCHAR (100) NULL,
    [ProductId]            INT            NULL,
    [Status]               INT            NULL,
    [CreatedDate]          DATETIME       NOT NULL,
    CONSTRAINT [PK_tblEmployeeLiability] PRIMARY KEY CLUSTERED ([LiabilityId] ASC),
    CONSTRAINT [FK_tblEmployeeLiability_tblEmployee] FOREIGN KEY ([EmployeeId]) REFERENCES [StriveLimoSalon].[tblEmployee] ([EmployeeId]),
    CONSTRAINT [FK_tblEmployeeLiability_tblProduct] FOREIGN KEY ([ProductId]) REFERENCES [StriveLimoSalon].[tblProduct] ([ProductId])
);

