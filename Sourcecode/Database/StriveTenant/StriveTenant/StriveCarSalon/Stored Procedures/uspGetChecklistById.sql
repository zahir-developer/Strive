CREATE PROCEDURE [StriveCarSalon].[uspGetChecklistById] --[StriveCarSalon].[uspGetChecklistById] 51
@ChecklistId int 
AS
BEGIN

SELECT 
	cl.ChecklistId,
	cl.Name, 
	cl.RoleId,
	tr.RoleName as RoleName
FROM [StriveCarSalon].[tblChecklist] cl
inner join [StriveCarSalon].[tblRoleMaster] tr on cl.RoleId = tr.RoleMasterId
where ISNULL(cl.IsDeleted,0)=0 and cl.ChecklistId =@ChecklistId
Order by ChecklistId DESC


SELECT 
cln.CheckListNotificationId,
cln.CheckListId,
cln.NotificationTime
FROM [StriveCarSalon].[tblCheckListNotification] cln
where ISNULL(cln.IsDeleted,0)=0 and cln.ChecklistId =@ChecklistId

END