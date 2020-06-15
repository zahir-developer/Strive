CREATE TABLE [Cus].[tblVehicleMake] (
    [VehicleMakeId]          INT          IDENTITY (1, 1) NOT NULL,
    [VehicleMakeDescription] VARCHAR (32) NOT NULL,
    [IsActive]               BIT          NOT NULL,
    [CreatedDate]            DATETIME     CONSTRAINT [DF_tblVehicleMake_CreatedDate] DEFAULT (getdate()) NOT NULL,
    CONSTRAINT [PK_tblVehicleMake_VehicleMakeId] PRIMARY KEY CLUSTERED ([VehicleMakeId] ASC)
);

