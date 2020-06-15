CREATE TABLE [Cus].[tblVehicleColor] (
    [VehicleColorid]          INT          IDENTITY (1, 1) NOT NULL,
    [VehicleColorDescription] VARCHAR (32) NOT NULL,
    [IsActive]                BIT          NOT NULL,
    [CreatedDate]             DATETIME     CONSTRAINT [DF_tblVehicleColor_CreatedDate] DEFAULT (getdate()) NOT NULL,
    CONSTRAINT [PK_tblVehicleColor_VehicleColorid] PRIMARY KEY CLUSTERED ([VehicleColorid] ASC)
);

