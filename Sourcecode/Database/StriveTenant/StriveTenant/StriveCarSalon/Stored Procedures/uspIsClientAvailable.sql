CREATE PROCEDURE [StriveCarSalon].[uspIsClientAvailable] 
@FirstName varchar(max),
@LastName varchar(max),
@PhoneNumber varChar(50) = null
as 
begin
	select 
	cl.ClientId,
	cl.FirstName,
	cl.LastName,
	tblca.PhoneNumber
	 from tblClient cl
	  left join [tblClientAddress] tblca ON(cl.ClientId = tblca.ClientId)
	where cl.FirstName =@FirstName and cl.LastName =@LastName and tblca.PhoneNumber = @PhoneNumber
	
end