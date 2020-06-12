USE MASTER
GO 
IF DB_ID('Mammoth_ADMDb') IS NOT NULL 
 DROP DATABASE Mammoth_ADMDb
GO
CREATE DATABASE Mammoth_ADMDb
GO 
USE Mammoth_ADMDb
GO 

CREATE SCHEMA Adm
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

ALTER TABLE [Adm].[tblUser]
ADD CONSTRAINT FK_tblUser_UserRoleid FOREIGN KEY (UserRoleid) REFERENCES [Adm].[tblUserRole](UserRoleid)
GO

ALTER TABLE [Adm].[tblRolePermission]
ADD CONSTRAINT FK_tblRolePermission_UserRoleid FOREIGN KEY (UserRoleid) REFERENCES [Adm].[tblUserRole](UserRoleid)
GO

ALTER TABLE [Adm].[tblRolePermission]
ADD CONSTRAINT FK_tblRolePermission_PermissionId FOREIGN KEY (PermissionId) REFERENCES [Adm].[tblPermission](PermissionId)
GO

ALTER TABLE [Adm].[tblEmployee]
ADD CONSTRAINT FK_tblEmployee_EmployeeRoleId FOREIGN KEY (EmployeeRoleId) REFERENCES [Adm].[tblEmployeeRole](EmployeeRoleId)
GO

ALTER TABLE [Adm].[tblEmployee]
ADD CONSTRAINT FK_tblEmployee_LocationId FOREIGN KEY (LocationId) REFERENCES [Adm].[tblLocation](LocationId)
GO

ALTER TABLE [Adm].[tblBay]
ADD CONSTRAINT FK_tblBay_LocationId FOREIGN KEY (LocationId) REFERENCES [Adm].[tblLocation](LocationId)
GO