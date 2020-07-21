CREATE PROC [StriveCarSalon].[uspSaveCashRegisterOthers]
(@tvpCashRegisterOthers tvpCashRegisterOthers READONLY)
AS
BEGIN

MERGE  [StriveCarSalon].[tblCashRegisterOthers] TRG
USING @tvpCashRegisterOthers SRC
ON (TRG.CashRegOtherId = SRC.CashRegOtherId)
WHEN MATCHED 
THEN

UPDATE SET TRG.CreditCard1=SRC.CreditCard1, TRG.CreditCard2=SRC.CreditCard2, TRG.CreditCard3=SRC.CreditCard3, TRG.Checks=SRC.Checks, 
    TRG.Payouts=SRC.Payouts, TRG.DateEntered= SRC.DateEntered

WHEN NOT MATCHED  THEN

INSERT (CreditCard1, CreditCard2, CreditCard3,Checks,Payouts,DateEntered)
 VALUES (SRC.CreditCard1, SRC.CreditCard2, SRC.CreditCard3,SRC.Checks,SRC.Payouts,SRC.DateEntered)

 output inserted.CashRegOtherId;

END