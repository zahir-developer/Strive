CREATE PROCEDURE [StriveCarSalon].[uspGetModelByMake] 
@makeId int
as 
--declare @categoryId int = (select top  1 id from tblCodeCategory where Category ='VehicleModel')
--[StriveCarSalon].[uspGetModelByMake]  11
begin
	select 
	 ModelId,
	 make.MakeValue,
	 CONCAT(ModelValue, ' / ', t.TypeValue) ModelValue
	from tblVehicleMake make
    inner join tblVehicleModel model on make.MakeId =model.MakeId
	JOIN tblVehicleType t on t.typeId = model.typeId
	where model.MakeId =@makeId 	
	and ISNULL(model.IsDeleted,0) =0
	ORDER BY ModelValue
	
end