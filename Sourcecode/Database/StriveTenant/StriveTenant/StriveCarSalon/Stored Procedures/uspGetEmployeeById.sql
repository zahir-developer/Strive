ALTER PROCEDURE  [StriveCarSalon].[uspGetEmployeeById] 
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
from tblEmployee emp
left join tblEmployeeAddress empAdd on emp.EmployeeId= empAdd.EmployeeId
left join tblEmployeeDetail empDetail on emp.EmployeeId = empDetail.EmployeeId
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
   empdoc.FileType from tblEmployeeDocument empdoc where 
   isnull(empdoc.IsActive,1)=1 and isnull(empdoc.IsDeleted,0)=0 and empdoc.EmployeeId = @EmployeeId


select row_number() OVER (
	ORDER BY  empLi.LiabilityId
   ) CollisionSequence,
    empli.EmployeeId,
	empli.ClientId,
tblcl.FirstName + ' '+ tblcl.LastName as ClientName,
	empLi.VehicleId,
	ISNULL(cvMfr.MakeValue,'') + ' '+	ISNULL(cvmo.ModelValue,'') + ' '+ ISNULL(cvCo.valuedesc,'') AS VehicleName,
  empLi.LiabilityId,
   empLiD.liabilityDetailId,
   lcv.valuedesc as LiabilityType,
   dcv.valuedesc as LiabilityDetailType,
   empLi.LiabilityDescription,
   empLiD.Amount,   
   empLiD.LiabilityDetailType as LiabilityDetailTypeId,
   empLi.CreatedDate
   from 
   tblEmployeeLiability empLi inner join 
   [GetTable]('LiabilityType') lcv on empLi.LiabilityType = lcv.valueid 
   inner join
  tblEmployeeLiabilityDetail empLid on empLi.LiabilityId = empLid.LiabilityId
   LEFT join
   [GetTable]('LiabilityDetailType') dcv on empLid.LiabilityDetailType = dcv.valueid
   
   INNER JOIN 
   tblClient tblcl on empLi.ClientId = tblcl.ClientId

   LEFT JOIN tblClientVehicle cvl ON empLi.VehicleId = cvl.VehicleId
   LEFT JOIN tblVehicleMake cvMfr ON cvl.VehicleMfr = cvMfr.MakeId
   LEFT JOIN tblVehicleModel cvMo ON cvl.VehicleModel = cvMo.ModelId and cvMo.MakeId = cvMfr.MakeId
   LEFT JOIN GetTable('VehicleColor') cvCo ON cvl.VehicleColor = cvCo.valueid

   where isnull(empLi.IsActive,1)=1 and isnull(empLi.IsDeleted,0)=0  and empLi.EmployeeId = @EmployeeId  

   select rm.RoleMasterId, empr.EmployeeId,empr.EmployeeRoleId, empr.RoleId,rm.RoleName as rolename from tblEmployeeRole empr inner join
   tblRoleMaster rm on empr.RoleId = rm.RoleMasterId where empr.EmployeeId=@EmployeeId  and isnull(empr.IsDeleted,0)=0

   select emplo.EmployeeId,EmployeeLocationId, emplo.LocationId,lo.Locationname from tblEmployeeLocation emplo inner join
   tblLocation lo on emplo.locationid=lo.locationid where emplo.employeeid=@EmployeeId
   and isnull(emplo.IsDeleted,0)=0 and emplo.IsActive = 1

   
   select ehr.EmployeeHourlyRateId, ehr.EmployeeId, ehr.LocationId,ehr.RoleId,ehr.HourlyRate,lo.LocationName,rm.RoleName
   from tblEmployeeHourlyRate ehr inner join
   tblLocation lo on ehr.locationid=lo.locationid 
   INNER join tblEmployee emp on  emp.EmployeeId =ehr.EmployeeId
   LEFT join tblRoleMaster rm on rm.RoleMasterId=ehr.RoleId
   
   where ehr.EmployeeId=@EmployeeId
   and isnull(ehr.IsDeleted,0)=0 and ehr.IsActive = 1

END

