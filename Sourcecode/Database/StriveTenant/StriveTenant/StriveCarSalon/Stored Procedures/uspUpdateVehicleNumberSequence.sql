-- =============================================
-- Author:		Zahir Hussain M
-- Create date: 08 Dec 2021
-- Description:	Update vehicle number sequence based on Client
-- [StriveCarSalon].[uspUpdateVehicleNumberSequence] 57398
-- =============================================
CREATE PROCEDURE [StriveCarSalon].[uspUpdateVehicleNumberSequence]
@ClientId INT, @VehicleId INT = NULL
AS
BEGIN

--DECLARE @ClientId INT = 57398, @VehicleId INT = 214279

IF @ClientId IS NULL OR @ClientId = 0
BEGIN 
Select TOP 1 @ClientId = ClientId from tblClientVehicle WHERE VehicleId = @VehicleId
END


IF @ClientId != 0 
BEGIN

DECLARE @VehicleNo INT = 1;

DECLARE Upd_Cur CURSOR FOR

Select top 50 VehicleId from tblClientVehicle where ClientId = @ClientId and IsDeleted = 0 and (VehicleId != @VehicleId OR @VehicleId IS NULL) order by VehicleId

OPEN Upd_Cur

FETCH next FROM Upd_cur into @VehicleId

WHILE @@FETCH_STATUS = 0

BEGIN

Update tblClientVehicle SET VehicleNumber = @VehicleNo WHERE VehicleId = @VehicleId

Print @VehicleId
Print @VehicleNo
SET @VehicleNo = @VehicleNo + 1;

FETCH next FROM Upd_Cur INTO @VehicleId

END

CLOSE Upd_Cur

DEALLOCATE Upd_Cur

END
END