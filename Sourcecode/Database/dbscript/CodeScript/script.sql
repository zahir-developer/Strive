USE [StriveTenant]
GO
/****** Object:  Table [StriveCarSalon].[tblCodeCategory]    Script Date: 7/29/2020 11:27:40 PM ******/
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
	[UpdatedDate] [datetimeoffset](7) NULL,
 CONSTRAINT [PK_tblCodeCategory] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [StriveCarSalon].[tblCodeValue]    Script Date: 7/29/2020 11:27:46 PM ******/
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
	[UpdatedDate] [datetimeoffset](7) NULL,
 CONSTRAINT [PK_tblCodeValue] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
ALTER TABLE [StriveCarSalon].[tblCodeValue]  WITH CHECK ADD  CONSTRAINT [FK_tblCodeValue_CategoryId] FOREIGN KEY([CategoryId])
REFERENCES [StriveCarSalon].[tblCodeCategory] ([id])
GO
ALTER TABLE [StriveCarSalon].[tblCodeValue] CHECK CONSTRAINT [FK_tblCodeValue_CategoryId]
GO
