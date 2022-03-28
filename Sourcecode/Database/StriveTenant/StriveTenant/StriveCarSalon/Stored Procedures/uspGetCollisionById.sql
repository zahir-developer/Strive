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


CREATE PROCEDURE [StriveCarSalon].[uspGetCollisionById] 
(@CollisionId int = null)
AS
BEGIN

--DECLARE @CollisionId int = 49

Select
LiabilityId,
EmployeeId,
el.ClientId,
cl.FirstName as ClientFirstName,
cl.LastName as ClientLastName,
VehicleId,
LiabilityType,
LiabilityDescription,
ProductId,
Status,
el.CreatedDate
FROM 
tblEmployeeLiability el
LEFT JOIN tblClient cl on cl.ClientId = el.ClientId

WHERE
el.isDeleted = 0  AND
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
[tblEmployeeLiabilityDetail] tbleld
INNER JOIN
tblEmployeeLiability tblel 
ON tbleld.LiabilityId = tblel.LiabilityId
WHERE
tblel.isDeleted = 0 and
(@CollisionId is null or tbleld.LiabilityId = @CollisionId)

END
