---------------------History---------------------------
-- ====================================================
-- 16-jun-2021, shalini - modified the query based.
--------------------------------------------------------
--[StriveCarSalon].[uspGetServiceById]578
--------------------------------------------------------

CREATE PROCEDURE [StriveCarSalon].[uspGetServiceById]
    (
     @ServiceId int)
AS 
BEGIN
select 
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
	svc.ServiceCategory,
	svc.IsCeramic,
	svc.LocationId,
	--added price
		svc.Price,
	svc.Description,	
	svc.DiscountServiceType,
	svc.DiscountType,
	svc.EstimatedTime,
	 isnull(svc.IsActive,1) as IsActive
	
	FROM tblService svc 
	--INNER JOIN [tblLocation] as loc ON (svc.LocationId = loc.LocationId)
	LEFT JOIN GetTable('ServiceType') cv ON (svc.ServiceType = cv.valueid)
	LEFT JOIN GetTable('CommisionType') ct ON (svc.CommisionType = ct.valueid)	
	LEFT JOIN GetTable('ServiceType') c ON (svc.DiscountServiceType = c.valueid)
	 WHERE  svc.ServiceId = @ServiceId 
END
