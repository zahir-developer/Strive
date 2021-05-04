CREATE Procedure [StriveCarSalon].[uspGetCustomerHistory]--[uspGetCustomerHistory]1,'2015-01-01','2021-02-02',null,1,10,'asc',null
@locationId int,
@fromDate date null,
@toDate date null,
@Query NVARCHAR(50) = NULL,
@PageNo INT = NULL,
@PageSize INT = NULL, 
@SortOrder VARCHAR(5) = 'ASC',
@SortBy VARCHAR(10) = NULL
AS
BEGIN
DECLARE @Skip INT = 0;
IF @PageSize is not NULL
BEGIN
SET @Skip = @PageSize * (@PageNo-1);
END

IF @PageSize is NULL
BEGIN
SET @PageSize = (Select count(1) from StriveCarSalon.tblClient);
SET @PageNo = 1;
SET @Skip = @PageSize * (@PageNo-1);
Print @PageSize
Print @PageNo
Print @Skip
END

DROP TABLE IF EXISTS #NeedOfPayment
SELECT DISTINCT tblj.JobId--,tblji.ServiceId ServiceUsed,tblcvmds.ServiceId ServicePurchased 
,CASE WHEN tblcvmds.ServiceId IS NULL THEN 'Y'
WHEN tblji.ServiceId = tblcvmds.ServiceId THEN 'N'
END AS PaymentNeed
INTO #NeedOfPayment
FROM tblJob tblj INNER JOIN tblClientVehicleMembershipDetails tblcvmd
ON tblj.VehicleId = tblcvmd.ClientVehicleId AND tblcvmd.IsActive =1 AND ISNULL(tblcvmd.IsDeleted,0)=0 AND tblj.IsActive=1 AND ISNULL(tblj.IsDeleted,0)=0
INNER JOIN tblMembership tblm ON tblcvmd.MembershipId = tblm.MembershipId AND tblm.IsActive=1 AND ISNULL(tblm.IsDeleted,0)=0
INNER JOIN tblJobItem tblJi ON tblj.JobId = tblji.JobId AND tblji.IsActive=1 AND ISNULL(tblji.IsDeleted,0)=0
LEFT JOIN tblClientVehicleMembershipService tblcvmds ON tblcvmds.ServiceId = tblji.ServiceId AND tblcvmds.IsActive=1 AND ISNULL(tblcvmds.IsDeleted,0)=0
where tblj.LocationId = @locationId AND tblj.JobDate between @fromDate and @toDate
ORDER BY tblj.JobId

DROP TABLE IF EXISTS #PaymentDo
SELECT JobId,PaymentNeed,ROW_NUMBER() OVER (PARTITION BY JobId ORDER BY PaymentNeed desc)
NeedToPay INTO #PaymentDo FROM #NeedOfPayment  

DROP TABLE IF EXISTS #PaymentNeedToDone
SELECT JobId,PaymentNeed INTO #PaymentNeedToDone FROM #PaymentDo WHERE NeedToPay=1

drop table if exists #checkout
SELECT 
	tblj.JobId,
	tblj.locationid,
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
       WHEN tblm.MembershipName IS NOT NULL AND js.valuedesc='Completed' AND pntd.PaymentNeed ='Y' THEN '#FF1493'-- TO SHOW MEMBERSHIP NAME
	   WHEN (ps.valuedesc !='Success' OR tbljp.PaymentStatus IS NULL) AND js.valuedesc='Completed' THEN '#008000' 
	   WHEN tblj.TimeIn !='' AND js.valuedesc!='Hold' AND st.valuedesc='Additional Services' and (ps.valuedesc != 'Success'OR tbljp.PaymentStatus IS NULL) THEN '#FFA500'
	   WHEN tblm.MembershipName IS NOT NULL AND js.valuedesc ='In Progress' AND st.valuedesc='Additional Services'  THEN '#FFA500'-- TO SHOW MEMBERSHIP NAME
	   WHEN tblm.MembershipName IS NOT NULL AND js.valuedesc='Completed' AND pntd.PaymentNeed ='N' THEN '#008000'-- TO SHOW MEMBERSHIP NAME
	   WHEN tblm.MembershipName IS NOT NULL AND js.valuedesc NOT IN('Completed','Hold')  THEN '#FF1493'-- TO SHOW MEMBERSHIP NAME
	   --WHEN tblm.MembershipName IS NOT NULL AND js.valuedesc ='In Progress' AND st.valuedesc='Additional Services'  THEN '#FFFF00'-- TO SHOW MEMBERSHIP NAME
	   WHEN ps.valuedesc ='Success' AND st.valuedesc IN('Additional Services') AND tblm.MembershipName IS NULL AND js.valuedesc NOT IN('Completed','Hold') THEN '#FFA500'
	   WHEN ps.valuedesc ='Success' AND js.valuedesc='Completed' THEN '#008000' -- TO SHOW PAID
	   WHEN ps.valuedesc ='Success' AND js.valuedesc NOT IN('Completed','Hold') THEN '#FF1493' -- TO SHOW PAID
	   WHEN (ps.valuedesc !='Success' OR tbljp.PaymentStatus IS NULL) AND js.valuedesc NOT IN('Completed','Hold') THEN '#FF1493' 
	   WHEN ps.valuedesc ='Success' AND js.valuedesc='Hold' THEN '#00BFFF' -- TO SHOW PAID
	   WHEN (ps.valuedesc !='Success' OR tbljp.PaymentStatus IS NULL) AND js.valuedesc='Hold' THEN '#00BFFF' 
	   WHEN tblj.TimeIn !='' AND (ps.valuedesc !='Success' OR tbljp.PaymentStatus IS NULL) AND js.valuedesc !='Hold' THEN '#FF1493'
	   WHEN tblm.MembershipName IS NULL THEN ''
	END AS ColorCode
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
    ON tblcv.VehicleId = tblcvmd.ClientVehicleId AND tblcvmd.IsActive = 1
    --AND tblcvmd.StartDate<=CAST(getdate() AS Date)
    --AND tblcvmd.EndDate>=CAST(getdate() AS Date)
LEFT JOIN
	tblMembership tblm  WITH(NOLOCK) ON(tblm.MembershipId = tblcvmd.MembershipId) AND tblm.IsActive = 1 AND ISNULL(tblm.IsDeleted,0)=0
LEFT JOIN
	tblJobPayment tbljp  WITH(NOLOCK) ON tblj.JobPaymentId = tbljp.JobPaymentId AND tbljp.IsProcessed=1 AND ISNULL(tbljp.IsRollBack,0)=0 AND tbljp.IsActive = 1 AND ISNULL(tbljp.IsDeleted,0)=0 
LEFT JOIN
	GetTable('PaymentStatus') ps ON(tbljp.PaymentStatus = ps.valueid)
LEFT JOIN 
    #PaymentNeedToDone pntd ON(tblj.JobId = pntd.JobId)
WHERE tblj.LocationId = @locationId and tblj.JobDate between @fromDate and @toDate and 
tblj.TicketNumber != '' and	jt.valuedesc IN('Wash','Detail') AND st.valuedesc IN('Washes','Details','Additional Services')
AND ISNULL(tblj.CheckOut,0)=0 AND tblj.IsActive = 1 AND tblc.IsActive = 1 AND tblcv.IsActive = 1 
AND tblji.IsActive = 1 AND tbls.IsActive = 1  
AND ISNULL(tblj.IsDeleted,0) = 0 AND ISNULL(tblc.IsDeleted,0) = 0 AND ISNULL(tblcv.IsDeleted,0) = 0 
AND ISNULL(tblji.IsDeleted,0) = 0 AND ISNULL(tbls.IsDeleted,0) = 0 AND ISNULL(tblcvmd.IsDeleted,0) = 0 
ORDER BY js.valuedesc 

ALTER Table  #Checkout ALTER COLUMN ColorCode Varchar(20)
UPDATE #Checkout SET ColorCode= CASE WHEN ServiceTypeName='Additional Services' THEN '1-'+ColorCode ELSE '2-'+ColorCode END

SELECT 
	  JobId
	  ,LocationId
	--,valuedesc
	--,JobPaymentId
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
	--, PaymentStatus
	,STUFF(MIN(ColorCode),1,2,'') AS ColorCode
	--, MembershipNameOrPaymentStatus
	--,JobStatusOrder
 FROM 
	#Checkout tmp
GROUP BY	 
	 Tmp.JobId
	 ,tmp.LocationId
	--,Tmp.valuedesc
	--,Tmp.JobPaymentId
	,TicketNumber
	,CustomerName
	,VehicleDescription
	,Checkin
	,Checkout
	,MembershipName
	--,PaymentStatus
	--,MembershipNameOrPaymentStatus
	--,JobStatusOrder
	order by Tmp.JobId desc 
	
OFFSET (@Skip) ROWS FETCH NEXT (@PageSize) ROWS ONLY
	END