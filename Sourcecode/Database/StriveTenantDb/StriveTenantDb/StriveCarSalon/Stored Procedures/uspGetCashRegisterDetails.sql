USE [StriveTenantDb]
GO

/****** Object:  StoredProcedure [StriveCarSalon].[uspGetCashRegisterDetails]    Script Date: 03-07-2020 21:22:50 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

/*
--SP to Retrieve CashIn and Cashout Register Details
*/
ALTER PROC [StriveCarSalon].[uspGetCashRegisterDetails] 
(
--@LocationId int,
-- @CashRegisterType int,
-- @UserId int,
@EnteredDate datetime
)
AS
BEGIN
 Select
CR.CashRegisterId AS CashRegisterId,
CR.CashRegisterType AS CashRegisterType,
CR.LocationId AS LocationId,
CR.DrawerId AS DrawerId,
CR.UserId AS UserId,
CR.EnteredDateTime AS EnteredDateTime,
CR.CashRegCoinId AS CashRegisterCoinId,
CR.CashRegBillId AS CashRegisterBillId,
CR.CashRegRollId AS CashRegisterRollId,
CR.CashRegOtherId AS CashRegisterOtherId,

CRC.CashRegCoinId AS CashRegisterCoin_CashRegisterCoinId,
CRC.Pennies AS CashRegisterCoin_Pennies,
CRC.Nickels AS CashRegisterCoin_Nickels,
CRC.Dimes AS CashRegisterCoin_Dimes,
CRC.Quarters AS CashRegisterCoin_Quarters,
CRC.HalfDollars AS CashRegisterCoin_HalfDollars,
CRC.DateEntered AS CashRegisterCoin_DateEntered,


CRB.CashRegBillId AS CashRegisterBill_CashRegisterBillId,
CRB.[1s] AS CashRegisterBill_Ones,
CRB.[5s] AS CashRegisterBill_Fives,
CRB.[10s] AS CashRegisterBill_Tens,
CRB.[20s] AS CashRegisterBill_Twenties,
CRB.[50s] AS CashRegisterBill_Fifties,
CRB.[100s] AS CashRegisterBill_Hundreds,
CRB.DateEntered AS CashRegisterBill_DateEntered,

CRR.CashRegRollId AS CashRegisterRoll_CashRegisterRollId,
CRR.Pennies AS CashRegisterRoll_Pennies,
CRR.Nickels AS CashRegisterRoll_Nickels,
CRR.Dimes AS CashRegisterRoll_Dimes,
CRR.Quarters AS CashRegisterRoll_Quarters,
CRR.HalfDollars AS CashRegisterRoll_HalfDollars,
CRR.DateEntered AS CashRegisterRoll_DateEntered,

CRO.CashRegOtherId AS CashRegisterOther_CashRegisterOtherId,
CRO.CreditCard1 AS CashRegisterOther_CreditCard1,
CRO.CreditCard2 AS CashRegisterOther_CreditCard2,
CRO.CreditCard3 AS CashRegisterOther_CreditCard3,
CRO.Checks AS CashRegisterOther_Checks,
CRO.Payouts AS CashRegisterOther_Payouts,
CRO.DateEntered AS CashRegisterOther_DateEntered

FROM [StriveCarSalon].[tblCashRegister] CR
INNER JOIN [StriveCarSalon].[tblCodeValue] CV on CV.id = CR.CashRegisterType
LEFT JOIN [StriveCarSalon].[tblCashRegisterBills] CRB on CRB.CashRegBillId = CR.CashRegBillId
LEFT JOIN [StriveCarSalon].[tblCashRegisterCoins] CRC on CRC.CashRegCoinId = CR.CashRegCoinId
LEFT JOIN [StriveCarSalon].[tblCashRegisterRolls] CRR on CRR.CashRegRollId = CR.CashRegRollId
LEFT JOIN [StriveCarSalon].[tblCashRegisterOthers] CRO on CRO.CashRegOtherId = CR.CashRegOtherId

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

GO


