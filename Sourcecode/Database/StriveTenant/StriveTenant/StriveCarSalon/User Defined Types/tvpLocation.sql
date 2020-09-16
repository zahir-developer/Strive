CREATE TYPE [StriveCarSalon].[tvpLocation] AS TABLE (
    [LocationId]          INT            NULL,
    [LocationType]        INT            NULL,
    [LocationName]        VARCHAR (100)  NULL,
    [LocationDescription] NVARCHAR (400) NULL,
    [IsFranchise]         BIT            NULL,
    [IsActive]            BIT            NULL,
    [TaxRate]             NVARCHAR (100) NULL,
    [SiteUrl]             NVARCHAR (200) NULL,
    [Currency]            INT            NULL,
    [Facebook]            NVARCHAR (400) NULL,
    [Twitter]             NVARCHAR (400) NULL,
    [Instagram]           NVARCHAR (400) NULL,
    [WifiDetail]          NVARCHAR (200) NULL,
    [WorkhourThreshold]   INT            NULL);

