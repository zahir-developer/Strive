CREATE procedure [StriveCarSalon].[uspGetAllServiceDetail]
AS
BEGIN
SELECT 
tbls.ServiceId,
tbls.ServiceName,
tbls.Cost as Price,
gt.id as ServiceTypeId,
gt.valuedesc AS ServiceType
FROM [StriveCarSalon].[tblService] tbls inner join [StriveCarSalon].GetTable('ServiceType') gt on (tbls.ServiceType=gt.valueid)
WHERE ISNULL(tbls.IsDeleted,0)=0
AND
tbls.IsActive=1 
END