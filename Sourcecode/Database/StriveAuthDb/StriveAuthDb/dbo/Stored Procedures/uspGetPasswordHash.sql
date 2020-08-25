CREATE Proc [dbo].[uspGetPasswordHash] --'caradmin@strive.com'
(@UserName as varchar(64))
AS
BEGIN
DECLARE @Pass as varchar(64) = null;
SELECT @Pass = PasswordHash from tblAuthMaster 
WHERE
EmailId=@UserName AND
usertype is not null AND
Tenantid is not null
AND
LockoutEnabled=0
select @pass as PasswordHash;
END