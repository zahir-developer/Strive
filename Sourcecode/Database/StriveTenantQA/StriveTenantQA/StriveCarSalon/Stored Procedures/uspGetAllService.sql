

CREATE proc [StriveCarSalon].[uspGetAllService]
as
begin
SELECT 
svc.ServiceId,
svc.ServiceType as ServiceTypeId,
svc.CommisionType as CommissionTypeId,
svc.ParentServiceId,
cv.valuedesc as ServiceType,
svc.Commision,
svc.Upcharges,
svc.ServiceName,
svc.Cost,
svc.IsActive
FROM [StriveCarSalon].tblService svc 
LEFT JOIN [striveCarSalon].GetTable('ServiceType') cv
ON svc.ServiceType = cv.valueid
WHERE isnull(svc.IsDeleted,0)=0
end


