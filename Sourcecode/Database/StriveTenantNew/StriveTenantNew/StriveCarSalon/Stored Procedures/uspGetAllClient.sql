
CREATE PROCEDURE [StriveCarSalon].[uspGetAllClient]
AS 
BEGIN

SELECT 
tblc.ClientId,
tblc.FirstName,
tblc.MiddleName,
tblc.LastName,
tblc.Gender,
tblc.MaritalStatus,
tblc.BirthDate,
tblc.CreatedDate,
tblc.IsActive,
tblc.Notes,
tblc.RecNotes,
tblc.Score,
tblc.NoEmail,
tblc.ClientType,
tblca.ClientAddressId AS ClientAddress_ClientAddressId,
tblca.ClientId AS ClientAddress_RelationshipId,
tblca.Address1 AS ClientAddress_Address1,
tblca.Address2 AS ClientAddress_Address2,
tblca.PhoneNumber AS ClientAddress_PhoneNumber,
tblca.PhoneNumber2 AS ClientAddress_PhoneNumber2,
tblca.Email AS ClientAddress_Email,
tblca.City AS ClientAddress_City,
tblca.State AS ClientAddress_State,
tblca.Country AS ClientAddress_Country,
tblca.Zip AS ClientAddress_Zip


FROM [StriveCarSalon].[tblClient] tblc 
     inner join [StriveCarSalon].[tblClientAddress] tblca ON(tblc.ClientId = tblca.ClientId)
	 inner join [StriveCarSalon].GetTable('ClientType') tblcv ON(tblc.ClientType = tblcv.valueid)

	 
WHERE ISNULL(tblc.IsDeleted,0) = 0
AND
isnull(tblc.IsDeleted,0)=0 and 
isnull(tblca.IsDeleted,0)=0 

END