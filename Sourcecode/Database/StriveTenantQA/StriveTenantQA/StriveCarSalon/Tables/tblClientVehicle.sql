CREATE TABLE [StriveCarSalon].[tblClientVehicle] (
    [VehicleId]      INT                IDENTITY (1, 1) NOT NULL,
    [ClientId]       INT                NULL,
    [LocationId]     INT                NULL,
    [VehicleNumber]  VARCHAR (20)       NULL,
    [VehicleMfr]     INT                NULL,
    [VehicleModel]   INT                NULL,
    [VehicleModelNo] INT                NULL,
    [VehicleYear]    VARCHAR (6)        NULL,
    [VehicleColor]   INT                NULL,
    [Upcharge]       INT                NULL,
    [Barcode]        VARCHAR (50)       NULL,
    [Notes]          VARCHAR (20)       NULL,
    [IsActive]       BIT                NULL,
    [IsDeleted]      BIT                NULL,
    [CreatedBy]      INT                NULL,
    [CreatedDate]    DATETIMEOFFSET (7) NULL,
    [UpdatedBy]      INT                NULL,
    [UpdatedDate]    DATETIMEOFFSET (7) NULL,
    CONSTRAINT [PK_tblClientVehicle] PRIMARY KEY CLUSTERED ([VehicleId] ASC),
    CONSTRAINT [FK_tblClientVehicle_ClientId] FOREIGN KEY ([ClientId]) REFERENCES [StriveCarSalon].[tblClient] ([ClientId]),
    CONSTRAINT [FK_tblClientVehicle_LocationId] FOREIGN KEY ([LocationId]) REFERENCES [StriveCarSalon].[tblLocation] ([LocationId]),
    CONSTRAINT [FK_tblClientVehicle_VehicleColor] FOREIGN KEY ([VehicleColor]) REFERENCES [StriveCarSalon].[tblCodeValue] ([id]),
    CONSTRAINT [FK_tblClientVehicle_VehicleMfr] FOREIGN KEY ([VehicleMfr]) REFERENCES [StriveCarSalon].[tblCodeValue] ([id]),
    CONSTRAINT [FK_tblClientVehicle_VehicleModel] FOREIGN KEY ([VehicleModel]) REFERENCES [StriveCarSalon].[tblCodeValue] ([id])
);


GO
CREATE NONCLUSTERED INDEX [IX_tblClientVehicle_ClientId]
    ON [StriveCarSalon].[tblClientVehicle]([ClientId] ASC);

