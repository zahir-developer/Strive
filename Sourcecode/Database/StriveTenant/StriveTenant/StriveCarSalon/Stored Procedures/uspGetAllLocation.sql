



-- =============================================================
-- Author:         Vineeth.B
-- Created date:   2020-07-01
-- Description:    Get All Location with search param or without 
-- =============================================================

----------------------------History-----------------------------
-- =============================================================
-- 16-09-2020, Vineeth - Added IsActive condition in tblLocation
--                       and tblLocationAddress
-- 30-09-2020, Vineeth - Added Start Time and End Time and
--						 Latitude and Longitude
----------------------------------------------------------------
-- =============================================================
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
	   CONVERT(VARCHAR(5),tbll.StartTime,108) AS StartTime,
	   CONVERT(VARCHAR(5),tbll.EndTime,108) AS EndTime,
	   tblla.Latitude,
	   tblla.Longitude,
	   tblla.PhoneNumber,
	   tblla.Email,
	   tbll.WorkhourThreshold,
	   tbll.IsFranchise,
	   tblla.Address1,
	   tblla.Address2,
	   isnull(tbll.IsActive,1) AS IsActive,
	  tbll.IsDeleted	   
FROM [StriveCarSalon].[tblLocation] tbll 
LEFT JOIN [StriveCarSalon].[tblLocationAddress] tblla ON(tbll.LocationId = tblla.LocationId)
LEFT JOIN [StriveCarSalon].GetTable('LocationType') tblcv ON(tbll.LocationType = tblcv.valueid)

WHERE
isnull(tbll.IsDeleted,0)=0 and isnull(tblla.IsDeleted,0)=0 and tbll.IsActive = 1 and tblla.IsActive = 1
AND
 (@LocationSearch is null or tbll.LocationName like '%'+@LocationSearch+'%'
 or tblla.Address1 like '%'+@LocationSearch+'%' or tblla.Address2 like '%'+@LocationSearch+'%'
 or tblla.PhoneNumber like '%'+@LocationSearch+'%'
 or tblla.Email like '%'+@LocationSearch+'%')
 Order By tbll.LocationId desc
END

