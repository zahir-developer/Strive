-- =================== HISTORY ==========================
-- 08-03-2022  | JUKI B  | Changes done to login using employee login id 
-- 30-03-2022  | JUKI B  | Removed deleted records when fetching user password hash
-- =============================================

CREATE PROCEDURE [dbo].[uspGetPasswordHash] --'caradmin@strive.com'
(@UserName as varchar(64))
AS
BEGIN
DECLARE @Pass as varchar(64) = null;
SELECT @Pass = PasswordHash from tblAuthMaster 
WHERE
(EmailId=@UserName OR LoginId=@UserName)
--AND
--usertype is not null AND
--Tenantid is not null
AND
LockoutEnabled=0
AND ISNULL(IsDeleted,0) = 0
select @pass as PasswordHash;
END