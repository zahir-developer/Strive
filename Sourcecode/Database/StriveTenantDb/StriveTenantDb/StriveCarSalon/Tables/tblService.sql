CREATE TABLE [StriveCarSalon].[tblService] (
    [ServiceId]       INT             IDENTITY (1, 1) NOT NULL,
    [ServiceName]     NVARCHAR (50)   NULL,
    [ServiceType]     INT             NULL,
    [LocationId]      INT             NULL,
    [Cost]            FLOAT (53)      NULL,
    [Commision]       BIT             NULL,
    [CommisionType]   INT             NULL,
    [Upcharges]       FLOAT (53)      NULL,
    [ParentServiceId] INT             NULL,
    [IsActive]        BIT             NULL,
    [DateEntered]     DATETIME        NULL,
    [CommissionCost]  DECIMAL (18, 2) NULL,
    CONSTRAINT [PK_tblService] PRIMARY KEY CLUSTERED ([ServiceId] ASC),
    CONSTRAINT [FK_tblService_tblCodeValue] FOREIGN KEY ([ServiceType]) REFERENCES [StriveCarSalon].[tblCodeValue] ([id]),
    CONSTRAINT [FK_tblService_tblLocation] FOREIGN KEY ([LocationId]) REFERENCES [StriveCarSalon].[tblLocation] ([LocationId])
);





