
CREATE PROCEDURE [CON].[uspGetServices]
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
	svc.Commision,
	svc.CommissionCost,
	svc.Upcharges,
	svc.ServiceName,
	svc.Cost,

	isnull(svc.IsActive,1) as IsActive
	FROM [CON].tblService svc 
	LEFT JOIN [CON].GetTable('ServiceType') cv ON (svc.ServiceType = cv.valueid)
	LEFT JOIN [CON].GetTable('CommisionType') ct ON (svc.CommisionType = ct.valueid)
WHERE isnull(svc.IsDeleted,0)=0
AND
 (@ServiceId is null or svc.ServiceId = @ServiceId) AND
 (@ServiceSearch is null or cv.valuedesc like '%'+@ServiceSearch+'%'
  or svc.ServiceName  like '%'+@ServiceSearch+'%') AND
  (@Status is null or svc.IsActive = @Status)
  
order by ServiceId Desc
end