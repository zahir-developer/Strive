CREATE PROCEDURE [StriveCarSalon].[uspGetAllLocation]
(@LocationSearch varchar(50)=null)

AS 
BEGIN
SELECT tbll.LocationId,
	   tblla.LocationAddressId,	
	   tbll.WashTimeMinutes,
	   tbll.LocationType as LocationTypeId,
	   tblcv.valuedesc as LocationTypeName,	
	   tbll.LocationName,
	   tblla.PhoneNumber,
	   tblla.Email,
	   tbll.WorkhourThreshold,
	   tbll.IsFranchise,
	   tblla.Address1,
	   tblla.Address2,
	   isnull(tbll.IsActive,1) AS IsActive  	   
FROM [StriveCarSalon].[tblLocation] tbll 
LEFT JOIN [StriveCarSalon].[tblLocationAddress] tblla ON(tbll.LocationId = tblla.LocationId)
LEFT JOIN [StriveCarSalon].GetTable('LocationType') tblcv ON(tbll.LocationType = tblcv.valueid)

WHERE
isnull(tbll.IsDeleted,0)=0 and isnull(tblla.IsDeleted,0)=0 
AND
 (@LocationSearch is null or tbll.LocationName like '%'+@LocationSearch+'%'
 or tblla.Address1 like '%'+@LocationSearch+'%' or tblla.Address2 like '%'+@LocationSearch+'%'
 or tblla.PhoneNumber like '%'+@LocationSearch+'%'
 or tblla.Email like '%'+@LocationSearch+'%')
 Order By LocationId desc
END