-- =================== HISTORY ==========================
-- 08-03-2022  | JUKI B  | Changes done to login using employee login id 
-- =============================================

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
	tm.TenantId as TenantId,
	la.ActionTypeId
	from [tblAuthMaster] am inner join 
	[tblSchemaAccess] sa on am.authid = sa.AuthId
	LEFT join
	[tblSchemaMaster] sm on sa.SchemaId = sm.SchemaId 
	LEFT join
	[tblSubscriptionMaster] asm on asm.SubscriptionId = sm.SubscriptionId
	LEFT JOIN
	[tblTenantMaster] tm on tm.tenantId =	sm.tenantid
	LEFT JOIN
	[tblLastAuth] la on am.authid=la.authid
	where 
	sa.IsDeleted=0 and sm.IsDeleted = 0 and am.LockoutEnabled=0 and isnull(asm.IsDeleted,0) = 0 and
	(am.EmailId =@LoginId OR am.Loginid=@LoginId ) and isnull(am.IsDeleted,0) = 0
	--and am.PasswordHash = @Password
end
--USPLogin 'caradmin@strive.com','pass@123'