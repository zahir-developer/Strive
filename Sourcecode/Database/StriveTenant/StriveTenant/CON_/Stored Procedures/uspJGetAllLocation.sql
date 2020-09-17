


CREATE PROCEDURE [CON].[uspJGetAllLocation]

AS 
BEGIN
SELECT tbll.LocationId,
	   tblla.LocationAddressId,	
	   tbll.LocationType as LocationTypeId,
	   tblcv.valuedesc as LocationTypeName,	
	   tbll.LocationName,
	   tblla.PhoneNumber,
	   tblla.Email,
	   isnull(tbll.IsActive,1) AS IsActive  	   
FROM [CON].[tblLocation] tbll 
LEFT JOIN [CON].[tblLocationAddress] tblla ON(tbll.LocationId = tblla.LocationId)
LEFT JOIN [CON].GetTable('LocationType') tblcv ON(tbll.LocationType = tblcv.valueid)

WHERE
isnull(tbll.IsDeleted,0)=0 and isnull(tblla.IsDeleted,0)=0 
END

