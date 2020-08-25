CREATE PROCEDURE [StriveCarSalon].[uspGetServiceWithPrice]

AS 
BEGIN
SELECT 
tblji.ServiceId,
tblji.Price,
tbls.ServiceName
FROM [StriveCarSalon].[tblJobItem] tblji inner join [StriveCarSalon].[tblService] tbls
ON(tblji.ServiceId = tbls.ServiceId)
END