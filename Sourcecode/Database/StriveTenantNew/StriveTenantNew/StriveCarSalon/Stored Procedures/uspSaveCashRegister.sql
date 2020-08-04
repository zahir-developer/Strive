




CREATE PROCEDURE [StriveCarSalon].[uspSaveCashRegister]
@tvpCashRegister tvpCashRegister READONLY,
@tvpCashRegisterBills tvpCashRegisterBills READONLY,
@tvpCashRegisterCoins tvpCashRegisterCoins READONLY,
@tvpCashRegisterOthers tvpCashRegisterOthers READONLY,
@tvpCashRegisterRolls  tvpCashRegisterRolls READONLY
AS 
BEGIN

DECLARE @CashRegRollId int ,
        @CashRegOthersId int ,
        @CashRegCoinsId int ,
        @CashRegBillsId int,
		@DrawerId int
		
---CashRegisterRolls--
MERGE  [StriveCarSalon].[tblCashRegisterRolls] TRG
USING @tvpCashRegisterRolls SRC
ON (TRG.CashRegRollId = SRC.CashRegRollId)
WHEN MATCHED 
THEN

UPDATE SET TRG.Pennies=SRC.Pennies, TRG.Nickels=SRC.Nickels, TRG.Dimes=SRC.Dimes, TRG.Quarters=SRC.Quarters, 
      TRG.HalfDollars=SRC.HalfDollars, TRG.DateEntered = SRC.DateEntered
WHEN NOT MATCHED  THEN

INSERT (Pennies, Nickels, Dimes,Quarters,HalfDollars,DateEntered)
 VALUES (SRC.Pennies, SRC.Nickels, SRC.Dimes,SRC.Quarters,SRC.HalfDollars,SRC.DateEntered);
 SELECT @CashRegRollId = scope_identity();


----CashRegisterOthers--
MERGE  [StriveCarSalon].[tblCashRegisterOthers] TRG
USING @tvpCashRegisterOthers SRC
ON (TRG.CashRegOtherId = SRC.CashRegOtherId)
WHEN MATCHED 
THEN

UPDATE SET TRG.CreditCard1=SRC.CreditCard1, TRG.CreditCard2=SRC.CreditCard2, TRG.CreditCard3=SRC.CreditCard3, TRG.Checks=SRC.Checks, 
    TRG.Payouts=SRC.Payouts, TRG.DateEntered= SRC.DateEntered

WHEN NOT MATCHED THEN

INSERT (CreditCard1, CreditCard2, CreditCard3,Checks,Payouts,DateEntered)
 VALUES (SRC.CreditCard1, SRC.CreditCard2, SRC.CreditCard3,SRC.Checks,SRC.Payouts,SRC.DateEntered);
 SELECT @CashRegOthersId = scope_identity();

---CashRegisterCoins---
MERGE  [StriveCarSalon].[tblCashRegisterCoins] TRG
USING @tvpCashRegisterCoins SRC
ON (TRG.CashRegCoinId = SRC.CashRegCoinId)
WHEN MATCHED 
THEN
UPDATE SET TRG.Pennies=SRC.Pennies, TRG.Nickels=SRC.Nickels, TRG.Dimes=SRC.Dimes, TRG.Quarters=SRC.Quarters, 
    TRG.HalfDollars=SRC.HalfDollars, TRG.DateEntered= SRC.DateEntered
WHEN NOT MATCHED  THEN
INSERT (Pennies, Nickels, Dimes,Quarters,HalfDollars,DateEntered)
 VALUES (SRC.Pennies, SRC.Nickels, SRC.Dimes,SRC.Quarters,SRC.HalfDollars,SRC.DateEntered);
 SELECT @CashRegCoinsId = scope_identity();

---CashRegisterBills--
MERGE  [StriveCarSalon].[tblCashRegisterBills] TRG
USING @tvpCashRegisterBills SRC
ON (TRG.CashRegBillId = SRC.CashRegBillId)
WHEN MATCHED 
THEN
UPDATE SET TRG.[1s] =SRC.[1s], TRG.[5s]=SRC.[5s], TRG.[10s]=SRC.[10s], TRG.[20s]=SRC.[20s], 
    TRG.[50s]=SRC.[50s],TRG.[100s]=SRC.[100s],TRG.[DateEntered]= SRC.[DateEntered]
    WHEN NOT MATCHED  THEN

INSERT ([1s], [5s], [10s],[20s],[50s],[100s],DateEntered)
 VALUES (SRC.[1s], SRC.[5s], SRC.[10s],SRC.[20s],SRC.[50s],SRC.[100s],SRC.DateEntered);
 SELECT @CashRegBillsId = scope_identity();

 
 ---CallRegister -- 
MERGE  [StriveCarSalon].[tblCashRegister] TRG
USING @tvpCashRegister SRC
ON (TRG.CashRegisterId = SRC.CashRegisterId)
WHEN MATCHED 
THEN

UPDATE SET TRG.CashRegisterType = SRC.CashRegisterType, 
TRG.LocationId = SRC.LocationId, TRG.DrawerId = (SELECT Top 1 DrawerId FROM [StriveCarSalon].[tblDrawer] where LocationId = SRC.LocationId), TRG.UserId = SRC.UserId,
TRG.EnteredDateTime = SRC.EnteredDateTime, TRG.CashRegisterCoinId = @CashRegCoinsId, TRG.CashRegisterBillId = @CashRegBillsId, TRG.CashRegisterRollId = @CashRegRollId,
TRG.CashRegisterOtherId = @CashRegOthersId

WHEN NOT MATCHED  
THEN

INSERT (CashRegisterType, LocationId, DrawerId,UserId,EnteredDateTime,CashRegisterCoinId,CashRegisterBillId,CashRegisterRollId,CashRegisterOtherId)
VALUES (SRC.CashRegisterType, SRC.LocationId,(SELECT Top 1 DrawerId FROM [StriveCarSalon].[tblDrawer] where LocationId = SRC.LocationId),SRC.UserId,SRC.EnteredDateTime,@CashRegCoinsId,@CashRegBillsId,@CashRegRollId,@CashRegOthersId);


END