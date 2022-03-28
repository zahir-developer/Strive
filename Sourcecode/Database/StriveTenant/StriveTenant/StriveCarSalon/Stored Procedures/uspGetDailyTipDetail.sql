CREATE PROCEDURE [StriveCarSalon].[uspGetDailyTipDetail] 
(@Date Date = null, @locationId int = null)
AS

-- =============================================
-- Author:		Arunkumae S
-- Create date: 09-11-2020
-- Description:	Gets the Daily tips detail 
-- =============================================
-------------------- History -------------------
-- =============================================

--25-08-2021 - Zahir - Changed Hour function to Minute.

BEGIN
 
;WITH Hours_Data
AS (
select 
	concat(E.FirstName,' ',E.LastName) EmployeeName,
	TC.EventDate ,
	CAST(CONVERT(decimal(9,2), DateDiff(MINUTE,InTime,OutTime))/60 as decimal(9,2)) LoginTime
from tblTimeClock TC 
inner join tblEmployee E on TC.EmployeeId = E.EmployeeId
where LocationId = @LocationId and EventDate = @Date And Tc.Isdeleted=0)

SELECT 
	EmployeeName,EventDate,SUM(ISNULL(Logintime,0)) AS HoursPerDay --(SUM(ISNULL(Logintime,0))/60) AS LoginHours 
FROM Hours_Data
Group by EmployeeName,EventDate

END