CREATE TYPE [StriveCarSalon].[tvpCashRegisterOthers] AS TABLE (
    [CashRegOtherId] INT             NULL,
    [CreditCard1]    DECIMAL (19, 4) NULL,
    [CreditCard2]    DECIMAL (19, 4) NULL,
    [CreditCard3]    DECIMAL (19, 4) NULL,
    [Checks]         DECIMAL (19, 4) NULL,
    [Payouts]        DECIMAL (19, 4) NULL,
    [DateEntered]    DATETIME        NULL);

