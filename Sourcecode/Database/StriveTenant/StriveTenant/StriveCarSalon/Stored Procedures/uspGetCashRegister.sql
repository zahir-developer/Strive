


CREATE PROC [StriveCarSalon].[uspGetCashRegister] --1,'CLOSEOUT','2020-08-26'
(
@LocationId int,
@CashRegisterType varchar(10), 
@CashRegisterDate varchar(10)
)

AS
BEGIN

DECLARE @CashRegisterTypeId INT = (
Select CV.id from StriveCarSalon.tblCodeCategory CC
JOIN StriveCarSalon.tblCodeValue CV on CV.CategoryId = CC.id
WHERE CV.CodeValue = @CashRegisterType)

DECLARE @DrawerID INT;
SELECT @DrawerID = DrawerId FROM [StriveCarSalon].[tblDrawer] WHERE LocationId=@LocationId

Select
CR.CashRegisterId ,
CR.CashRegisterType,
CR.LocationId ,
CR.DrawerId,
CR.CashRegisterDate 
FROM 
strivecarsalon.tblCashRegister CR 
WHERE
CR.LocationId = @LocationId AND
CR.CashRegisterType = @CashRegisterTypeId AND
CR.CashRegisterDate = @CashRegisterDate AND
--CR.DrawerId = @DrawerID AND
isnull(CR.isDeleted,0) = 0  

SELECT 
CRC.CashRegCoinId,
CRC.CashRegisterId,
CRC.Pennies ,
CRC.Nickels ,
CRC.Dimes ,
CRC.Quarters ,
CRC.HalfDollars
FROM
[StriveCarSalon].[tblCashRegisterCoins] CRC
INNER JOIN
strivecarsalon.tblCashRegister CR 
ON CRC.cashRegisterId = CR.CashRegisterId
WHERE
CR.LocationId = @LocationId AND
CR.CashRegisterType = @CashRegisterTypeId AND
CR.CashRegisterDate = @CashRegisterDate AND
--CR.DrawerId = @DrawerID AND
isnull(CR.isDeleted,0) = 0  


SELECT
CRR.CashRegRollId,
CRR.CashRegisterId,
CRR.Pennies,
CRR.Nickels,
CRR.Dimes,
CRR.Quarters,
CRR.HalfDollars
FROM
[StriveCarSalon].[tblCashRegisterRolls] CRR
INNER JOIN
strivecarsalon.tblCashRegister CR 
ON CRR.cashRegisterId = CR.CashRegisterId
WHERE
CR.LocationId = @LocationId AND
CR.CashRegisterType = @CashRegisterTypeId AND
CR.CashRegisterDate = @CashRegisterDate AND
--CR.DrawerId = @DrawerID AND
isnull(CR.isDeleted,0) = 0  

SELECT
CRB.CashRegBillId,
CRB.CashRegisterId,
CRB.[s1],
CRB.[s5],
CRB.[s10],
CRB.[s20],
CRB.[s50],
CRB.[s100]
FROM
[StriveCarSalon].[tblCashRegisterBills] CRB
INNER JOIN
strivecarsalon.tblCashRegister CR 
ON CRB.cashRegisterId = CR.CashRegisterId
WHERE
CR.LocationId = @LocationId AND
CR.CashRegisterType = @CashRegisterTypeId AND
CR.CashRegisterDate = @CashRegisterDate AND
--CR.DrawerId = @DrawerID AND
isnull(CR.isDeleted,0) = 0  


SELECT
CRO.CashRegOtherId,
CRO.CashRegisterId,
CRO.CreditCard1,
CRO.CreditCard2,
CRO.CreditCard3,
CRO.Checks,
CRO.Payouts
FROM
[StriveCarSalon].[tblCashRegisterOthers] CRO
INNER JOIN
strivecarsalon.tblCashRegister CR 
ON CRO.cashRegisterId = CR.CashRegisterId
WHERE
CR.LocationId = @LocationId AND
CR.CashRegisterType = @CashRegisterTypeId AND
CR.CashRegisterDate = @CashRegisterDate AND
--CR.DrawerId = @DrawerID AND
isnull(CR.isDeleted,0) = 0  
END

