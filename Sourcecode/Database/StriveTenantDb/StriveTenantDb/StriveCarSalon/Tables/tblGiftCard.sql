CREATE TABLE [StriveCarSalon].[tblGiftCard] (
    [GiftCardId]   INT           IDENTITY (1, 1) NOT NULL,
    [LocationId]   INT           NULL,
    [GiftCardCode] NVARCHAR (10) NULL,
    [GiftCardName] NVARCHAR (20) NULL,
    [ExpiryDate]   DATETIME      NULL,
    [IsActive]     BIT           NULL,
    [IsDeleted]    BIT           NULL,
    [CreatedBy]    INT           NULL,
    [CreatedDate]  DATETIME      NULL,
    [Comments]     NVARCHAR (50) NULL,
    CONSTRAINT [PK_tblGiftCard] PRIMARY KEY CLUSTERED ([GiftCardId] ASC),
    CONSTRAINT [FK_tblGiftCard_tblLocation] FOREIGN KEY ([LocationId]) REFERENCES [StriveCarSalon].[tblLocation] ([LocationId])
);

