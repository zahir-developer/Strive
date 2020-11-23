


CREATE FUNCTION [StriveCarSalon].[GetClientName]
 (@Id as int)
returns  varchar(50)
as
begin

DECLARE @ClientName VARCHAR(50) = '';
	IF (@Id <> 0) 
	begin
		  
		  select TOP(1) @ClientName = FirstName + MiddleName + LastName from TblClient C (NoLock)
		  where C.ClientId = @Id
	end
	return(ISNULL(@ClientName,''));
end