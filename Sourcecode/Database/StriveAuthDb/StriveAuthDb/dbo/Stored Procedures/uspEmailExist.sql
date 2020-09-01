


CREATE PROCEDURE [dbo].[uspEmailExist]
(@Email varchar(50))

AS 
BEGIN

DECLARE @Count int;
SELECT @Count = (SELECT COUNT(1)
FROM tblAuthMaster
WHERE EmailId=@Email)

IF @Count >0
BEGIN
SELECT EmailExist = 1;
END
else
BEGIN
SELECT EmailExist = 0;
END
END