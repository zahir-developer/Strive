CREATE PROCEDURE [CON].[uspUserCreation]
(  
@SchemaName VARCHAR(64),  
@TenantName VARCHAR(64),  
@UserName VARCHAR(64),  
@PasswordHash VARCHAR(128),
@AuthId INT,
@CloneSchema VARCHAR(64)
)  
AS 


/*DECLARE @schemaName VARCHAR(20)= 'zN4';
DECLARE @TenantName VARCHAR(20) = @schemaName,
@UserName VARCHAR(20) = 'Strive'+ @schemaName +'user',
@PasswordHash VARCHAR(20) = 'pass@123',
@AuthId INT = 2567
*/  
DECLARE @SQL NVARCHAR (MAX)  
--,  
--@DEFAULTPWD NVARCHAR (16)='Pass@123'  
  
BEGIN  
SET NOCOUNT ON;  
  
/* CREATING THE NEW LOGIN FOR THE DATABASE */  
  
IF(ISNULL(@UserName,' ') != ' ' ) AND (ISNULL(@PasswordHash,' ') != ' ' )  
  
BEGIN  
  
SET @SQL = 'USE [Master]' + ';' +'  
  
IF NOT EXISTS (SELECT TOP 1 1 FROM SYS.SYSLOGINS WHERE NAME='''+@UserName+''')  
  
BEGIN  
CREATE LOGIN  ' + @UserName + ' WITH PASSWORD=''' + @PasswordHash  + ''',CHECK_EXPIRATION=OFF, CHECK_POLICY=OFF;  
END'  
  

EXEC ( @sql);  
  
END  
  
/* CREATING THE NEW SCHEMA */  
  
IF(ISNULL(@SchemaName,0) !='' )  
BEGIN  
  
SET @SQL = 'IF SCHEMA_ID('''+@SchemaName+''') IS NULL  
EXEC(''CREATE  SCHEMA  ' +  + + @SchemaName+''')' ;  
  
EXEC ( @sql);  
  
END  
  
/* USER CREATION */  
  
IF(ISNULL(@UserName,0) != '')  
  
BEGIN  
  
SET @SQL  =   
'USE [Master]' + ';' +  '  
  
IF NOT EXISTS (SELECT TOP 1 1 FROM SYS.SYSUSERS WHERE ISLOGIN=1 AND NAME='''+@UserName+''')  
CREATE  USER  ' + @UserName +  '  FOR LOGIN '  + @UserName + ';';  
  
EXEC ( @SQL);  
  
END  
  
SET @SQL = '  
IF NOT EXISTS (SELECT TOP 1 1 FROM SYS.SYSUSERS WHERE ISLOGIN=1 AND NAME='''+@UserName+''')  
CREATE USER ' + @UserName + ' FOR LOGIN ' + @UserName + ' WITH DEFAULT_SCHEMA=' + @SchemaName  + ';  
  
EXEC sp_addrolemember ''db_ddladmin'','''+@UserName+'''  
  
';  
  
EXEC ( @SQL);  
  
--/* Providing the Respective access to the New users  */  
  
SET @sql = 'GRANT SELECT, INSERT, UPDATE, DELETE, ALTER, CREATE SEQUENCE, EXECUTE ON SCHEMA :: ' + @SchemaName + ' TO '  + @UserName + ';';  
  
EXEC ( @sql);  

  
EXEC [CON].[uspCopyDbobjects] @CloneSchema,@SchemaName
EXEC [CON].[uspMasterDataInsert] @SchemaName, @AuthId, @CloneSchema
  
END
GO


