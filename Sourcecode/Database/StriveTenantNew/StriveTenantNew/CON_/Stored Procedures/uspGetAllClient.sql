
CREATE PROCEDURE [CON].[uspGetAllClient] 
AS 
BEGIN

SELECT 
tblc.ClientId,
tblc.FirstName,
tblc.LastName,
tblc.IsActive,
tblc.ClientType,
ct.valuedesc AS Type,
tblca.PhoneNumber AS PhoneNumber,
tblca.Address1,
tblca.Address2


FROM [CON].[tblClient] tblc 
     inner join [CON].[tblClientAddress] tblca ON(tblc.ClientId = tblca.ClientId)
	 inner join strivecarsalon.GetTable('ClientType') ct ON tblc.ClientType = ct.valueid

	 
WHERE ISNULL(tblc.IsDeleted,0) = 0
AND
isnull(tblc.IsDeleted,0)=0 and 
isnull(tblca.IsDeleted,0)=0 
order by ClientId Desc

END