




CREATE PROCEDURE [CON].[uspGetEmployeeRoles]
as
begin
select 
tblcv.CategoryId,
tblcc.Category,
tblcv.id AS CodeId,
tblcv.CodeValue,
tblcv.CodeShortValue,
tblcv.SortOrder

from [CON].[tblCodeValue] tblcv inner join 
[CON].[tblCodeCategory] tblcc on(tblcv.CategoryId = tblcc.id) 
where tblcc.Category='EmployeeRole'
AND tblcv.IsDeleted=0 AND tblcc.IsDeleted=0
end