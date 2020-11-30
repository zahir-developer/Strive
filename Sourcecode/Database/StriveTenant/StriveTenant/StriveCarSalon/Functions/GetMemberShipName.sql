

CREATE FUNCTION [StriveCarSalon].[GetMemberShipName]
 (@Id as int)
returns  varchar(50)
as
begin

DECLARE @MembershipName VARCHAR(50) = '';
	IF (@Id <> 0) 
	begin
		  
		  select TOP(1) @MembershipName = M.MembershipName from tblMembership M (NoLock)
		  where M.MembershipId = @Id
	end
	return(ISNULL(@MembershipName,''));
end