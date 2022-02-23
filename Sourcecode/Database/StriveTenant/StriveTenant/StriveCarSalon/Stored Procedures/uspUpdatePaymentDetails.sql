
CREATE PROCEDURE [StriveCarSalon].[uspUpdatePaymentDetails]
@date DATETIME,
@attempts INT,
@ClientMembershipId INT
AS
BEGIN
	UPDATE tblClientVehicleMembershipDetails
	SET LastPaymentDate = @date,
	FailedAttempts = @attempts
	WHERE ClientMembershipId = @ClientMembershipId
END