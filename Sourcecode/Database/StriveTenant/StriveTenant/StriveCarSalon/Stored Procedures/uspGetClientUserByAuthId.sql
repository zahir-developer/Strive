-- =============================================
-- Author:		Zahir
-- Create date: 07-Dec-2020
-- Description:	Client login Authenticate
--EXEC [StriveCarSalon].[uspGetClientUserByAuthId] 1720
-- =============================================
-- =============================================
-- --------------History------------------------
-- =============================================
-- 20-05-2021, Zahir	- Changed the roleName from client to Customer


CREATE PROCEDURE [StriveCarSalon].[uspGetClientUserByAuthId] 
(@AuthId int)
as
begin

DROP TABLE IF EXISTS #Client
SELECT 
c.ClientId,
c.FirstName,
c.LastName,
c.AuthId
INTO #Client
FROM tblClient c 
WHERE c.AuthId = @AuthId 


Select * from #Client

SELECT 
c.ClientId, 
c.FirstName,
c.LastName,
rolmas.RoleMasterId as RoleId,
rolmas.RoleName,
module.ModuleName,
modscrn.ViewName,
fld.FieldName
FROM #Client c 
JOIN tblRoleMaster rolmas on rolmas.RoleName = 'Customer'
left join TblRolePermission rolper on rolper.RoleId = rolmas.RoleMasterId AND ISNULL(rolper.IsDeleted,0)=0 AND ISNULL(rolper.IsActive, 1) = 1
left join TblModule module on rolper.ModuleId=module.ModuleId AND ISNULL(module.IsDeleted,0)=0 AND ISNULL(module.IsActive, 1) = 1
left join TblModuleScreen modscrn on rolper.ModuleScreenId=modscrn.ModuleScreenId AND ISNULL(modscrn.IsDeleted,0)=0 AND ISNULL(modscrn.IsActive, 1) = 1
left join TblField fld  on rolper.FieldId=fld.FieldId AND ISNULL(fld.IsDeleted,0)=0 AND ISNULL(fld.IsActive, 1) = 1
WHERE ISNULL(rolmas.IsDeleted,0) = 0
AND ISNULL(module.IsDeleted,0) = 0

end