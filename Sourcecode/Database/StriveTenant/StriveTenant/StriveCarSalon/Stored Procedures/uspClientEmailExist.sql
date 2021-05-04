

CREATE PROCEDURE [StriveCarSalon].[uspClientEmailExist]
(@Email varchar(50) Null )

AS 

BEGIN 
SELECT ClientId,Email
FROM [StriveCarSalon].[tblClientAddress]
WHERE Email=@Email
AND ISNULL(IsDeleted,0)=0
END