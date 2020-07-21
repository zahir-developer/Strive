CREATE TYPE [StriveCarSalon].[tvpCashRegisterCoins] AS TABLE (
    [CashRegCoinId] INT      NULL,
    [Pennies]       INT      NULL,
    [Nickels]       INT      NULL,
    [Dimes]         INT      NULL,
    [Quarters]      INT      NULL,
    [HalfDollars]   INT      NULL,
    [DateEntered]   DATETIME NULL);

