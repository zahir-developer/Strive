





CREATE   procedure [StriveCarSalon].[uspGetCheckedInVehicleDetails] 
AS

-- =============================================
-- Author:		Vineeth B
-- Create date: 28-09-2020
-- Description:	To get Unchecked Vehicle Details
-- =============================================

-----------------------------------------------------------------------------------------------------------------------------------------
-- Rev | Date Modified  | Developer	| Change Summary
-----------------------------------------------------------------------------------------------------------------------------------------
--  1  |  2020-Oct-12   | Vineeth	| MODIFIED Status to MembershipNameOrPaymentStatus and added Wash and Detail JobType 
--									  order by JobDate AND added ServiceType condition and check null conditionin ColorCode and 
--									  MembershipNameOrPaymentStatus and added JobId col, WITH(NOLOCK)
--  2  |  2020-Oct-12   | Vineeth   | Added Checkout condition
-----------------------------------------------------------------------------------------------------------------------------------------

BEGIN

drop table if exists #checkout
SELECT 
	tblj.JobId,
	js.valuedesc,
	tbljp.JobId AS JobPaymentId,
	tblj.TicketNumber,
	CONCAT(tblc.FirstName,' ',tblc.LastName) AS CustomerName,
	CONCAT(vm.valuedesc,' ',vmo.valuedesc,'/',vc.valuedesc) AS VehicleDescription,
	tbls.ServiceName,
	st.valuedesc AS ServiceTypeName,
	CASE 	WHEN st.valuedesc='Additional Services' THEN tbls.ServiceName END AS AdditionalServices,
	CASE	WHEN st.valuedesc !='Additional Services' THEN tbls.ServiceName END AS [Services],
	tbls.Cost,
	CONVERT(VARCHAR(5),tblj.TimeIn,108) AS Checkin,
	CONVERT(VARCHAR(5),tblj.EstimatedTimeOut,108) AS Checkout,
	ISNULL(tblm.MembershipName,'') AS MembershipName,
	ISNULL(ps.valuedesc,'') AS PaymentStatus,
	CASE
	   WHEN (ps.valuedesc !='Success' OR tbljp.PaymentStatus IS NULL) AND js.valuedesc='Completed' THEN '#008000' 
	   WHEN tblj.TimeIn !='' AND js.valuedesc!='Hold' AND st.valuedesc='Additional Services' and (ps.valuedesc != 'Success'OR tbljp.PaymentStatus IS NULL) THEN '#FFA500'
	   WHEN tblm.MembershipName IS NOT NULL AND js.valuedesc ='In Progress' AND st.valuedesc='Additional Services'  THEN '#FFA500'-- TO SHOW MEMBERSHIP NAME
	   WHEN tblm.MembershipName IS NOT NULL AND js.valuedesc='Completed' THEN '#008000'-- TO SHOW MEMBERSHIP NAME
	   WHEN tblm.MembershipName IS NOT NULL AND js.valuedesc NOT IN('Completed','Hold')  THEN '#FF1493'-- TO SHOW MEMBERSHIP NAME
	   --WHEN tblm.MembershipName IS NOT NULL AND js.valuedesc ='In Progress' AND st.valuedesc='Additional Services'  THEN '#FFFF00'-- TO SHOW MEMBERSHIP NAME
	   WHEN ps.valuedesc ='Success' AND js.valuedesc='Completed' THEN '#008000' -- TO SHOW PAID
	   WHEN ps.valuedesc ='Success' AND js.valuedesc NOT IN('Completed','Hold') THEN '#FF1493' -- TO SHOW PAID
	   WHEN (ps.valuedesc !='Success' OR tbljp.PaymentStatus IS NULL) AND js.valuedesc NOT IN('Completed','Hold') THEN '#FF1493' 
	   WHEN ps.valuedesc ='Success' AND js.valuedesc='Hold' THEN '#00BFFF' -- TO SHOW PAID
	   WHEN (ps.valuedesc !='Success' OR tbljp.PaymentStatus IS NULL) AND js.valuedesc='Hold' THEN '#00BFFF' 
	   WHEN tblj.TimeIn !='' AND (ps.valuedesc !='Success' OR tbljp.PaymentStatus IS NULL) AND js.valuedesc !='Hold' THEN '#FF1493'
	   WHEN tblm.MembershipName IS NULL THEN ''
	END AS ColorCode,
	CASE
	    WHEN ps.valuedesc = 'Success' THEN 'PAID'
		WHEN (ps.valuedesc != 'Success' OR ps.valuedesc IS NULL) THEN js.valuedesc
	END AS MembershipNameOrPaymentStatus,
	CASE  
	    WHEN js.valuedesc='Completed' THEN 1
		WHEN js.valuedesc='In Progress' THEN 2
		WHEN js.valuedesc='Waiting' THEN 3
		WHEN js.valuedesc='Hold' THEN 4
	END AS JobStatusOrder
INTO 
	#Checkout
FROM 
	tblJob tblj WITH(NOLOCK)
INNER JOIN
	GetTable('JobType') jt ON(tblj.JobType = jt.valueid)
INNER JOIN
	GetTable('JobStatus') js ON(tblj.JobStatus = js.valueid)
INNER JOIN
	tblClient tblc  WITH(NOLOCK) ON(tblj.ClientId = tblc.ClientId)
INNER JOIN
	tblClientVehicle tblcv  WITH(NOLOCK) ON(tblj.VehicleId = tblcv.VehicleId)
INNER JOIN
	GetTable('VehicleManufacturer') vm ON(tblcv.VehicleMfr = vm.valueid)
INNER JOIN
	GetTable('VehicleModel') vmo ON(tblcv.VehicleModel = vmo.valueid)
INNER JOIN
	GetTable('VehicleColor') vc ON(tblcv.VehicleColor = vc.valueid)
INNER JOIN
	tblJobItem tblji  WITH(NOLOCK) ON(tblji.JobId = tblj.JobId)
INNER JOIN
	tblService tbls  WITH(NOLOCK) ON(tblji.ServiceId = tbls.ServiceId)
INNER JOIN 
	GetTable('ServiceType') st ON(tbls.ServiceType = st.valueid)
LEFT JOIN
    tblClientVehicleMembershipDetails tblcvmd  WITH(NOLOCK)
    ON tblcv.VehicleId = tblcvmd.ClientVehicleId
    --AND tblcvmd.StartDate<=CAST(getdate() AS Date)
    --AND tblcvmd.EndDate>=CAST(getdate() AS Date)
LEFT JOIN
	tblMembership tblm  WITH(NOLOCK) ON(tblm.MembershipId = tblcvmd.MembershipId)
LEFT JOIN
	tblJobPayment tbljp  WITH(NOLOCK) ON(tblj.JobId = tbljp.JobId)
LEFT JOIN
	GetTable('PaymentStatus') ps ON(tbljp.PaymentStatus = ps.valueid)
WHERE 
tblj.TicketNumber != '' and	jt.valuedesc IN('Wash','Detail') AND st.valuedesc IN('Washes','Details','Additional Services')
AND ISNULL(tblj.CheckOut,0)=0 AND tblj.IsActive = 1 AND tblc.IsActive = 1 AND tblcv.IsActive = 1 
AND tblji.IsActive = 1 AND tbls.IsActive = 1 AND (tblcvmd.IsActive = 1 OR tblcvmd.IsActive IS NULL) 
AND (tblm.IsActive = 1 OR tblm.IsActive IS NULL) AND (tbljp.IsActive = 1 OR tbljp.IsActive IS NULL)
AND ISNULL(tblj.IsDeleted,0) = 0 AND ISNULL(tblc.IsDeleted,0) = 0 AND ISNULL(tblcv.IsDeleted,0) = 0 
AND ISNULL(tblji.IsDeleted,0) = 0 AND ISNULL(tbls.IsDeleted,0) = 0 AND ISNULL(tblcvmd.IsDeleted,0) = 0 
AND ISNULL(tblm.IsDeleted,0) = 0 AND ISNULL(tbljp.IsDeleted,0)=0 

ORDER BY js.valuedesc 

ALTER Table  #Checkout ALTER COLUMN ColorCode Varchar(20)
UPDATE #Checkout SET ColorCode= CASE WHEN ServiceTypeName='Additional Services' THEN '1-'+ColorCode ELSE '2-'+ColorCode END

SELECT 
	  JobId
	,valuedesc
	,JobPaymentId
	, TicketNumber
	, CustomerName
	, VehicleDescription
	, STUFF(
   (SELECT DISTINCT ', ' + AdditionalServices 
    FROM #Checkout C
	WHERE C.JobId= tmp.JobID
    FOR XML PATH('')
	), 1, 2, '')  AS AdditionalServices,
	 STUFF(
   (SELECT DISTINCT', ' + [Services]
    FROM #Checkout C1
	WHERE C1.JobId= tmp.JobID
    FOR XML PATH('')
	), 1, 2, '')  AS [Services]
	, SUM(Cost) AS Cost
	, Checkin
	, Checkout
	, MembershipName
	, PaymentStatus
	,STUFF(MIN(ColorCode),1,2,'') AS ColorCode
	, MembershipNameOrPaymentStatus
	,JobStatusOrder
 FROM 
	#Checkout tmp
GROUP BY	 
	 Tmp.JobId
	,Tmp.valuedesc
	,Tmp.JobPaymentId
	,TicketNumber
	,CustomerName
	,VehicleDescription
	,Checkin
	,Checkout
	,MembershipName
	,PaymentStatus
	,MembershipNameOrPaymentStatus
	,JobStatusOrder
	order by Tmp.JobId desc 
	END