CREATE proc [StriveCarSalon].[uspGetServices] 
(@ServiceId int=null,@ServiceSearch varchar(50)=null,@Status bit = null)
as
begin
SELECT 
	svc.ServiceId,
	svc.ServiceType as ServiceTypeId,
	svc.CommisionType as CommissionTypeId,
	svc.ParentServiceId,
	cv.valuedesc as ServiceType,
	ct.valuedesc as CommisionType,
	svc.CommissionCost as CommissionCost,
	svc.Commision,
	svc.Upcharges,
	svc.ServiceName,
	svc.Cost,

	isnull(svc.IsActive,1) as IsActive
	FROM [StriveCarSalon].tblService svc 
	LEFT JOIN [striveCarSalon].GetTable('ServiceType') cv ON (svc.ServiceType = cv.valueid)
	LEFT JOIN [striveCarSalon].GetTable('CommisionType') ct ON (svc.CommisionType = ct.valueid)
WHERE isnull(svc.IsDeleted,0)=0
AND
 (@ServiceId is null or svc.ServiceId = @ServiceId) AND
 (@ServiceSearch is null or cv.valuedesc like '%'+@ServiceSearch+'%'
  or svc.ServiceName  like '%'+@ServiceSearch+'%') AND
  (@Status is null or svc.IsActive = @Status) 
  Order by ServiceId desc
end


