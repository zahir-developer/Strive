
CREATE PROC  [StriveCarSalon].[uspGetEmployeeById] 
(@EmployeeId int)
AS
BEGIN

select 
emp.EmployeeId,
empDetail.EmployeeCode,
emp.firstname,
emp.LastName,
emp.Gender,
empAdd.EmployeeAddressId,
empAdd.Address1,
empAdd.PhoneNumber,
--add state city column
empAdd.State,
empAdd.City,
emp.ImmigrationStatus,
emp.AlienNo,
emp.WorkPermit,
emp.SSNo,
empAdd.Email,
empDetail.EmployeeDetailId,
empDetail.HiredDate,
empDetail.WashRate,
empDetail.ComRate,
empDetail.DetailRate,
empDetail.AuthId,
emp.IsActive as Status,
empDetail.Tip,
empDetail.Exemptions,
empDetail.ComType,
emp.Tips
from StriveCarSalon.tblEmployee emp
left join strivecarsalon.tblEmployeeAddress empAdd on emp.EmployeeId= empAdd.EmployeeId
left join StriveCarSalon.tblEmployeeDetail empDetail on emp.EmployeeId = empDetail.EmployeeId
where emp.EmployeeId = @EmployeeId 
--isnull(empAdd.IsActive,1)=1 and isnull(empDetail.IsDeleted,0)=0 and 



select row_number() OVER (
	ORDER BY  empdoc.EmployeeDocumentId
   ) DocumentSequence , 
    empdoc.EmployeeId,
   empdoc.EmployeeDocumentId, 
   empdoc.[FileName], 
   empdoc.CreatedDate, 
   empdoc.UpdatedDate, 
   empdoc.FilePath,
   empdoc.FileType from StriveCarSalon.tblEmployeeDocument empdoc where 
   isnull(empdoc.IsActive,1)=1 and isnull(empdoc.IsDeleted,0)=0 and empdoc.EmployeeId = @EmployeeId


select row_number() OVER (
	ORDER BY  empLi.LiabilityId
   ) CollisionSequence,
    empli.EmployeeId,
	empli.ClientId,
tblcl.FirstName + ' '+ tblcl.LastName as ClientName,
	empLi.VehicleId,
	cvMfr.valuedesc + ' '+	cvmo.valuedesc + ' '+ cvCo.valuedesc AS VehicleName,
  empLi.LiabilityId,
   empLiD.liabilityDetailId,
   lcv.valuedesc as LiabilityType,
   dcv.valuedesc as LiabilityDetailType,
   empLi.LiabilityDescription,
   empLiD.Amount,   
   empLiD.LiabilityDetailType as LiabilityDetailTypeId,
   empLi.CreatedDate
   from 
   StriveCarSalon.tblEmployeeLiability empLi inner join 
   [StriveCarSalon].[GetTable]('LiabilityType') lcv on empLi.LiabilityType = lcv.valueid 
   inner join
   StriveCarSalon.tblEmployeeLiabilityDetail empLid on empLi.LiabilityId = empLid.LiabilityId
   LEFT join
   [StriveCarSalon].[GetTable]('LiabilityDetailType') dcv on empLid.LiabilityDetailType = dcv.valueid
   
   LEFT JOIN 
   StriveCarSalon.tblClient tblcl on empLi.ClientId = tblcl.ClientId

   LEFT JOIN strivecarsalon.tblClientVehicle cvl ON empLi.VehicleId = cvl.VehicleId
 
  LEFT JOIN strivecarsalon.GetTable('VehicleManufacturer') cvMfr ON cvl.VehicleMfr = cvMfr.valueid
LEFT JOIN strivecarsalon.GetTable('VehicleModel') cvMo ON cvl.VehicleModel = cvMo.valueid
LEFT JOIN strivecarsalon.GetTable('VehicleColor') cvCo ON cvl.VehicleColor = cvCo.valueid

   where isnull(empLi.IsActive,1)=1 and isnull(empLi.IsDeleted,0)=0  and empLi.EmployeeId = @EmployeeId  

   select rm.RoleMasterId, empr.EmployeeId,empr.EmployeeRoleId, empr.roleid,rm.RoleName as rolename from strivecarsalon.tblEmployeeRole empr inner join
   StriveCarSalon.tblRoleMaster rm on empr.RoleId = rm.RoleMasterId where empr.EmployeeId=@EmployeeId  and isnull(empr.IsDeleted,0)=0

   select emplo.EmployeeId,EmployeeLocationId, emplo.LocationId,lo.Locationname from strivecarsalon.tblEmployeeLocation emplo inner join
   StriveCarSalon.tblLocation lo on emplo.locationid=lo.locationid where emplo.employeeid=@EmployeeId
   and isnull(emplo.IsDeleted,0)=0 and emplo.IsActive = 1

END
--	[StriveCarSalon].uspGetEmployeeById 1
--select * from [StriveCarSalon].[GetTable]('LiabilityType')
--select * from [StriveCarSalon].[GetTable]('LiabilityDetailType')
--select * from StriveCarSalon.tblEmployeeLiability
--select * from StriveCarSalon.tblEmployeeLiabilityDetail

