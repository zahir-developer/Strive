



CREATE proc [StriveCarSalon].[uspGetEmployeeRoles]
as
begin
select 
tblcv.CategoryId,
tblcc.Category,
tblcv.id AS CodeId,
tblcv.CodeValue,
tblcv.CodeShortValue,
tblcv.SortOrder

from [StriveCarSalon].[tblCodeValue] tblcv inner join 
[StriveCarSalon].[tblCodeCategory] tblcc on(tblcv.CategoryId = tblcc.id) 
where tblcc.Category='EmployeeRole'
AND tblcv.IsDeleted=0 AND tblcc.IsDeleted=0
end
