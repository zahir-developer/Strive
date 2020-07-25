﻿CREATE TABLE [StriveCarSalon].[tblProduct] (
    [ProductId]           INT                IDENTITY (1, 1) NOT NULL,
    [ProductName]         VARCHAR (50)       NULL,
    [ProductCode]         VARCHAR (10)       NULL,
    [ProductDescription]  VARCHAR (50)       NULL,
    [ProductType]         INT                NULL,
    [LocationId]          INT                NULL,
    [VendorId]            INT                NULL,
    [Size]                INT                NULL,
    [SizeDescription]     VARCHAR (10)       NULL,
    [Quantity]            FLOAT (53)         NULL,
    [QuantityDescription] VARCHAR (10)       NULL,
    [Cost]                FLOAT (53)         NULL,
    [IsTaxable]           BIT                NULL,
    [TaxAmount]           FLOAT (53)         NULL,
    [ThresholdLimit]      INT                NULL,
    [IsActive]            BIT                NULL,
    [IsDeleted]           BIT                NULL,
    [CreatedBy]           INT                NULL,
    [CreatedDate]         DATETIMEOFFSET (7) NULL,
    [UpdatedBy]           INT                NULL,
    [UpdatedDate]         DATETIMEOFFSET (7) NULL
);

