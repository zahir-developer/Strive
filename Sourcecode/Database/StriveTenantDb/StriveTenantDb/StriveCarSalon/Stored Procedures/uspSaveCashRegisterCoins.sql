CREATE PROC [StriveCarSalon].[uspSaveCashRegisterCoins]
(@tvpCashRegisterCoins tvpCashRegisterCoins READONLY)
AS
BEGIN

MERGE  [StriveCarSalon].[tblCashRegisterCoins] TRG
USING @tvpCashRegisterCoins SRC
ON (TRG.CashRegCoinId = SRC.CashRegCoinId)
WHEN MATCHED 
THEN
UPDATE SET TRG.Pennies=SRC.Pennies, TRG.Nickels=SRC.Nickels, TRG.Dimes=SRC.Dimes, TRG.Quarters=SRC.Quarters, 
    TRG.HalfDollars=SRC.HalfDollars, TRG.DateEntered= SRC.DateEntered
WHEN NOT MATCHED  THEN

INSERT (Pennies, Nickels, Dimes,Quarters,HalfDollars,DateEntered)
 VALUES (SRC.Pennies, SRC.Nickels, SRC.Dimes,SRC.Quarters,SRC.HalfDollars,SRC.DateEntered)

 output inserted.CashRegCoinId;

END