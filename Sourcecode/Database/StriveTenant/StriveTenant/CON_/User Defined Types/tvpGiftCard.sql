CREATE TYPE [CON].[tvpGiftCard] AS TABLE (
    [GiftCardId]   INT            NOT NULL,
    [LocationId]   INT            NULL,
    [GiftCardCode] NVARCHAR (20)  NULL,
    [GiftCardName] NVARCHAR (40)  NULL,
    [ExpiryDate]   DATETIME       NULL,
    [Comments]     NVARCHAR (100) NULL,
    [IsActive]     BIT            NULL,
    [IsDeleted]    BIT            NULL);

