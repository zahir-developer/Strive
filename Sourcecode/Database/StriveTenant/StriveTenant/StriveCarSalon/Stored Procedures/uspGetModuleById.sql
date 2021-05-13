CREATE procedure [StriveCarSalon].[uspGetModuleById]
(@TenantId int = NULL)

as
begin
	select 
	ModuleId,
	ModuleName,
	IsActive
	from  tblmodule
end