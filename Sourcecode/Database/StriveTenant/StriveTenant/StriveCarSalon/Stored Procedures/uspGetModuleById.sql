CREATE procedure [StriveCarSalon].[uspGetModuleById]
(@TenantId int)

as
begin
	select 
	ModuleId,
	ModuleName,
	IsActive
	from  tblmodule
end