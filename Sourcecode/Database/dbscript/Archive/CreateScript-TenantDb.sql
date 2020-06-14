USE MASTER
GO 
IF DB_ID('StriveTenantDb') IS NOT NULL 
 DROP DATABASE StriveTenantDb
GO
CREATE DATABASE StriveTenantDb
GO 
USE StriveTenantDb
GO 

CREATE SCHEMA Adm
GO 
CREATE SCHEMA Cus
GO 

CREATE TABLE [Cus].[tblCustomer]
(
	  Customerid				Bigint			NOT NULL IDENTITY(1,1) CONSTRAINT PK_tblCustomer_UserRoleid PRIMARY KEY
	, FirstName					Varchar(32)		NOT NULL
	, MiddleName				Varchar(32)		NULL
	, LastName					Varchar(32)		NULL
	, ContactMail				Varchar(64)		NOT NULL
	, SSNo						Varchar(16)		NOT NULL
	, LocationId				int				NOT NULL
	, Address1					Varchar(100)	NOT NULL
	, Address2					Varchar(50)		NULL
	, City						varchar(64)		NOT NULL
	, State						varchar(64)		NOT NULL
	, Zip						varchar(12)		NOT NULL
	, CustomerTypeId			int				NOT NULL
	, CustomerCorpId			Int				NULL
	, PrimaryContactNumber		Varchar(14)		NOT NULL
	, SecondaryContactNumber	Varchar(14)		NULL
	, PrimaryContactType		Varchar(14)		NOT NULL
	, SecondaryContactType		Varchar(14)		NULL
	, MailAllowed				bit				NOT NULL
	, CustomerScoreCategory		char(2)			NULL
	, NumberofAccounts			int				NULL
	, Notes						Varchar (8000)	NULL
	, RecordNotes				Varchar (8000)	NULL
	, IsActive					bit				NOT NULL
	, CreatedDate				DateTime		NOT NULL	CONSTRAINT DF_tblCustomer_CreatedDate DEFAULT (GETDATE())
)

GO

CREATE TABLE [Cus].[tblVehicleColor]
(
	  VehicleColorid			int			NOT NULL IDENTITY(1,1) CONSTRAINT PK_tblVehicleColor_VehicleColorid PRIMARY KEY
	, VehicleColorDescription	varchar(32) NOT NULL
	, IsActive					bit			NOT NULL
	, CreatedDate				DateTime	NOT NULL	CONSTRAINT DF_tblVehicleColor_CreatedDate DEFAULT (GETDATE())
)
GO

CREATE TABLE [Cus].[tblVehicleMake]
(
	  VehicleMakeId				int			NOT NULL IDENTITY(1,1) CONSTRAINT PK_tblVehicleMake_VehicleMakeId PRIMARY KEY
	, VehicleMakeDescription	varchar(32)	NOT NULL
	, IsActive					bit			NOT NULL
	, CreatedDate				DateTime	NOT NULL	CONSTRAINT DF_tblVehicleMake_CreatedDate DEFAULT (GETDATE())
)
GO

CREATE TABLE [Cus].[tblVehicleModel]
(
	  VehicleModelId			int			NOT NULL IDENTITY(1,1) CONSTRAINT PK_tblVehicleModel_VehicleMakeId PRIMARY KEY
	, VehicleModelDescription	varchar(64)	NOT NULL
	, VehicleMakeId				int			NOT NULL
	, IsActive					bit			NOT NULL
	, CreatedDate				DateTime	NOT NULL	CONSTRAINT DF_tblVehicleModel_CreatedDate DEFAULT (GETDATE())
)
GO

CREATE TABLE [Cus].[tblServiceType]
(
	  ServiceTypeId				int			NOT NULL IDENTITY(1,1) CONSTRAINT PK_tblServiceType_ServiceTypeId PRIMARY KEY
	, ServiceTypeDescription	varchar(32)	NOT NULL
	, IsActive					bit			NOT NULL
	, CreatedDate				DateTime	NOT NULL	CONSTRAINT DF_tblServiceType_CreatedDate DEFAULT (GETDATE())
)
GO

CREATE TABLE [Cus].[tblWashInformation]
(
	  CustomerId		bigint		NOT NULL IDENTITY(1,1) CONSTRAINT PK_tblWashInformation_CustomerId PRIMARY KEY
	, VehicleMakeId		int			NOT NULL
	, VehicleModelId	Bigint		NOT NULL
	, VehicleColorid	int			NOT NULL
	, DateIn			Date		NOT NULL
	, DateOut			Date		NOT NULL
	, TimeIn			Time		NOT NULL
	, TimeOut			Time		NOT NULL
	, ResEmployeeId		int			NOT NULL
	, Amount			Decimal(9,2)NULL
	, IsActive			bit			NOT NULL	CONSTRAINT DF_tblWashInformation_IsActive DEFAULT (1)
	, CreatedDate		DateTime	NOT NULL	CONSTRAINT DF_tblWashInformation_CreatedDate DEFAULT (GETDATE())
)
GO
