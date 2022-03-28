CREATE PROCEDURE [StriveCarSalon].[uspGetCollision]

AS
BEGIN
Select
LiabilityId,
EmployeeId,
LiabilityType,
LiabilityDescription,
ProductId,
Status
FROM 
tblEmployeeLiability  
WHERE
isnull(isDeleted,0) = 0 

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
isnull(tblel.isDeleted,0) = 0

END
