CREATE TABLE [Cus].[tblVehicleModel] (
    [VehicleModelId]          INT          IDENTITY (1, 1) NOT NULL,
    [VehicleModelDescription] VARCHAR (64) NOT NULL,
    [VehicleMakeId]           INT          NOT NULL,
    [IsActive]                BIT          NOT NULL,
    [CreatedDate]             DATETIME     CONSTRAINT [DF_tblVehicleModel_CreatedDate] DEFAULT (getdate()) NOT NULL,
    CONSTRAINT [PK_tblVehicleModel_VehicleMakeId] PRIMARY KEY CLUSTERED ([VehicleModelId] ASC)
);

