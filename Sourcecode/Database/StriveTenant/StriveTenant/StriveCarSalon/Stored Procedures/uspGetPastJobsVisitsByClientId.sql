﻿

CREATE PROC [StriveCarSalon].[uspGetPastJobsVisitsByClientId] 
@ClientId INT--=76
-- ===============================================================
-- Author:		Vineeth
-- Create date: 01-10-2020
-- Description:	Retrieve Past 3 visits of each vehicle by ClientId
-- ===============================================================
AS
BEGIN

DROP TABLE IF EXISTS #CLientVehicle

SELECT 
tblj.JobId,
cvl.VehicleId
,cvl.Barcode
,cvMfr.valuedesc AS Make
,cvMo.valuedesc AS Model
,cvCo.valuedesc AS Color
,tblj.JobDate AS DetailVisitDate
,tbls.ServiceName 
,st.valuedesc AS DetailOrAdditionalService
,tbls.Cost 
,jt.valuedesc AS WashOrDetailJobType
,DENSE_RANK ()  OVER( PArtition BY tblj.VehicleId  ORDER BY tblj.JobId,Tblj.Jobdate DESC ) Ranking
INTO #CLientVehicle
FROM [StriveCarSalon].[tblClientVehicle] cvl
INNER JOIN [StriveCarSalon].GetTable('VehicleManufacturer') cvMfr ON cvl.VehicleMfr = cvMfr.valueid
INNER JOIN [StriveCarSalon].GetTable('VehicleModel') cvMo ON cvl.VehicleModel = cvMo.valueid
INNER JOIN [StriveCarSalon].GetTable('VehicleColor') cvCo ON cvl.VehicleColor = cvCo.valueid
INNER JOIN [StriveCarSalon].tblJob tblj ON tblj.VehicleId = cvl.VehicleId
INNER JOIN [StriveCarSalon].tblJobItem tblji ON tblj.JobId = tblji.JobId
INNER JOIN [StriveCarSalon].tblService tbls ON tblji.ServiceId = tbls.ServiceId
INNER JOIN [StriveCarSalon].GetTable('ServiceType') st ON st.valueid = tbls.ServiceType
INNER JOIN [StriveCarSalon].GetTable('JobType') jt ON jt.valueid = tblj.JobType
WHERE st.valuedesc IN('Details','Additional Services') AND jt.valuedesc IN('Wash','Detail') 
AND cvl.IsActive = 1 AND tblj.IsActive = 1 and tblji.IsActive = 1 and tbls.IsActive = 1
AND ISNULL(cvl.IsDeleted,0) = 0 AND ISNULL(tblj.IsDeleted,0) = 0 and ISNULL(tblji.IsDeleted,0) = 0 and ISNULL(tbls.IsDeleted,0) = 0
AND tblj.JobDate IS NOT NULL
AND cvl.ClientId=@ClientId
ORDER BY tblj.JobId,cvl.VehicleId DESC

SELECT * FROM #CLientVehicle Where Ranking <4 
ORDER BY Ranking 

END