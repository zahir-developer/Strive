CREATE TABLE [StriveCarSalon].[tblGiftCard] (
    [GiftCardId]     INT                IDENTITY (1, 1) NOT NULL,
    [LocationId]     INT                NULL,
    [ClientId]       INT                NULL,
    [GiftCardCode]   VARCHAR (10)       NULL,
    [GiftCardName]   VARCHAR (20)       NULL,
    [ActivationDate] DATETIMEOFFSET (7) NULL,
    [TotalAmount]    DECIMAL (18, 2)    NULL,
    [Comments]       VARCHAR (50)       NULL,
    [IsActive]       BIT                NULL,
    [IsDeleted]      BIT                NULL,
    [CreatedBy]      INT                NULL,
    [CreatedDate]    DATETIMEOFFSET (7) NULL,
    [UpdatedBy]      INT                NULL,
    [UpdatedDate]    DATETIMEOFFSET (7) NULL,
    [Barcode]        VARCHAR (50)       NULL,
    [Email]          VARCHAR (100)      NULL,
    CONSTRAINT [PK_tblGiftCard] PRIMARY KEY CLUSTERED ([GiftCardId] ASC),
    CONSTRAINT [FK_tblGiftCard_LocationId] FOREIGN KEY ([LocationId]) REFERENCES [StriveCarSalon].[tblLocation] ([LocationId]),
    CONSTRAINT [FK_tblGiftCard_tblClient] FOREIGN KEY ([ClientId]) REFERENCES [StriveCarSalon].[tblClient] ([ClientId])
);













