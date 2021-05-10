CREATE PROCEDURE [StriveCarSalon].[uspGetChecklist] 
AS
BEGIN

SELECT 
	cl.ChecklistId, 
	cl.Name, 
	cl.RoleId,
	tr.RoleName as RoleName
	,STUFF((SELECT  ', ' + CAST(cln.NotificationTime  AS varchar(5))
    FROM [tblCheckListNotification] cln
	WHERE cln.CheckListId = cl.ChecklistId AND cln.IsDeleted = 0
    FOR XML PATH('')
	), 1, 2, '')  AS NotificationTime
	--,cln.NotificationTime
FROM [tblChecklist] cl
inner join [tblRoleMaster] tr on cl.RoleId = tr.RoleMasterId
--left join [StriveCarSalon].[tblCheckListNotification] cln on cln.CheckListId = cl.ChecklistId
where ISNULL(cl.IsDeleted,0)=0

 Order by ChecklistId DESC
END