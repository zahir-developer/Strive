--[StriveCarSalon].[uspGetModelByMake]  93
CREATE proc [StriveCarSalon].[uspGetModelByMake] 
@makeId int
as 
--declare @categoryId int = (select top  1 id from tblCodeCategory where Category ='VehicleModel')
begin
	select 
	 ModelId,
	 make.MakeValue,
	 ModelValue	
	from tblVehicleMake make
    inner join tblVehicleModel model on make.MakeId =model.MakeId
	where model.MakeId =@makeId 	
	and ISNULL(model.IsDeleted,0) =0
	ORDER BY ModelValue
	
end