


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
------------------------------------------------
-- =============================================

CREATE proc [StriveCarSalon].[uspGetClientAndVehicleDetail]
(@BarCode varchar(50))
AS
BEGIN
select tblc.ClientId,
tblc.FirstName,
tblc.MiddleName,
tblc.LastName,
tblc.Gender,
tblc.BirthDate,
tblcv.VehicleId,
tblcv.VehicleNumber,
tblcv.VehicleMfr,
tblcv.VehicleModel as VehicleModelId,
gt.valuedesc as VehicleModel,
tblcv.VehicleColor,
tblcv.VehicleModelNo,
tblcv.VehicleYear
 from [tblClient] tblc 
 inner join [tblClientVehicle] tblcv on(tblc.ClientId = tblcv.ClientId) 
 inner join GetTable('VehicleModel') gt on(tblcv.VehicleModel = gt.valueid)
 AND
 tblcv.Barcode=@BarCode
 AND
 ISNULL(tblc.IsDeleted,0)=0
 AND
 ISNULL(tblcv.IsDeleted,0)=0
 AND
 tblc.IsActive=1
 AND
 tblcv.IsActive=1
END
