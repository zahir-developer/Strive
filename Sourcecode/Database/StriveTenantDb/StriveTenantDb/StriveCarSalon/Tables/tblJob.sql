CREATE TABLE [StriveCarSalon].[tblJob] (
    [JobId]            INT           IDENTITY (1, 1) NOT NULL,
    [TicketNumber]     NVARCHAR (10) NULL,
    [BarCode]          NVARCHAR (20) NULL,
    [LocationId]       INT           NULL,
    [ClientId]         INT           NULL,
    [JobType]          INT           NULL,
    [VehicleId]        INT           NULL,
    [TimeIn]           DATETIME      NULL,
    [EstimatedTimeOut] DATETIME      NULL,
    [ActualTimeOut]    DATETIME      NULL,
    [CreatedBy]        INT           NULL,
    [CreatedDate]      DATETIME      NULL,
    [JobStatus]        INT           NULL,
    CONSTRAINT [PK_tblJob] PRIMARY KEY CLUSTERED ([JobId] ASC)
);

