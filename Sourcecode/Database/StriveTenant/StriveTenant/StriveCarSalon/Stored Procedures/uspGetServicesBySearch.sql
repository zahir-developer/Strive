CREATE PROCEDURE [StriveCarSalon].[uspGetServicesBySearch]
(@ServiceType varchar(50)=null,@ServiceName varchar(50)=null,@Status int = null)
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
	isnull(svc.IsActive,1) as IsActive
	FROM tblService svc 
	LEFT JOIN GetTable('ServiceType') cv
	ON svc.ServiceType = cv.valueid
WHERE isnull(svc.IsDeleted,0)=0
AND
 (@ServiceType is null or cv.valuedesc = @ServiceType) AND
 (@ServiceName is null or svc.ServiceName = @ServiceName) AND
 (@Status is null or svc.IsActive = @Status)
end


