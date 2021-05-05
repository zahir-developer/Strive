
CREATE  PROCEDURE [StriveCarSalon].[uspGetPastDetailsByClientId] 
@ClientId INT 

-- ===============================================================
-- Author:		Vineeth
-- Create date: 01-10-2020
-- Description:	Retrieve Past 3 visits of each vehicle by ClientId
-- ===============================================================
AS

BEGIN

WITH cte_past_3_services_eachvehicle AS(
SELECT 
tblj.JobId,
tblj.VehicleId,
Tblj.jobDate,
ROW_NUMBER () OVER( PARTITION BY tblj.VehicleId ORDER BY Tblj.JobId DESC) Ranking
FROM 
	[tblClientVehicle] tblcv 
INNER JOIN 
	[tblJob] tblj 
ON tblcv.VehicleId = tblj.VehicleId
WHERE tblj.ClientId = @ClientId
AND tblj.IsActive = 1
AND tblcv.IsActive = 1
AND ISNULL(tblj.IsDeleted,0) = 0
AND ISNULL(tblcv.IsDeleted,0) = 0
)


SELECT 
	  tblj.JobId
	, cvl.VehicleId
	, cvl.Barcode
	, cvMfr.valuedesc AS Make
	, cvMo.valuedesc AS Model
	, cvCo.valuedesc AS Color
	, tblj.JobDate AS DetailVisitDate
	, tbls.ServiceName 
	, st.valuedesc AS DetailOrAdditionalService
	, tbls.Cost 
	, jt.valuedesc AS WashOrDetailJobType
FROM [tblClientVehicle] cvl
LEFT JOIN GetTable('VehicleManufacturer') cvMfr ON cvl.VehicleMfr = cvMfr.valueid
LEFT JOIN GetTable('VehicleModel') cvMo ON cvl.VehicleModel = cvMo.valueid
LEFT JOIN GetTable('VehicleColor') cvCo ON cvl.VehicleColor = cvCo.valueid
LEFT JOIN tblJob tblj ON tblj.VehicleId = cvl.VehicleId AND tblj.IsActive = 1 AND ISNULL(tblj.IsDeleted,0) = 0 
LEFT JOIN cte_past_3_services_eachvehicle  ON tblj.JobId = cte_past_3_services_eachvehicle.JobId
LEFT JOIN tblJobItem tblji ON cte_past_3_services_eachvehicle.JobId = tblji.JobId AND tblji.IsActive = 1 AND ISNULL(tblji.IsDeleted,0) = 0 
LEFT JOIN tblService tbls ON tblji.ServiceId = tbls.ServiceId  AND tbls.IsActive = 1 AND ISNULL(tbls.IsDeleted,0) = 0
LEFT JOIN GetTable('ServiceType') st ON st.valueid = tbls.ServiceType
LEFT JOIN GetTable('JobType') jt ON jt.valueid = tblj.JobType
WHERE 
	cte_past_3_services_eachvehicle.Ranking<=3 
AND	st.valuedesc IN('Details','Additional Services') 
AND jt.valuedesc IN('Detail') 
AND cvl.IsActive = 1
AND ISNULL(cvl.IsDeleted,0) = 0 
AND tblj.JobDate IS NOT NULL
ORDER BY cvl.VehicleId , tblj.JobId DESC

END