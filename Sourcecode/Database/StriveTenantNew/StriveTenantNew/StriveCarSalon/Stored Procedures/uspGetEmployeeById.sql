﻿CREATE PROC  StriveCarSalon.uspGetEmployeeById 
(@EmployeeId int)
AS
BEGIN

select 
emp.EmployeeId,
empDetail.EmployeeCode,
emp.firstname,
emp.LastName,
emp.Gender,
empAdd.Address1,
empAdd.PhoneNumber,
emp.ImmigrationStatus,
emp.SSNo,
empAdd.Email,
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
	ORDER BY  empdoc.DocumentId
   ) DocumentSequence , 
   empdoc.DocumentId, 
   empdoc.[FileName], 
   empdoc.CreatedDate, 
   empdoc.UpdatedDate, 
   empdoc.FilePath,
   empdoc.FileType from StriveCarSalon.tblEmployeeDocument empdoc where 
   isnull(empdoc.IsActive,1)=1 and isnull(empdoc.IsDeleted,0)=0 and empdoc.EmployeeId = @EmployeeId


select row_number() OVER (
	ORDER BY  empLi.LiabilityId
   ) CollisionSequence,
   empLi.LiabilityId,
   empLiD.liabilityDetailId,
   lcv.valuedesc as LiabilityType,
   dcv.valuedesc as LiabilityDetailType,
   empLi.LiabilityDescription,
   empLiD.Amount,
   empLiD.LiabilityDetailType
   from 
   StriveCarSalon.tblEmployeeLiability empLi inner join 
   [StriveCarSalon].[GetTable]('LiabilityType') lcv on empLi.LiabilityType = lcv.valueid 
   inner join
   StriveCarSalon.tblEmployeeLiabilityDetail empLid on empLi.LiabilityId = empLid.LiabilityId
   inner join
   [StriveCarSalon].[GetTable]('LiabilityDetailType') dcv on empLid.LiabilityDetailType = dcv.valueid
   where isnull(empLi.IsActive,1)=1 and isnull(empLi.IsDeleted,0)=0  and empLi.EmployeeId = @EmployeeId  

   select empr.roleid,rm.RoleName as rolename from strivecarsalon.tblEmployeeRole empr inner join
   StriveCarSalon.tblRoleMaster rm on empr.RoleId = rm.RoleMasterId where empr.EmployeeId=@EmployeeId

END
--StriveCarSalon.uspGetEmployeeById  1
--select * from [StriveCarSalon].[GetTable]('LiabilityType')
--select * from [StriveCarSalon].[GetTable]('LiabilityDetailType')
--select * from StriveCarSalon.tblEmployeeLiability
--select * from StriveCarSalon.tblEmployeeLiabilityDetail