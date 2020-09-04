

CREATE PROCEDURE [CON].[uspGetCashRegisterDetails_OLD] 
(
--@LocationId int,
-- @CashRegisterType int,
-- @UserId int,
@EnteredDate datetime
)
AS
BEGIN
 SELECT 
 cr.CashRegisterId  AS CashRegisterId
,cr.CashRegisterDate AS EnteredDateTime
,cv.codevalue		AS RegisterType 
,crc.pennies		AS CashRegisterCoin_Pennis
,crc.nickels		AS CashRegisterCoin_Nickels
,crc.dimes		    AS CashRegisterCoin_Dimes
,crc.quarters		AS CashRegisterCoin_Quarters
,crc.halfdollars	AS CashRegisterCoin_HalfDollars
,crb.[S1]			AS CashRegisterBill_Ones
,crb.[S5]			AS CashRegisterBill_Fives
,crb.[S10]			AS CashRegisterBill_Tens
,crb.[S20]			AS CashRegisterBill_Twenties
,crb.[S50]			AS CashRegisterBill_Fifties
,crb.[S100]			AS CashRegisterBill_Hundreds
,crr.pennies		AS CashRegisterRoll_Pennies
,crr.nickels		AS CashRegisterRoll_Nickels
,crr.dimes		    AS CashRegisterRoll_Dimes
,crr.quarters		AS CashRegisterRoll_Quarters
,crr.Halfdollars	AS CashRegisterRoll_HalfDollars
,cro.creditcard1 	AS CashRegisterOther_CreditCard1
,cro.creditcard2 	AS CashRegisterOther_CreditCard2
,cro.creditcard3 	AS CashRegisterOther_CreditCard3
,cro.checks 	    AS CashRegisterOther_Checks
,cro.payouts 	    AS CashRegisterOther_Payouts

 FROM
 [CON].tblcashregister					cr 
 INNER	JOIN [CON].tblcodevalue			cv		ON cr.cashregistertype=cv.id
 LEFT	JOIN [CON].tblcashregistercoins	crc		ON cr.CashRegisterID=crc.CashRegisterID
 LEFT	JOIN [CON].tblcashregisterBills	crb		ON cr.CashRegisterID=crb.CashRegisterID
 LEFT	JOIN [CON].tblcashregisterRolls	crr		ON cr.CashRegisterID=crr.CashRegisterID
 LEFT	JOIN [CON].tblcashregisterOthers	cro		ON cr.CashRegisterID=cro.CashRegisterID
 --WHERE
 --cr.LocationId=@LocationId  
 --AND
 --cr.CashRegisterType =@CashRegisterType
 --AND
-- cr.EnteredDateTime = @EnteredDate
 --AND
 --cr.UserId =@UserId
 --AND
 --cr.DrawerId in (SELECT DrawerId FROM tblDrawer WHERE LocationId=@LocationId)
 END