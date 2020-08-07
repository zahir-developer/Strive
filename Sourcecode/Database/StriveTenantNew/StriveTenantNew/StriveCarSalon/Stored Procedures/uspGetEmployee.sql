




CREATE proc [StriveCarSalon].[uspGetEmployee]
as
begin
select tblemp.EmployeeId,
tblemp.FirstName,
tblemp.MiddleName,
tblemp.LastName,
tblemp.Gender,
tblemp.SSNo,
tblemp.MaritalStatus,
tblemp.IsCitizen,
tblemp.AlienNo,
tblemp.BirthDate,
tblemp.ImmigrationStatus,
tblemp.CreatedDate,
tblemp.IsActive,

tblempdet.EmployeeDetailId AS EmployeeDetail_EmployeeDetailId,
tblempdet.EmployeeId       AS EmployeeDetail_EmployeeId,
tblempdet.EmployeeCode     AS EmployeeDetail_EmployeeCode,
tblempdet.AuthId           AS EmployeeDetail_AuthId,
tblempdet.LocationId       AS EmployeeDetail_LocationId,
tblempdet.PayRate          AS EmployeeDetail_PayRate,
tblempdet.SickRate         AS EmployeeDetail_SickRate,
tblempdet.VacRate          AS EmployeeDetail_VacRate,
tblempdet.ComRate          AS EmployeeDetail_ComRate,
tblempdet.HiredDate        AS EmployeeDetail_HiredDate,
tblempdet.Salary           AS EmployeeDetail_Salary,
tblempdet.Tip              AS EmployeeDetail_Tip,
tblempdet.LRT              AS EmployeeDetail_LRT,
tblempdet.Exemptions       AS EmployeeDetail_Exemptions,
tblempdet.IsActive         AS EmployeeDetail_IsActive,

tblempadd.EmployeeAddressId        AS EmployeeAddress_EmployeeAddressId,
tblempadd.EmployeeId   AS EmployeeAddress_EmployeeId,
tblempadd.Address1         AS EmployeeAddress_Address1,
tblempadd.Address2         AS EmployeeAddress_Address2,
tblempadd.PhoneNumber      AS EmployeeAddress_PhoneNumber,
tblempadd.PhoneNumber2     AS EmployeeAddress_PhoneNumber2,
tblempadd.Email            AS EmployeeAddress_Email,
tblempadd.City             AS EmployeeAddress_City,
tblempadd.State            AS EmployeeAddress_State,
tblempadd.Zip              AS EmployeeAddress_Zip,
tblempadd.IsActive         AS EmployeeAddress_IsActive,
tblempadd.Country          AS EmployeeAddress_Country,

tblemprol.EmployeeId	    AS EmployeeRoles_EmployeeId,
tblemprol.RoleId		    AS EmployeeRoles_RoleId,
tblemprol.IsActive          AS EmployeeRoles_IsActive,
tblemprol.IsDefault         AS EmployeeRoles_IsDefault,
tblemprol.EmployeeRoleId AS EmployeeRoles_EmployeeRolesId

from [StriveCarSalon].tblEmployee tblemp inner join 
[StriveCarSalon].tblEmployeeDetail tblempdet on(tblemp.EmployeeId = tblempdet.EmployeeId) inner join 
[StriveCarSalon].tblEmployeeAddress tblempadd on(tblemp.EmployeeId = tblempadd.EmployeeId) inner join
[StriveCarSalon].tblEmployeeRole tblemprol on (tblemp.EmployeeId = tblemprol.EmployeeId)
AND
tblemp.IsDeleted=0 AND tblempdet.IsDeleted=0 AND tblempadd.IsDeleted=0 AND tblemprol.IsDeleted=0

end