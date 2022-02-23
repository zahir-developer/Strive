
CREATE PROCEDURE [StriveCarSalon].[uspGetMerchantDetails] 
@LocationId INT = NULL,
@IsRecurring BIT = NULL
AS
BEGIN
	SELECT   MD.MerchantDetailId,
			MD.LocationId,
			MD.UserName,
			MD.MID,
			MD.Password,
			MD.URL
	FROM  tblMerchantDetail MD
	JOIN tblLocation l ON l.LocationId = MD.LocationId

	where (MD.LocationId = @LocationId OR @LocationId = 0) AND 
	ISNULL(MD.IsActive,0) = 1 AND ISNULL(MD.isDeleted,0) = 0 AND 
	ISNULL(l.IsActive,0) = 1 AND ISNULL(l.isDeleted,0) = 0 
	AND ISNULL(IsRecurring,0) = @IsRecurring
END