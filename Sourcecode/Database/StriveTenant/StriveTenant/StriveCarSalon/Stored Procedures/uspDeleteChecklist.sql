create PROCEDURE [StriveCarSalon].[uspDeleteChecklist]
(@ChecklistId int)
AS 
BEGIN
    UPDATE [StriveCarSalon].[tblChecklist] 
    SET  IsDeleted=1  WHERE ChecklistId = @ChecklistId
END