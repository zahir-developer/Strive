
CREATE FUNCTION [StriveCarSalon].[GetMemberShip] (@Id as int)
returns @result table (MembershipId int, MembershipName varchar(50))
as
begin

	IF (@Id <> 0) 
	begin
		  Insert into @result
		  select M.MembershipId, M.MembershipName as MembershipName from tblMembership M
		  where M.MembershipId = @Id
	end
	return 
end