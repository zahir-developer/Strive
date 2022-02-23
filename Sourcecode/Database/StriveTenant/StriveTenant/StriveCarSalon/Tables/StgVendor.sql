CREATE TABLE [StriveCarSalon].[StgVendor] (
    [VendorID]   INT           NULL,
    [LocationID] INT           NOT NULL,
    [VenID]      INT           NOT NULL,
    [Vendor]     VARCHAR (50)  NULL,
    [vencontact] VARCHAR (50)  NULL,
    [venaddr1]   VARCHAR (50)  NULL,
    [venaddr2]   VARCHAR (50)  NULL,
    [vencity]    VARCHAR (50)  NULL,
    [venst]      VARCHAR (2)   NULL,
    [VenZip]     CHAR (5)      NULL,
    [venPhone]   CHAR (14)     NULL,
    [venFax]     CHAR (14)     NULL,
    [venURL]     VARCHAR (100) NULL,
    [venEmail]   VARCHAR (100) NULL,
    [venAcc]     VARCHAR (50)  NULL
);

