CREATE PROC	uspSaveLogin
(
@Logintbl as tvpAuthMaster readonly,
@TenantGuid as uniqueidentifier
)AS
BEGIN

DECLARE @NewUserGuid as UNIQUEIDENTIFIER;
DECLARE @AuthId as int;
SET @NewUserGuid = NEWID();

Set @AuthId =0;

MERGE tblAuthMaster TRG
USING
@Logintbl SRC 
ON
(TRG.AuthId = SRC.AuthId AND TRG.UserGuid = SRC.UserGuid)
WHEN MATCHED
THEN

UPDATE SET 
TRG.EmailId		= SRC.EmailID,
TRG.MobileNumber= SRC.MobileNumber,
TRG.EmailVerified= SRC.EmailVerified,
TRG.LockoutEnabled= SRC.LockoutEnabled,
TRG.PasswordHash= SRC.PasswordHash,
TRG.SecurityStamp= SRC.SecurityStamp
WHEN NOT MATCHED
THEN

	INSERT (
	UserGuid
	,EmailId
	,MobileNumber
	,EmailVerified
	,LockoutEnabled
	,PasswordHash
	,SecurityStamp
	,CreatedDate) VALUES
	(
	@NewUserGuid,
	SRC.EmailId,
	SRC.MobileNumber,
	SRC.EmailVerified,
	0,
	SRC.PasswordHash,
	1,
	SRC.CreatedDate);

SET @AuthId = ISNULL(@AuthId, SCOPE_IDENTITY());


   IF NOT EXISTS (SELECT 1 FROM tblSchemaAccess 
                   WHERE AuthId =@AuthId)
   BEGIN
      INSERT INTO tblSchemaAccess
		select 
		@AuthId,
		sm.schemaid,0 from tblschemamaster sm
		inner join tbltenantmaster tm on tm.tenantid=sm.tenantid where tm.TenantGuid = @TenantGuid

   END
END