CREATE procedure [StriveCarSalon].[uspGetModuleById]
(@TenantId int = NULL)

as
begin
	select 
	m.ModuleId,
	m.ModuleName,
	m.IsActive,
	m.Description
	from tblmodule m 
	where m.IsDeleted = 0

	select 
	ms.ModuleScreenId,
	ms.ViewName,
	ms.Description,
	ms.ModuleId,
	ms.IsActive
	from tblModuleScreen ms
	where ms.IsDeleted = 0

end