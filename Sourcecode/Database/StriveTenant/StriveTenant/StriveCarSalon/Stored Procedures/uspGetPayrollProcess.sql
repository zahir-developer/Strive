--[StriveCarSalon].[uspGetPayrollProcess]'2021-01-22','2021-01-25',null,1
CREATE PROCEDURE [StriveCarSalon].[uspGetPayrollProcess] 
@FromDate date,
@ToDate date,
@employeeId int null,
@locationId int null
AS
BEGIN

SELECT 
tblp.PayrollProcessId,
tblp.FromDate,
tblp.ToDate,
tblp.LocationId,
tble.PayrollEmployeeId,
tble.EmployeeId 
FROM  tblPayrollProcess tblp
inner join tblPayrollemployee tble on tblp.PayrollProcessId =tble.PayRollProcessId
WHERE tblp.FromDate between @FromDate and @ToDate 
and tblp.ToDate between @FromDate and @ToDate  and (tble.EmployeeId =@employeeId or @EmployeeId is null)
and (tblp.LocationId =@locationId or @locationId is null)
AND tblp.IsActive = 1 AND ISNULL(tblp.IsDeleted,0)=0
END