CREATE TABLE [StriveCarSalon].[tblService] (
    [ServiceId]           INT                IDENTITY (1, 1) NOT NULL,
    [ServiceName]         VARCHAR (50)       NULL,
    [ServiceType]         INT                NULL,
    [LocationId]          INT                NULL,
    [Cost]                DECIMAL (19, 4)    NULL,
    [Commision]           BIT                NULL,
    [CommisionType]       INT                NULL,
    [Upcharges]           VARCHAR (100)      NULL,
    [ParentServiceId]     INT                NULL,
    [CommissionCost]      DECIMAL (18, 2)    NULL,
    [IsActive]            BIT                NULL,
    [IsDeleted]           BIT                NULL,
    [CreatedBy]           INT                NULL,
    [CreatedDate]         DATETIMEOFFSET (7) NULL,
    [UpdatedBy]           INT                NULL,
    [UpdatedDate]         DATETIMEOFFSET (7) NULL,
    [Description]         VARCHAR (250)      NULL,
    [DiscountServiceType] INT                NULL,
    [DiscountType]        VARCHAR (100)      NULL,
    [Price]               DECIMAL (19, 4)    NULL,
    [IsCeramic]           BIT                NULL,
    [ServiceCategory]     INT                NULL,
    [EstimatedTime]       DECIMAL (18, 2)    NULL,
    CONSTRAINT [PK_tblService] PRIMARY KEY CLUSTERED ([ServiceId] ASC)
);











