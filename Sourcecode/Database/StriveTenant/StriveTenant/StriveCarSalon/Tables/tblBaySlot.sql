CREATE TABLE [StriveCarSalon].[tblBaySlot] (
    [BaySlotId]   INT                IDENTITY (1, 1) NOT NULL,
    [BayId]       INT                NULL,
    [Slot]        TIME (7)           NULL,
    [IsActive]    BIT                NULL,
    [IsDeleted]   BIT                NULL,
    [CreatedBy]   INT                NULL,
    [CreatedDate] DATETIMEOFFSET (7) NULL,
    [UpdatedBy]   INT                NULL,
    [UpdatedDate] DATETIMEOFFSET (7) NULL,
    CONSTRAINT [PK_tblBaySlot] PRIMARY KEY CLUSTERED ([BaySlotId] ASC),
    CONSTRAINT [FK_tblBaySlot_BayId] FOREIGN KEY ([BayId]) REFERENCES [StriveCarSalon].[tblBay] ([BayId])
);

