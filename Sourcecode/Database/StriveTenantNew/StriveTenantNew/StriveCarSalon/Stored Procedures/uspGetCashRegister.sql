
CREATE PROC [StriveCarSalon].[uspGetCashRegister] 
(@LocationId int)
AS
BEGIN
 SELECT 
 cr.cashregisterid  AS CashRegisterId,
 cr.EnteredDateTime AS EnteredDateTime,
 cv.codevalue		AS RegisterType, 
 crc.pennies		AS CashRegisterCoins_Pennis,
 crc.nickels		AS CashRegisterCoins_Nickels,
 crb.[1s]			AS CashRegisterBills_Ones,
 crb.[5s]			AS CashRegisterBills_Fives,
 crr.pennies		AS CashRegisterRolls_pennies,
 crr.nickels		AS CashRegisterRolls_nickels,
 cro.[checks]		AS CashRegisterOther_checks,
 cro.creditcard1 	AS CashRegisterOther_Creditcard1
 FROM
 tblcashregister					cr 
 INNER	JOIN tblcodevalue			cv		ON cr.cashregistertype=cv.id
 LEFT	JOIN StriveCarSalon.tblcashregistercoins	crc		ON crc.CashRegCoinId= crc.cashregcoinid
 LEFT	JOIN StriveCarSalon.tblcashregisterBills	crb		ON crb.CashRegBillId= crb.cashregBillid
 LEFT	JOIN StriveCarSalon.tblcashregisterRolls	crr		ON crr.CashRegRollId=crr.cashregrollid
 LEFT	JOIN StriveCarSalon.tblcashregisterOthers	cro		ON cr.CashRegOtherId=cro.cashregotherid
 WHERE
 cr.LocationId=@LocationId  
 AND
 cr.DrawerId in (SELECT DrawerId FROM tblDrawer WHERE LocationId=@LocationId)
 END