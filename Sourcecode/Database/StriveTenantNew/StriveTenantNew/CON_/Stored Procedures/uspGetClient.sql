
CREATE PROCEDURE [CON].[uspGetClient]  
(@ClientId int)
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
tblca.ClientAddressId,
tblca.ClientId as ClientRelatioshipId,
tblca.Address1,
tblca.Address2,
tblca.PhoneNumber,
tblca.PhoneNumber2,
tblca.Email,
tblca.City,
tblca.State,
tblca.Country,
tblca.Zip,
tblca.IsActive


FROM [CON].[tblClient] tblc inner join [CON].[tblClientAddress] tblca
		   ON(tblc.ClientId = tblca.ClientId) 
           WHERE ISNULL(tblc.IsDeleted,0)=0 AND ISNULL(tblc.IsActive,1)=1 and
		   (@ClientId is null or tblc.ClientId = @ClientId)  
		 
END