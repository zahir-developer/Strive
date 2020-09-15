CREATE TYPE [StriveCarSalonTest].[tvpService] AS TABLE (
    [ServiceId]       INT            NULL,
    [ServiceName]     NVARCHAR (200) NULL,
    [ServiceType]     INT            NULL,
    [LocationId]      INT            NULL,
    [Cost]            FLOAT (53)     NULL,
    [Commision]       BIT            NULL,
    [CommisionType]   INT            NULL,
    [Upcharges]       FLOAT (53)     NULL,
    [ParentServiceId] INT            NULL,
    [IsActive]        BIT            NULL,
    [DateEntered]     DATETIME       NULL,
    [CommissionCost]  DECIMAL (18)   NOT NULL);

