CREATE PROCEDURE [StriveCarSalon].[uspGetChecklist] 
AS
BEGIN

SELECT 
	cl.ChecklistId, 
	cl.Name, 
	cl.RoleId,
	tr.RoleName as RoleName

FROM [StriveCarSalon].[tblChecklist] cl
inner join [StriveCarSalon].[tblRoleMaster] tr on cl.RoleId = tr.RoleMasterId
where ISNULL(cl.IsDeleted,0)=0

 Order by ChecklistId DESC
END