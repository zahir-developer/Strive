




-- =============================================
-- Author:		Vineeth B
-- Create date: 20-08-2020
-- Description:	To get Service against MemberId
-- =============================================
CREATE PROCEDURE [StriveCarSalon].[uspGetMembershipListSetupByMembershipId] 
(@MembershipId int)
AS 
BEGIN
SELECT 
tblms.MembershipServiceId,
tblms.MembershipId,
tbls.Upcharges,
tblms.ServiceId,
tblms.IsActive,
tblms.IsDeleted,
gt.valuedesc as ServiceType
FROM 
[StriveCarSalon].[tblMembershipService] tblms
inner join [StriveCarSalon].[tblService] tbls ON(tblms.ServiceId= tbls.ServiceId)
inner join [StriveCarSalon].GetTable('ServiceType') gt ON(tbls.ServiceType = gt.valueid)
WHERE tblms.MembershipId=@MembershipId AND
ISNULL(tblms.IsDeleted,0)=0 AND 
ISNULL(tbls.IsDeleted,0)=0
END
