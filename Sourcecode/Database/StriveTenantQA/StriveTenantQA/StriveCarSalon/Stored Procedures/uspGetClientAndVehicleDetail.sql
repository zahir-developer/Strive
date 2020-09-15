


-- =================================================================
-- Author:              Vineeth.B
-- Created date:        2020-08-20
-- LastModified date: 
-- LastModified Author: 
-- Description:         To get Client and Vehicle details by barcode
-- =================================================================



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
gt.valuedesc as VehicleModel,
tblcv.VehicleColor,
tblcv.VehicleModelNo,
tblcv.VehicleYear
 from [StriveCarSalon].[tblClient] tblc 
 inner join [StriveCarSalon].[tblClientVehicle] tblcv on(tblc.ClientId = tblcv.ClientId) 
 inner join [StriveCarSalon].GetTable('VehicleModel') gt on(tblcv.VehicleModel = gt.valueid)
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
