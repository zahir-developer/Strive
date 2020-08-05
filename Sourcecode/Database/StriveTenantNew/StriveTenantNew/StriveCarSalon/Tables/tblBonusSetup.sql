CREATE TABLE [StriveCarSalon].[tblBonusSetup] (
    [BonusSetupId]           INT                IDENTITY (1, 1) NOT NULL,
    [LocationId]             INT                NULL,
    [BonusMonth]             INT                NULL,
    [BonusYear]              INT                NULL,
    [BonusSlabName]          VARCHAR (10)       NULL,
    [MininumRange]           INT                NULL,
    [MaximumRange]           INT                NULL,
    [BonusAmount]            DECIMAL (16)       NULL,
    [StartsWithPaymentCycle] INT                NULL,
    [Comments]               VARCHAR (50)       NULL,
    [IsActive]               BIT                NULL,
    [IsDeleted]              BIT                NULL,
    [CreatedBy]              INT                NULL,
    [CreatedDate]            DATETIMEOFFSET (7) NULL,
    [UpdatedBy]              INT                NULL,
    [UpdatedDate]            DATETIMEOFFSET (7) NULL,
    CONSTRAINT [PK_tblBonusSetup] PRIMARY KEY CLUSTERED ([BonusSetupId] ASC),
    CONSTRAINT [FK_tblBonusSetup_LocationId] FOREIGN KEY ([LocationId]) REFERENCES [StriveCarSalon].[tblLocation] ([LocationId])
);





