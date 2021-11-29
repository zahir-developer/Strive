-- [StriveCarSalon].[uspGetDailySalesReport] '2021-05-20',1
CREATE PROCEDURE [StriveCarSalon].[uspGetDailySalesReport] 
@Date date , @LocationId int
AS
BEGIN

DROP TABLE  IF EXISTS #MerchandizeCount

(Select tblj.JobId,SUM(tbljpi.Quantity)MerchandizeQuantity,SUM(tbljpi.Quantity * tbljpi.Price) MerchandizePrice into #MerchandizeCount from tblJob tblj 
inner join tblJobProductItem tbljpi 
on(tblj.JobId = tbljpi.JobId) inner join tblProduct tbp on(tbljpi.ProductId = tbp.ProductId)
INNER JOIN GetTable('ProductType') pt on pt.valueid = tbp.ProductType and pt.valuedesc ='Merchandize' 
INNER JOIN tblJobPayment tbljp on(tblj.JobId = tbljp.JobId)
INNER JOIN GetTable('JobStatus') js ON(tblj.JobStatus = js.valueid) --AND js.valuedesc='Completed'
where  tblj.IsActive=1 and tbljpi.IsActive=1 and tbljp.IsActive=1 and
ISNULL(tblj.IsDeleted,0)=0 and ISNULL(tbljpi.IsDeleted,0)=0 and ISNULL(tbljp.IsDeleted,0)=0 --and (@LocationId IS NULL or tblj.LocationId=@LocationId)
AND tblj.JobDate=@Date and tblj.LocationId = @LocationId
--AND tblj.JobDate='2020-09-29' 
GROUP BY tblj.JobId)

DROP TABLE  IF EXISTS #CostOfAllService
(Select tblj.JobId,SUM(tblji.Price) CostOfAllService into #CostOfAllService from tblJob tblj 
INNER JOIN tblJobItem tblji 
on(tblj.JobId = tblji.JobId) inner join tblService tbls on(tblji.ServiceId = tbls.ServiceId)
INNER JOIN GetTable('JobType') jt on jt.valueid = tblj.JobType and jt.valuedesc in('Wash','Detail')
INNER JOIN GetTable('ServiceType') st on st.valueid = tbls.ServiceType 
INNER JOIN GetTable('JobStatus') js ON(tblj.JobStatus = js.valueid) --AND js.valuedesc='Completed'
where  tblj.IsActive=1 and tblji.IsActive=1 and tbls.IsActive=1 and
ISNULL(tblj.IsDeleted,0)=0 and ISNULL(tblji.IsDeleted,0)=0 and ISNULL(tbls.IsDeleted,0)=0 --and (@LocationId IS NULL or tblj.LocationId=@LocationId)
AND tblj.JobDate=@Date and tblj.LocationId = @LocationId
GROUP BY tblj.JobId)


SELECT 
    DISTINCT 
	tblj.JobId,
    tblj.TicketNumber,
	tblj.TimeIn AS TimeIn,
	tblj.ActualTimeOut AS TimeOut,
	tblj.EstimatedTimeOut AS Est,
	--SUBSTRING(CONVERT(VARCHAR(8),tblj.TimeIn,108),0,6) AS TimeIn,
	--SUBSTRING(CONVERT(VARCHAR(8),tblj.EstimatedTimeOut,108),0,6) AS Est,
	--SUBSTRING(CONVERT(VARCHAR(8),tblj.ActualTimeOut,108),0,6) AS TimeOut,
	DATEDIFF(minute,tblj.ActualTimeOut,tblj.EstimatedTimeOut) as Deviation,
	--DATEDIFF(minute,SUBSTRING(CONVERT(VARCHAR(8),tblj.ActualTimeOut,108),0,6),SUBSTRING(CONVERT(VARCHAR(8),tblj.EstimatedTimeOut,108),0,6))/60 as Deviation,
	--((SUBSTRING(CONVERT(VARCHAR(8),tblj.EstimatedTimeOut,108),0,6))+(SUBSTRING(CONVERT(VARCHAR(8),tblj.TimeIn,108),0,6)))Deviation,
	tbls.ServiceName,
	st.valuedesc AS ServiceType,
	ISNULL(MC.MerchandizeQuantity,0) MerchandiseItemsPurchased,
	tblcv.Barcode,
	vc.valuedesc AS Color,
	--vmo.valuedesc AS Model,
	vmo.ModelValue 	AS Model,
	make.MakeValue AS Make,
	CONCAT(tblc.FirstName,' ',tblc.LastName) AS CustomerName,
	tblca.PhoneNumber,
	(co.CostOfAllService + ISNULL(MC.MerchandizePrice,'0.00')) AS Amount,
	CASE WHEN tblcvmd.ClientMembershipId IS NULL THEN 'Drive Up'
	ELSE 'Membership' END AS Type
FROM 
	tblJob tblj WITH(NOLOCK)
INNER JOIN
    #CostOfAllService co ON(tblj.JobId = co.JobId)
INNER JOIN
	GetTable('JobType') jt ON(tblj.JobType = jt.valueid)
INNER JOIN
	GetTable('JobStatus') js ON(tblj.JobStatus = js.valueid) AND js.valuedesc='Completed'
INNER JOIN
	tblClient tblc  WITH(NOLOCK) ON(tblj.ClientId = tblc.ClientId)
INNER JOIN
	tblClientAddress tblca  WITH(NOLOCK) ON(tblc.ClientId = tblca.ClientId)
INNER JOIN
	tblClientVehicle tblcv  WITH(NOLOCK) ON(tblj.VehicleId = tblcv.VehicleId)
INNER JOIN
	tblVehicleModel
	--GetTable('VehicleModel')
	vmo ON(tblcv.VehicleModel = vmo.ModelId)
	JOIN tblVehicleMake make on make.MakeId =vmo.MakeId
INNER JOIN
	GetTable('VehicleColor') vc ON(tblcv.VehicleColor = vc.valueid)
INNER JOIN
	tblJobItem tblji  WITH(NOLOCK) ON(tblji.JobId = tblj.JobId)
INNER JOIN
	tblService tbls  WITH(NOLOCK) ON(tblji.ServiceId = tbls.ServiceId)
INNER JOIN 
	GetTable('ServiceType') st ON(tbls.ServiceType = st.valueid)
LEFT JOIN 
    #MerchandizeCount MC ON(tblj.JobId = MC.JobId)
INNER JOIN
    tblJobPayment tbljp ON(tblj.JobId = tbljp.JobId)
--LEFT JOIN
--    tblJobProductItem tbljpi WITH(NOLOCK) ON(tblj.JobId = tbljpi.JobId)AND tbljpi.IsActive = 1 AND ISNULL(tbljpi.IsDeleted,0)=0
--LEFT JOIN 
--    tblProduct tblp on(tbljpi.ProductId = tblp.ProductId) AND tblp.IsActive = 1 AND ISNULL(tblp.IsDeleted,0) = 0
--LEFT JOIN 
--    GetTable('ProductType') pt on pt.valueid = tblp.ProductType and pt.valuedesc ='Merchandize'
LEFT JOIN
    tblClientVehicleMembershipDetails tblcvmd ON(tblj.VehicleId = tblcvmd.ClientVehicleId) AND tblcvmd.IsActive = 1 AND ISNULL(tblcvmd.IsDeleted,0)=0
WHERE 
tblj.TicketNumber != '' and	jt.valuedesc IN('Wash','Detail')  and st.valuedesc in('Wash Package','Detail Package')
AND tblj.JobDate=@Date and tblj.LocationId = @LocationId
AND tblj.IsActive = 1 AND tblc.IsActive = 1 AND tblcv.IsActive = 1 
AND tblji.IsActive = 1 AND tbls.IsActive = 1 
AND tblca.IsActive = 1
AND ISNULL(tblj.IsDeleted,0) = 0 AND ISNULL(tblc.IsDeleted,0) = 0 AND ISNULL(tblcv.IsDeleted,0) = 0 
AND ISNULL(tblji.IsDeleted,0) = 0 AND ISNULL(tbls.IsDeleted,0) = 0 
AND ISNULL(tblca.IsDeleted,0) = 0
ORDER BY tblj.JobId desc
END