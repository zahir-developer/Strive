


CREATE PROCEDURE [StriveCarSalon].[uspGetCollisionByEmpId] 
(@EmployeeId int = null, @CollisionId int = null)
AS
BEGIN
Select
	 empL.LiabilityId 
	,empL.LiabilityType AS TypeId
	,cvL.valuedesc AS Liabilitytype
	,empL.LiabilityDescription AS [Description]
	,empLD.Amount
	,empL.CreatedDate
FROM 
tblEmployeeLiability empL
LEFT JOIN  tblEmployeeLiabilityDetail empLd ON empL.LiabilityId = empLd.LiabilityId
INNER JOIN GetTable('LiabilityType') cvL on empL.LiabilityType = cvL.valueid
WHERE
 (@EmployeeId is null or empl.EmployeeId = @EmployeeId) AND
 (@CollisionId is null or empl.LiabilityId = @CollisionId)
AND
cvL.valuedesc='Collision' AND
isnull(empL.isDeleted,0) = 0  AND
isnull(empLd.isDeleted,0) = 0

END
