CREATE TABLE [StriveCarSalon].[Stg_GiftCardHist] (
    [LocationID]      INT           NOT NULL,
    [GiftCardTID]     INT           NOT NULL,
    [GiftCardID]      CHAR (10)     NOT NULL,
    [TransDte]        SMALLDATETIME NULL,
    [TransType]       CHAR (10)     NULL,
    [TransAmt]        MONEY         NULL,
    [TransUserID]     INT           NULL,
    [ALt_TransUserID] INT           NULL,
    [RecID]           INT           NULL
);

