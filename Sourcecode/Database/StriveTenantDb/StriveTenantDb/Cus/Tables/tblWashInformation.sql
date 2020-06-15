CREATE TABLE [Cus].[tblWashInformation] (
    [CustomerId]     BIGINT         IDENTITY (1, 1) NOT NULL,
    [VehicleMakeId]  INT            NOT NULL,
    [VehicleModelId] BIGINT         NOT NULL,
    [VehicleColorid] INT            NOT NULL,
    [DateIn]         DATE           NOT NULL,
    [DateOut]        DATE           NOT NULL,
    [TimeIn]         TIME (7)       NOT NULL,
    [TimeOut]        TIME (7)       NOT NULL,
    [ResEmployeeId]  INT            NOT NULL,
    [Amount]         DECIMAL (9, 2) NULL,
    [IsActive]       BIT            CONSTRAINT [DF_tblWashInformation_IsActive] DEFAULT ((1)) NOT NULL,
    [CreatedDate]    DATETIME       CONSTRAINT [DF_tblWashInformation_CreatedDate] DEFAULT (getdate()) NOT NULL,
    CONSTRAINT [PK_tblWashInformation_CustomerId] PRIMARY KEY CLUSTERED ([CustomerId] ASC)
);

