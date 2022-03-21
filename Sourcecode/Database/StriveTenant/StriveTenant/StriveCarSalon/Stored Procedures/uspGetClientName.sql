CREATE PROCEDURE [StriveCarSalon].[uspGetClientName] 
@ClientName varchar(50)
AS 
BEGIN

Select
    cl.ClientId,
    cl.FirstName,
	cl.LastName,
    cl.ClientType,
	cl.IsActive,
	ct.valuedesc AS Type,
	ca.Address1,
	ca.Address2,
	ca.PhoneNumber,
	ca.PhoneNumber2
	FROM tblclient cl 
	inner join tblClientAddress as ca ON cl.ClientId=ca.ClientId 
	inner join strivecarsalon.GetTable('ClientType') ct ON cl.ClientType = ct.valueid
	WHERE isnull(cl.IsDeleted,0)=0 and cl.IsActive = 1
AND
 ((@ClientName is null or cl.FirstName  like @ClientName+'%') OR
  (@ClientName is null or cl.LastName  like @ClientName+'%'))

END
