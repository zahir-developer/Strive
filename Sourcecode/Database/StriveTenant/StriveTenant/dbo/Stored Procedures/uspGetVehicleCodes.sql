CREATE proc [dbo].[uspGetVehicleCodes] 
as 
begin
	select cat.id as CategoryId, cat.Category, val.id as codeid, val.codevalue as codevalue, val.codeshortvalue as codeshortvalue, val.sortorder 
	from StriveCarSalon.tblCodeCategory cat inner join StriveCarSalon.tblCodeValue val on
	cat.id=val.categoryid
	where 
	Category in('VehicleColor','VehicleManufacturer','VehicleModel','Upcharge','Make','UpchargeType','score')
end
