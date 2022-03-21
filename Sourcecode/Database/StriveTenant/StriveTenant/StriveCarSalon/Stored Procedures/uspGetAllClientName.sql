---------------------History---------------------------
-- ====================================================
-- 03-jun-2021, Vetriselvi - Unable to search newly added client                   

-------------------------------------------------------
CREATE PROCEDURE [StriveCarSalon].[uspGetAllClientName] 

@Name varchar(max)null
as 
begin

select 
cl.ClientId,
cl.FirstName,
cl.LastName,
cl.IsActive,
cl.IsDeleted from tblClient cl
where cl.IsActive = 1 
and ISNULL(cl.IsDeleted,0) = 0 and
(@name is null or LOWER(trim (ISNULL(cl.FirstName,'') + ' ' + ISNULL(cl.LastName,'')))  like LOWER(@name)+'%') 
	
end