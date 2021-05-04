CREATE procedure [StriveCarSalon].[uspGetAllServiceDetail]
@locationId int=null
AS
BEGIN
SELECT 
tbls.ServiceId,
tbls.DiscountType AS DiscountType,
tbls.DiscountServiceType AS DiscountServiceType,
tbls.ServiceName,
tbls.Upcharges,
tbls.Cost, 
tbls.Price,
tbls.LocationId,
gt.valueid as ServiceTypeId,
gt.valuedesc AS ServiceTypeName 
FROM [StriveCarSalon].[tblService] tbls 
inner join [StriveCarSalon].GetTable('ServiceType') gt on (tbls.ServiceType=gt.valueid)
WHERE ISNULL(tbls.IsDeleted,0)=0 
--and (tbls.LocationId =@locationId or @locationId is NULL)
AND
tbls.IsActive=1 
END