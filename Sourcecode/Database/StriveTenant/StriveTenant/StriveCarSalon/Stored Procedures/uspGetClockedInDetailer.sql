﻿

CREATE PROCEDURE [StriveCarSalon].[uspGetClockedInDetailer] --[StriveCarSalon].[uspGetClockedInDetailer] '2021-03-11',1
@Datetime DATETIME = NULL, @LocationId int = NULL
AS

BEGIN
declare @RoleId int =(select RoleMasterId from tblRoleMaster where RoleName='Detailer')
Select distinct e.EmployeeId, e.FirstName, e.LastName,tc.LocationId
FROM StriveCarSalon.tblTimeClock tc
Inner JOIN StriveCarSalon.tblEmployee e on e.EmployeeId = tc.EmployeeId 
inner join tblemployeerole er on er.EmployeeId =tc.EmployeeId
WHERE ( tc.InTime <= @DateTime OR tc.OutTime is NULL)
 and er.RoleId =@RoleId and (tc.LocationId= @LocationId OR @LocationId IS NULL)
AND ISNULL(tc.IsDeleted,0) = 0 AND tc.IsActive = 1
AND ISNULL(e.IsDeleted,0) = 0 AND e.IsActive = 1
END