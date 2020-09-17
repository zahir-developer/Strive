CREATE TABLE [CON].[tblJob] (
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
    CONSTRAINT [PK_tblJob] PRIMARY KEY CLUSTERED ([JobId] ASC),
    CONSTRAINT [FK_tblJob_ClientId] FOREIGN KEY ([ClientId]) REFERENCES [CON].[tblClient] ([ClientId]),
    CONSTRAINT [FK_tblJob_JobStatus] FOREIGN KEY ([JobStatus]) REFERENCES [CON].[tblCodeValue] ([id]),
    CONSTRAINT [FK_tblJob_JobType] FOREIGN KEY ([JobType]) REFERENCES [CON].[tblCodeValue] ([id]),
    CONSTRAINT [FK_tblJob_LocationId] FOREIGN KEY ([LocationId]) REFERENCES [CON].[tblLocation] ([LocationId]),
    CONSTRAINT [FK_tblJob_VehicleId] FOREIGN KEY ([VehicleId]) REFERENCES [CON].[tblClientVehicle] ([VehicleId])
);


GO
CREATE NONCLUSTERED INDEX [IX_TblJob_ClientId]
    ON [CON].[tblJob]([ClientId] ASC);


GO
CREATE NONCLUSTERED INDEX [IX_TblJob_JobDate]
    ON [CON].[tblJob]([JobDate] ASC);

