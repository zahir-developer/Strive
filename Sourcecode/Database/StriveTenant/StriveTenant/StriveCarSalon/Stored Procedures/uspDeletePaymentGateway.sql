

CREATE PROCEDURE [StriveCarSalon].[uspDeletePaymentGateway] 
(
@PaymentGatewayId int, @UserId int, @Date datetimeoffset, @IsDeleted BIT = 1
)
AS 
BEGIN


UPDATE [tblPaymentGateway] set
IsDeleted = @IsDeleted,
UpdatedBy=@UserId,
UpdatedDate = @Date
WHERE PaymentGatewayId = @PaymentGatewayId


END