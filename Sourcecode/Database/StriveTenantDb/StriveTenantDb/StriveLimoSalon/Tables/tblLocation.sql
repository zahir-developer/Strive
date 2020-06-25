CREATE TABLE [StriveLimoSalon].[tblLocation] (
    [LocationId]          INT            IDENTITY (1, 1) NOT NULL,
    [LocationType]        INT            NULL,
    [LocationName]        VARCHAR (100)  NULL,
    [LocationDescription] NVARCHAR (200) NULL,
    [IsFranchise]         BIT            NULL,
    [IsActive]            BIT            NULL,
    [TaxRate]             NVARCHAR (50)  NULL,
    [SiteUrl]             NVARCHAR (100) NULL,
    [Currency]            INT            NULL,
    [Facebook]            NVARCHAR (200) NULL,
    [Twitter]             NVARCHAR (200) NULL,
    [Instagram]           NVARCHAR (200) NULL,
    [WifiDetail]          NVARCHAR (100) NULL,
    [WorkhourThreshold]   INT            NULL,
    CONSTRAINT [PK_tblLocation] PRIMARY KEY CLUSTERED ([LocationId] ASC)
);

