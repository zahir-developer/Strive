-- ====================================================
-- Author:		Vineeth B
-- Create date: 15-08-2020
-- Description:	Retrieves All Wash Ticket details based on search parameter
-- ====================================================

---------------------History---------------------------
-- ====================================================
-- 25-08-2020, Vineeth - changed jobtype to servicetype
-- 26-08-2020, Zahir Hussain -- 1. Change Join from INNER to LEFT, 2.Vehicle details taken from Job table instead of Client Vehicle table. 
-- 25-01-2020, Zahir Hussain -- Added Offset and Skip logic for pagination. EXEC [StriveCarSalon].[uspGetAllJob] 1, NULL, 1, 50, NULL, NULL
-- 07-jun-2021, shalini - pagenumber and count for nullquery changes					 
-- 07-jun-2021, shalini - added unk for null make and model	
-- 16-06-2021, Shalini -removed wildcard from Query
-- 21-06-2021,shalini - added location filter in query 

------------------------------------------------		 


-------------------------------------------------------
--	EXEC [StriveCarSalon].[uspGetAllJob] 1, 'Brady', 1, 800, 'ASC', null,'2021-01-02','2021-05-02'
-- ====================================================

CREATE PROCEDURE [StriveCarSalon].[uspGetAllJob_New]

@locationId INT = NULL, 
@Query NVARCHAR(50) = NULL,
@PageNo INT = NULL, 
@PageSize INT = NULL,
@SortOrder VARCHAR(5) = NULL, 
@SortBy VARCHAR(50) = NULL,
@StartDate date, 
@EndDate date
 
AS
BEGIN

DECLARE @WashJobType INT = (Select top 1 valueId from GetTable('JobType') where valuedesc = 'Wash')

DROP TABLE IF EXISTS #tblJob

Select tbj.JobId, tbj.locationId, ClientId, JobDate, TimeIn, EstimatedTimeOut, JobPaymentId, Make, Model, Color, tbls.ServiceName, tblcv.valuedesc as ServiceType into #tblJob from tblJob tbj
INNER join tblJobItem tblji on tbj.JobId = tblji.JobId AND ISNULL(tblji.IsDeleted,0) = 0
INNER join tblService tbls on tblji.ServiceId = tbls.ServiceId
INNER join GetTable('ServiceType') tblcv on tbls.ServiceType = tblcv.valueid and tblcv.valuedesc='Wash Package'

where tbj.IsDeleted =0 AND JobDate Between @StartDate and @StartDate AND tbj.JobType = @WashJobType

DECLARE @Skip INT = 0;

IF @PageSize is not NULL
BEGIN
SET @Skip = @PageSize * (@PageNo-1);
END

IF @PageSize is NULL
BEGIN
SET @PageSize = (Select count(1) from tblJob);
SET @PageNo = 1;
SET @Skip = @PageSize * (@PageNo-1);
Print @PageSize
Print @PageNo
Print @Skip
END
Drop TABLE IF EXISTS #GetAllJobs
Select 
tbj.JobId
,tbj.JobDate
,tbj.JobId as TicketNumber
,CONCAT(tblc.FirstName,' ',tblc.LastName) AS ClientName
,tblca.PhoneNumber
,IsNull(model.ModelValue,'Unk') AS Model,
 IsNull(make.MakeValue,'Unk') As Make,
cvCo.valuedesc AS Color
,tbj.TimeIn
,tbj.EstimatedTimeOut
,tbj.ServiceName
,tbj.ServiceType
,tbj.JobPaymentId
,ps.valuedesc
,tbljp.paymentstatus,
CASE
when (ps.valuedesc != 'Success'OR tbljp.PaymentStatus IS NULL) then 'False'
when ps.valuedesc = 'Success' then 'True'
End AS IsPaid
into #GetAllJobs
from 
#tblJob tbj 
LEFT join tblClient tblc on tbj.ClientId = tblc.ClientId
LEFT JOIN tblJobPayment tbljp  WITH(NOLOCK) ON tbj.JobPaymentId = tbljp.JobPaymentId AND tbljp.IsProcessed=1 AND ISNULL(tbljp.IsRollBack,0)=0 AND tbljp.IsActive = 1 AND ISNULL(tbljp.IsDeleted,0)=0 
LEFT JOIN GetTable('PaymentStatus') ps ON(tbljp.PaymentStatus = ps.valueid)
--LEFT join StriveCarSalon.tblClientVehicle tblclv on tbj.VehicleId = tblclv.VehicleId
LEFT join tblClientAddress tblca on tbj.ClientId = tblca.ClientId

Left join tblVehicleMake make on tbj.Make=make.MakeId
Left join tblvehicleModel model on tbj.Model= model.ModelId	and make.MakeId = model.MakeId
LEFT JOIN GetTable('VehicleColor') cvCo ON tbj.Color = cvCo.valueid
WHERE
(tbj.locationId = @locationId OR @locationId is NULL)
 and (tbj.jobdate between @StartDate and @EndDate or (@StartDate is NULL and @EndDate is Null))

--AND tblcv.valuedesc='Wash Package' 

--AND isnull(tblc.IsDeleted,0)=0
--AND isnull(tblji.IsDeleted,0)=0
--AND isnull(tbls.IsDeleted,0)=0 
AND
((@Query is null OR @Query = '') OR (@Query != '' AND	
(tbj.JobId like @Query 
OR CONCAT(model.ModelValue,' , ',make.MakeValue,' , ',cvCo.valuedesc) like '%' +@Query+ '%' 
OR	tblc.FirstName like @Query+'%'
OR	tblc.lastName like @Query+'%'
OR	tbj.ServiceName like @Query+'%'
OR	model.ModelValue like @Query+'%'								
OR	make.MakeValue like @Query+'%'
OR	cvCo.valuedesc like @Query+'%'
OR CONCAT(tblc.FirstName,' ',tblc.LastName)like '%'+@Query+'%' )))

GROUP BY 

tbj.JobId
,tbj.JobDate
,tblc.FirstName
,tblc.LastName
,tblca.PhoneNumber
,model.ModelValue
,make.MakeValue
,cvCo.valuedesc
,tbj.TimeIn
,tbj.EstimatedTimeOut
,tbj.ServiceName
,tbj.ServiceType
,tbj.JobPaymentId
,ps.valuedesc
,tbljp.paymentstatus
ORDER BY 
--tbj.TicketNumber
CASE WHEN @SortBy = 'TicketNumber' AND @SortOrder='ASC' THEN tbj.JobId END ASC,
CASE WHEN @SortBy = 'FirstName' AND @SortOrder='ASC' THEN tblc.FirstName END ASC,
CASE WHEN @SortBy = 'PhoneNumber' AND @SortOrder='ASC' THEN tblca.PhoneNumber END ASC,
CASE WHEN @SortBy = 'Make' AND @SortOrder='ASC' THEN make.MakeValue END ASC,
CASE WHEN @SortBy = 'TimeIn' AND @SortOrder='ASC' THEN tbj.TimeIn END ASC,
CASE WHEN @SortBy = 'EstimatedTimeOut' AND @SortOrder='ASC' THEN tbj.EstimatedTimeOut END ASC,
CASE WHEN @SortBy = 'ServiceName' AND @SortOrder='ASC' THEN tbj.ServiceName END ASC,
CASE WHEN @SortBy IS NULL AND @SortOrder='ASC' THEN 1 END ASC,
----DESC
CASE WHEN @SortBy = 'TicketNumber' AND @SortOrder='DESC' THEN tbj.JobId END DESC,
CASE WHEN @SortBy = 'FirstName' AND @SortOrder='DESC' THEN tblc.FirstName END DESC,
CASE WHEN @SortBy = 'PhoneNumber' AND @SortOrder='DESC' THEN tblca.PhoneNumber END DESC,
CASE WHEN @SortBy = 'Make' AND @SortOrder='DESC' THEN make.MakeValue END DESC,
CASE WHEN @SortBy = 'TimeIn' AND @SortOrder='DESC' THEN tbj.TimeIn END DESC,
CASE WHEN @SortBy = 'EstimatedTimeOut' AND @SortOrder='DESC' THEN tbj.EstimatedTimeOut END DESC,
CASE WHEN @SortBy = 'ServiceName' AND @SortOrder='DESC' THEN tbj.ServiceName END DESC,
CASE WHEN @SortBy IS NULL AND @SortOrder='DESC' THEN tbj.JobId END ASC,
CASE WHEN @SortBy IS NULL AND @SortOrder IS NULL THEN tbj.JobId END ASC

OFFSET (@Skip) ROWS FETCH NEXT (@PageSize) ROWS ONLY

select * from #GetAllJobs

IF @Query IS NULL OR @Query = ''
BEGIN 

select count(distinct jobId) as Count from #tblJob tbj

END

IF @Query IS Not NULL AND @Query != ''
BEGIN

select count(distinct jobId) as Count from #tblJob tbj 
LEFT join tblClient tblc on tbj.ClientId = tblc.ClientId
Left join tblVehicleMake make on tbj.Make=make.MakeId
Left join tblvehicleModel model on tbj.Model= model.ModelId	and make.MakeId = model.MakeId
LEFT JOIN GetTable('VehicleColor') cvCo ON tbj.Color = cvCo.valueid
WHERE (tbj.JobId like @Query
OR	tblc.FirstName like @Query+'%'
OR	tblc.lastName like @Query+'%'
OR	tbj.ServiceName like @Query+'%'
OR	model.ModelValue like @Query+'%'								
OR	make.MakeValue like @Query+'%'
OR  cvCo.valuedesc like @Query+'%'
OR	cvCo.valuedesc like @Query+'%'
OR CONCAT(tblc.FirstName,' ',tblc.LastName)like '%'+@Query+'%' )

END

END