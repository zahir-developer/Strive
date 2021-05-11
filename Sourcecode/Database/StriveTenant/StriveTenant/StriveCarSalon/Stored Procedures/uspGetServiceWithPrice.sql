CREATE PROCEDURE [StriveCarSalon].[uspGetServiceWithPrice]

AS 
BEGIN
SELECT 
tblji.ServiceId,
tblji.Price,
tbls.ServiceName
FROM [tblJobItem] tblji inner join [tblService] tbls
ON(tblji.ServiceId = tbls.ServiceId)
END
