CREATE PROCEDURE [StriveCarSalon].[uspGetUpchargeByType] 
@ModelId int,@ServiceType int, @LocationId int = NULL
as 
--[StriveCarSalon].[uspGetUpchargeByType]  21,11779
begin
declare @category varchar =(select top 1 type.category 
                            From tblVehicleModel model 
	                        inner join tblVehicleType type on model.TypeId =type.TypeId
	                        where model.ModelId = @ModelId)

select top 1 ServiceId,ServiceName,Upcharges,Price, ServiceType as ServiceTypeId
from tblservice tbls
left join  GetTable('ServiceCategory') tblsc on tbls.ServiceCategory =tblsc.valueid
where  ServiceType= @ServiceType and tblsc.valuedesc =@category -- Upcharges like '%'+@category+'%'
--and (tbls.LocationId = @LocationId or @LocationId is NULL)
and tbls.IsDeleted = 0
end