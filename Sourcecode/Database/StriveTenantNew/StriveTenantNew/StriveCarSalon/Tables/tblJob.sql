CREATE TABLE [StriveCarSalon].[tblJob] (
    [JobId]            INT                IDENTITY (1, 1) NOT NULL,
    [TicketNumber]     VARCHAR (10)       NULL,
    [BarCode]          VARCHAR (20)       NULL,
    [LocationId]       INT                NULL,
    [ClientId]         INT                NULL,
    [JobType]          INT                NULL,
    [VehicleId]        INT                NULL,
    [TimeIn]           DATETIMEOFFSET (7) NULL,
    [EstimatedTimeOut] DATETIMEOFFSET (7) NULL,
    [ActualTimeOut]    DATETIMEOFFSET (7) NULL,
    [JobStatus]        INT                NULL,
    [IsActive]         BIT                NULL,
    [IsDeleted]        BIT                NULL,
    [CreatedBy]        INT                NULL,
    [CreatedDate]      DATETIMEOFFSET (7) NULL,
    [UpdatedBy]        INT                NULL,
    [UpdatedDate]      DATETIMEOFFSET (7) NULL,
    CONSTRAINT [PK_tblJob] PRIMARY KEY CLUSTERED ([JobId] ASC),
    CONSTRAINT [FK_tblJob_tblClient] FOREIGN KEY ([ClientId]) REFERENCES [StriveCarSalon].[tblClient] ([ClientId]),
    CONSTRAINT [FK_tblJob_tblClientVehicle] FOREIGN KEY ([VehicleId]) REFERENCES [StriveCarSalon].[tblClientVehicle] ([VehicleId]),
    CONSTRAINT [FK_tblJob_tblLocation] FOREIGN KEY ([LocationId]) REFERENCES [StriveCarSalon].[tblLocation] ([LocationId])
);



