/****** Object:  StoredProcedure [dbo].[uspCreateTenant]    Script Date: 25-03-2022 11:18:08 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO




CREATE PROCEDURE [dbo].[uspCreateTenant]
(@FirstName varchar(50),@LastName varchar(50),@Address varchar(50),@PhoneNumber varchar(50),@MobileNumber varchar(50),@TenantName varchar(50), @TenantEmail varchar(100), @Subscriptionid int=null, @SchemaPasswordHash varchar(500),@ExpiryDate date
,@SubscriptionDate date,@PaymentDate date,@Locations int,@State int,@City int,@ZipCode varchar(10),@IsActive bit)
AS
BEGIN

/*
DECLARE @FirstName varchar(50) = 'Telliant',
@LastName varchar(50) = 'H',
@Address varchar(50) = 'Ad',
@PhoneNumber varchar(50) = NULL,
@MobileNumber varchar(50)= NULL,
@TenantName varchar(50) = 'Telliant', 
@TenantEmail varchar(100) = 'telliant@telliant.net',
@Subscriptionid int = 0,
@SchemaPasswordHash varchar(200)='mAxzxAaaMfZlFAr4fQpcDsDvMTUtDIAk-J4nw/g3FlcxsB5qWh8OiVr0ilyg=',
@ExpiryDate date = NULL
,@SubscriptionDate date = NULL,@PaymentDate date=NULL,@Locations int = 5,@State int,@City int,@ZipCode varchar(10),@IsActive bit = 1
*/

DECLARE @DefaultCloneSchema VARCHAR(64) = 'Strive_TenantSetup';

SET NOCOUNT ON

DECLARE @schemaName VARCHAR(60);
DECLARE @schemaUser VARCHAR(64);
DECLARE @TenantId INT
DECLARE @SchemaId INT
DECLARE @ClientId INT
DECLARE @AUTHId INT

Insert Into tblclient(FirstName,LastName, ClientEmail,Address,PhoneNumber,MobileNumber,State ,City ,ZipCode )
 values (@FirstName,@LastName, @TenantEmail,@Address,@PhoneNumber,@MobileNumber,@State,@City ,@ZipCode)
set @ClientId = SCOPE_IDENTITY()


DECLARE @EmailIndex INT = CHARINDEX('@', @TenantEmail);

--SET @schemaName ='Strive_' + CAST(CAST(RAND() * 1000000 AS int) AS nvarchar(50)) + '_' +@TenantName

SET @schemaName ='Strive_' + SUBSTRING(@TenantEmail, 0, @EmailIndex)

SET @schemaUser = @schemaName +'user'

DECLARE @Guid NVARCHAR(50) = NEWID();

INSERT INTO tblTenantMaster(TenantGuid, SubscriptionId,clientid,IsActive, IsDeleted,EmpSize, ExpiryDate, CreatedDate,SubscriptionDate,PaymentDate)
values
(@Guid,@Subscriptionid,@ClientId,@IsActive,0,100,@ExpiryDate,getdate(),@SubscriptionDate,@PaymentDate)

SET @TenantId = scope_identity()

INSERT INTO tblTenantDetail(TenantId,TenantName, MaxLocation, ColorTheme, Currency,TelantLogoUrl)
VALUES
(@TenantId,@TenantName, @Locations, 'Default','USD','default.png')

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

EXEC [StriveTenant_Setup_Jan22].[CON].[uspUserCreation] @schemaName,@TenantName,@schemaUser,@SchemaPasswordHash,@AuthId,@DefaultCloneSchema

Select @Guid

END




GO

