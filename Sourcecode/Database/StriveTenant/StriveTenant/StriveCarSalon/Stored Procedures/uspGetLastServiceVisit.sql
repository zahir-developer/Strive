
-- =============================================
-- Author:		Zahir Hussain M
-- Create date: 06-Sep-2021
-- Description:	Returns the Last Service Visit Job
-- Example: [StriveCarSalon].[uspGetLastServiceVisit] NULL, 1, NULL, 16804, NULL
-- ================================================
-- ---------------History--------------------------
-- ================================================

CREATE PROCEDURE [StriveCarSalon].[uspGetLastServiceVisit] 
(
@PageNo INT = NULL, 
@PageSize INT = NULL,
@ClientId int = NULL, 
@VehicleId int = NULL, 
@LocationId INT = NULL, 
@Query NVARCHAR(50) = NULL,
@SortOrder VARCHAR(5) = NULL, 
@SortBy VARCHAR(50) = NULL,
@StartDate date = NULL, 
@EndDate date = NULL)

AS
BEGIN

DECLARE @Skip INT = 0;

IF @PageNo is not NULL
BEGIN
SET @PageNo = 1;
END

IF @PageSize is NULL
BEGIN
SET @PageSize = 1;
END

IF @PageSize is not NULL
BEGIN
SET @Skip = @PageSize * (@PageNo-1);
END

Print @Skip

IF @PageSize is NULL
BEGIN
SET @PageSize = 10;
SET @PageNo = 1;
SET @Skip = @PageSize * (@PageNo-1);
END

DECLARE @ReviewNote varchar(50);

DROP TABLE IF EXISTS #JobIds
select jobId into #JobIds from tbljob tbj
WHERE (tbj.ClientId = @ClientId OR @ClientId is NULL) AND (tbj.VehicleId = @VehicleId OR @VehicleId is NULL)
AND (tbj.locationId = @locationId OR @locationId is NULL)
AND (tbj.jobdate between @StartDate and @EndDate or (@StartDate is NULL and @EndDate is Null))
AND ((tbj.ClientId = @ClientId OR @ClientId is NULL) AND (tbj.VehicleId = @VehicleId OR @VehicleId is NULL)
AND isnull(tbj.IsDeleted,0)=0)
ORDER BY tbj.JobDate DESC

OFFSET (0) ROWS 

FETCH NEXT (@PageSize) ROWS ONLY


Select 
tbj.JobId
,tbj.TicketNumber
,tbj.LocationId
,tbj.Barcode
,tbj.ClientId
,CONCAT(tblc.FirstName,' ',tblc.LastName) AS ClientName
,tbj.VehicleId
,ISNULL(tbj.Make,0) as Make
,ISNULL(tbj.Model,0) as Model
,ISNULL(tbj.Color,0) as Color
,model.ModelValue AS VehicleModel
,make.MakeValue As VehicleMake
,cvCo.valuedesc as VehicleColor
,tbj.JobType 
,tblcv.valuedesc as JobTypeName
,tbj.JobDate
,tbj.TimeIn
,tbj.EstimatedTimeOut
,tbj.ActualTimeOut
,tbj.JobStatus
,tbj.Notes as ReviewNote
,ISNULL(ps.valuedesc,'NotPaid') AS Paymentstatus
,Case
when (ps.valuedesc != 'Success'OR tbljp.PaymentStatus IS NULL) then 'False'
when ps.valuedesc = 'Success' then 'True'
End AS IsPaid

from #JobIds j
JOIN tblJob tbj on j.JobId = tbj.JobId
LEFT JOIN tblJobPayment tbljp  WITH(NOLOCK) ON tbj.JobPaymentId = tbljp.JobPaymentId AND tbljp.IsProcessed=1 AND ISNULL(tbljp.IsRollBack,0)=0 AND tbljp.IsActive = 1 AND ISNULL(tbljp.IsDeleted,0)=0 
LEFT JOIN GetTable('PaymentStatus') ps ON(tbljp.PaymentStatus = ps.valueid)
LEFT JOIN tblClientVehicle tblclv on tbj.VehicleId = tblclv.VehicleId 
LEFT JOIN tblClient tblc on tbj.ClientId = tblc.ClientId
LEFT JOIN GetTable('JobType') tblcv on tbj.JobType = tblcv.valueid
Left join tblVehicleMake make on tbj.Make=make.MakeId
Left join tblvehicleModel model on tbj.Model= model.ModelId
LEFT JOIN GetTable('VehicleColor') cvCo ON tbj.Color = cvCo.valueid

ORDER BY JobDate DESC

OFFSET (0) ROWS 

FETCH NEXT (@PageSize) ROWS ONLY


Select 
JobItemId,
j.JobId,
tblji.ServiceId,
s.ServiceName,
cv.CodeValue as ServiceType,
s.ServiceType as ServiceTypeId,
Commission,
tblji.Price,
Quantity,
ReviewNote,
tblji.IsActive,
tblji.IsDeleted,
tblji.CreatedBy,
tblji.CreatedDate
from #JobIds j
JOIN tblJobItem tblji on j.JobId = tblji.JobId
INNER JOIN tblService s on s.ServiceId = tblji.ServiceId
INNER JOIN tblCodeValue cv on cv.id = s.ServiceType
WHERE isnull(tblji.IsDeleted,0)=0 

END