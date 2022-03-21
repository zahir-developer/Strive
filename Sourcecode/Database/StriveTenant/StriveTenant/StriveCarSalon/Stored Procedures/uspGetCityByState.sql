CREATE PROCEDURE [StriveCarSalon].[uspGetCityByState] 
@stateId int
as 
declare @categoryId int = (select top  1 id from tblCodeCategory where Category ='City')
begin
	select 
	val.id as CityId,
	val.codevalue as CityName,
	val.codeshortvalue as CityShortName,
	val.sortorder 
	from tblCodeCategory cat
    inner join tblCodeValue val on cat.id=val.categoryid
	where categoryId =@categoryId and val.ParentId =@stateId and ISNULL(val.IsDeleted,0) =0
	ORDER BY CodeValue
	
end