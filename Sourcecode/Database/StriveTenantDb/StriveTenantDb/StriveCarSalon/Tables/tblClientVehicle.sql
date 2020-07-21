CREATE TABLE [StriveCarSalon].[tblClientVehicle] (
    [VehicleId]      INT            NOT NULL,
    [ClientId]       INT            NULL,
    [LocationId]     INT            NULL,
    [VehicleNumber]  NVARCHAR (50)  NULL,
    [VehicleMake]    INT            NULL,
    [VehicleModel]   INT            NULL,
    [VehicleModelNo] INT            NULL,
    [VehicleYear]    NVARCHAR (10)  NULL,
    [VehicleColor]   INT            NULL,
    [Upcharge]       INT            NULL,
    [Barcode]        NVARCHAR (50)  NULL,
    [Notes]          NVARCHAR (100) NULL,
    [CreatedDate]    DATETIME       NULL,
    CONSTRAINT [PK_tblClientVehicle] PRIMARY KEY CLUSTERED ([VehicleId] ASC),
    CONSTRAINT [FK_tblClientVehicle_tblClient] FOREIGN KEY ([ClientId]) REFERENCES [StriveCarSalon].[tblClient] ([ClientId]),
    CONSTRAINT [FK_tblClientVehicle_tblLocation] FOREIGN KEY ([LocationId]) REFERENCES [StriveCarSalon].[tblLocation] ([LocationId])
);

