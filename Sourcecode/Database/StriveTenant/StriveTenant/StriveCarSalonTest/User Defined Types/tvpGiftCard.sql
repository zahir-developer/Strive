CREATE TYPE [StriveCarSalonTest].[tvpGiftCard] AS TABLE (
    [GiftCardId]   INT            NOT NULL,
    [LocationId]   INT            NULL,
    [GiftCardCode] NVARCHAR (40)  NULL,
    [GiftCardName] NVARCHAR (80)  NULL,
    [ExpiryDate]   DATETIME       NULL,
    [Comments]     NVARCHAR (200) NULL,
    [IsActive]     BIT            NULL,
    [IsDeleted]    BIT            NULL);

