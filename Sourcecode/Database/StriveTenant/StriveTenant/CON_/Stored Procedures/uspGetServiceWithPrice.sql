
CREATE PROCEDURE [CON].[uspGetServiceWithPrice]

AS 
BEGIN
SELECT 
tblji.ServiceId,
tblji.Price,
tbls.ServiceName
FROM [CON].[tblJobItem] tblji inner join [CON].[tblService] tbls
ON(tblji.ServiceId = tbls.ServiceId)
END
