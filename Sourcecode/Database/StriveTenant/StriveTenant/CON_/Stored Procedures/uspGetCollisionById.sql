

-- =============================================
-- Author:		Zahir Hussain
-- Create date: 20-08-2020
-- Description:	Retrieves Collision details by Id
-- =============================================

---------------------History--------------------
-- =============================================
-- <Modified Date>, <Author> - <Description>
-- 25-08-2020, Zahir - Added Liability Details

------------------------------------------------
-- =============================================


CREATE PROCEDURE [CON].[uspGetCollisionById] 
(@CollisionId int = null)
AS
BEGIN

--DECLARE @CollisionId int = 49

Select
LiabilityId,
EmployeeId,
ClientId,
VehicleId,
LiabilityType,
LiabilityDescription,
ProductId,
IsDeleted,
Status
FROM 
strivecarsalon.tblEmployeeLiability  
WHERE
Isnull(isDeleted,0) = 0  AND
(@CollisionId is null or LiabilityId = @CollisionId)

SELECT 
tbleld.LiabilityDetailId,
tbleld.LiabilityId,
tbleld.LiabilityDetailType,
tbleld.Amount,
tbleld.PaymentType ,
tbleld.DocumentPath, 
tbleld.Description 
FROM
[CON].[tblEmployeeLiabilityDetail] tbleld
INNER JOIN
strivecarsalon.tblEmployeeLiability tblel 
ON tbleld.LiabilityId = tblel.LiabilityId
WHERE

isnull(tblel.isDeleted,0) = 0 and
(@CollisionId is null or tbleld.LiabilityId = @CollisionId)

END
