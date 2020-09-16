CREATE TYPE [StriveSuperAdminTest].[tvpClientVehicle] AS TABLE (
    [VehicleId]      INT            NULL,
    [ClientId]       INT            NULL,
    [LocationId]     INT            NULL,
    [VehicleNumber]  NVARCHAR (100) NULL,
    [VehicleMake]    INT            NULL,
    [VehicleModel]   INT            NULL,
    [VehicleModelNo] INT            NULL,
    [VehicleYear]    NVARCHAR (20)  NULL,
    [VehicleColor]   INT            NULL,
    [Upcharge]       INT            NULL,
    [Barcode]        NVARCHAR (100) NULL,
    [Notes]          NVARCHAR (200) NULL,
    [CreatedDate]    DATETIME       NULL);

