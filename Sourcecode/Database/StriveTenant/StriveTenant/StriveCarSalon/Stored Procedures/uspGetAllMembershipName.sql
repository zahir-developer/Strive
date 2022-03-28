-- =============================================
-- Author:		Shalini
-- Create date: 30-03-2021
-- Description:	Retreives All Membershipname
-- =============================================

------------------------------------------------

CREATE PROCEDURE [StriveCarSalon].[uspGetAllMembershipName]
(@locationId int = null)
AS 
BEGIN

SELECT 
   M.MembershipId,
   M.MembershipName
   
FROM tblMembership M 
Where --(@locationId is null or @locationId = 0) or M.LocationId = @locationId and 
(ISNULL(M.IsDeleted,0) = 0 or m.MembershipName like 'None%')
GROUP BY M.MembershipName, M.MembershipId

END