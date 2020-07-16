CREATE proc [dbo].[uspGetSchemaByGuid]
(@UserGuid as uniqueidentifier)
as
begin
	select userguid as UserGuid, sm.dbschemaname as Schemaname,sm.dbusername as Username,sm.dbpassword as [Password]
	from [tblAuthMaster] am inner join 
	[tblSchemaAccess] sa on am.authid = sa.AuthId
	inner join
	[tblSchemaMaster] sm on sa.SchemaId = sm.SchemaId 
	inner join
	[tblSubscriptionMaster] asm on asm.SubscriptionId = sm.SubscriptionId
	where 
	sa.IsDeleted=0 and sm.IsDeleted = 0 and am.LockoutEnabled=0 and asm.IsDeleted = 0 and
	am.UserGuid = @UserGuid
end
--USPLogin 'F746219A-5DD7-4DAC-BF0E-826110E29836'