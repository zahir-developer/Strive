--[StriveCarSalon].[uspGetUpchargeByType]  21,11779
CREATE proc [StriveCarSalon].[uspGetUpchargeByType] 
@ModelId int,@ServiceType int
as 
begin
declare @category varchar =(select type.category 
                            From tblVehicleModel model 
	                        inner join tblVehicleType type on model.TypeId =type.TypeId
	                        where model.ModelId = @ModelId)

select ServiceId,ServiceName,Upcharges,Cost 
from tblservice tbls
left join  GetTable('ServiceCategory') tblsc on tbls.ServiceCategory =tblsc.valueid
where  ServiceType= @ServiceType and tblsc.valuedesc =@category -- Upcharges like '%'+@category+'%'
	
end