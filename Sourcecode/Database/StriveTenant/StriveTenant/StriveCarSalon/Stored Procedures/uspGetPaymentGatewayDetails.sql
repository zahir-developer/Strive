

CREATE PROCEDURE [StriveCarSalon].[uspGetPaymentGatewayDetails]
AS
BEGIN
	SELECT PaymentGatewayId,
	PaymentGatewayName,
	BaseURL,
	APIKey
	FROM tblPaymentGateway
	WHERE IsActive = 1 AND
	IsDeleted = 0

END