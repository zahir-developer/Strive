CREATE TYPE [CON].[tvpClientVehicle] AS TABLE (
    [VehicleId]      INT            NULL,
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
    [CreatedDate]    DATETIME       NULL);

