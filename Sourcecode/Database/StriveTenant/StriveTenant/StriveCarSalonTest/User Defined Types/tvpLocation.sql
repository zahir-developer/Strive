CREATE TYPE [StriveCarSalonTest].[tvpLocation] AS TABLE (
    [LocationId]          INT            NULL,
    [LocationType]        INT            NULL,
    [LocationName]        VARCHAR (100)  NULL,
    [LocationDescription] NVARCHAR (800) NULL,
    [IsFranchise]         BIT            NULL,
    [IsActive]            BIT            NULL,
    [TaxRate]             NVARCHAR (200) NULL,
    [SiteUrl]             NVARCHAR (400) NULL,
    [Currency]            INT            NULL,
    [Facebook]            NVARCHAR (800) NULL,
    [Twitter]             NVARCHAR (800) NULL,
    [Instagram]           NVARCHAR (800) NULL,
    [WifiDetail]          NVARCHAR (400) NULL,
    [WorkhourThreshold]   INT            NULL);

