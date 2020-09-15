

CREATE PROCEDURE [CON].[uspGetAllService]
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
FROM [CON].tblService svc 
LEFT JOIN [CON].GetTable('ServiceType') cv
ON svc.ServiceType = cv.valueid
WHERE isnull(svc.IsDeleted,0)=0
end


