-- =============================================
-- Author:		Shalini
-- Create date: 07-feb-2021
-- Description: To get clocked in detailer
-- =============================================
-- 04-Jul-2021 - Zahir - Employee Email address added.
-- 25-Jul-2021 - Zahir - Resulting roles other than detailer issue fixed. 
-- 25-Aug-2021 - Vetriselvi - Only detailer details should return
-- =============================================
CREATE PROCEDURE [StriveCarSalon].[uspGetClockedInDetailer] 
@Datetime DATETIME = NULL, @LocationId int = NULL
AS

BEGIN

--declare @Datetime DATETIME = '2021-08-24', @LocationId int = 1
declare @RoleId int =(select RoleMasterId from tblRoleMaster where RoleName='Detailer')
Select distinct e.EmployeeId, e.FirstName, e.LastName, tc.LocationId, ea.Email,tc.EventDate
FROM tblTimeClock tc
Inner JOIN tblEmployee e on e.EmployeeId = tc.EmployeeId 
inner join tblemployeerole er on er.EmployeeId =tc.EmployeeId
LEFT join tblEmployeeAddress ea on ea.EmployeeId = e.EmployeeId and ea.Email != ''
WHERE (Cast(tc.InTime AS datetime) <= @Datetime and (Cast(tc.OutTime AS datetime) >= @Datetime OR tc.OutTime IS NULL)) and
tc.RoleId = @RoleId and
(tc.LocationId= @LocationId OR @LocationId IS NULL) and
CONVERT(VARCHAR(10), @DateTime, 111) = EventDate and
ISNULL(tc.IsDeleted,0) = 0 AND tc.IsActive = 1 and
ISNULL(e.IsDeleted,0) = 0 AND e.IsActive = 1
AND ER.RoleId = @RoleId
END