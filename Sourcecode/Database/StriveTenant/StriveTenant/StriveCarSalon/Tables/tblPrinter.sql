CREATE TABLE [dbo].[tblPrinter]
(
	[PrinterId] INT NOT NULL PRIMARY KEY, 
    [PrinterName] VARCHAR(64) NULL, 
    [IpAddress] VARCHAR(20) NULL,
	[IsActive] [bit] NULL,
	[IsDeleted] [bit] NULL,
	[CreatedBy] [int] NULL,
	[CreatedDate] [datetimeoffset](7) NULL,
	[UpdatedBy] [int] NULL,
	[UpdatedDate] [datetimeoffset](7) NULL,
	LocationId int,
	CONSTRAINT FK_PrinterLocation
	FOREIGN KEY (LocationId) REFERENCES tblLocation(LocationId)
)
