



-- =============================================
-- Author:		Vineeth B
-- Create date: 10-09-2020
-- Description:	To get Wash Time by LocationId
-- =============================================
-----------------------------------------------------------------------------------------
-- Rev | Date Modified  | Developer	    | Change Summary
-----------------------------------------------------------------------------------------
--  1  |  2020-Sep-23   | Vineeth		| Added notes not null or empty condition
--  2  |  2020-Sep-30   | Vineeth       | Added ticket number 
-----------------------------------------------------------------------------------------
CREATE PROC [StriveCarSalon].[uspGetPastClientNotesByClientId] 
(@ClientId int)
AS
BEGIN
SELECT 
tblj.VehicleId,
tblj.TicketNumber,
tblcv.VehicleNumber,
cvMfr.valuedesc AS VehicleMake,
cvMo.valuedesc AS VehicleModel,
cvCo.valuedesc AS VehicleColor,
tblj.Notes
FROM tblJob tblj 
INNER JOIN tblClientVehicle tblcv ON(tblj.VehicleId = tblcv.VehicleId) 
INNER JOIN strivecarsalon.GetTable('VehicleManufacturer') cvMfr ON tblcv.VehicleMfr = cvMfr.valueid
INNER JOIN strivecarsalon.GetTable('VehicleModel') cvMo ON tblcv.VehicleModel = cvMo.valueid
INNER JOIN strivecarsalon.GetTable('VehicleColor') cvCo ON tblcv.VehicleColor = cvCo.valueid
WHERE tblj.ClientId=@ClientId
AND 
(
tblj.Notes IS NOT NULL and tblj.Notes not like '')
AND
ISNULL(tblj.IsDeleted,0) = 0
AND
ISNULL(tblcv.IsDeleted,0) = 0
AND
tblj.IsActive=1
AND
tblcv.IsActive=1
END
