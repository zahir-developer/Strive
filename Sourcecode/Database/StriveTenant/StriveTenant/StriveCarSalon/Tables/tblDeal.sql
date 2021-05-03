CREATE TABLE [StriveCarSalon].[tblDeal] (
    [DealId]      INT                IDENTITY (1, 1) NOT NULL,
    [DealName]    VARCHAR (50)       NULL,
    [TimePeriod]  INT                NULL,
    [Deals]       BIT                NULL,
    [StartDate]   DATE               NULL,
    [EndDate]     DATE               NULL,
    [IsActive]    BIT                NULL,
    [IsDeleted]   BIT                NULL,
    [CreatedBy]   INT                NULL,
    [CreatedDate] DATETIMEOFFSET (7) NULL,
    [UpdatedBy]   INT                NULL,
    [UpdatedDate] DATETIMEOFFSET (7) NULL,
    CONSTRAINT [PK_tblDeal] PRIMARY KEY CLUSTERED ([DealId] ASC)
);

