-- =================================================================
-- Author:              Vineeth.B
-- Created date:        2020-08-20
-- LastModified date: 
-- LastModified Author: 
-- Description:         To get Client and Vehicle details by barcode
-- =================================================================

---------------------History--------------------
-- =============================================
-- 28-08-2020, Vineeth - Add vehicle model id
-- 24-05-2021, shalini added where Condition
------------------------------------------------
--[StriveCarSalon].[uspGetClientAndVehicleDetail] 230920213
-- =============================================

CREATE PROCEDURE [StriveCarSalon].[uspGetClientAndVehicleDetail] 
(@BarCode varchar(50))
AS
BEGIN
select tblc.ClientId,
ISNULL(tblc.FirstName, 'Drive') FirstName,
tblc.MiddleName,
ISNULL(tblc.LastName, 'Up') LastName,
tblc.Gender,
tblc.BirthDate,
tblcv.VehicleId,
tblcv.VehicleNumber,
tblcv.VehicleMfr,
tblcv.VehicleModel as VehicleModelId,
vmo.ModelValue as VehicleModel,
tblcv.VehicleColor,
tblcv.VehicleModelNo,
tblcv.VehicleYear,
Trim(tblcv.Barcode),
ca.Email
from tblClientVehicle tblcv
LEFT join [tblClient] tblc on(tblc.ClientId = tblcv.ClientId) and ISNULL(tblc.IsDeleted,0)=0
left join tblClientAddress ca on tblc.clientId = ca.clientId
LEFT JOIN tblVehicleModel vmo ON(tblcv.VehicleModel = vmo.ModelId) and tblcv.VehicleMfr = vmo.MakeId
--inner join GetTable('VehicleModel') gt on(tblcv.VehicleModel = gt.valueid)
where
ISNULL(tblcv.IsDeleted,0)=0
AND
tblcv.IsActive=1
AND tblcv.barcode = @barcode
END
