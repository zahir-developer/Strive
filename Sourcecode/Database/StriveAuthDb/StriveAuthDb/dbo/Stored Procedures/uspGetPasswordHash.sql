CREATE Proc uspGetPasswordHash
(@UserName as varchar(64))
AS
BEGIN
DECLARE @Pass as varchar(64) = null;
SELECT @Pass = PasswordHash from tblAuthMaster 
WHERE
EmailId=@UserName
AND
LockoutEnabled=0
select @pass as PasswordHash;
END