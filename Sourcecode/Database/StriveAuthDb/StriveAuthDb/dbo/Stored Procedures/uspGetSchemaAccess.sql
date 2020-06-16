CREATE proc [dbo].[uspGetSchemaAccess]
(@UserId as uniqueidentifier)
as
begin
	select sm.dbschemaname,sm.dbusername,sm.dbpassword
	from [tblAuthMaster] am inner join 
	[tblSchemaAccess] sa on am.authid = sa.AuthId
	inner join
	[tblSchemaMaster] sm on sa.SchemaId = sm.SchemaId 
	inner join
	[tblSubscriptionMaster] asm on asm.SubscriptionId = sm.SubscriptionId
	where 
	sa.IsDeleted=0 and sm.IsDeleted = 0 and am.LockoutEnabled=0 and asm.IsDeleted = 0 and
	userguid=@UserId
end
--USP_GetSchemaAccess '5CE73F6D-2018-4F46-B4D2-55C5930F46BC'