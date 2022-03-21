CREATE PROCEDURE [StriveCarSalon].[uspGetCodes]
(@Category varchar(50)=null, @CategoryId int=null)
as 
begin
	select cat.id as CategoryId, cat.Category, val.id as codeid, val.codevalue as codevalue, val.codeshortvalue as codeshortvalue, val.sortorder 
	from tblCodeCategory cat inner join tblCodeValue val on
	cat.id=val.categoryid
	where 
	(@Category is null or Category = @Category or (@Category = 'ALL' AND Category != 'City'))
	and 
	(@CategoryId is null or CategoryId = @CategoryId)
	and ISNULL(val.IsDeleted,0) =0
	order by SortOrder,val.CodeValue

end