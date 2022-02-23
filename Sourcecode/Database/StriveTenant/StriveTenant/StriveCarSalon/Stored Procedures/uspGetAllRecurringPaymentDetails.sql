 --select getdate(),convert(varchar(5),getdate(),110)
 --ALTER TABLE tblClientVehicleMembershipDetails 
 --ADD LastPaymentDate DATE
CREATE PROCEDURE [StriveCarSalon].[uspGetAllRecurringPaymentDetails]
@LocationId INT,
@FailedAttempts INT,
@date date
AS
BEGIN
	SELECT   cvmd.ExpiryDate,
				cvmd.ProfileId,
				cvmd.AccountId
	FROM	tblClientVehicleMembershipDetails cvmd
	JOIN	tblClientVehicle cv ON cv.VehicleId = cvmd.ClientVehicleId
	JOIN	tblClient c ON c.ClientId = cv.ClientId
	WHERE	ISNULL(cvmd.FailedAttempts,0) = 0
			AND ProfileId is not null and AccountId is not null
			AND cvmd.EndDate IS NULL
			AND c.LocationId = @LocationId
	
END