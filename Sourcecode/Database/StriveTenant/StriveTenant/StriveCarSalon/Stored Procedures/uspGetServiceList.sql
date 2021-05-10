

-- =============================================
-- Author:		Vineeth.B
-- Create date: 25-08-2020
-- Description:	To get service list
-- =============================================

-- =============================================
-- 17-09-2020 - Zahir - Added Cost column.
-- =============================================

CREATE PROCEDURE [StriveCarSalon].[uspGetServiceList]
AS 
BEGIN
SELECT 
tbls.ServiceId,
tbls.ServiceName,
tbls.ServiceType,
tbls.Upcharges,
tbls.Cost as Price,
gt.valuedesc AS ServiceTypeName
FROM 
[tblService] tbls
inner join GetTable('ServiceType') gt 
ON(tbls.ServiceType = gt.valueid)
where isnull(tbls.IsDeleted,0)=0
AND isnull(tbls.IsActive,1)=1
order by tbls.ServiceType,tbls.ServiceId
END
