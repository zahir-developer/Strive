-- =============================================  
-- Author:  JUKI B  
-- Create date: 31-03-2022  
-- Description: To fetch printer detail by location
-- =============================================  
---------------------History--------------------  
-- =============================================  
-- 31-03-2022, Juki - Added  
------------------------------------------------  
CREATE PROCEDURE uspGetPrinterByLocation 
(@Location int)
AS
BEGIN
Select PrinterId, PrinterName, IpAddress, LocationId from tblPrinter
where LocationId = IIF(@Location IS NULL, LocationId, @Location) 
END