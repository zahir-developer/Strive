CREATE PROC	[dbo].[uspSaveTenantUserMap]
(
@AuthId INT,
@TenantGuid as uniqueidentifier
)AS
BEGIN

   IF NOT EXISTS (SELECT 1 FROM tblSchemaAccess 
                   WHERE AuthId =@AuthId)
   BEGIN
      INSERT INTO tblSchemaAccess
		select 
		@AuthId,
		sm.schemaid,0 from tblschemamaster sm
		inner join tbltenantmaster tm on tm.tenantid=sm.tenantid where tm.TenantGuid = @TenantGuid

		INSERT INTO tblLastAuth
		SELECT @AuthId,1, GETDATE()
   END
END