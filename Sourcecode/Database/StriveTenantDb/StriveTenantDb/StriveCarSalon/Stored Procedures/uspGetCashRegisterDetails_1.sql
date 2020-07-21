CREATE PROC [StriveCarSalon].[uspGetCashRegisterDetails] 
(
@LocationId int,
@CashRegisterType varchar(10), 
@EnteredDate varchar(10)
)

AS
BEGIN

DECLARE @CodeValueID INT = (
--DECLARE @CashRegisterType VARCHAR(10) = 'CloseOut'
Select TOP 1 CV.id from StriveCarSalon.tblCodeCategory CC
JOIN StriveCarSalon.tblCodeValue CV on CV.CategoryId = CC.id
WHERE CV.CodeValue = @CashRegisterType)

Select
TOP 1
CR.CashRegisterId AS CashRegisterId,
CR.CashRegisterType AS CashRegisterType,
CR.LocationId AS LocationId,
CR.DrawerId AS DrawerId,
CR.UserId AS UserId,
CR.EnteredDateTime AS EnteredDateTime,
CR.CashRegisterCoinId AS CashRegCoinId,
CR.CashRegisterBillId AS CashRegBillId,
CR.CashRegisterRollId AS CashRegRollId,
CR.CashRegisterOtherId AS CashRegOtherId,

CRC.CashRegCoinId AS CashRegisterCoin_CashRegCoinId,
CRC.Pennies AS CashRegisterCoin_Pennies,
CRC.Nickels AS CashRegisterCoin_Nickels,
CRC.Dimes AS CashRegisterCoin_Dimes,
CRC.Quarters AS CashRegisterCoin_Quarters,
CRC.HalfDollars AS CashRegisterCoin_HalfDollars,
CRC.DateEntered AS CashRegisterCoin_DateEntered,

CRB.CashRegBillId AS CashRegisterBill_CashRegBillId,
CRB.[1s] AS CashRegisterBill_Ones,
CRB.[5s] AS CashRegisterBill_Fives,
CRB.[10s] AS CashRegisterBill_Tens,
CRB.[20s] AS CashRegisterBill_Twenties,
CRB.[50s] AS CashRegisterBill_Fifties,
CRB.[100s] AS CashRegisterBill_Hundreds,
CRB.DateEntered AS CashRegisterBill_DateEntered,

CRR.CashRegRollId AS CashRegisterRoll_CashRegRollId,
CRR.Pennies AS CashRegisterRoll_Pennies,
CRR.Nickels AS CashRegisterRoll_Nickels,
CRR.Dimes AS CashRegisterRoll_Dimes,
CRR.Quarters AS CashRegisterRoll_Quarters,
CRR.HalfDollars AS CashRegisterRoll_HalfDollars,
CRR.DateEntered AS CashRegisterRoll_DateEntered,

CRO.CashRegOtherId AS CashRegisterOther_CashRegOtherId,
CRO.CreditCard1 AS CashRegisterOther_CreditCard1,
CRO.CreditCard2 AS CashRegisterOther_CreditCard2,
CRO.CreditCard3 AS CashRegisterOther_CreditCard3,
CRO.Checks AS CashRegisterOther_Checks,
CRO.Payouts AS CashRegisterOther_Payouts,
CRO.DateEntered AS CashRegisterOther_DateEntered

FROM [StriveCarSalon].[tblCashRegister] CR
INNER JOIN [StriveCarSalon].[tblCodeValue] CV on CV.id = CR.CashRegisterType
LEFT JOIN [StriveCarSalon].[tblCashRegisterBills] CRB on CRB.CashRegBillId = CR.CashRegisterBillId
LEFT JOIN [StriveCarSalon].[tblCashRegisterCoins] CRC on CRC.CashRegCoinId = CR.CashRegisterCoinId
LEFT JOIN [StriveCarSalon].[tblCashRegisterRolls] CRR on CRR.CashRegRollId = CR.CashRegisterRollId
LEFT JOIN [StriveCarSalon].[tblCashRegisterOthers] CRO on CRO.CashRegOtherId = CR.CashRegisterOtherId

WHERE CR.LocationId=@LocationId
AND CV.id = @CodeValueID
AND CONVERT(date, CR.EnteredDateTime) = @EnteredDate
AND CR.DrawerId in (SELECT DrawerId FROM tblDrawer WHERE LocationId=@LocationId) ORDER BY CR.EnteredDateTime DESC
END