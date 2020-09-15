CREATE TABLE [CON].[tblEmployeeLiability] (
    [LiabilityId]          INT                IDENTITY (1, 1) NOT NULL,
    [EmployeeId]           INT                NULL,
    [LiabilityType]        INT                NULL,
    [LiabilityDescription] VARCHAR (20)       NULL,
    [ProductId]            INT                NULL,
    [Status]               INT                NULL,
    [IsActive]             BIT                NULL,
    [IsDeleted]            BIT                NULL,
    [CreatedBy]            INT                NULL,
    [CreatedDate]          DATETIMEOFFSET (7) NULL,
    [UpdatedBy]            INT                NULL,
    [UpdatedDate]          DATETIMEOFFSET (7) NULL,
    [VehicleId]            INT                NULL,
    [ClientId]             INT                NULL,
    CONSTRAINT [PK_tblEmployeeLiability] PRIMARY KEY CLUSTERED ([LiabilityId] ASC),
    CONSTRAINT [FK_tblEmployeeLiability_ClientId] FOREIGN KEY ([ClientId]) REFERENCES [CON].[tblClient] ([ClientId]),
    CONSTRAINT [FK_tblEmployeeLiability_EmployeeId] FOREIGN KEY ([EmployeeId]) REFERENCES [CON].[tblEmployee] ([EmployeeId]),
    CONSTRAINT [FK_tblEmployeeLiability_ProductId] FOREIGN KEY ([ProductId]) REFERENCES [CON].[tblProduct] ([ProductId]),
    CONSTRAINT [FK_tblEmployeeLiability_VehicleId] FOREIGN KEY ([VehicleId]) REFERENCES [CON].[tblClientVehicle] ([VehicleId])
);


GO
CREATE NONCLUSTERED INDEX [IX_tblEmployeeLiability_EmployeeId]
    ON [CON].[tblEmployeeLiability]([EmployeeId] ASC);

