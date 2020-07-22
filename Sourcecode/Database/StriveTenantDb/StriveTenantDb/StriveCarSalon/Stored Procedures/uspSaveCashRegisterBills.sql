CREATE PROC [StriveCarSalon].[uspSaveCashRegisterBills]
(@tvpCashRegisterBills tvpCashRegisterBills READONLY)
AS
BEGIN

MERGE  [StriveCarSalon].[tblCashRegisterBills] TRG
USING @tvpCashRegisterBills SRC
ON (TRG.CashRegBillId = SRC.CashRegBillId)
WHEN MATCHED 
THEN
	UPDATE SET 
	TRG.[1s] =SRC.[1s], 
	TRG.[5s]=SRC.[5s], 
	TRG.[10s]=SRC.[10s], 
	TRG.[20s]=SRC.[20s], 
	TRG.[50s]=SRC.[50s],
	TRG.[100s]=SRC.[100s],
	TRG.[DateEntered]= SRC.[DateEntered]

WHEN NOT MATCHED  THEN

	INSERT ([1s], [5s], [10s],[20s],[50s],[100s],DateEntered)
	VALUES (SRC.[1s], SRC.[5s], SRC.[10s],SRC.[20s],SRC.[50s],SRC.[100s],SRC.DateEntered)

 output inserted.CashRegBillId;

END