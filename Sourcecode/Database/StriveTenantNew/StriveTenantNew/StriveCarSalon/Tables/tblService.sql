﻿CREATE TABLE [StriveCarSalon].[tblService] (
    [ServiceId]       INT                IDENTITY (1, 1) NOT NULL,
    [ServiceName]     VARCHAR (50)       NULL,
    [ServiceType]     INT                NULL,
    [LocationId]      INT                NULL,
    [Cost]            FLOAT (53)         NULL,
    [Commision]       BIT                NULL,
    [CommisionType]   INT                NULL,
    [Upcharges]       FLOAT (53)         NULL,
    [ParentServiceId] INT                NULL,
    [CommissionCost]  DECIMAL (18, 2)    NULL,
    [IsActive]        BIT                NULL,
    [IsDeleted]       BIT                NULL,
    [CreatedBy]       INT                NULL,
    [CreatedDate]     DATETIMEOFFSET (7) NULL,
    [UpdatedBy]       INT                NULL,
    [UpdatedDate]     DATETIMEOFFSET (7) NULL,
    CONSTRAINT [PK_tblService] PRIMARY KEY CLUSTERED ([ServiceId] ASC),
    CONSTRAINT [FK_tblService_tblCodeValue] FOREIGN KEY ([ServiceType]) REFERENCES [StriveCarSalon].[tblCodeValue] ([id]),
    CONSTRAINT [FK_tblService_tblCodeValue1] FOREIGN KEY ([CommisionType]) REFERENCES [StriveCarSalon].[tblCodeValue] ([id]),
    CONSTRAINT [FK_tblService_tblLocation] FOREIGN KEY ([LocationId]) REFERENCES [StriveCarSalon].[tblLocation] ([LocationId])
);





