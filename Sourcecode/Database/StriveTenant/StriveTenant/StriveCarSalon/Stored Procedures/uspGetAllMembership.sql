-- =============================================
-- Author:		Zahir Hussain
-- Create date: 24-08-2020
-- Description:	Retreives All Membership details and services
-- =============================================

---------------------History--------------------
-- =============================================
-- <Modified Date>, <Author> - <Description>
-- 15-05-2021, Shalini - Added sub query to filter based on the service.
-- 08-june-2021,Shalini -added discountedPrice column

------------------------------------------------
-- =============================================

CREATE PROCEDURE [StriveCarSalon].[uspGetAllMembership]
(@MembershipSearch varchar(50) = null)
AS 
BEGIN
select *
from (
SELECT 
   M.MembershipId,
	M.DiscountedPrice,
   M.MembershipName, 
   REPLACE(REPLACE(
   STUFF(
   (SELECT ', ' + S.ServiceName 
    FROM tblMembershipService MS
	LEFT JOIN tblService S on MS.ServiceId = S.ServiceId and MS.MembershipId = M.MembershipId
	WHERE S.ServiceId = MS.ServiceId and ISNULL(MS.IsDeleted,0)=0
	Group by S.ServiceName
    FOR XML PATH('')
	), 1, 1, '') 
	, ' ' ,''), ',', ', ') AS Services,
	M.IsActive,
	M.CreatedDate
FROM tblMembership M Where
--(@MembershipSearch is null or m.MembershipName like'%'+ @MembershipSearch+'%' )
--and
ISNULL(M.IsDeleted,0)=0
GROUP BY M.MembershipName, M.MembershipId, M.IsActive, M.CreatedDate,	M.DiscountedPrice

) a
where (@MembershipSearch is null or a.MembershipName like'%'+ @MembershipSearch+'%'  or a.Services like'%'+ @MembershipSearch+'%')
ORDER BY  a.IsActive desc


END
