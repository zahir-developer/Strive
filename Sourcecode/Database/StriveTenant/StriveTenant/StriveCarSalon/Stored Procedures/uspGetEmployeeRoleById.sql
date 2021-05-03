
CREATE PROCEDURE [StriveCarSalon].[uspGetEmployeeRoleById] --[StriveCarSalon].[uspGetEmployeeRoleById] 144
(@EmployeeId int)
AS
BEGIN

  select Distinct rm.RoleMasterId
  , empr.EmployeeId
  ,empr.EmployeeRoleId
  , empr.roleid
  ,rm.RoleName as rolename
   from strivecarsalon.tblEmployeeRole empr
    inner join   StriveCarSalon.tblRoleMaster rm on empr.RoleId = rm.RoleMasterId 
   where empr.EmployeeId=@EmployeeId  and isnull(empr.IsDeleted,0)=0

END