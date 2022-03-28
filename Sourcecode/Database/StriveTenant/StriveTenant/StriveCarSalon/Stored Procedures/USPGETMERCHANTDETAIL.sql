
CREATE PROCEDURE [StriveCarSalon].[USPGETMERCHANTDETAIL]
@tblLocationId int,
@userName VARCHAR(150),
@password VARCHAR(15)
AS
BEGIN
	select Top 1
	MerchantDetailId,
	tblm.UserName,
	tblm.MID,
	tblm.Password,
	tblm.LocationId
	from tblMerchantDetail tblm
	where tblm.LocationId = @tblLocationId
	AND tblm.UserName = @userName
	AND tblm.Password = @password
	AND isnull(IsActive,1) = 1 AND isnull(isDeleted,0) = 0
END