CREATE PROCEDURE [StriveCarSalon].[uspGetClientCardDetails]
(@ClientId int)
AS 
BEGIN
SELECT 
Id,
CardType,
CardNumber,
ExpiryDate,
ClientId,
VehicleId,
MembershipId,
IsActive,
IsDeleted
FROM
[StriveCarSalon].[tblClientCardDetails] WHERE ClientId=@ClientId AND ISNULL(IsDeleted,0)=0

END