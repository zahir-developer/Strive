
-- ====================================================
-- Author:		Vineeth B
-- Create date: 15-08-2020
-- Description:	Retrieves Job and Job item details
-- ====================================================

---------------------History---------------------------
-- ====================================================
-- 25-08-2020, Vineeth - changed jobtype to servicetype
-- 26-08-2020, Zahir Hussain -- 1. Change Join from INNER to LEFT, 2.Vehicle details taken from Job table instead of Client Vehicle table. 
-- 25-01-2020, Zahir Hussain -- Added Offset and Skip logic for pagination. EXEC [StriveCarSalon].[uspGetAllJob] 1, NULL, 1, 50, NULL, NULL
							 --	EXEC [StriveCarSalon].[uspGetAllJob] 1, 'JIM DAVIS', 1, 10, 'ASC', null,'2021-01-02','2021-03-02'

-------------------------------------------------------
-- ====================================================

CREATE PROC [StriveCarSalon].[uspGetAllJob]
@locationId INT = NULL, 
@Query NVARCHAR(50) = NULL,
 @PageNo INT = NULL, 
 @PageSize INT = NULL,
 @SortOrder VARCHAR(5) = 'ASC', 
 @SortBy VARCHAR(50) = NULL,
 @StartDate date = NULL, 
 @EndDate date = NULL
AS
BEGIN

DECLARE @Skip INT = 0;

IF @PageSize is not NULL
BEGIN
SET @Skip = @PageSize * (@PageNo-1);
END

IF @PageSize is NULL
BEGIN
SET @PageSize = (Select count(1) from StriveCarSalon.tblJob);
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
,tbj.TicketNumber
,CONCAT(tblc.FirstName,' ',tblc.LastName) AS ClientName
,tblca.PhoneNumber
,CONCAT(cvMo.valuedesc,', ',cvMfr.valuedesc,', ',cvCo.valuedesc) AS VehicleName
,tbj.TimeIn
,tbj.EstimatedTimeOut
,tbls.ServiceName
,tbls.ServiceType
into #GetAllJobs
from 
StriveCarSalon.tblJob tbj 
INNER join StriveCarSalon.tblJobItem tblji on tbj.JobId = tblji.JobId
LEFT join StriveCarSalon.tblService tbls on tblji.ServiceId = tbls.ServiceId
LEFT join StriveCarSalon.tblClient tblc on tbj.ClientId = tblc.ClientId
--LEFT join StriveCarSalon.tblClientVehicle tblclv on tbj.VehicleId = tblclv.VehicleId
LEFT join StriveCarSalon.tblClientAddress tblca on tbj.ClientId = tblca.ClientId
LEFT join StriveCarSalon.GetTable('ServiceType') tblcv on tbls.ServiceType = tblcv.valueid and tblcv.valuedesc='Wash Package'
LEFT JOIN strivecarsalon.GetTable('VehicleManufacturer') cvMfr ON tbj.Make = cvMfr.valueid
LEFT JOIN strivecarsalon.GetTable('VehicleModel') cvMo ON tbj.Model = cvMo.valueid
LEFT JOIN strivecarsalon.GetTable('VehicleColor') cvCo ON tbj.Color = cvCo.valueid
WHERE

(tbj.locationId = @locationId OR @locationId is NULL)
 and
(tbj.jobdate between @StartDate and @EndDate or (@StartDate is NULL and @EndDate is Null))
AND tblcv.valuedesc='Wash Package' AND isnull(tbj.IsDeleted,0)=0
--AND isnull(tblc.IsDeleted,0)=0
--AND isnull(tblji.IsDeleted,0)=0
--AND isnull(tbls.IsDeleted,0)=0 
AND
(@Query is null OR (@Query != '' AND	(tbj.TicketNumber like '%' +@Query+ '%' 
OR CONCAT(cvMo.valuedesc,' , ',cvMfr.valuedesc,' , ',cvCo.valuedesc) like '%' +@Query+ '%' 
								OR	tblc.FirstName like '%'+@Query+'%'
								OR	tblc.lastName like '%'+@Query+'%'
								OR	tbls.ServiceName like '%'+@Query+'%'
								OR	cvMfr.valuedesc like '%'+@Query+'%'								
								OR	cvMo.valuedesc like '%'+@Query+'%'
								OR	cvCo.valuedesc like '%'+@Query+'%'
								OR	tbj.jobdate like '%'+@Query+'%'
								OR CONCAT(tblc.FirstName,' ',tblc.LastName)like '%'+@Query+'%' )))

GROUP BY 

tbj.JobId
,tbj.JobDate
,tbj.TicketNumber
,tblc.FirstName
,tblc.LastName
,tblca.PhoneNumber
,cvMo.valuedesc
,cvMfr.valuedesc
,cvCo.valuedesc
,tbj.TimeIn
,tbj.EstimatedTimeOut
,tbls.ServiceName
,tbls.ServiceType,
tbj.IsDeleted

ORDER BY 
--tbj.TicketNumber
CASE WHEN @SortBy = 'TicketNumber' AND @SortOrder='ASC' THEN tbj.TicketNumber END ASC,
CASE WHEN @SortBy = 'FirstName' AND @SortOrder='ASC' THEN tblc.FirstName END ASC,
CASE WHEN @SortBy = 'PhoneNumber' AND @SortOrder='ASC' THEN tblca.PhoneNumber END ASC,
CASE WHEN @SortBy = 'Make' AND @SortOrder='ASC' THEN cvMo.valuedesc END ASC,
CASE WHEN @SortBy = 'TimeIn' AND @SortOrder='ASC' THEN tbj.TimeIn END ASC,
CASE WHEN @SortBy = 'EstimatedTimeOut' AND @SortOrder='ASC' THEN tbj.EstimatedTimeOut END ASC,
CASE WHEN @SortBy = 'ServiceName' AND @SortOrder='ASC' THEN tbls.ServiceName END ASC,
CASE WHEN @SortBy IS NULL AND @SortOrder='ASC' THEN 1 END ASC,
----DESC
CASE WHEN @SortBy = 'TicketNumber' AND @SortOrder='DESC' THEN tbj.TicketNumber END DESC,
CASE WHEN @SortBy = 'FirstName' AND @SortOrder='DESC' THEN tblc.FirstName END DESC,
CASE WHEN @SortBy = 'PhoneNumber' AND @SortOrder='DESC' THEN tblca.PhoneNumber END DESC,
CASE WHEN @SortBy = 'Make' AND @SortOrder='DESC' THEN cvMo.valuedesc END DESC,
CASE WHEN @SortBy = 'TimeIn' AND @SortOrder='DESC' THEN tbj.TimeIn END DESC,
CASE WHEN @SortBy = 'EstimatedTimeOut' AND @SortOrder='DESC' THEN tbj.EstimatedTimeOut END DESC,
CASE WHEN @SortBy = 'ServiceName' AND @SortOrder='DESC' THEN tbls.ServiceName END DESC,
CASE WHEN @SortBy IS NULL AND @SortOrder='DESC' THEN tbls.ServiceName END ASC,

CASE WHEN @SortBy IS NULL AND @SortOrder IS NULL THEN tbj.JobId END DESC

OFFSET (@Skip) ROWS FETCH NEXT (@PageSize) ROWS ONLY



select * from #GetAllJobs

IF @Query IS NULL OR @Query = ''
BEGIN 
select count(1) as Count from StriveCarSalon.tblJob where 
ISNULL(IsDeleted,0) = 0 

END

IF @Query IS Not NULL AND @Query != ''
BEGIN
select count(1) as Count from #GetAllJobs
END
END
