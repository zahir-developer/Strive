-- =============================================
-- Author:		Zahir Hussain M
-- Create date: 18-Nov-2020
-- Description:	Returns the time clock details of the Employees based on location and date. Sample EXEC Strivecarsalon.uspGetTimeClockEmployeeHourDetail 2034, '2020-11-17'
--[StriveCarSalon].[uspGetTimeClockEmployeeHourDetail] 2,'2022-02-23','2022-02-23'
-- 2021-05-19 -shalini -round off wash hours to two decimal places
--  Vetriselvi 2021-09-29  - Fixed timeclock issue 
--  Vetriselvi 2021-09-29  - Reverted the changes done for LoginTime
--  Vetriselvi 2021-10-06  - Added new parameter to calculate the dynamic time
--  Vetriselvi 2021-10-12  - Addind time to hh mm format
--  Vetriselvi 2021-11-10  - Added total hours and hours should be in decimal format
--  Juki	   2022-02-23  - Added other hours with total hours.
--  Juki       2022-03-08  - Removed deleted employees record
-- =============================================
CREATE PROCEDURE [StriveCarSalon].[uspGetTimeClockEmployeeHourDetail]
	@locationId INT,
	@date datetime,
	@currentDate datetime = null
AS
BEGIN

DROP TABLE IF EXISTS #Hours_Data

select e.employeeId,
	E.FirstName,
	E.LastName,
	TC.EventDate,InTime,
CASE WHEN ISNULL(OutTime,'') = '' 
THEN 
	CASE WHEN (@currentDate <= InTime or @currentDate = null)
	THEN 0
	ELSE 
		CASE WHEN
			CAST(InTime AS DATE) = CAST(@currentDate AS DATE) 
			THEN DATEDIFF(MI, InTime, ISNULL(OutTime,@currentDate)) 
			ELSE 0
		END
	END 
ELSE DATEDIFF(MI, InTime, ISNULL(OutTime,InTime))
END as LoginTime,

	rm.RoleName INTO #Hours_Data
from tblTimeClock TC 
inner join tblEmployee E on TC.EmployeeId = E.EmployeeId
inner join tblRoleMaster rm on rm.RoleMasterId = tc.RoleId
where LocationId = @LocationId and EventDate = @Date and tc.InTime is Not NULL
AND ISNULL(TC.IsDeleted,0) = 0 AND tc.IsActive = 1 AND ISNULL(E.IsDeleted, 0) = 0


DROP TABLE IF EXISTS #FinalHours_Data
--select * from #Hours_Data
;WITH FinalResult AS (	
SELECT 
EmployeeId, FirstName, LastName,
CASE WHEN RoleName='Washer' THEN  LoginTime ELSE 0 END AS TotalWashHours,
CASE WHEN RoleName='Detailer' THEN  LoginTime ELSE 0 END AS TotalDetailHours,
CASE WHEN RoleName != 'Washer' AND RoleName != 'Detailer' THEN  LoginTime ELSE 0 END AS OtherHours
FROM #Hours_Data
)


select EmployeeId, FirstName, LastName,
SUM(TotalWashHours) TotalWashHours,
SUM(TotalDetailHours) AS TotalDetailHours,
SUM(OtherHours) AS OtherHours
 INTO #FinalHours_Data
 from FinalResult
 GROUP By EmployeeId, FirstName, LastName

Select DISTINCT EmployeeId, FirstName, LastName,CAST(TotalWashHours/60.00 AS DECIMAL(18,2))
--CAST(TotalWashHours/60 AS VARCHAR(10))+  CASE WHEN TotalWashHours%60 >=10 THEN CAST((TotalWashHours%60)/60.00 AS VARCHAR(10)) ELSE CAST(FORMAT(((TotalWashHours%60)/60.00), 'd2') AS VARCHAR(10)) END 
WashHours ,
--CAST(TotalDetailHours/60 AS VARCHAR(10))+ '.'+ CASE WHEN TotalDetailHours%60 >=10 THEN CAST(TotalDetailHours%60 AS VARCHAR(10)) ELSE CAST(FORMAT((TotalDetailHours%60), 'd2') AS VARCHAR(10)) END
CAST(TotalDetailHours/60.00 AS DECIMAL(18,2)) DetailHours,
--CAST(OtherHours/60 AS VARCHAR(10))+ '.'+ CASE WHEN OtherHours%60 >=10 THEN CAST(OtherHours%60 AS VARCHAR(10)) ELSE CAST(FORMAT((OtherHours%60), 'd2') AS VARCHAR(10)) END  
CAST(OtherHours/60.00 AS DECIMAL(18,2)) OtherHours,
--CAST((TotalWashHours+TotalDetailHours)/60 AS VARCHAR(10))+ '.'+ CASE WHEN (TotalWashHours+TotalDetailHours)%60 >=10 THEN CAST((TotalWashHours+TotalDetailHours)%60 AS VARCHAR(10)) ELSE CAST(FORMAT(((TotalWashHours+TotalDetailHours)%60), 'd2') AS VARCHAR(10)) END 
CAST((TotalWashHours+TotalDetailHours+OtherHours)/60.00 AS DECIMAL(18,2)) TotalHours 
 from #FinalHours_Data

SELECT TimeClockId, EmployeeId, RoleId, rm.RoleName, InTime, OutTime, CONVERT(VARCHAR(5), InTime, 108) as TimeIn, CONVERT(VARCHAR(5), OutTime, 108) as TimeOut,
tc.Status,tc.EventDate
,@LocationId as LocationId, l.LocationName
,Convert(datetimeoffset,'') as TotalHours,FORMAT(tc.EventDate, 'dddd') AS 'Day'
FROM tblTimeClock tc
INNER JOIN tblRoleMaster rm on rm.RoleMasterId = tc.RoleId
INNER JOIN tblLocation l on l.LocationId = tc.LocationId
WHERE tc.LocationId = @LocationId AND tc.EventDate = @Date AND (tc.IsDeleted = 0 OR tc.IsDeleted is null)
AND tc.IsActive = 1



END
