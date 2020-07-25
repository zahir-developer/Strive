CREATE TABLE [StriveCarSalon].[tblClientVehicle] (
    [VehicleId]      INT                NOT NULL,
    [ClientId]       INT                NULL,
    [LocationId]     INT                NULL,
    [VehicleNumber]  VARCHAR (20)       NULL,
    [VehicleMake]    INT                NULL,
    [VehicleModel]   INT                NULL,
    [VehicleModelNo] INT                NULL,
    [VehicleYear]    VARCHAR (6)        NULL,
    [VehicleColor]   INT                NULL,
    [Upcharge]       INT                NULL,
    [Barcode]        NVARCHAR (50)      NULL,
    [Notes]          VARCHAR (20)       NULL,
    [IsActive]       BIT                NULL,
    [IsDeleted]      BIT                NULL,
    [CreatedBy]      INT                NULL,
    [CreatedDate]    DATETIMEOFFSET (7) NULL,
    [UpdatedBy]      INT                NULL,
    [UpdatedDate]    DATETIMEOFFSET (7) NULL
);

