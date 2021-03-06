
CREATE PROCEDURE [StriveCarSalon].[uspGetLocation]

AS 
BEGIN
SELECT tbll.LocationId,
	   tbll.LocationType,
	   tbll.LocationName,
	   tbll.LocationDescription,
	   tbll.IsFranchise,
	   tbll.IsActive,
	   tbll.TaxRate,
	   tbll.SiteUrl,
	   tbll.Currency,
	   tbll.Facebook,
	   tbll.Twitter,
	   tbll.Instagram,
	   tbll.WifiDetail,
	   tbll.WorkhourThreshold,
	   tbll.WashTimeMinutes,
	   tblla.LocationAddressId			AS LocationAddress_LocationAddressId,
	   tblla.LocationId					AS LocationAddress_LocationId,
	   tblla.Address1					AS LocationAddress_Address1,
	   tblla.Address2					AS LocationAddress_Address2,
	   tblla.PhoneNumber				AS LocationAddress_PhoneNumber,
	   tblla.PhoneNumber2				AS LocationAddress_PhoneNumber2,
	   tblla.Email						AS LocationAddress_Email,
	   tblla.City						AS LocationAddress_City,
	   tblla.State						AS LocationAddress_State,
	   tblla.Zip						AS LocationAddress_Zip,
	   tblla.IsActive					AS LocationAddress_IsActive,
	   tblla.Country					AS LocationAddress_Country

FROM [StriveCarSalon].[tblLocation] tbll inner join [StriveCarSalon].[tblLocationAddress] tblla
		   ON(tbll.LocationId = tblla.LocationId)
		   AND tbll.IsDeleted=0 AND tblla.IsDeleted=0
END
