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
-- 10-04-2020, Zahir   - Added location Email address table

-- 16-06-2021, Shalini - Removed wildcard from Query

-- 20-06-2021, Zahir - Query Optimization, Removed Email and Address2 from Search condition
------------------------------------------------
----------------------------------------------------------------
-- =============================================================
CREATE PROCEDURE [StriveCarSalon].[uspGetAllLocation]
(@LocationSearch varchar(50)=null)
AS 
BEGIN
SELECT tbll.LocationId,
	   tblla.LocationAddressId,	
	   tbll.WashTimeMinutes,
	   --tbll.LocationType as LocationTypeId,
	   --tblcv.valuedesc as LocationTypeName,	
	   tbll.LocationName,
	   --CONVERT(VARCHAR(5),tbll.StartTime,108) AS StartTime,
	   --CONVERT(VARCHAR(5),tbll.EndTime,108) AS EndTime,
	   tblla.Latitude,
	   tblla.Longitude,
	   tblla.PhoneNumber,
	   STUFF((SELECT Distinct ', ' + le.EmailAddress  
    FROM [tblLocationEmail] le
	WHERE le.LocationId = tbll.LocationId and IsDeleted = 0
    FOR XML PATH('')
	), 1, 2, '')  AS Email,
	   --tblla.Email,
	   tbll.WorkhourThreshold,
	   tbll.IsFranchise,
	   tblla.Address1,
	   tblla.Address2,
       tbllo.OffSet1,
       tbllo.OffSet1On,
       tbllo.OffSetA,
       tbllo.OffSetB,
       tbllo.OffSetB,
       tbllo.OffSetC,
       tbllo.OffSetD,
       tbllo.OffSetE,
       tbllo.OffSetF,
	   tblc.valuedesc as City,
	   tblco.valuedesc as Country,
	   tblla.Zip,
	   tbls.valuedesc as State,	 
	   tbll.IsActive,
	  tbll.IsDeleted	   
FROM [tblLocation] tbll (NOLOCK)
INNER JOIN [tblLocationAddress] tblla (NOLOCK) ON(tbll.LocationId = tblla.LocationId)
LEFT JOIN [tblLocationOffset] tbllo (NOLOCK) ON (tbll.LocationId =tbllo.LocationId)
LEFT JOIN GetTable('City') tblc ON(tblla.city = tblc.valueid)
LEFT JOIN GetTable('State') tbls ON(tblla.State = tbls.valueid)
LEFT JOIN GetTable('Country') tblco ON(tblla.Country = tblco.valueid)


WHERE tbll.IsDeleted =0 and
 tbll.IsActive = 1 
AND
 (@LocationSearch is null or tbll.LocationName like @LocationSearch+'%'
 or tblla.Address1 like @LocationSearch+'%' 
 --or tblla.Address2 like @LocationSearch+'%'
 or tblla.PhoneNumber like @LocationSearch+'%'
 --or tblla.Email like @LocationSearch+'%'
 )
 Order By tbll.LocationId desc
END

