/*
02/07/2020 - Alter procedure [StriveCarSalon].[uspGetCashRegisterDetails] 
added few columns to fetch
*/
CREATE PROC [StriveCarSalon].[uspGetCashRegisterDetails_OLD] 
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
,cr.EnteredDateTime AS EnteredDateTime
,cv.codevalue		AS RegisterType 
,crc.pennies		AS CashRegisterCoin_Pennis
,crc.nickels		AS CashRegisterCoin_Nickels
,crc.dimes		    AS CashRegisterCoin_Dimes
,crc.quarters		AS CashRegisterCoin_Quarters
,crc.halfdollars	AS CashRegisterCoin_HalfDollars
,crb.[1s]			AS CashRegisterBill_Ones
,crb.[5s]			AS CashRegisterBill_Fives
,crb.[10s]			AS CashRegisterBill_Tens
,crb.[20s]			AS CashRegisterBill_Twenties
,crb.[50s]			AS CashRegisterBill_Fifties
,crb.[100s]			AS CashRegisterBill_Hundreds
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
 [StriveCarSalon].tblcashregister					cr 
 INNER	JOIN [StriveCarSalon].tblcodevalue			cv		ON cr.cashregistertype=cv.id
 LEFT	JOIN [StriveCarSalon].tblcashregistercoins	crc		ON cr.coins=crc.cashregcoinid
 LEFT	JOIN [StriveCarSalon].tblcashregisterBills	crb		ON cr.coins=crb.cashregBillid
 LEFT	JOIN [StriveCarSalon].tblcashregisterRolls	crr		ON cr.coins=crr.cashregrollid
 LEFT	JOIN [StriveCarSalon].tblcashregisterOthers	cro		ON cr.coins=cro.cashregotherid
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