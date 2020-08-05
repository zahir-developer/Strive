
CREATE proc [StriveCarSalon].[uspGetCollisionById] 
(@CollisionId int)
as
begin
select tblempl.LiabilityId,
tblempl.EmployeeId,
tblempl.LiabilityType,
tblempl.LiabilityDescription,
tblempl.ProductId,
tblempl.Status,
tblempL.CreatedDate,

tblempld.LiabilityDetailId      AS LiabilityDetail_LiabilityDetailId,
tblempld.LiabilityId            AS LiabilityDetail_LiabilityId,
tblempld.LiabilityDetailType          AS LiabilityDetail_LiabilityDetailType,
tblempld.Amount                AS LiabilityDetail_Amount,
tblempld.PaymentType            AS LiabilityDetail_PaymentType,
tblempld.DocumentPath               AS LiabilityDetail_DocumentPath,
tblempld.Description              AS LiabilityDetail_Description,
tblempld.CreatedDate               AS LiabilityDetail_CreatedDate,
tblempld.IsActive               AS LiabilityDetail_IsActive

from [StriveCarSalon].tblEmployeeLiability tblempl inner join 
[StriveCarSalon].tblEmployeeLiabilityDetail tblempld on(tblempl.LiabilityId = tblempld.LiabilityId)

where tblempl.LiabilityId = @CollisionId
end