
CREATE PROCEDURE [CON].[uspSaveCashRegisterMain]
--(@tvpCashRegister tvpCashRegister READONLY)
AS
BEGIN

SELECT GETDATE()
/*
MERGE  [CON].[tblCashRegister] TRG
USING @tvpCashRegister SRC
ON (TRG.CashRegisterId = SRC.CashRegisterId)
WHEN MATCHED 
THEN
--set @DrawerId = select DrawerId from [CON].[tblDrawer] where LocationId = SRC.LocationId;

UPDATE SET TRG.CashRegisterType = SRC.CashRegisterType, 
TRG.LocationId = SRC.LocationId, TRG.DrawerId = (SELECT Top 1 DrawerId FROM [CON].[tblDrawer] where LocationId = SRC.LocationId), TRG.UserId = SRC.UserId,
TRG.EnteredDateTime = SRC.EnteredDateTime, TRG.CashRegisterCoinId = SRC.CashRegCoinId, TRG.CashRegisterBillId = SRC.CashRegBillId, TRG.CashRegisterRollId = SRC.CashRegRollId,
TRG.CashRegisterOtherId = SRC.CashRegOtherId
--SELECT @DrawerId =  DrawerId FROM [CON].[tblDrawer] where LocationId = LocationId

WHEN NOT MATCHED  
THEN

INSERT (CashRegisterType, LocationId, DrawerId,UserId,EnteredDateTime,CashRegisterCoinId,CashRegisterBillId,CashRegisterRollId,CashRegisterOtherId)
VALUES (SRC.CashRegisterType, SRC.LocationId,(SELECT Top 1 DrawerId FROM [CON].[tblDrawer] where LocationId = SRC.LocationId),
SRC.UserId,
SRC.EnteredDateTime,
SRC.CashRegCoinId,
SRC.CashRegBillId,
SRC.CashRegRollId,
SRC.CashRegOtherId);
--SELECT @DrawerId =  DrawerId FROM [CON].[tblDrawer] where LocationId = LocationId
*/
END