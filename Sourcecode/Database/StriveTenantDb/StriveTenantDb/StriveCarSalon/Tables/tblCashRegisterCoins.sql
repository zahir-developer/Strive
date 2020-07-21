CREATE TABLE [StriveCarSalon].[tblCashRegisterCoins] (
    [CashRegCoinId] INT      IDENTITY (1, 1) NOT NULL,
    [Pennies]       INT      NULL,
    [Nickels]       INT      NULL,
    [Dimes]         INT      NULL,
    [Quarters]      INT      NULL,
    [HalfDollars]   INT      NULL,
    [DateEntered]   DATETIME NULL,
    CONSTRAINT [PK_tblCashRegisterCoins] PRIMARY KEY CLUSTERED ([CashRegCoinId] ASC)
);

