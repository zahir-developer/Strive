
CREATE PROCEDURE  [StriveCarSalon].[uspGetEmployeeHourlyRateById] 
(@EmployeeId int)
AS
BEGIN  
   select 
    ehr.EmployeeId,
	emp.FirstName,
	emp.LastName,
    ehr.LocationId,
	lo.LocationName,
	ehr.RoleId,
	rm.RoleName,
	ehr.HourlyRate
    from strivecarsalon.tblEmployeeHourlyRate ehr
	inner join StriveCarSalon.tblLocation lo on ehr.locationid=lo.locationid 
    inner join StriveCarSalon.tblEmployee emp on  emp.EmployeeId =ehr.EmployeeId
    LEFT join StriveCarSalon.tblRoleMaster rm on rm.RoleMasterId=ehr.RoleId   
    where ehr.EmployeeId=@EmployeeId
    and isnull(ehr.IsDeleted,0)=0 and ehr.IsActive = 1

	--Select * from StriveCarSalon.tblEmployeeHourlyRate where employeeId = 1520

END