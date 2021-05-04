
-- =============================================
-- Author:		Shalini
-- Create date: 30-03-2021
-- Description:	Retreives All Membershipname
-- =============================================



Create PROCEDURE [StriveCarSalon].[uspGetAllMembershipName]
(@MembershipSearch varchar(50) = null)
AS 
BEGIN

SELECT 
   M.MembershipId,
   M.MembershipName
   
FROM StriveCarSalon.tblMembership M Where
(@MembershipSearch is null or m.MembershipName like'%'+ @MembershipSearch+'%' )
and (ISNULL(M.IsDeleted,0)=0 or m.MembershipName like'%None%')
GROUP BY M.MembershipName, M.MembershipId

END