USE [StriveTenant]
GO
/****** Object:  Table [StriveCarSalon].[tblCodeCategory]    Script Date: 7/27/2020 10:27:48 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [StriveCarSalon].[tblCodeCategory](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[Category] [varchar](50) NULL,
	[IsDeleted] [bit] NULL,
	[CreatedBy] [int] NULL,
	[CreatedDate] [datetimeoffset](7) NULL,
	[UpdatedBy] [int] NULL,
	[UpdatedDate] [datetimeoffset](7) NULL
) ON [PRIMARY]
GO
/****** Object:  Table [StriveCarSalon].[tblCodeValue]    Script Date: 7/27/2020 10:27:48 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [StriveCarSalon].[tblCodeValue](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[CategoryId] [int] NOT NULL,
	[CodeValue] [varchar](100) NULL,
	[Description] [varchar](100) NULL,
	[CodeShortValue] [varchar](10) NULL,
	[ParentId] [int] NULL,
	[SortOrder] [int] NULL,
	[IsDeleted] [bit] NULL,
	[CreatedBy] [int] NULL,
	[CreatedDate] [datetimeoffset](7) NULL,
	[UpdatedBy] [int] NULL,
	[UpdatedDate] [datetimeoffset](7) NULL
) ON [PRIMARY]
GO
