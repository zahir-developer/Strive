--[StriveCarSalon].[uspGetEmployeeRoleById] 144
CREATE PROCEDURE [StriveCarSalon].[uspGetEmployeeRoleById] 
(@EmployeeId int)
AS
BEGIN

  select Distinct rm.RoleMasterId
  , empr.EmployeeId
  ,empr.EmployeeRoleId
  , empr.roleid
  ,rm.RoleName as rolename
   from tblEmployeeRole empr
    inner join   tblRoleMaster rm on empr.RoleId = rm.RoleMasterId 
   where empr.EmployeeId=@EmployeeId  and isnull(empr.IsDeleted,0)=0

END