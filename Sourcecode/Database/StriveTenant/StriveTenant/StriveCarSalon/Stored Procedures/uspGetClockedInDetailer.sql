
-- =============================================
-- Author:		shalini
-- Create date: 07-feb-2021
-- Description: To get clocked in detailer
-- =============================================

--[StriveCarSalon].[uspGetClockedInDetailer] '03-25-2021 02:55:56 ',20
CREATE PROCEDURE [StriveCarSalon].[uspGetClockedInDetailer] 
@Datetime DATETIME = NULL, @LocationId int = NULL
AS

BEGIN
declare @RoleId int =(select RoleMasterId from tblRoleMaster where RoleName='Detailer')
Select distinct tc.InTime,e.EmployeeId, e.FirstName, e.LastName,tc.LocationId
FROM tblTimeClock tc
Inner JOIN tblEmployee e on e.EmployeeId = tc.EmployeeId 
inner join tblemployeerole er on er.EmployeeId =tc.EmployeeId
WHERE ( (Cast(tc.InTime AS datetime) >= @DateTime ))--or tc.OutTime is NULL)
 and er.RoleId =@RoleId and (tc.LocationId= @LocationId OR @LocationId IS NULL)
AND ISNULL(tc.IsDeleted,0) = 0 AND tc.IsActive = 1
AND ISNULL(e.IsDeleted,0) = 0 AND e.IsActive = 1
END