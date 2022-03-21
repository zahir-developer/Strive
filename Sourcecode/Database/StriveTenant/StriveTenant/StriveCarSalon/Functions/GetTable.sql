CREATE FUNCTION [StriveCarSalon].[GetTable] 
(@category as varchar(100))
returns @result table (id int, category varchar(100), valueid int, valuedesc varchar(100), shortValue varchar(10) )
as
begin

	IF (@category ='PMSSOURCEPATH')
	begin
		  Insert into @result
		  select cv.id, cv.codevalue as category, cv1.id as valueid, cv1.codevalue as valuedesc, '' from tblcodecategory co inner join
		tblCodeValue cv on co.id=cv.categoryid
		inner join tblcodevalue cv1 on cv.id = cv1.parentid 
		where co.category=@category and cv.isDeleted = 0
	end

	else
	begin
		Insert into @result
		select cat.id, cat.category, val.id as valueid, val.codevalue as valuedesc, val.CodeShortValue as shortValue  from 
		tblcodecategory cat inner join tblcodevalue val on cat.id = val.categoryid where category=@category and val.isDeleted = 0
	end
	return 
end
