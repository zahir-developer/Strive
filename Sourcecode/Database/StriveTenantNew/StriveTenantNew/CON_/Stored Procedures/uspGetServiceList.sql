


-- =============================================
-- Author:		Vineeth.B
-- Create date: 25-08-2020
-- Description:	To get service list by serviceid
-- =============================================
CREATE PROCEDURE [CON].[uspGetServiceList]
AS 
BEGIN
SELECT 
tbls.ServiceId,
tbls.ServiceName,
tbls.ServiceType,
gt.valuedesc AS ServiceTypeName
FROM 
[CON].[tblService] tbls
inner join [CON].GetTable('ServiceType') gt 
ON(tbls.ServiceType = gt.valueid)
where isnull(tbls.IsDeleted,0)=0
AND isnull(tbls.IsActive,1)=1
order by tbls.ServiceType,tbls.ServiceId
END