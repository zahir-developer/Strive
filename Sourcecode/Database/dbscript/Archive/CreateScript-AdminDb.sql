USE MASTER
GO 
IF DB_ID('StriveAdminDb') IS NOT NULL 
 DROP DATABASE StriveAdminDb
GO
CREATE DATABASE StriveAdminDb
GO 
USE StriveAdminDb
GO 

CREATE SCHEMA Adm
GO 
CREATE SCHEMA Cus
GO 

CREATE TABLE [Adm].[tblUser]
(
	  UserId			int			NOT NULL IDENTITY(1,1) CONSTRAINT PK_tblUser_UserId PRIMARY KEY
	, LoginDescription	varchar(32) NOT NULL
	, FirstName			varchar(32) NOT NULL
	, MiddleName		varchar(32) NULL
	, LastName			varchar(32) NULL
	, ContactNumber		varchar(14) NOT NULL
	, EmailVerified		smallint	NOT NULL
	, IsActive			bit			NOT NULL
	, PasswordHash		varchar(64)	NOT NULL
	, UserRoleid		int			NOT NULL
	, CreatedDate		DateTime	NOT NULL	CONSTRAINT DF_tblUser_CreatedDate DEFAULT (GETDATE())
)

GO

CREATE TABLE [Adm].[tblUserRole]
(
	  UserRoleid			int			NOT NULL IDENTITY(1,1) CONSTRAINT PK_tblUserRole_UserRoleid PRIMARY KEY
	, UserRoleDescription	varchar(32) NOT NULL
	, IsActive				bit			NOT NULL
	, CreatedDate			DateTime	NOT NULL	CONSTRAINT DF_tblUserRole_CreatedDate DEFAULT (GETDATE())
)
GO

CREATE TABLE [Adm].[tblPermission]
(
	  PermissionId			int			NOT NULL IDENTITY(1,1) CONSTRAINT PK_tblPermission_PermissionId PRIMARY KEY
	, PermissionName		varchar(32)	NOT NULL
	, IsActive				bit			NOT NULL
	, CreatedDate			DateTime	NOT NULL	CONSTRAINT DF_tblPermission_CreatedDate DEFAULT (GETDATE())
)
GO

CREATE TABLE [Adm].[tblRolePermission]
(
	  RolePermissionId		int			NOT NULL IDENTITY(1,1) CONSTRAINT PK_tblRolePermission_RolePermissionId PRIMARY KEY
	, RolePermissionName	varchar(32)	NOT NULL
	, IsActive				bit			NOT NULL
	, UserRoleId			int			NOT NULL
	, PermissionId			int			NOT NULL
	, CreatedDate			DateTime	NOT NULL	CONSTRAINT DF_tblRolePermission_CreatedDate DEFAULT (GETDATE())
)

GO

CREATE TABLE [Adm].[tblEmployee]
(
	  EmployeeId			int			NOT NULL IDENTITY(1,1) CONSTRAINT PK_tblEmployee_EmployeeId PRIMARY KEY
	, EmployeeFirstName		varchar(32)	NOT NULL
	, EmployeeMiddleName	varchar(32) NOT NULL
	, EmployeeLastName		varchar(32)	NOT NULL
	, EmployeeRoleId		int			NOT NULL
	, ContactNumber			varchar(14)	NOT NULL
	, LocationId			int			NOT NULL
	, ContactMail			varchar(64)	NULL
	, Address				Varchar(200)NULL
	, City					varchar(64)	NULL
	, State					varchar(64)	NULL
	, Zip					varchar(12)	NULL
	, PayRate				Decimal(9,2)NULL
	, SickRate				Decimal(9,2)NULL
	, VacRate				Decimal(9,2)NULL
	, ComRate				Decimal(9,2)NULL
	, HireDate				Date		NULL
	, BirthDate				Date		NULL
	, Gender				Varchar(12) NOT NULL
	, SSNo					Decimal(9,0)NOT NULL
	, MaritalStatus			Varchar(12)	NULL
	, Exempt				Decimal(9,2)NULL
	, Salery				Decimal(9,2)NULL
	, LRT					Datetime	NOT NULL
	, TIP					bit			NOT NULL
	, Citizen				bit			NOT NULL
	, AlienNo				Varchar(12)	NULL
	, IsActive				bit			NOT NULL
	, CreatedDate			DateTime	NOT NULL	CONSTRAINT DF_tblEmployee_CreatedDate DEFAULT (GETDATE())
)
GO

CREATE TABLE [Adm].[tblEmployeeRole]
(
	  EmployeeRoleId			int			NOT NULL IDENTITY(1,1) CONSTRAINT PK_tblEmployeeRole_EmployeeRoleId PRIMARY KEY
	, EmployeeRoleDescription	varchar(32)	NOT NULL
	, IsActive					bit			NOT NULL
	, CreatedDate				DateTime	NOT NULL	CONSTRAINT DF_tblEmployeeRole_CreatedDate DEFAULT (GETDATE())
)
GO

CREATE TABLE [Adm].[tblLocation]
(
	  LocationId			int			NOT NULL IDENTITY(1,1) CONSTRAINT PK_tblLocation_LocationId PRIMARY KEY
	, EmployeeRoleDescription	varchar(32)	NOT NULL
	, IsActive					bit			NOT NULL
	, CreatedDate				DateTime	NOT NULL	CONSTRAINT DF_tblLocation_CreatedDate DEFAULT (GETDATE())
)
GO

CREATE TABLE [Adm].[tblBay]
(
	  BayId	int			NOT NULL IDENTITY(1,1) CONSTRAINT PK_tblBay_BayId PRIMARY KEY
	, BayDescription	varchar(32)
	, LocationID	int
	, IsActive	bit
	, CreatedDate				DateTime	NOT NULL	CONSTRAINT DF_tblBay_CreatedDate DEFAULT (GETDATE())

)
GO
