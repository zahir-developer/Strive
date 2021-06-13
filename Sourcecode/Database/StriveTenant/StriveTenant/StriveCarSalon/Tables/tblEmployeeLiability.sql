CREATE TABLE [StriveCarSalon].[tblEmployeeLiability] (
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
    [LiabilityDate]        DATE               NULL,
    [LocationId]           INT                NULL,
    CONSTRAINT [PK_tblEmployeeLiability] PRIMARY KEY CLUSTERED ([LiabilityId] ASC),
    CONSTRAINT [FK_tblEmployeeLiability_ClientId] FOREIGN KEY ([ClientId]) REFERENCES [StriveCarSalon].[tblClient] ([ClientId]),
    CONSTRAINT [FK_tblEmployeeLiability_EmployeeId] FOREIGN KEY ([EmployeeId]) REFERENCES [StriveCarSalon].[tblEmployee] ([EmployeeId]),
    CONSTRAINT [FK_tblEmployeeLiability_LiabilityType] FOREIGN KEY ([LiabilityType]) REFERENCES [StriveCarSalon].[tblCodeValue] ([id]),
    CONSTRAINT [FK_tblEmployeeLiability_ProductId] FOREIGN KEY ([ProductId]) REFERENCES [StriveCarSalon].[tblProduct] ([ProductId]),
    CONSTRAINT [FK_tblEmployeeLiability_VehicleId] FOREIGN KEY ([VehicleId]) REFERENCES [StriveCarSalon].[tblClientVehicle] ([VehicleId])
);








GO
CREATE NONCLUSTERED INDEX [IX_tblEmployeeLiability_EmployeeId]
    ON [StriveCarSalon].[tblEmployeeLiability]([EmployeeId] ASC);

