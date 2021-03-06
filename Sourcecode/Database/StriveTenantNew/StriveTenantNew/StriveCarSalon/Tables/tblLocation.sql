CREATE TABLE [StriveCarSalon].[tblLocation] (
    [LocationId]          INT                IDENTITY (1, 1) NOT NULL,
    [LocationType]        INT                NULL,
    [LocationName]        VARCHAR (100)      NULL,
    [LocationDescription] VARCHAR (50)       NULL,
    [ColorCode]           NVARCHAR (10)      NULL,
    [WashTimeMinutes]     INT                NULL,
    [IsFranchise]         BIT                NULL,
    [TaxRate]             VARCHAR (10)       NULL,
    [SiteUrl]             VARCHAR (100)      NULL,
    [Currency]            INT                NULL,
    [Facebook]            VARCHAR (100)      NULL,
    [Twitter]             VARCHAR (100)      NULL,
    [Instagram]           VARCHAR (100)      NULL,
    [WifiDetail]          VARCHAR (20)       NULL,
    [WorkhourThreshold]   INT                NULL,
    [StartTime]           DATETIMEOFFSET (7) NULL,
    [EndTime]             DATETIMEOFFSET (7) NULL,
    [IsActive]            BIT                NULL,
    [IsDeleted]           BIT                NULL,
    [CreatedBy]           INT                NULL,
    [CreatedDate]         DATETIMEOFFSET (7) NULL,
    [UpdatedBy]           INT                NULL,
    [UpdatedDate]         DATETIMEOFFSET (7) NULL,
    CONSTRAINT [PK_tblLocation] PRIMARY KEY CLUSTERED ([LocationId] ASC),
    CONSTRAINT [FK_tblLocation_LocationType] FOREIGN KEY ([LocationType]) REFERENCES [StriveCarSalon].[tblCodeValue] ([id])
);









