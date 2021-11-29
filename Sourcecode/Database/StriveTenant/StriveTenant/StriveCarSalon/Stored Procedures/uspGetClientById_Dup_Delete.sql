
CREATE PROCEDURE [StriveCarSalon].[uspGetClientById_Dup_Delete] 
(@ClientId int =null)
AS 
BEGIN

SELECT 
tblc.ClientId,
tblc.FirstName,
tblc.MiddleName,
tblc.LastName,
tblc.Amount,
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
tblca.ClientAddressId AS ClientAddressId,
tblca.ClientId AS RelationshipId,
tblca.Address1 AS Address1,
tblca.Address2 AS Address2,
tblca.PhoneNumber AS PhoneNumber,
tblca.PhoneNumber2 AS PhoneNumber2,
tblca.Email AS Email,
tblca.City AS City,
tblca.State AS State,
tblca.Country AS Country,
tblca.Zip AS Zip


FROM [StriveCarSalon].[tblClient] tblc 
     inner join [StriveCarSalon].[tblClientAddress] tblca ON(tblc.ClientId = tblca.ClientId)

	 
WHERE ISNULL(tblc.IsDeleted,0) = 0
AND
isnull(tblc.IsDeleted,0)=0 and 
isnull(tblca.IsDeleted,0)=0 AND
(@ClientId is null or tblc.ClientId = @ClientId)

END