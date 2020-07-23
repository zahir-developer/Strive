CREATE TABLE [StriveCarSalon].[tblTimeClock] (
    [Id]          BIGINT        IDENTITY (1, 1) NOT NULL,
    [UserId]      INT           NOT NULL,
    [LocationId]  INT           NOT NULL,
    [RoleId]      INT           NULL,
    [EventDate]   DATETIME      NULL,
    [InTime]      DATETIME      NULL,
    [OutTime]     DATETIME      NULL,
    [EventType]   INT           NULL,
    [UpdatedBy]   INT           NULL,
    [UpdatedFrom] NVARCHAR (10) NULL,
    [UpdatedDate] DATETIME      NULL,
    [Status]      BIT           CONSTRAINT [DF_tblTimeClock_Status] DEFAULT ((0)) NOT NULL,
    [Comments]    NVARCHAR (20) NOT NULL
);

