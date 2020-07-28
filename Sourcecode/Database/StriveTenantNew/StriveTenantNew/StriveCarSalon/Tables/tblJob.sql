CREATE TABLE [StriveCarSalon].[tblJob] (
    [JobId]            INT                IDENTITY (1, 1) NOT NULL,
    [TicketNumber]     VARCHAR (10)       NULL,
    [BarCode]          VARCHAR (20)       NULL,
    [LocationId]       INT                NOT NULL,
    [ClientId]         INT                NULL,
    [VehicleId]        INT                NULL,
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
    CONSTRAINT [FK_tblJob_tblClient] FOREIGN KEY ([ClientId]) REFERENCES [StriveCarSalon].[tblClient] ([ClientId]),
    CONSTRAINT [FK_tblJob_tblClientVehicle] FOREIGN KEY ([VehicleId]) REFERENCES [StriveCarSalon].[tblClientVehicle] ([VehicleId]),
    CONSTRAINT [FK_tblJob_tblCodeValue] FOREIGN KEY ([JobType]) REFERENCES [StriveCarSalon].[tblCodeValue] ([id]),
    CONSTRAINT [FK_tblJob_tblCodeValue1] FOREIGN KEY ([JobStatus]) REFERENCES [StriveCarSalon].[tblCodeValue] ([id]),
    CONSTRAINT [FK_tblJob_tblLocation] FOREIGN KEY ([LocationId]) REFERENCES [StriveCarSalon].[tblLocation] ([LocationId])
);






GO
CREATE NONCLUSTERED INDEX [IX_TblJob_JobDate]
    ON [StriveCarSalon].[tblJob]([JobDate] ASC);


GO
CREATE NONCLUSTERED INDEX [IX_TblJob_ClientId]
    ON [StriveCarSalon].[tblJob]([ClientId] ASC);

