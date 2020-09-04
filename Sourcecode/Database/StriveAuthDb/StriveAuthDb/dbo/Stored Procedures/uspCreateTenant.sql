
CREATE PROCEDURE [dbo].[uspCreateTenant]
(@TenantName varchar(50), @TenantEmail varchar(100), @Subscriptionid int=null, @SchemaPasswordHash varchar(200),@ExpiryDate date)
AS
BEGIN

SET NOCOUNT ON

DECLARE @schemaName VARCHAR(60);
DECLARE @schemaUser VARCHAR(64);
DECLARE @TenantId INT
DECLARE @SchemaId INT
DECLARE @ClientId INT
DECLARE @AUTHId INT

Insert Into tblclient(ClientName, ClientEmail) values (@TenantName, @TenantEmail)
set @ClientId = SCOPE_IDENTITY()

SET @schemaName ='Strive'+@TenantName
SET @schemaUser = @schemaName +'user'

INSERT INTO tblTenantMaster(TenantGuid, SubscriptionId,clientid,IsActive, IsDeleted,EmpSize, ExpiryDate, CreatedDate)
values
(NEWID(),@Subscriptionid,@ClientId,1,0,100,@ExpiryDate,getdate())

SET @TenantId = scope_identity()

INSERT INTO tblTenantDetail(TenantId,TenantName, ColorTheme, Currency,TelantLogoUrl)
VALUES
(@TenantId,@TenantName,'Default','USD','default.png')

INSERT INTO tblSchemaMaster (clientid,subdomain,DBSchemaName, DBUserName, DBPassword, SubscriptionId, StatusId, IsDeleted, ExpiryDate, CreatedDate, TenantId)
VALUES
(@ClientId,@SchemaName,@schemaName,@schemaUser,@SchemaPasswordHash,@Subscriptionid,1,0,@ExpiryDate,getdate(),@TenantId)

SET @SchemaId = scope_identity()


UPDATE tblTenantMaster SET SchemaId=@SchemaId Where TenantId=@TenantId

INSERT INTO tblAuthMaster(UserGuid,EmailId,MobileNumber,SecurityStamp,EmailVerified,LockoutEnabled,PasswordHash,CreatedDate,UserType,TenantId)
VALUES
(NewID(),@TenantEmail,'000-000-0000',1,1,0,@SchemaPasswordHash,GETDATE(),3,@TenantId)

SET @AUTHId = scope_identity()

INSERT INTO tblSchemaAccess (AuthId,SchemaId,IsDeleted)
VALUES (@AuthId,@SchemaId,0)

EXEC [StriveSuperAdminTest].[CON].[uspUserCreation] @schemaName,@TenantName,@schemaUser,@SchemaPasswordHash,@AuthId

END