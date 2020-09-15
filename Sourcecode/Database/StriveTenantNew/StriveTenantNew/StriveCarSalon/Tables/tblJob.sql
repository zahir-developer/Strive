CREATE TABLE [StriveCarSalon].[tblJob] (
    [JobId]            INT                IDENTITY (1, 1) NOT NULL,
    [TicketNumber]     VARCHAR (10)       NULL,
    [LocationId]       INT                NOT NULL,
    [ClientId]         INT                NULL,
    [VehicleId]        INT                NULL,
    [Make]             INT                NULL,
    [Model]            INT                NULL,
    [Color]            INT                NULL,
    [JobType]          INT                NULL,
    [JobDate]          DATE               NOT NULL,
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
    [Notes]            TEXT               NULL,
    CONSTRAINT [PK_tblJob] PRIMARY KEY CLUSTERED ([JobId] ASC),
    CONSTRAINT [FK_tblJob_ClientId] FOREIGN KEY ([ClientId]) REFERENCES [StriveCarSalon].[tblClient] ([ClientId]),
    CONSTRAINT [FK_tblJob_JobStatus] FOREIGN KEY ([JobStatus]) REFERENCES [StriveCarSalon].[tblCodeValue] ([id]),
    CONSTRAINT [FK_tblJob_JobType] FOREIGN KEY ([JobType]) REFERENCES [StriveCarSalon].[tblCodeValue] ([id]),
    CONSTRAINT [FK_tblJob_LocationId] FOREIGN KEY ([LocationId]) REFERENCES [StriveCarSalon].[tblLocation] ([LocationId]),
    CONSTRAINT [FK_tblJob_VehicleId] FOREIGN KEY ([VehicleId]) REFERENCES [StriveCarSalon].[tblClientVehicle] ([VehicleId])
);














GO
CREATE NONCLUSTERED INDEX [IX_TblJob_JobDate]
    ON [StriveCarSalon].[tblJob]([JobDate] ASC);


GO
CREATE NONCLUSTERED INDEX [IX_TblJob_ClientId]
    ON [StriveCarSalon].[tblJob]([ClientId] ASC);

