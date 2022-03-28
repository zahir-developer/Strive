CREATE PROCEDURE [StriveCarSalon].[uspGetPastClientNotesByClientId] 
(@ClientId int)
AS
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
BEGIN
SELECT 
tblj.VehicleId,
tblj.TicketNumber,
tblj.Notes
FROM tblJob tblj 
INNER JOIN tblClientVehicle tblcv ON(tblj.VehicleId = tblcv.VehicleId) 
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
