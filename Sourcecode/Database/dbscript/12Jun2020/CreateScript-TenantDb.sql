USE MASTER
GO 
IF DB_ID('Mammoth_TenantDb') IS NOT NULL 
 DROP DATABASE Mammoth_TenantDb
GO
CREATE DATABASE Mammoth_TenantDb
GO 
USE Mammoth_TenantDb
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
	, CustomerTypeId			Smallint		NOT NULL
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

CREATE TABLE [Cus].[tblCorpCustomer]
(
	  CorpCustomerId			int			NOT NULL IDENTITY(1,1) CONSTRAINT PK_tblCorpCustomer_CorpCustomerId PRIMARY KEY
	, CorpCustomerDescription	Varchar(64) NOT NULL
	, IsActive					bit			NOT NULL
	, CreatedDate				DateTime	NOT NULL	CONSTRAINT DF_tblCorpCustomer_CreatedDate DEFAULT (GETDATE())
)
GO

CREATE TABLE [Cus].[tblCustomerType]
(
	  CustomerTypeId			Smallint	NOT NULL IDENTITY(1,1) CONSTRAINT PK_tblCustomerType_CustomerTypeId PRIMARY KEY
	, CustomerTypeDescription	varchar(12) NOT NULL
	, IsActive					bit			NOT NULL
	, CreatedDate				DateTime	NOT NULL	CONSTRAINT DF_tblCustomerType_CreatedDate DEFAULT (GETDATE())
)
GO

CREATE TABLE [Cus].[tblVehicleColor]
(
	  VehicleColorid			smallint	NOT NULL IDENTITY(1,1) CONSTRAINT PK_tblVehicleColor_VehicleColorid PRIMARY KEY
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
	  ServiceTypeId				Smallint	NOT NULL IDENTITY(1,1) CONSTRAINT PK_tblServiceType_ServiceTypeId PRIMARY KEY
	, ServiceTypeDescription	varchar(32)	NOT NULL
	, IsActive					bit			NOT NULL
	, CreatedDate				DateTime	NOT NULL	CONSTRAINT DF_tblServiceType_CreatedDate DEFAULT (GETDATE())
)
GO

CREATE TABLE [Cus].[tblLead]
(
	  LeadId			bigint		NOT NULL IDENTITY(1,1) CONSTRAINT PK_tblLead_LeadId PRIMARY KEY
	, CustomerId		bigint		NOT NULL 
	, VehicleMakeId		int			NOT NULL
	, VehicleModelId	int		NOT NULL
	, VehicleColorid	smallint	NOT NULL
	, DateIn			Date		NOT NULL
	, DateOut			Date		NOT NULL
	, TimeIn			Time		NOT NULL
	, TimeOut			Time		NOT NULL
	, CheckOutId		smallint	NOT NULL
	, SalesrepId		int			NOT NULL
	, ServiceTypeId		Smallint	NOT NULL
	, CategoryTypeId	Smallint	NOT NULL
	, PaymentStatusId	Smallint	NOT NULL
	, Amount			Decimal(9,2)NULL
	, IsActive			bit			NOT NULL	CONSTRAINT DF_tblWashInformation_IsActive DEFAULT (1)
	, CreatedDate		DateTime	NOT NULL	CONSTRAINT DF_tblWashInformation_CreatedDate DEFAULT (GETDATE())
)
GO

CREATE TABLE [Cus].[tblCategoryType]
(
	  CategoryTypeId			Smallint	NOT NULL IDENTITY(1,1) CONSTRAINT PK_tblCategoryType_CategoryTypeId PRIMARY KEY
	, CategoryTypeDescription	varchar(32)	NOT NULL
	, IsActive					bit			NOT NULL
	, CreatedDate				DateTime	NOT NULL	CONSTRAINT DF_tblCategoryType_CreatedDate DEFAULT (GETDATE())
)
GO

CREATE TABLE [Cus].[tblPaymentStatus]
(
	  PaymentStatusId			Smallint	NOT NULL IDENTITY(1,1) CONSTRAINT PK_tblPaymentStatus_PaymentStatusId PRIMARY KEY
	, PaymentStatusDescription	varchar(32)	NOT NULL
	, IsActive					bit			NOT NULL
	, CreatedDate				DateTime	NOT NULL	CONSTRAINT DF_tblPaymentStatus_CreatedDate DEFAULT (GETDATE())
)
GO

CREATE TABLE [Cus].[tblCheckOut]
(
	  CheckOutId				Smallint	NOT NULL IDENTITY(1,1) CONSTRAINT PK_tblCheckOutStatus_CheckOutId PRIMARY KEY
	, CheckOutDescription		varchar(32)	NOT NULL
	, IsActive					bit			NOT NULL
	, CreatedDate				DateTime	NOT NULL	CONSTRAINT DF_tblCheckOutStatus_CreatedDate DEFAULT (GETDATE())
)
GO


ALTER TABLE [Cus].[tblCustomer]
ADD CONSTRAINT FK_tblCustomer_CustomerTypeId FOREIGN KEY (CustomerTypeId) REFERENCES [Cus].[tblCustomerType](CustomerTypeId)
GO

ALTER TABLE [Cus].[tblVehicleModel]
ADD CONSTRAINT FK_tblVehicleModel_VehicleMakeId FOREIGN KEY (VehicleMakeId) REFERENCES [Cus].[tblVehicleMake](VehicleMakeId)
GO

ALTER TABLE [Cus].[tblLead]
ADD CONSTRAINT FK_tblLead_CustomerId FOREIGN KEY (CustomerId) REFERENCES [Cus].[tblCustomer](CustomerId)
GO

ALTER TABLE [Cus].[tblLead]
ADD CONSTRAINT FK_tblLead_VehicleMakeId FOREIGN KEY (VehicleMakeId) REFERENCES [Cus].[tblVehicleMake](VehicleMakeId)
GO

ALTER TABLE [Cus].[tblLead]
ADD CONSTRAINT FK_tblLead_VehicleModelId FOREIGN KEY (VehicleModelId) REFERENCES [Cus].[tblVehicleModel](VehicleModelId)
GO

ALTER TABLE [Cus].[tblLead]
ADD CONSTRAINT FK_tblLead_VehicleColorid FOREIGN KEY (VehicleColorid) REFERENCES [Cus].[tblVehicleColor](VehicleColorid)
GO

ALTER TABLE [Cus].[tblLead]
ADD CONSTRAINT FK_tblLead_CheckOutId FOREIGN KEY (CheckOutId) REFERENCES [Cus].[tblCheckOut](CheckOutId)
GO

ALTER TABLE [Cus].[tblLead]
ADD CONSTRAINT FK_tblLead_ServiceTypeId FOREIGN KEY (ServiceTypeId) REFERENCES [Cus].[tblServiceType](ServiceTypeId)
GO

ALTER TABLE [Cus].[tblLead]
ADD CONSTRAINT FK_tblLead_CategoryTypeId FOREIGN KEY (CategoryTypeId) REFERENCES [Cus].[tblCategoryType](CategoryTypeId)
GO

ALTER TABLE [Cus].[tblLead]
ADD CONSTRAINT FK_tblLead_PaymentStatusId FOREIGN KEY (PaymentStatusId) REFERENCES [Cus].[tblPaymentStatus](PaymentStatusId)
GO

