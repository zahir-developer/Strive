CREATE TABLE [StriveCarSalon].[Stg_CustAcc] (
    [CustAccID]     INT           NOT NULL,
    [LocationID]    INT           NOT NULL,
    [ClientID]      INT           NOT NULL,
    [VehID]         INT           NULL,
    [CurrentAmt]    MONEY         NULL,
    [ActiveDte]     SMALLDATETIME NULL,
    [LastUpdate]    SMALLDATETIME NULL,
    [LastUpdateBy]  INT           NULL,
    [Type]          INT           NULL,
    [Status]        INT           NULL,
    [MonthlyCharge] MONEY         NULL,
    [Limit]         MONEY         NULL,
    [CCno]          CHAR (20)     NULL,
    [cctype]        INT           NULL,
    [CCEXP]         CHAR (5)      NULL
);

