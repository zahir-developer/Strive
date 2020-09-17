CREATE PROCEDURE [StriveCarSalon].[uspGetServiceByItemList]
AS 
BEGIN
SELECT 
tbls.ServiceId,
tbls.ServiceName,
tbls.ServiceType,
tbls.Upcharges,
gt.valuedesc AS ServiceTypeName,
tblji.Price,
tblji.Quantity
FROM 
[StriveCarSalon].[tblService] tbls
inner join [StriveCarSalon].[tblJobItem] tblji ON tbls.ServiceId = tblji.ServiceId
inner join [StriveCarSalon].GetTable('ServiceType') gt 
ON(tbls.ServiceType = gt.valueid)
where isnull(tbls.IsDeleted,0)=0
AND isnull(tbls.IsActive,1)=1
order by tbls.ServiceType,tbls.ServiceId
END
