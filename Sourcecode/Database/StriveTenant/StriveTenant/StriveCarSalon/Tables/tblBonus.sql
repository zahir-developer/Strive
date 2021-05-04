CREATE TABLE [StriveCarSalon].[tblBonus] (
    [BonusId]                  INT                IDENTITY (1, 1) NOT NULL,
    [LocationId]               INT                NULL,
    [BonusStatus]              INT                NULL,
    [BonusMonth]               INT                NULL,
    [BonusYear]                INT                NULL,
    [NoOfBadReviews]           INT                NULL,
    [BadReviewDeductionAmount] DECIMAL (16, 2)    NULL,
    [NoOfCollisions]           INT                NULL,
    [CollisionDeductionAmount] DECIMAL (16, 2)    NULL,
    [TotalBonusAmount]         DECIMAL (16, 2)    NULL,
    [IsActive]                 BIT                NULL,
    [IsDeleted]                BIT                NULL,
    [CreatedBy]                INT                NULL,
    [CreatedDate]              DATETIMEOFFSET (7) NULL,
    [UpdatedBy]                INT                NULL,
    [UpdatedDate]              DATETIMEOFFSET (7) NULL,
    CONSTRAINT [PK_tblBonus] PRIMARY KEY CLUSTERED ([BonusId] ASC),
    CONSTRAINT [FK_tblBonus_BonusStatus] FOREIGN KEY ([BonusStatus]) REFERENCES [StriveCarSalon].[tblCodeValue] ([id]),
    CONSTRAINT [FK_tblBonus_LocationId] FOREIGN KEY ([LocationId]) REFERENCES [StriveCarSalon].[tblLocation] ([LocationId])
);

