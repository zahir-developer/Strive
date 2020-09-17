CREATE PROCEDURE [StriveCarSalon].[uspGetCashRegisterDetails] 
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
--CR.UserId AS UserId,
CR.CashRegisterDate AS EnteredDateTime,
CRC.CashRegCoinId,
CRB.CashRegBillId, 
CRR.CashRegRollId,
CRO.CashRegOtherId,

CRC.CashRegCoinId AS CashRegisterCoin_CashRegisterCoinId,
CRC.Pennies AS CashRegisterCoin_Pennies,
CRC.Nickels AS CashRegisterCoin_Nickels,
CRC.Dimes AS CashRegisterCoin_Dimes,
CRC.Quarters AS CashRegisterCoin_Quarters,
CRC.HalfDollars AS CashRegisterCoin_HalfDollars,
CRC.CreatedDate AS CashRegisterCoin_DateEntered,

CRB.CashRegBillId AS CashRegisterBill_CashRegisterBillId,
CRB.[s1] AS CashRegisterBill_Ones,
CRB.[s5] AS CashRegisterBill_Fives,
CRB.[s10] AS CashRegisterBill_Tens,
CRB.[s20] AS CashRegisterBill_Twenties,
CRB.[s50] AS CashRegisterBill_Fifties,
CRB.[s100] AS CashRegisterBill_Hundreds,
CRB.CreatedDate AS CashRegisterBill_DateEntered,

CRR.CashRegRollId AS CashRegisterRoll_CashRegisterRollId,
CRR.Pennies AS CashRegisterRoll_Pennies,
CRR.Nickels AS CashRegisterRoll_Nickels,
CRR.Dimes AS CashRegisterRoll_Dimes,
CRR.Quarters AS CashRegisterRoll_Quarters,
CRR.HalfDollars AS CashRegisterRoll_HalfDollars,
CRR.CreatedDate AS CashRegisterRoll_DateEntered,

CRO.CashRegOtherId AS CashRegisterOther_CashRegisterOtherId,
CRO.CreditCard1 AS CashRegisterOther_CreditCard1,
CRO.CreditCard2 AS CashRegisterOther_CreditCard2,
CRO.CreditCard3 AS CashRegisterOther_CreditCard3,
CRO.Checks AS CashRegisterOther_Checks,
CRO.Payouts AS CashRegisterOther_Payouts,
CRO.CreatedDate AS CashRegisterOther_DateEntered

FROM [StriveCarSalon].[tblCashRegister] CR
INNER JOIN [StriveCarSalon].[tblCodeValue] CV on CV.id = CR.CashRegisterType
LEFT JOIN [StriveCarSalon].[tblCashRegisterBills] CRB on CRB.CashRegisterId = CR.CashRegisterId
LEFT JOIN [StriveCarSalon].[tblCashRegisterCoins] CRC on CRC.CashRegisterId = CR.CashRegisterId
LEFT JOIN [StriveCarSalon].[tblCashRegisterRolls] CRR on CRR.CashRegisterId = CR.CashRegisterId
LEFT JOIN [StriveCarSalon].[tblCashRegisterOthers] CRO on CRO.CashRegisterId = CR.CashRegisterId

WHERE CR.LocationId=@LocationId
AND CV.id = @CodeValueID
AND CONVERT(date, CR.CashRegisterDate) = @EnteredDate
AND CR.DrawerId in (SELECT DrawerId FROM [StriveCarSalon].[tblDrawer] WHERE LocationId=@LocationId) ORDER BY CR.CashRegisterDate DESC
END

