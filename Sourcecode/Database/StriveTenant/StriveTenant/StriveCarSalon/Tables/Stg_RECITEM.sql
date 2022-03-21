CREATE TABLE [StriveCarSalon].[Stg_RECITEM] (
    [RecItemID]  INT       NOT NULL,
    [LocationID] INT       NOT NULL,
    [recId]      INT       NOT NULL,
    [ProdID]     INT       NOT NULL,
    [Comm]       MONEY     NULL,
    [Price]      MONEY     NULL,
    [QTY]        INT       NULL,
    [stotal]     MONEY     NULL,
    [taxable]    TINYINT   NULL,
    [taxamt]     MONEY     NULL,
    [gtotal]     MONEY     NULL,
    [peradj]     TINYINT   NULL,
    [GiftCardID] CHAR (10) NULL
);

