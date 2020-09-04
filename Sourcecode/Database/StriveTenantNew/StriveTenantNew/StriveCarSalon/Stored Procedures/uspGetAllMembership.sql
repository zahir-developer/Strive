CREATE PROCEDURE [StriveCarSalon].[uspGetAllMembership]
(@MembershipSearch varchar(50) = null)
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
FROM StriveCarSalon.tblMembership M Where
(@MembershipSearch is null or m.MembershipName like'%'+ @MembershipSearch+'%')
--WHERE ISNULL(M.IsDeleted,0)=0
GROUP BY M.MembershipName, M.MembershipId, M.IsActive, M.CreatedDate
ORDER BY M.MembershipId desc


END