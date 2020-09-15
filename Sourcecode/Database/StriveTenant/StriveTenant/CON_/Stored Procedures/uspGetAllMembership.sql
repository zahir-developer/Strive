

-- =============================================
-- Author:		Zahir Hussain
-- Create date: 24-08-2020
-- Description:	Retreives All Membership details and services
-- =============================================

---------------------History--------------------
-- =============================================
-- <Modified Date>, <Author> - <Description>
-- 
-- 

------------------------------------------------
-- =============================================



CREATE PROCEDURE [CON].[uspGetAllMembership]
AS 
BEGIN

SELECT 
   M.MembershipId,
   M.MembershipName, 
   REPLACE(REPLACE(
   STUFF(
   (SELECT ', ' + S.ServiceName 
    FROM StriveCarSalon.tblMembershipService MS
	LEFT JOIN StriveCarSalon.tblService S on MS.ServiceId = S.ServiceId and MS.MembershipId = M.MembershipId
	WHERE S.ServiceId = MS.ServiceId
	Group by S.ServiceName
    FOR XML PATH('')
	), 1, 1, '') 
	, ' ' ,''), ',', ', ') AS Services,
	M.IsActive,
	M.CreatedDate
FROM StriveCarSalon.tblMembership M
WHERE ISNULL(M.IsDeleted,0)=0
GROUP BY M.MembershipName, M.MembershipId, M.IsActive, M.CreatedDate
ORDER BY M.MembershipId


END
