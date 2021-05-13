--[StriveCarSalon].[uspGetUpchargeTypeByModel]  93
CREATE proc [StriveCarSalon].[uspGetUpchargeTypeByModel] 
@modelId int
as 
declare @categoryId int = (select top  1 id from tblCodeCategory where Category ='UpchargeType')
begin
	select 
	val.id as UpchargeId,
	val.codevalue as UpchargeName
	from tblCodeCategory cat
    inner join tblCodeValue val on cat.id=val.categoryid
	where categoryId =@categoryId 
	--and val.ParentId =@modelId 
	and ISNULL(val.IsDeleted,0) =0
	ORDER BY CodeValue
	
end