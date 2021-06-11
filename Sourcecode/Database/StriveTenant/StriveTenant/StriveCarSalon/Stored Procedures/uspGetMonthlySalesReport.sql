
 
CREATE procedure [StriveCarSalon].[uspGetMonthlySalesReport]
(@LocationId INT, @FromDate date, @EndDate date)

AS
-- =============================================
-- Author:		Vineeth B
-- Create date: 23-10-2020
-- Description:	To get Monthly sales report info
-- [StriveCarSalon].[uspGetMonthlySalesReport] 1,'2021-05-01','2021-05-31'
-- =============================================
BEGIN
DECLARE @RoleMasterId int = (select RoleMasterId from tblRoleMaster where RoleName='Manager')

select 
tblT.EmployeeId,
CONCAT(tblE.FirstName,' ',tblE.LastName) AS EmployeeName,
MIN(tblT.InTime) FirstLoginDate,
MAX(tblT.OutTime) LastLoginDate
FROM 
tblTimeClock tblT 
INNER JOIN tblEmployee tblE ON(tblT.EmployeeId = tblE.EmployeeId)
WHERE tblT.LocationId=@LocationId
AND tblT.RoleId = @RoleMasterId AND tblT.EventDate>=@FromDate
AND tblT.EventDate<=@EndDate
AND tblT.IsActive=1 AND tblE.IsActive = 1 AND ISNULL(tblT.IsDeleted,0)=0 AND ISNULL(tblE.IsDeleted,0)=0
Group By tblT.EmployeeId, tblE.FirstName, tblE.LastName

DROP TABLE IF EXISTS #monthlyreport 
SELECT DISTINCT tblji.JobId,
tblS.ServiceName AS Description,
tblS.Price,
ISNULL(tblJi.Quantity,0) AS ServiceNumber,
--ISNULL(tblJPi.Quantity,0) AS ProductNumber,
(ISNULL(tblJi.Quantity,0)) AS Number,
ISNULL((tblJi.Quantity * tblS.Price),0) AS Total--,
--tblJ.jobdate
--,tblTC.EmployeeId
INTO #monthlyreport FROM tblJob tblJ 
INNER JOIN tblJobItem tblJi on(tblJ.JobId = tblJi.JobId) 
--LEFT JOIN tblJobProductItem tblJPi on(tblJ.JobId = tblJPi.JobId) 
INNER JOIN tblService tblS on(tblJi.ServiceId = tblS.ServiceId) 
--INNER JOIN tblJobServiceEmployee tblJSE on(tblJi.JobItemId = tblJSE.JobItemId)
--INNER JOIN tblTimeClock tblTC on(tblJSE.EmployeeId = tblTC.EmployeeId)
WHERE tblJ.IsActive = 1
AND tblJi.IsActive = 1 
AND tblS.IsActive = 1
--AND tblJSE.IsActive = 1
--AND tblTC.IsActive = 1
AND ISNULL(tblJ.IsDeleted,0)=0 
AND ISNULL(tblJi.IsDeleted,0)=0 
AND ISNULL(tblS.IsDeleted,0)=0 
--AND ISNULL(tblJSE.IsDeleted,0)=0
--AND ISNULL(tblTC.IsDeleted,0)=0
AND tblj.LocationId = @LocationId
AND tblj.jobdate>=@FromDate
AND tblj.JobDate<=@EndDate


SELECT Description,Price,--jobdate,
SUM(Number) AS Number,SUM(Total)AS Total
FROM #monthlyreport where price is not null
Group By Description,Price--,jobdate 

END