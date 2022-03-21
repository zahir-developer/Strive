CREATE TABLE [StriveCarSalon].[tblJob] (
    [JobId]            INT                IDENTITY (1, 1) NOT NULL,
    [TicketNumber]     NVARCHAR (50)      NULL,
    [LocationId]       INT                NOT NULL,
    [ClientId]         INT                NULL,
    [VehicleId]        INT                NULL,
    [Barcode]          NVARCHAR (50)      NULL,
    [Make]             INT                NULL,
    [Model]            INT                NULL,
    [Color]            INT                NULL,
    [Notes]            TEXT               NULL,
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
    [CheckOut]         BIT                CONSTRAINT [DF__tblJob__CheckOut] DEFAULT ((0)) NULL,
    [CheckOutTime]     DATETIME           NULL,
    [JobPaymentId]     INT                NULL,
    [IsHold]           BIT                NULL,
    [RefRecId]         INT                NULL,
    [VehicleModelDesc] VARCHAR (250)      NULL,
    [JobStartTime]     DATETIMEOFFSET (7) NULL,
    [VTypeId]          INT                NULL,
    CONSTRAINT [PK_tblJob] PRIMARY KEY CLUSTERED ([JobId] ASC),
    CONSTRAINT [FK_tblJob_ClientId] FOREIGN KEY ([ClientId]) REFERENCES [StriveCarSalon].[tblClient] ([ClientId]),
    CONSTRAINT [FK_tblJob_JobPaymentId] FOREIGN KEY ([JobPaymentId]) REFERENCES [StriveCarSalon].[tblJobPayment] ([JobPaymentId]),
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

