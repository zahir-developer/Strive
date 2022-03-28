CREATE TABLE [StriveCarSalon].[tblClientDeal] (
    [ClientDealId] INT      IDENTITY (1, 1) NOT NULL,
    [DealId]       INT      NULL,
    [ClientId]     INT      NULL,
    [DealCount]    INT      NULL,
    [StartDate]    DATETIME NULL,
    [EndDate]      DATETIME NULL,
    [UpdatedBy]    INT      NULL,
    [CreatedBy]    INT      NULL,
    [IsDeleted]    INT      NULL,
    CONSTRAINT [PK_tblClientDeal] PRIMARY KEY CLUSTERED ([ClientDealId] ASC),
    CONSTRAINT [FK_tblClientDeal_tblDeal] FOREIGN KEY ([DealId]) REFERENCES [StriveCarSalon].[tblDeal] ([DealId])
);

