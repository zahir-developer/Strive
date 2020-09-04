
CREATE PROCEDURE  [CON].[uspGetEmployeeById] 
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
emp.ImmigrationStatus,
emp.SSNo,
empAdd.Email,
empDetail.EmployeeDetailId,
empDetail.HiredDate,
empDetail.PayRate,
empDetail.ComRate,
emp.IsActive as Status,
empDetail.Tip,
empDetail.Exemptions
from StriveCarSalon.tblEmployee emp
left join strivecarsalon.tblEmployeeAddress empAdd on emp.EmployeeId= empAdd.EmployeeId
left join StriveCarSalon.tblEmployeeDetail empDetail on emp.EmployeeId = empDetail.EmployeeId
where isnull(empAdd.IsActive,1)=1 and isnull(empDetail.IsDeleted,0)=0 and emp.EmployeeId = @EmployeeId


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
   empLi.LiabilityId,
   empLiD.liabilityDetailId,
   lcv.valuedesc as LiabilityType,
   dcv.valuedesc as LiabilityDetailType,
   empLi.LiabilityDescription,
   empLiD.Amount,
   empLiD.LiabilityDetailType as LiabilityDetailTypeId
   from 
   StriveCarSalon.tblEmployeeLiability empLi inner join 
   [CON].[GetTable]('LiabilityType') lcv on empLi.LiabilityType = lcv.valueid 
   inner join
   StriveCarSalon.tblEmployeeLiabilityDetail empLid on empLi.LiabilityId = empLid.LiabilityId
   LEFT join
   [CON].[GetTable]('LiabilityDetailType') dcv on empLid.LiabilityDetailType = dcv.valueid
   where isnull(empLi.IsActive,1)=1 and isnull(empLi.IsDeleted,0)=0  and empLi.EmployeeId = @EmployeeId  

   select rm.RoleMasterId, empr.EmployeeId,empr.EmployeeRoleId, empr.roleid,rm.RoleName as rolename from strivecarsalon.tblEmployeeRole empr inner join
   StriveCarSalon.tblRoleMaster rm on empr.RoleId = rm.RoleMasterId where empr.EmployeeId=@EmployeeId

   select emplo.EmployeeId,EmployeeLocationId, emplo.LocationId,lo.Locationname from strivecarsalon.tblEmployeeLocation emplo inner join
   StriveCarSalon.tblLocation lo on emplo.locationid=lo.locationid where emplo.employeeid=@EmployeeId

END
--	[CON].uspGetEmployeeById 1
--select * from [CON].[GetTable]('LiabilityType')
--select * from [CON].[GetTable]('LiabilityDetailType')
--select * from StriveCarSalon.tblEmployeeLiability
--select * from StriveCarSalon.tblEmployeeLiabilityDetail