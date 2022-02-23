CREATE    PROCEDURE [StriveCarSalon].[usptblTimeClockInsert]  
AS  
  
BEGIN  
--INSERT Records In to tblTimeClock  
--Truncate Table CON.tblTimeClock  
  
INSERT INTO StriveCarSalon.tblTimeClock(EmployeeId,LocationId,RoleId,EventDate,InTime,OutTime,[Status],[ClockId],  
        IsActive,IsDeleted,CreatedBy,CreatedDate,UpdatedBy,UpdatedDate)  
SELECT Distinct   
 Emp.employeeId,  
 Tim.LocationID,  
 Rm.RoleMasterId AS RoleId,  
 Tim.Cdate  AS EventDate,  
 Tim.Cdatetime AS InTime,  
 Timo.Cdatetime AS OutTime,  
 1    AS [Status],  
 Tim.ClockID  AS ClockId,  
 1    AS IsActive,  
 0    AS IsDeleted,  
 Emp.EmployeeId AS CreatedBy,  
 Tim.editdate AS CreatedDate,  
 1    AS UpdatedBy,  
 GetUTCdate() AS UpdatedDate  
 FROM   
 StgTimeclock_in Tim  
JOIN   
 tblEmployee emp  
ON  emp.EmployeeId=tim.UserID  
JOIN   
 tblEmployeeLocation empL  
ON  empl.EmployeeId=emp.employeeid  
AND  tim.LocationID=empl.LocationId  
JOIN   
 tblRoleMaster RM  
ON  Rm.RoleAlias=Tim.CType  
LEFT JOIN   
 StgTimeClock_Out Timo  
ON  timo.UserID=tim.UserID  
AND  timo.LocationID=tim.LocationID  
AND  timo.Cdate= tim.Cdate  
AND  timo.CType=tim.ctype  
AND  timo.Rnk=tim.RNk  
LEFT JOIN   
 TblTimeClock TCl  
ON  Tcl.clockId=Tim.ClockId  
AND  Tcl.LocationId=Tim.LocationId  
WHERE Tcl.ClockId IS NULL AND Tcl.LocationId IS NULL  
  
  
END