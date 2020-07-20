CREATE proc [dbo].[uspLogin]
(@LoginId as varchar(50),
@Password as varchar(50))
as
begin
	select 
	am.AuthId, userguid as UserGuid, 
	sm.dbschemaname as Schemaname,
	sm.dbusername as Username,
	sm.dbpassword as [Password],
	tm.TenantGuid as TenantGuid,
	la.ActionTypeId
	from [tblAuthMaster] am inner join 
	[tblSchemaAccess] sa on am.authid = sa.AuthId
	inner join
	[tblSchemaMaster] sm on sa.SchemaId = sm.SchemaId 
	inner join
	[tblSubscriptionMaster] asm on asm.SubscriptionId = sm.SubscriptionId
	INNER JOIN
	[tblTenantMaster] tm on tm.tenantId =	sm.tenantid
	INNER JOIN
	[tblLastAuth] la on am.authid=la.authid
	where 
	sa.IsDeleted=0 and sm.IsDeleted = 0 and am.LockoutEnabled=0 and asm.IsDeleted = 0 and
	am.EmailId =@LoginId 
	--and am.PasswordHash = @Password
end
--USPLogin 'caradmin@strive.com','pass@123'