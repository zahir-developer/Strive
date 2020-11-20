CREATE TABLE [dbo].[LocationAddress] (
    [LocationAddressId] INT          IDENTITY (1, 1) NOT NULL,
    [LocationId]        INT          NULL,
    [Address1]          VARCHAR (50) NULL,
    [Address2]          VARCHAR (50) NULL,
    PRIMARY KEY CLUSTERED ([LocationAddressId] ASC)
);

