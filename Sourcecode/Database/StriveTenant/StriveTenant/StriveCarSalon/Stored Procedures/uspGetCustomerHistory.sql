-------------history-----------------
-- =============================================
-- 1  shalini 2021-06-01  -added order by desc jobdate
-- 2  shalini 2021-06-16  -removed wildcard from query

-- =============================================
--[uspGetCustomerHistory]1,'2021-01-01','2021-12-31',null,1,10,'asc','FirstName'
CREATE Procedure [StriveCarSalon].[uspGetCustomerHistory]
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
SET @PageSize = (Select count(1) from tblClient);
SET @PageNo = 1;
SET @Skip = @PageSize * (@PageNo-1);
Print @PageSize
Print @PageNo
Print @Skip
END

DROP TABLE IF EXISTS #GetCustomerDetails

drop table if exists #JobServices
SELECT 
	tblj.JobId,
	tblj.Jobdate as ServiceDate,
	tblj.locationid,
	tblj.TicketNumber,
	tblc.ClientId,
	tblcv.VehicleId,
	tblcv.barcode,
     tblc.FirstName,
	tblc.LastName,
	tbls.ServiceName,
	st.valuedesc AS ServiceTypeName,
	CASE 	WHEN st.valuedesc='Additional Services' THEN tbls.ServiceName END AS AdditionalServices,
	CASE	WHEN st.valuedesc !='Additional Services' THEN tbls.ServiceName END AS [Services],
	ISNULL(tblm.MembershipName,'') AS MembershipName,
	ISNULL(ps.valuedesc,'') AS PaymentStatus
	
INTO 
	#Jobs
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
WHERE tblj.LocationId = @locationId and tblj.JobDate between @fromDate and @toDate and 
tblj.TicketNumber != '' and	jt.valuedesc IN('Wash','Detail') AND st.valuedesc IN('Wash Package','Detail Package','Additional Services')
AND ISNULL(tblj.CheckOut,0)=0 AND tblj.IsActive = 1 AND tblc.IsActive = 1 AND tblcv.IsActive = 1 
AND tblji.IsActive = 1 AND tbls.IsActive = 1  
AND ISNULL(tblj.IsDeleted,0) = 0 AND ISNULL(tblc.IsDeleted,0) = 0 AND ISNULL(tblcv.IsDeleted,0) = 0 
AND ISNULL(tblji.IsDeleted,0) = 0 AND ISNULL(tbls.IsDeleted,0) = 0 AND ISNULL(tblcvmd.IsDeleted,0) = 0 
and 
 ((@Query is null or tblc.FirstName  like @Query+'%') OR
  (@Query is null or tblc.LastName  like @Query+'%') OR
  (@Query is null or tblc.ClientId  like @Query+'%') OR
  (@Query is null or tblj.JobDate  like @Query+'%') OR
  (@Query is null or tblcv.VehicleId  like @Query+'%') OR
  (@Query is null or tblj.TicketNumber  like @Query+'%') OR
  (@Query is null or tblm.MembershipName  like @Query+'%') OR
  (@Query is null or tblcv.Barcode  like @Query+'%') OR
  (@Query is null or tbls.ServiceName  like @Query+'%'))
ORDER BY  tblj.Jobdate desc

SELECT 
	  JobId
	  ,LocationId
	, TicketNumber
	,ServiceDate
	,ClientId
	,VehicleId
	, FirstName
	,LastName
	, STUFF(
   (SELECT DISTINCT ', ' + AdditionalServices 
    FROM #Jobs C
	WHERE C.JobId= tmp.JobID
    FOR XML PATH('')
	), 1, 2, '')  AS AdditionalServices,
	 STUFF(
   (SELECT DISTINCT', ' + [Services]
    FROM #Jobs C1
	WHERE C1.JobId = tmp.JobID
    FOR XML PATH('')
	), 1, 2, '')  AS [Services]
	
	, MembershipName
	,Barcode
	into #GetCustomerDetails
 FROM 
	#Jobs tmp 
GROUP BY	 
	 Tmp.JobId
	 ,tmp.LocationId
	,ServiceDate
	,TicketNumber
	,FirstName
	,LastName
	,ClientId
	,VehicleId
	,MembershipName
	,Barcode
	order by 
CASE WHEN @SortBy = 'FirstName' AND @SortOrder='ASC' THEN FirstName END ASC,
CASE WHEN @SortBy = 'LastName' AND @SortOrder='ASC' THEN LastName END ASC,
CASE WHEN @SortBy = 'ServiceDate' AND @SortOrder='ASC' THEN ServiceDate END ASC,
CASE WHEN @SortBy = 'VehicleId' AND @SortOrder='ASC' THEN VehicleId END ASC,
CASE WHEN @SortBy = 'ClientId' AND @SortOrder='ASC' THEN ClientId END ASC,
CASE WHEN @SortBy = 'TicketNumber' AND @SortOrder='ASC' THEN TicketNumber END ASC,
CASE WHEN @SortBy = 'MambershipName' AND @SortOrder='ASC' THEN MembershipName END ASC,

CASE WHEN @SortBy = 'Barcode' AND @SortOrder='ASC' THEN Barcode END ASC,
CASE WHEN @SortBy IS NULL AND @SortOrder='ASC' THEN 1 END ASC,
----DESC

CASE WHEN @SortBy = 'FirstName' AND @SortOrder='DESC' THEN FirstName END DESC,
CASE WHEN @SortBy = 'LastName' AND @SortOrder='DESC' THEN LastName END DESC,
CASE WHEN @SortBy = 'ServiceDate' AND @SortOrder='DESC' THEN ServiceDate END DESC,
CASE WHEN @SortBy = 'VehicleId' AND @SortOrder='DESC' THEN VehicleId END DESC,
CASE WHEN @SortBy = 'ClientId' AND @SortOrder='DESC' THEN ClientId END DESC,
CASE WHEN @SortBy = 'TicketNumber' AND @SortOrder='DESC' THEN TicketNumber END DESC,
CASE WHEN @SortBy = 'MambershipName' AND @SortOrder='DESC' THEN MembershipName END DESC,

CASE WHEN @SortBy = 'Barcode' AND @SortOrder='DESC' THEN Barcode END DESC,
CASE WHEN @SortBy IS NULL AND @SortOrder='DESC' THEN FirstName END DESC,

CASE WHEN @SortBy IS NULL AND @SortOrder IS NULL THEN FirstName END ASC
	
OFFSET (@Skip) ROWS FETCH NEXT (@PageSize) ROWS ONLY



select * from #GetCustomerDetails


IF @Query IS NULL OR @Query = ''
BEGIN 
select count(1) as Count from tblClient where 
ISNULL(IsDeleted,0) = 0 

END

IF @Query IS Not NULL AND @Query != ''
BEGIN
select count(1) as Count from #GetCustomerDetails
END

	END