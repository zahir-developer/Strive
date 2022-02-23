CREATE PROCEDURE [StriveCarSalon].[uspGetRolePermissions]
@Employeeid int
AS
BEGIN

SELECT 
emp.EmployeeId, 
emp.FirstName,
emp.LastName,
rolper.RoleId,
rolmas.RoleName,
module.ModuleName,
modscrn.ViewName,
fld.FieldName
FROM tblEmployee emp 
left join tblEmployeeRole emprol on emprol.EmployeeId=emp.EmployeeId
left join tblRoleMaster rolmas on emprol.RoleId=rolmas.RoleMasterId
left join TblRolePermissionDetail rolper on rolper.RoleId=emprol.RoleId
left join TblModule module on rolper.ModuleId=module.ModuleId
left join TblModuleScreen modscrn on rolper.ModuleScreenId=modscrn.ModuleScreenId
left join TblField fld  on rolper.FieldId=fld.FieldId
 WHERE emp.EmployeeId =@Employeeid 
 AND  emp.IsActive =1
 AND emprol.IsActive=1
 AND  ISNULL( rolmas.IsActive,1) =1
 AND rolper.IsActive =1
 AND module.IsActive =1
 AND modscrn.IsActive =1
 AND fld.IsActive =1
 AND ISNULL(emp.IsDeleted,0)=0  
 AND ISNULL(emprol.IsDeleted,0)=0
 AND ISNULL(rolmas.IsDeleted,0)=0
 AND ISNULL(rolper.IsDeleted,0)=0
 AND ISNULL(modscrn.IsDeleted,0)=0 
 AND ISNULL(fld.IsDeleted,0)=0
 AND ISNULL(module.IsDeleted,0)=0
END