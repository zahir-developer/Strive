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
((@name is null or trim (cl.FirstName)  like @name+'%') OR
(@name is null or cl.LastName  like @name+'%'))
	
end