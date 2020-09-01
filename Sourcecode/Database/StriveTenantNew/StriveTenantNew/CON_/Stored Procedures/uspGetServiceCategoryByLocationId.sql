
-- =============================================================
-- Author:              Vineeth.B
-- Created date:        2020-08-20
-- LastModified date: 
-- LastModified Author: 
-- Description:         To get All Service category by LocationId
-- ==============================================================


create procedure [CON].[uspGetServiceCategoryByLocationId]
(@LocationId int)
AS
BEGIN
SELECT 
tbls.ServiceId,
tbls.ServiceName,
gt.valuedesc AS CodeValue 
FROM [CON].[tblService] tbls inner join [CON].GetTable('ServiceType') gt on (tbls.ServiceType=gt.valueid)
AND
tbls.LocationId=@LocationId
AND
ISNULL(tbls.IsDeleted,0)=0
AND
tbls.IsActive=1
END