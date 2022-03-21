CREATE PROCEDURE [StriveCarSalon].[uspGetClient]
(@ClientId int)
AS 
BEGIN

SELECT Top 1
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
tblc.AuthId,
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
tblc.IsCreditAccount,
tblc.LocationId
--tblca.IsActive


FROM [tblClient] tblc 
inner join [tblClientAddress] tblca
ON(tblc.ClientId = tblca.ClientId) 
WHERE ISNULL(tblc.IsDeleted,0)=0 --AND ISNULL(tblc.IsActive,1)=1 
AND
(@ClientId is null or tblc.ClientId = @ClientId)  
		 
END
