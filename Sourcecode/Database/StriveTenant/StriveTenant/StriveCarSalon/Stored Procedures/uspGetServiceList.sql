

-- =============================================
-- Author:		Vineeth.B
-- Create date: 25-08-2020
-- Description:	To get service list by serviceid
-- =============================================
CREATE PROCEDURE [StriveCarSalon].[uspGetServiceList]
AS 
BEGIN
SELECT 
tbls.ServiceId,
tbls.ServiceName,
tbls.ServiceType,
tbls.Upcharges,
gt.valuedesc AS ServiceTypeName
FROM 
[StriveCarSalon].[tblService] tbls
inner join [StriveCarSalon].GetTable('ServiceType') gt 
ON(tbls.ServiceType = gt.valueid)
where isnull(tbls.IsDeleted,0)=0
AND isnull(tbls.IsActive,1)=1
order by tbls.ServiceType,tbls.ServiceId
END
