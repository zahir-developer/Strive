
CREATE PROCEDURE [StriveCarSalon].[uspGetAllServiceDetail]
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
tbls.IsCeramic,
tbls.Price,
tbls.LocationId,
tbls.EstimatedTime,
tbls.[Description],
gt.id as ServiceTypeId,
gt.CodeValue AS ServiceTypeName 
FROM [tblService] tbls 
inner join tblCodeValue gt on (tbls.ServiceType=gt.id)
WHERE ISNULL(tbls.IsDeleted,0)=0 and ISNULL(tbls.IsActive,1)=1 
--and (tbls.LocationId =@locationId or @locationId is NULL)
AND
tbls.IsActive=1 
ORDER BY tbls.ServiceName ASC
END