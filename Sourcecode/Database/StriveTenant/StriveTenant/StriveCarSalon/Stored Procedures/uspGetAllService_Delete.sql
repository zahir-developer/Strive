
CREATE proc [StriveCarSalon].[uspGetAllService_Delete]
as
begin
SELECT 
svc.ServiceId,
svc.ServiceType as ServiceTypeId,
svc.CommisionType as CommissionTypeId,
ct.valuedesc as CommissionType,
svc.ParentServiceId,
cv.valuedesc as ServiceType,
svc.Commision,
svc.Upcharges,
svc.ServiceName,
svc.Cost,
svc.IsActive
FROM [StriveCarSalon].tblService svc 
LEFT JOIN [striveCarSalon].GetTable('ServiceType') cv ON svc.ServiceType = cv.valueid 
LEFT JOIN [striveCarSalon].GetTable('CommisionType') ct on ct.valueid = svc.CommisionType
WHERE isnull(svc.IsDeleted,0)=0
end

Select * from [striveCarSalon].GetTable('CommisionType')