﻿CREATE TABLE [StriveCarSalon].[tblJobItem] (
    [JobItemId]   INT                IDENTITY (1, 1) NOT NULL,
    [JobId]       INT                NULL,
    [ServiceId]   INT                NULL,
    [Commission]  DECIMAL (16, 2)    NULL,
    [Price]       DECIMAL (16, 2)    NULL,
    [Quantity]    INT                NULL,
    [ReviewNote]  VARCHAR (50)       NULL,
    [IsActive]    BIT                NULL,
    [IsDeleted]   BIT                NULL,
    [CreatedBy]   INT                NULL,
    [CreatedDate] DATETIMEOFFSET (7) NULL,
    [UpdatedBy]   INT                NULL,
    [UpdatedDate] DATETIMEOFFSET (7) NULL
);

