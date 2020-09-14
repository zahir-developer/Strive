





-- =============================================
-- Author:		Vineeth B
-- Create date: 20-08-2020
-- Description:	To get Service against MemberId
-- =============================================
CREATE PROCEDURE [CON].[uspGetMembershipListSetupByMembershipId] 
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
[CON].[tblMembershipService] tblms
inner join [CON].[tblService] tbls ON(tblms.ServiceId= tbls.ServiceId)
inner join [CON].GetTable('ServiceType') gt ON(tbls.ServiceType = gt.valueid)
WHERE tblms.MembershipId=@MembershipId AND
ISNULL(tblms.IsDeleted,0)=0 AND 
ISNULL(tbls.IsDeleted,0)=0
END
