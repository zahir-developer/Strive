-- ===============================================================
--------------------------------History---------------------------
-- ===============================================================

 --07-Jul-2021 - Vetriselvi - Changed JobId to JobPaymentId in where condition for table tblJobPayment
 --25-Aug-2021 - Vetriselvi - All payed washes should return
 --30-Sep-2021 - Zahir - Time different column changed (EstimatedTimeOut,ActualTimeOut)
 --13-Oct-2021 - Zahir - Query optimization, Detail jobs also retreived and JobType Added.
 --27-Oct-2021 - Zahir - Added Estimated Time and PaymentTypes
 --19-Oct-2021 - Juki  - Fixed Make, Model, Color unk for Driveup customer

-- ===============================================================
-- EXEC [StriveCarSalon].[uspGetDailySalesReport] '2021-10-25', 1
-- ===============================================================

CREATE PROCEDURE [StriveCarSalon].[uspGetDailySalesReport] 
@Date date, @LocationId int
AS

BEGIN

DECLARE @WashServiceId INT = (SELECT valueid FROM GetTable('ServiceType') WHERE valuedesc='Wash Package')

DECLARE @DetailServiceId INT = (SELECT valueid FROM GetTable('ServiceType') WHERE valuedesc='Detail Package')

Declare @WashId INT = (Select valueid from GetTable('JobType') where valuedesc='Wash')
DECLARE @CompletedJobStatus INT = (SELECT valueid FROM GetTable('JobStatus') WHERE valuedesc='Completed')


DROP TABLE IF EXISTS #tblJob

Select jobId, TicketNumber, ClientId, VehicleId, TimeIn, EstimatedTimeOut, ActualTimeOut, JobPaymentId, cv.CodeValue as JobType, Make, Model, Color into #tblJob 
from tblJob tblj
INNER JOIN GetTable('JobStatus') js ON(tblj.JobStatus = js.valueid)
INNER JOIN GetTable('JobType') jt on jt.valueid = tblj.JobType and jt.valuedesc in('Wash','Detail')
INNER JOIN tblCodeValue cv on cv.id = tblj.JobType
where tblj.JobDate=@Date and tblj.LocationId = @LocationId and tblj.IsActive=1 and tblj.IsDeleted=0 and tblj.JobStatus=@CompletedJobStatus

DROP TABLE IF EXISTS #tblJobItem

Select JobItemId, j.JobId, tblji.ServiceId, tblji.Price into #tblJobItem from tblJobItem tblji
JOIN #tblJob j on j.JobId = tblji.JobId
where tblji.IsActive=1 and ISNULL(tblji.IsDeleted,0)=0

DROP TABLE  IF EXISTS #MerchandizeCount

Select tblj.JobId, SUM(tbljpi.Quantity) MerchandizeQuantity, SUM(tbljpi.Quantity * tbljpi.Price + tbljp.TaxAmount) MerchandizePrice into #MerchandizeCount 
from #tblJob tblj 
INNER JOIN tblJobProductItem tbljpi on(tblj.JobId = tbljpi.JobId) inner join tblProduct tbp on(tbljpi.ProductId = tbp.ProductId)
INNER JOIN GetTable('ProductType') pt on pt.valueid = tbp.ProductType --and pt.valuedesc ='Merchandize' 
INNER JOIN tblJobPayment tbljp on(tblj.JobPaymentId = tbljp.JobPaymentId)
 --AND js.valuedesc='Completed'
where tbljpi.IsActive=1 and tbljp.IsActive=1 and ISNULL(tbljpi.IsDeleted,0)=0 and ISNULL(tbljp.IsDeleted,0)=0 --and (@LocationId IS NULL or tblj.LocationId=@LocationId)

GROUP BY tblj.JobId 

DROP TABLE  IF EXISTS #CostOfAllService
(Select tblj.JobId,SUM(tblji.Price) CostOfAllService into #CostOfAllService 
from #tblJob tblj 
INNER JOIN #tblJobItem tblji 
on(tblj.JobId = tblji.JobId) 
inner join tblService tbls on(tblji.ServiceId = tbls.ServiceId)
INNER JOIN GetTable('ServiceType') st on st.valueid = tbls.ServiceType 
where tbls.IsActive=1 and ISNULL(tbls.IsDeleted,0)=0 --and (@LocationId IS NULL or tblj.LocationId=@LocationId)
GROUP BY tblj.JobId)
--select * from #CostOfAllService

SELECT 
    DISTINCT 
	tblj.JobId,
    tblj.TicketNumber,
	tblj.TimeIn AS TimeIn,
	tblj.ActualTimeOut AS TimeOut,
	tblj.EstimatedTimeOut AS Est,
	DATEDIFF(minute,tblj.TimeIn,tblj.EstimatedTimeOut) as EstTime,
	DATEDIFF(minute,tblj.EstimatedTimeOut,tblj.ActualTimeOut) as Deviation,
	tbls.ServiceName,
	tblj.JobType as ServiceType,
	--st.valuedesc AS ServiceType,
	ISNULL(MC.MerchandizeQuantity,0) MerchandiseItemsPurchased,
	ISNULL(tblcv.Barcode, '-') as Barcode,
	ISNULL(vc.valuedesc, 'Unk ') AS Color,
	--vmo.valuedesc AS Model,
	ISNULL(vmo.ModelValue, 'Unk') AS Model,
	ISNULL(make.MakeValue, 'Unk') AS Make,
	CONCAT(ISNULL(tblc.FirstName,'DRIVE'),' ',ISNULL(tblc.LastName, 'UP')) AS CustomerName,
	tblca.PhoneNumber,
	(co.CostOfAllService + ISNULL(MC.MerchandizePrice,'0.00')) AS Amount,
	CASE WHEN tblcvmd.ClientMembershipId IS NULL THEN 'Drive Up'
	ELSE 'Membership' END AS Type,
    (STUFF((SELECT Distinct ', ' + cv.codevalue
    FROM tblJobPaymentDetail pd
	JOIN tblcodevalue cv on cv.id = pd.paymentType and pd.IsDeleted = 0
	WHERE pd.jobPaymentId = tblj.jobPaymentId and cv.codevalue in ('Cash', 'Card', 'GiftCard', 'Membership', 'Account')
    FOR XML PATH('')
	), 1, 2, ''))  AS PaymentType
FROM 
	#tblJob tblj WITH(NOLOCK)
LEFT JOIN
    #CostOfAllService co ON(tblj.JobId = co.JobId)
LEFT JOIN
	tblClient tblc  WITH(NOLOCK) ON(tblj.ClientId = tblc.ClientId)
LEFT JOIN
	tblClientAddress tblca  WITH(NOLOCK) ON(tblc.ClientId = tblca.ClientId)
LEFT JOIN
	tblClientVehicle tblcv  WITH(NOLOCK) ON(tblj.VehicleId = tblcv.VehicleId)
LEFT JOIN
	tblVehicleModel
	--GetTable('VehicleModel')
	vmo ON(tblj.Model = vmo.ModelId)
	LEFT JOIN tblVehicleMake make on make.MakeId =tblj.Make
LEFT JOIN
	GetTable('VehicleColor') vc ON(tblj.Color = vc.valueid)
INNER JOIN
	#tblJobItem tblji  WITH(NOLOCK) ON(tblji.JobId = tblj.JobId)
INNER JOIN
	tblService tbls  WITH(NOLOCK) ON(tblji.ServiceId = tbls.ServiceId)
LEFT JOIN 
    #MerchandizeCount MC ON(tblj.JobId = MC.JobId)
INNER JOIN
    tblJobPayment tbljp ON(tblj.JobPaymentId = tbljp.JobPaymentId)
LEFT JOIN
    tblClientVehicleMembershipDetails tblcvmd ON(tblj.VehicleId = tblcvmd.ClientVehicleId) --AND tblcvmd.IsActive = 1 AND ISNULL(tblcvmd.IsDeleted,0)=0

WHERE tbls.ServiceType = @WashServiceId OR tbls.ServiceType = @DetailServiceId
ORDER BY tblj.JobId desc

END