CREATE TABLE [dbo].[tblSubscriptionMaster] (
    [SubscriptionId]   INT           IDENTITY (1, 1) NOT NULL,
    [SubscriptionName] VARCHAR (100) NOT NULL,
    [StatusId]         INT           NOT NULL,
    [IsDeleted]        SMALLINT      NOT NULL,
    [CreatedEmpId]     INT           NOT NULL,
    [ExpiryDate]       DATETIME      NULL,
    [CreatedDate]      DATETIME      NULL,
    CONSTRAINT [PK_SubscriptionMaster_SubscriptionId] PRIMARY KEY CLUSTERED ([SubscriptionId] ASC),
    CONSTRAINT [UK_SubscriptionMaster_SubscriptionName] UNIQUE NONCLUSTERED ([SubscriptionName] ASC)
);

