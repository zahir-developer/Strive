CREATE TABLE [StriveCarSalon].[tblBonusRange] (
    [BonusRangeId] INT                IDENTITY (1, 1) NOT NULL,
    [BonusId]      INT                NULL,
    [Min]          INT                NULL,
    [Max]          INT                NULL,
    [BonusAmount]  DECIMAL (16, 2)    NULL,
    [Total]        DECIMAL (16, 2)    NULL,
    [IsActive]     BIT                NULL,
    [IsDeleted]    BIT                NULL,
    [CreatedBy]    INT                NULL,
    [CreatedDate]  DATETIMEOFFSET (7) NULL,
    [UpdatedBy]    INT                NULL,
    [UpdatedDate]  DATETIMEOFFSET (7) NULL,
    CONSTRAINT [PK_tblBonusRange] PRIMARY KEY CLUSTERED ([BonusRangeId] ASC),
    CONSTRAINT [FK_tblBonusRange_BonusId] FOREIGN KEY ([BonusId]) REFERENCES [StriveCarSalon].[tblBonus] ([BonusId])
);

