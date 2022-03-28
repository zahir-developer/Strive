CREATE TABLE [StriveCarSalon].[Stg_CustAccHist] (
    [CustAccID]    INT           NOT NULL,
    [CustAccTID]   INT           NOT NULL,
    [TXLocationID] INT           NOT NULL,
    [TXCustID]     INT           NOT NULL,
    [TXRecID]      INT           NULL,
    [TXType]       CHAR (20)     NULL,
    [TXAmt]        MONEY         NULL,
    [TXDte]        SMALLDATETIME NULL,
    [TXuser]       INT           NULL,
    [TXNote]       TEXT          NULL,
    [Archive]      BIT           NULL,
    [InvoiceID]    INT           NULL
);

