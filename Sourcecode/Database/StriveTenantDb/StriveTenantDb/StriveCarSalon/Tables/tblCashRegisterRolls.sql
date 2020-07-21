CREATE TABLE [StriveCarSalon].[tblCashRegisterRolls] (
    [CashRegRollId] INT      IDENTITY (1, 1) NOT NULL,
    [Pennies]       INT      NULL,
    [Nickels]       INT      NULL,
    [Dimes]         INT      NULL,
    [Quarters]      INT      NULL,
    [HalfDollars]   INT      NULL,
    [DateEntered]   DATETIME NULL,
    CONSTRAINT [PK_tblCashRegisterRolls] PRIMARY KEY CLUSTERED ([CashRegRollId] ASC)
);

