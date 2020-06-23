USE [MPOS]
GO

/****** Object:  Table [dbo].[ASPNETSecurity]    Script Date: 1/17/2016 3:50:30 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

SET ANSI_PADDING ON
GO

CREATE TABLE [dbo].[ASPNETSecurity](
	[LocationID] [int] NOT NULL,
	[UserID] [int] IDENTITY(1,1) NOT NULL,
	[UserName] [varchar](50) NOT NULL,
	[Password] [nchar](16) NOT NULL,
	[Role] [int] NOT NULL,
	[CurrentIP] [nchar](15) NULL,
	[Active] [bit] NULL,
 CONSTRAINT [PK_ASPNETSecurity] PRIMARY KEY CLUSTERED 
(
	[UserID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON)
)

GO

SET ANSI_PADDING OFF
GO

/****** Object:  Table [dbo].[cashd]    Script Date: 1/17/2016 3:50:32 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[cashd](
	[LocationID] [int] NOT NULL,
	[cashdid] [int] NOT NULL,
	[ndate] [datetime] NOT NULL,
	[DrawerNo] [int] NULL,
	[CIuserID] [int] NULL,
	[COUserID] [int] NULL,
	[CIp] [int] NULL,
	[CIn] [int] NULL,
	[CId] [int] NULL,
	[CIq] [int] NULL,
	[CIh] [int] NULL,
	[CI1] [int] NULL,
	[CI5] [int] NULL,
	[CI10] [int] NULL,
	[CI20] [int] NULL,
	[CI50] [int] NULL,
	[CI100] [int] NULL,
	[COp] [int] NULL,
	[COn] [int] NULL,
	[COd] [int] NULL,
	[COq] [int] NULL,
	[COh] [int] NULL,
	[CO1] [int] NULL,
	[CO5] [int] NULL,
	[CO10] [int] NULL,
	[CO20] [int] NULL,
	[CO50] [int] NULL,
	[CO100] [int] NULL,
	[CITime] [smalldatetime] NULL,
	[COTime] [smalldatetime] NULL,
	[CITotal] [money] NULL,
	[COTotal] [money] NULL,
	[CIRp] [int] NULL,
	[CIRn] [int] NULL,
	[CIRd] [int] NULL,
	[CIRq] [int] NULL,
	[CORp] [int] NULL,
	[CORn] [int] NULL,
	[CORd] [int] NULL,
	[CORq] [int] NULL,
	[COChecks] [money] NULL,
	[COCreditCards] [money] NULL,
	[COCreditCards2] [money] NULL,
	[COPayouts] [money] NULL,
	[COCreditCards3] [money] NULL,
	[COCreditCards4] [money] NULL,
	[cd] [money] NULL,
	[cdday] [int] NULL,
	[bcday] [int] NULL,
	[amxday] [int] NULL,
	[dcday] [int] NULL,
	[dsday] [int] NULL,
	[md] [money] NULL,
	[pcchrg] [money] NULL,
 CONSTRAINT [PK_cashd] PRIMARY KEY CLUSTERED 
(
	[LocationID] ASC,
	[cashdid] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON)
)

GO

/****** Object:  Table [dbo].[client]    Script Date: 1/17/2016 3:50:41 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

SET ANSI_PADDING ON
GO

CREATE TABLE [dbo].[client](
	[clientid] [int] NOT NULL,
	[LocationID] [int] NOT NULL,
	[fname] [varchar](50) NULL,
	[lname] [varchar](50) NULL,
	[addr1] [varchar](50) NULL,
	[addr2] [varchar](50) NULL,
	[city] [char](20) NULL,
	[st] [char](2) NULL,
	[zip] [nchar](5) NULL,
	[Ctype] [int] NULL,
	[C_Corp] [int] NULL,
	[Phone] [char](14) NULL,
	[Phone2] [char](14) NULL,
	[PhoneType] [char](10) NULL,
	[Phone2Type] [char](10) NULL,
	[Email] [varchar](80) NULL,
	[NoEmail] [bit] NULL,
	[Score] [char](1) NULL,
	[status] [int] NULL,
	[account] [bit] NULL,
	[StartDT] [smalldatetime] NULL,
	[Notes] [text] NULL,
	[RecNote] [text] NULL,
 CONSTRAINT [PK_client] PRIMARY KEY CLUSTERED 
(
	[clientid] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON)
)

GO

SET ANSI_PADDING OFF
GO

/****** Object:  Table [dbo].[CustAcc]    Script Date: 1/17/2016 3:50:45 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

SET ANSI_PADDING ON
GO

CREATE TABLE [dbo].[CustAcc](
	[CustAccID] [int] NOT NULL,
	[LocationID] [int] NOT NULL,
	[ClientID] [int] NOT NULL,
	[VehID] [int] NULL,
	[CurrentAmt] [money] NULL,
	[ActiveDte] [smalldatetime] NULL,
	[LastUpdate] [smalldatetime] NULL,
	[LastUpdateBy] [int] NULL,
	[Type] [int] NULL,
	[Status] [int] NULL,
	[MonthlyCharge] [money] NULL,
	[Limit] [money] NULL,
	[CCno] [char](20) NULL,
	[cctype] [int] NULL,
	[CCEXP] [char](5) NULL,
 CONSTRAINT [PK_CustAcc_1] PRIMARY KEY CLUSTERED 
(
	[CustAccID] ASC,
	[LocationID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON)
)

GO

SET ANSI_PADDING OFF
GO

/****** Object:  Table [dbo].[CustAccHist]    Script Date: 1/17/2016 3:50:48 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

SET ANSI_PADDING ON
GO

CREATE TABLE [dbo].[CustAccHist](
	[CustAccID] [int] NOT NULL,
	[CustAccTID] [int] NOT NULL,
	[TXLocationID] [int] NOT NULL,
	[TXCustID] [int] NOT NULL,
	[TXRecID] [int] NULL,
	[TXType] [char](20) NULL,
	[TXAmt] [money] NULL,
	[TXDte] [smalldatetime] NULL,
	[TXuser] [int] NULL,
	[TXNote] [text] NULL,
	[Archive] [bit] NULL,
	[InvoiceID] [int] NULL,
 CONSTRAINT [PK_CustAccHist] PRIMARY KEY CLUSTERED 
(
	[CustAccID] ASC,
	[CustAccTID] ASC,
	[TXLocationID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON)
)

GO

SET ANSI_PADDING OFF
GO

/****** Object:  Table [dbo].[CUSTACCSTM]    Script Date: 1/17/2016 3:50:51 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[CUSTACCSTM](
	[CustAccID] [int] NOT NULL,
	[InvoiceID] [int] NOT NULL,
	[STMDate] [smalldatetime] NULL,
	[STMBY] [int] NULL,
	[STMAMT] [money] NULL,
	[PMTDate] [smalldatetime] NULL,
	[PMTBy] [int] NULL,
	[PMTAMT] [money] NULL,
	[currentamt] [money] NULL,
	[Amount] [money] NULL,
 CONSTRAINT [PK_CUSTACCSTM] PRIMARY KEY CLUSTERED 
(
	[CustAccID] ASC,
	[InvoiceID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON)
)

GO

/****** Object:  Table [dbo].[DetailComp]    Script Date: 1/17/2016 3:50:53 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[DetailComp](
	[LocationID] [int] NOT NULL,
	[DetailCompID] [int] NOT NULL,
	[UserID] [int] NOT NULL,
	[RecID] [int] NOT NULL,
	[CdateTime] [datetime] NULL,
	[ComAmt] [money] NULL,
	[paid] [bit] NULL,
	[ComPer] [int] NULL,
	[Comm] [money] NULL,
 CONSTRAINT [PK_DetailComp_1] PRIMARY KEY CLUSTERED 
(
	[LocationID] ASC,
	[DetailCompID] ASC,
	[UserID] ASC,
	[RecID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON)
)

GO

/****** Object:  Table [dbo].[DetailItem]    Script Date: 1/17/2016 3:50:55 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

SET ANSI_PADDING ON
GO

CREATE TABLE [dbo].[DetailItem](
	[DetailItemID] [int] NOT NULL,
	[ItemDesc] [varchar](50) NULL,
 CONSTRAINT [PK_DetailItem] PRIMARY KEY CLUSTERED 
(
	[DetailItemID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON)
)

GO

SET ANSI_PADDING OFF
GO

/****** Object:  Table [dbo].[DetailProd]    Script Date: 1/17/2016 3:50:55 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[DetailProd](
	[DetailItemID] [int] NOT NULL,
	[ProdID] [int] NOT NULL,
 CONSTRAINT [PK_DetailProd] PRIMARY KEY CLUSTERED 
(
	[DetailItemID] ASC,
	[ProdID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON)
)

GO

/****** Object:  Table [dbo].[GiftCard]    Script Date: 1/17/2016 3:50:56 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

SET ANSI_PADDING ON
GO

CREATE TABLE [dbo].[GiftCard](
	[GiftCardID] [char](10) NOT NULL,
	[LocationID] [int] NULL,
	[RecID] [int] NULL,
	[ActiveDte] [smalldatetime] NULL,
	[CurrentAmt] [money] NULL,
 CONSTRAINT [PK_GiftCard] PRIMARY KEY CLUSTERED 
(
	[GiftCardID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON)
)

GO

SET ANSI_PADDING OFF
GO

/****** Object:  Table [dbo].[GiftCardHist]    Script Date: 1/17/2016 3:50:57 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

SET ANSI_PADDING ON
GO

CREATE TABLE [dbo].[GiftCardHist](
	[LocationID] [int] NOT NULL,
	[GiftCardTID] [int] NOT NULL,
	[GiftCardID] [char](10) NOT NULL,
	[TransDte] [smalldatetime] NULL,
	[TransType] [char](10) NULL,
	[TransAmt] [money] NULL,
	[TransUserID] [int] NULL,
	[RecID] [int] NULL,
 CONSTRAINT [PK_GiftCardHist_1] PRIMARY KEY CLUSTERED 
(
	[LocationID] ASC,
	[GiftCardTID] ASC,
	[GiftCardID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON)
)

GO

SET ANSI_PADDING OFF
GO

/****** Object:  Table [dbo].[LM_List]    Script Date: 1/17/2016 3:50:58 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

SET ANSI_PADDING ON
GO

CREATE TABLE [dbo].[LM_List](
	[ListType] [smallint] NOT NULL,
	[Description] [varchar](255) NULL,
	[valuewidth] [tinyint] NULL,
	[descwidth] [tinyint] NULL,
	[Type] [tinyint] NULL,
	[DataType] [char](1) NULL,
 CONSTRAINT [PK_LM_List] PRIMARY KEY CLUSTERED 
(
	[ListType] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON)
)

GO

SET ANSI_PADDING OFF
GO

/****** Object:  Table [dbo].[LM_ListItem]    Script Date: 1/17/2016 3:50:59 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

SET ANSI_PADDING ON
GO

CREATE TABLE [dbo].[LM_ListItem](
	[ListType] [smallint] NOT NULL,
	[ListCode] [smallint] NOT NULL,
	[ListValue] [varchar](255) NULL,
	[ListDesc] [varchar](255) NULL,
	[ItemType] [tinyint] NULL,
	[ItemOrder] [smallint] NULL,
	[Active] [bit] NOT NULL,
 CONSTRAINT [PK_LM_ListItem] PRIMARY KEY CLUSTERED 
(
	[ListType] ASC,
	[ListCode] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON)
)

GO

SET ANSI_PADDING OFF
GO

/****** Object:  Table [dbo].[LM_Locations]    Script Date: 1/17/2016 3:51:01 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

SET ANSI_PADDING ON
GO

CREATE TABLE [dbo].[LM_Locations](
	[LocationID] [int] NOT NULL,
	[LocationDesc] [varchar](255) NULL,
	[Address1] [varchar](150) NULL,
	[Address2] [varchar](150) NULL,
	[City] [varchar](150) NULL,
	[State] [char](2) NULL,
	[PostalCode] [varchar](10) NULL,
	[Phone] [varchar](25) NULL,
	[CurrentStatus] [varchar](50) NULL,
	[TaxRate] [varchar](50) NULL,
	[TicketPrinterURL] [varchar](50) NULL,
	[RecPrinterURL] [varchar](50) NULL,
	[SiteURL] [varchar](50) NULL,
 CONSTRAINT [PK_LM_Locations] PRIMARY KEY CLUSTERED 
(
	[LocationID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON)
)

GO

SET ANSI_PADDING OFF
GO

/****** Object:  Table [dbo].[LM_Users]    Script Date: 1/17/2016 3:51:03 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

SET ANSI_PADDING ON
GO

CREATE TABLE [dbo].[LM_Users](
	[LocationID] [int] NOT NULL,
	[UserID] [smallint] NOT NULL,
	[LoginID] [varchar](16) NOT NULL,
	[Password] [varchar](16) NULL,
	[Initials] [varchar](5) NULL,
	[LastName] [varchar](25) NULL,
	[FirstName] [varchar](25) NULL,
	[Phone] [varchar](25) NULL,
	[Fax] [varchar](25) NULL,
	[Email] [varchar](50) NULL,
	[Active] [bit] NOT NULL,
	[CurrentIP] [varchar](15) NULL,
	[RoleID] [int] NULL,
	[Address] [varchar](80) NULL,
	[City] [varchar](50) NULL,
	[State] [char](2) NULL,
	[Zip] [char](5) NULL,
	[PayRate] [nchar](10) NULL,
	[SickRate] [nchar](10) NULL,
	[VacRate] [nchar](10) NULL,
	[ComRate] [nchar](10) NULL,
	[Hire] [datetime] NULL,
	[birth] [datetime] NULL,
	[Gender] [char](10) NULL,
	[SSNo] [char](11) NULL,
	[MStat] [char](1) NULL,
	[Exempt] [smallint] NULL,
	[Salery] [nchar](10) NULL,
	[LRT] [datetime] NULL,
	[TIP] [bit] NULL,
	[citizen] [tinyint] NULL,
	[AlienNo] [varchar](50) NULL,
	[AuthDate] [smalldatetime] NULL,
 CONSTRAINT [PK_LM_Users] PRIMARY KEY CLUSTERED 
(
	[LocationID] ASC,
	[UserID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON)
)

GO

SET ANSI_PADDING OFF
GO

/****** Object:  Table [dbo].[LM_zipcodes]    Script Date: 1/17/2016 3:51:09 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

SET ANSI_PADDING ON
GO

CREATE TABLE [dbo].[LM_zipcodes](
	[city] [char](20) NULL,
	[st] [char](2) NULL,
	[zip] [char](5) NOT NULL,
 CONSTRAINT [PK_LM_zipcodes] PRIMARY KEY CLUSTERED 
(
	[zip] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON)
)

GO

SET ANSI_PADDING OFF
GO

/****** Object:  Table [dbo].[Payment]    Script Date: 1/17/2016 3:51:09 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

SET ANSI_PADDING ON
GO

CREATE TABLE [dbo].[Payment](
	[PMTID] [int] NOT NULL,
	[InvoiceID] [int] NULL,
	[PMTAMT] [money] NULL,
	[PAYDate] [smalldatetime] NULL,
	[PMTUSER] [int] NULL,
	[PMTDesc] [varchar](50) NULL,
	[PMTNote] [text] NULL,
 CONSTRAINT [PK_Payment] PRIMARY KEY CLUSTERED 
(
	[PMTID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON)
)

GO

SET ANSI_PADDING OFF
GO

/****** Object:  Table [dbo].[PrintRec]    Script Date: 1/17/2016 3:51:11 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

SET ANSI_PADDING ON
GO

CREATE TABLE [dbo].[PrintRec](
	[PrintID] [int] IDENTITY(1,1) NOT NULL,
	[LocationID] [int] NULL,
	[RecID] [int] NULL,
	[PrinterName] [char](20) NULL,
 CONSTRAINT [PK_PrintRec] PRIMARY KEY CLUSTERED 
(
	[PrintID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON)
)

GO

SET ANSI_PADDING OFF
GO

/****** Object:  Table [dbo].[PrintTicket]    Script Date: 1/17/2016 3:51:12 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

SET ANSI_PADDING ON
GO

CREATE TABLE [dbo].[PrintTicket](
	[PrintID] [int] IDENTITY(1,1) NOT NULL,
	[LocationID] [int] NOT NULL,
	[Recid] [int] NOT NULL,
	[Printer] [int] NOT NULL,
	[Report] [varchar](50) NOT NULL,
 CONSTRAINT [PK_PrintTicket] PRIMARY KEY CLUSTERED 
(
	[PrintID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON)
)

GO

SET ANSI_PADDING OFF
GO

/****** Object:  Table [dbo].[ProdKit]    Script Date: 1/17/2016 3:51:13 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[ProdKit](
	[KitID] [int] NOT NULL,
	[ProductID] [int] NOT NULL,
 CONSTRAINT [PK_ProdKit] PRIMARY KEY CLUSTERED 
(
	[KitID] ASC,
	[ProductID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON)
)

GO

/****** Object:  Table [dbo].[ProdOpt]    Script Date: 1/17/2016 3:51:13 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[ProdOpt](
	[OptID] [int] NOT NULL,
	[ProductID] [int] NOT NULL,
 CONSTRAINT [PK_ProdOpt] PRIMARY KEY CLUSTERED 
(
	[OptID] ASC,
	[ProductID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON)
)

GO

/****** Object:  Table [dbo].[Product]    Script Date: 1/17/2016 3:51:14 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

SET ANSI_PADDING ON
GO

CREATE TABLE [dbo].[Product](
	[ProdID] [int] NOT NULL,
	[cat] [int] NOT NULL,
	[dept] [int] NOT NULL,
	[Descript] [char](20) NOT NULL,
	[Number] [tinyint] NULL,
	[Price] [money] NOT NULL,
	[Comm] [money] NULL,
	[Inv] [tinyint] NOT NULL,
	[Taxable] [tinyint] NOT NULL,
	[PerAdj] [tinyint] NOT NULL,
	[KIT] [tinyint] NULL,
	[KitID] [int] NULL,
	[Bcode] [char](16) NULL,
	[EstTime] [numeric](5, 2) NULL,
	[vendor] [int] NULL,
	[altvendor] [int] NULL,
	[vpartnumb] [varchar](20) NULL,
	[Manufact] [varchar](20) NULL,
	[Model] [varchar](20) NULL,
	[Cost] [money] NULL,
	[vendcost] [money] NULL,
	[status] [tinyint] NULL,
	[editby] [int] NULL,
	[edited] [datetime] NULL,
	[createdby] [int] NULL,
	[created] [datetime] NULL,
	[Opt] [tinyint] NULL,
	[OptID] [int] NULL,
	[Notes] [text] NULL,
 CONSTRAINT [PK_Product] PRIMARY KEY CLUSTERED 
(
	[ProdID] ASC,
	[cat] ASC,
	[dept] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON)
)

GO

SET ANSI_PADDING OFF
GO

/****** Object:  Table [dbo].[ProStat]    Script Date: 1/17/2016 3:51:19 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

SET ANSI_PADDING ON
GO

CREATE TABLE [dbo].[ProStat](
	[LocationID] [int] NOT NULL,
	[ProDate] [char](6) NOT NULL,
	[Washes] [int] NULL,
	[Details] [int] NULL,
	[Extras] [decimal](18, 2) NULL,
	[TotalHours] [decimal](18, 0) NULL,
	[TotalRev] [decimal](18, 0) NULL,
	[TotalLabor] [decimal](18, 0) NULL,
	[PerRev] [decimal](18, 0) NULL,
	[TotalSalery] [decimal](18, 0) NULL,
	[DetailComp] [decimal](18, 0) NULL,
	[Washers] [int] NULL,
	[Detailers] [int] NULL,
	[Greaters] [int] NULL,
	[Managers] [int] NULL,
	[AvgLabor] [decimal](18, 2) NULL,
	[MaxLabor] [decimal](18, 2) NULL,
	[MinLabor] [decimal](18, 2) NULL,
	[AvgWaitTime] [decimal](18, 2) NULL,
	[MaxWaitTime] [decimal](18, 2) NULL,
	[MinWaitTime] [decimal](18, 2) NULL,
 CONSTRAINT [PK_ProStat] PRIMARY KEY CLUSTERED 
(
	[LocationID] ASC,
	[ProDate] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON)
)

GO

SET ANSI_PADDING OFF
GO

/****** Object:  Table [dbo].[REC]    Script Date: 1/17/2016 3:51:23 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

SET ANSI_PADDING ON
GO

CREATE TABLE [dbo].[REC](
	[LocationID] [int] NOT NULL,
	[recid] [int] NOT NULL,
	[clientid] [int] NULL,
	[esttime] [datetime] NULL,
	[vehID] [int] NULL,
	[VehMan] [int] NULL,
	[VModel] [char](40) NULL,
	[VehMod] [int] NULL,
	[VehColor] [int] NULL,
	[datein] [datetime] NULL,
	[dateout] [datetime] NULL,
	[accamt] [money] NULL,
	[tax] [money] NULL,
	[gtotal] [money] NULL,
	[cashamt] [money] NULL,
	[chargeamt] [money] NULL,
	[cashback] [money] NULL,
	[cardtype] [int] NULL,
	[totalamt] [money] NULL,
	[PerAdj] [numeric](18, 0) NULL,
	[Status] [int] NULL,
	[SalesRep] [int] NULL,
	[Notes] [text] NULL,
	[Line] [int] NULL,
	[DayCnt] [int] NULL,
	[cashoutid] [int] NULL,
	[CardNo] [varchar](20) NULL,
	[Approval] [varchar](20) NULL,
	[Signiture] [image] NULL,
	[CheckAmt] [money] NULL,
	[ChkNo] [char](10) NULL,
	[ChkPhone] [char](14) NULL,
	[chkDL] [char](16) NULL,
	[GiftCardAmt] [money] NULL,
	[GiftCardID] [char](10) NULL,
	[CloseDte] [smalldatetime] NULL,
	[VINL5] [char](5) NULL,
	[reg] [tinyint] NULL,
	[adv] [int] NULL,
	[QABy] [int] NULL,
	[estMin] [varchar](50) NULL,
	[Labor] [decimal](18, 0) NULL,
 CONSTRAINT [PK_REC_1] PRIMARY KEY CLUSTERED 
(
	[LocationID] ASC,
	[recid] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON)
)

GO

SET ANSI_PADDING OFF
GO

/****** Object:  Table [dbo].[RECITEM]    Script Date: 1/17/2016 3:51:32 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

SET ANSI_PADDING ON
GO

CREATE TABLE [dbo].[RECITEM](
	[RecItemID] [int] NOT NULL,
	[LocationID] [int] NOT NULL,
	[recId] [int] NOT NULL,
	[ProdID] [int] NOT NULL,
	[Comm] [money] NULL,
	[Price] [money] NULL,
	[QTY] [int] NULL,
	[stotal] [money] NULL,
	[taxable] [tinyint] NULL,
	[taxamt] [money] NULL,
	[gtotal] [money] NULL,
	[peradj] [tinyint] NULL,
	[GiftCardID] [char](10) NULL,
 CONSTRAINT [PK_RECITEM] PRIMARY KEY CLUSTERED 
(
	[RecItemID] ASC,
	[LocationID] ASC,
	[recId] ASC,
	[ProdID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON)
)

GO

SET ANSI_PADDING OFF
GO

/****** Object:  Table [dbo].[RECQA]    Script Date: 1/17/2016 3:51:34 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

SET ANSI_PADDING ON
GO

CREATE TABLE [dbo].[RECQA](
	[LocationID] [int] NOT NULL,
	[RECID] [int] NOT NULL,
	[QADesc] [varchar](50) NOT NULL,
	[QAvalue] [bit] NULL,
	[QAOrder] [int] NULL,
	[QAType] [int] NULL,
 CONSTRAINT [PK_RECQA] PRIMARY KEY CLUSTERED 
(
	[LocationID] ASC,
	[RECID] ASC,
	[QADesc] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON)
)

GO

SET ANSI_PADDING OFF
GO

/****** Object:  Table [dbo].[RECQA2]    Script Date: 1/17/2016 3:51:36 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

SET ANSI_PADDING ON
GO

CREATE TABLE [dbo].[RECQA2](
	[LocationID] [int] NOT NULL,
	[RecID] [int] NOT NULL,
	[REMDesc] [varchar](50) NOT NULL,
	[REMValue] [bit] NULL,
	[REMOrder] [int] NULL,
	[REMType] [int] NULL,
	[RemDate] [smalldatetime] NULL,
 CONSTRAINT [PK_RECQA2] PRIMARY KEY CLUSTERED 
(
	[LocationID] ASC,
	[RecID] ASC,
	[REMDesc] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON)
)

GO

SET ANSI_PADDING OFF
GO

/****** Object:  Table [dbo].[ScanIn]    Script Date: 1/17/2016 3:51:37 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

SET ANSI_PADDING ON
GO

CREATE TABLE [dbo].[ScanIn](
	[LocationID] [int] NOT NULL,
	[RecID] [int] NOT NULL,
	[Dept] [char](10) NULL,
	[UPC] [nchar](10) NULL,
	[Type] [char](10) NULL,
	[Wash] [char](10) NULL,
	[Air] [char](10) NULL,
	[UpCharge] [char](10) NULL,
	[AddService] [char](50) NULL,
	[Print1] [char](1) NULL,
	[NoEng] [char](1) NULL,
	[Tag] [char](10) NULL,
	[Make] [char](10) NULL,
	[VModel] [char](40) NULL,
	[Model] [char](10) NULL,
	[Color] [char](10) NULL,
	[LastWash] [char](10) NULL,
	[UserID] [int] NULL,
 CONSTRAINT [PK_ScanIn] PRIMARY KEY CLUSTERED 
(
	[LocationID] ASC,
	[RecID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON)
)

GO

SET ANSI_PADDING OFF
GO

/****** Object:  Table [dbo].[stats]    Script Date: 1/17/2016 3:51:41 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

SET ANSI_PADDING ON
GO

CREATE TABLE [dbo].[stats](
	[LocationID] [int] NOT NULL,
	[Washers] [int] NULL,
	[Detailers] [int] NULL,
	[Labor] [decimal](18, 0) NULL,
	[TotalLabor] [decimal](18, 0) NULL,
	[Totalhours] [decimal](18, 0) NULL,
	[TotalSalery] [decimal](18, 0) NULL,
	[DetailComp] [decimal](18, 0) NULL,
	[TotalRev] [decimal](18, 0) NULL,
	[CurrentWaitTime] [char](50) NULL,
	[CurrentStatus] [varchar](50) NULL,
 CONSTRAINT [PK_stats] PRIMARY KEY CLUSTERED 
(
	[LocationID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON)
)

GO

SET ANSI_PADDING OFF
GO

/****** Object:  Table [dbo].[sysdiagrams]    Script Date: 1/17/2016 3:51:43 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

SET ANSI_PADDING ON
GO

CREATE TABLE [dbo].[sysdiagrams](
	[name] [sysname] NOT NULL,
	[principal_id] [int] NOT NULL,
	[diagram_id] [int] IDENTITY(1,1) NOT NULL,
	[version] [int] NULL,
	[definition] [varbinary](max) NULL,
PRIMARY KEY CLUSTERED 
(
	[diagram_id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON),
 CONSTRAINT [UK_principal_name] UNIQUE NONCLUSTERED 
(
	[principal_id] ASC,
	[name] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON)
)

GO

SET ANSI_PADDING OFF
GO

/****** Object:  Table [dbo].[temp_EODHours]    Script Date: 1/17/2016 3:51:45 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

SET ANSI_PADDING ON
GO

CREATE TABLE [dbo].[temp_EODHours](
	[LocationID] [int] NOT NULL,
	[UserID] [int] NOT NULL,
	[LoginID] [varchar](16) NULL,
	[ReportDate] [smalldatetime] NULL,
	[Payee] [varchar](50) NULL,
	[lname] [varchar](50) NULL,
	[Hours] [decimal](10, 2) NULL,
 CONSTRAINT [PK_temp_EODHours] PRIMARY KEY CLUSTERED 
(
	[LocationID] ASC,
	[UserID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON)
)

GO

SET ANSI_PADDING OFF
GO

/****** Object:  Table [dbo].[temp_EODProd]    Script Date: 1/17/2016 3:51:46 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

SET ANSI_PADDING ON
GO

CREATE TABLE [dbo].[temp_EODProd](
	[LocationID] [int] NOT NULL,
	[Cat] [int] NOT NULL,
	[ProdID] [int] NOT NULL,
	[Type] [varchar](10) NULL,
	[Descript] [varchar](30) NULL,
	[Com] [int] NULL,
	[Sched] [int] NULL,
 CONSTRAINT [PK_temp_EODProd] PRIMARY KEY CLUSTERED 
(
	[LocationID] ASC,
	[Cat] ASC,
	[ProdID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON)
)

GO

SET ANSI_PADDING OFF
GO

/****** Object:  Table [dbo].[TimeClock]    Script Date: 1/17/2016 3:51:48 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

SET ANSI_PADDING ON
GO

CREATE TABLE [dbo].[TimeClock](
	[LocationID] [int] NOT NULL,
	[UserID] [int] NOT NULL,
	[ClockID] [int] NOT NULL,
	[Cdatetime] [datetime] NULL,
	[Caction] [int] NULL,
	[editby] [int] NULL,
	[editdate] [datetime] NULL,
	[Paid] [bit] NULL,
	[CType] [char](2) NULL,
 CONSTRAINT [PK_TimeClock_1] PRIMARY KEY CLUSTERED 
(
	[LocationID] ASC,
	[UserID] ASC,
	[ClockID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON)
)

GO

SET ANSI_PADDING OFF
GO

/****** Object:  Table [dbo].[TimeSheet]    Script Date: 1/17/2016 3:51:50 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

SET ANSI_PADDING ON
GO

CREATE TABLE [dbo].[TimeSheet](
	[LocationID] [int] NOT NULL,
	[SheetID] [int] NOT NULL,
	[Weekno] [int] NULL,
	[yearno] [int] NULL,
	[weekof] [smalldatetime] NULL,
	[status] [int] NULL,
	[empcnt] [int] NULL,
	[totalamt] [char](10) NULL,
	[submitdate] [datetime] NULL,
	[appdate] [datetime] NULL,
	[compdate] [datetime] NULL,
 CONSTRAINT [PK_TimeSheet_1] PRIMARY KEY CLUSTERED 
(
	[LocationID] ASC,
	[SheetID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON)
)

GO

SET ANSI_PADDING OFF
GO

/****** Object:  Table [dbo].[UserAdj]    Script Date: 1/17/2016 3:51:52 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

SET ANSI_PADDING ON
GO

CREATE TABLE [dbo].[UserAdj](
	[LocationID] [int] NOT NULL,
	[UserID] [int] NOT NULL,
	[AdjID] [int] NOT NULL,
	[sheetID] [int] NOT NULL,
	[actAmt] [money] NULL,
	[actdate] [smalldatetime] NULL,
	[actdesc] [varchar](600) NULL,
	[editby] [int] NULL,
	[editdate] [datetime] NULL,
 CONSTRAINT [PK_UserAdj_1] PRIMARY KEY CLUSTERED 
(
	[LocationID] ASC,
	[UserID] ASC,
	[AdjID] ASC,
	[sheetID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON)
)

GO

SET ANSI_PADDING OFF
GO

/****** Object:  Table [dbo].[UserCol]    Script Date: 1/17/2016 3:51:55 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

SET ANSI_PADDING ON
GO

CREATE TABLE [dbo].[UserCol](
	[LocationID] [int] NOT NULL,
	[UserID] [int] NOT NULL,
	[CollID] [int] NOT NULL,
	[ActID] [int] NOT NULL,
	[ActDate] [smalldatetime] NULL,
	[Acttype] [char](10) NULL,
	[actAmt] [money] NULL,
	[ActDesc] [varchar](600) NULL,
	[EditBy] [int] NULL,
	[EditDate] [datetime] NULL,
	[sheetID] [int] NULL,
 CONSTRAINT [PK_UserCol_1] PRIMARY KEY CLUSTERED 
(
	[LocationID] ASC,
	[UserID] ASC,
	[CollID] ASC,
	[ActID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON)
)

GO

SET ANSI_PADDING OFF
GO

/****** Object:  Table [dbo].[UserUnif]    Script Date: 1/17/2016 3:51:57 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

SET ANSI_PADDING ON
GO

CREATE TABLE [dbo].[UserUnif](
	[LocationID] [int] NOT NULL,
	[UserID] [int] NOT NULL,
	[UnifID] [int] NOT NULL,
	[ActID] [int] NOT NULL,
	[ActDate] [smalldatetime] NULL,
	[ActType] [char](10) NULL,
	[ActAmt] [money] NULL,
	[ProdID] [int] NULL,
	[Editby] [int] NULL,
	[EditDate] [datetime] NULL,
	[ActCost] [money] NULL,
	[ActQty] [int] NULL,
	[actDesc] [varchar](600) NULL,
	[SheetID] [int] NULL,
 CONSTRAINT [PK_UserUnif_1] PRIMARY KEY CLUSTERED 
(
	[LocationID] ASC,
	[UserID] ASC,
	[UnifID] ASC,
	[ActID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON)
)

GO

SET ANSI_PADDING OFF
GO

/****** Object:  Table [dbo].[vehical]    Script Date: 1/17/2016 3:52:01 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

SET ANSI_PADDING ON
GO

CREATE TABLE [dbo].[vehical](
	[vehid] [int] NOT NULL,
	[LocationID] [int] NULL,
	[clientid] [int] NULL,
	[upc] [char](10) NULL,
	[tag] [char](10) NULL,
	[vehnum] [int] NULL,
	[make] [char](10) NULL,
	[model] [char](10) NULL,
	[vyear] [nchar](4) NULL,
	[Color] [char](10) NULL,
	[vmodel] [char](40) NULL,
 CONSTRAINT [PK_vehical] PRIMARY KEY CLUSTERED 
(
	[vehid] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON)
)

GO

SET ANSI_PADDING OFF
GO

/****** Object:  Table [dbo].[vendor]    Script Date: 1/17/2016 3:52:03 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

SET ANSI_PADDING ON
GO

CREATE TABLE [dbo].[vendor](
	[LocationID] [int] NOT NULL,
	[VenID] [int] NOT NULL,
	[Vendor] [varchar](50) NULL,
	[vencontact] [varchar](50) NULL,
	[venaddr1] [varchar](50) NULL,
	[venaddr2] [varchar](50) NULL,
	[vencity] [varchar](50) NULL,
	[venst] [varchar](2) NULL,
	[VenZip] [char](5) NULL,
	[venPhone] [char](14) NULL,
	[venFax] [char](14) NULL,
	[venURL] [varchar](100) NULL,
	[venEmail] [varchar](100) NULL,
	[venAcc] [varchar](50) NULL,
 CONSTRAINT [PK_vendor] PRIMARY KEY CLUSTERED 
(
	[VenID] ASC,
	[LocationID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON)
)

GO

SET ANSI_PADDING OFF
GO

ALTER TABLE [dbo].[client] ADD  CONSTRAINT [DF_client_account]  DEFAULT ((0)) FOR [account]
GO

ALTER TABLE [dbo].[CustAccHist] ADD  CONSTRAINT [DF_CustAccHist_Archive]  DEFAULT ((0)) FOR [Archive]
GO

ALTER TABLE [dbo].[LM_Users] ADD  CONSTRAINT [DF_LM_Users_Salery]  DEFAULT ((0)) FOR [Salery]
GO

ALTER TABLE [dbo].[LM_Users] ADD  CONSTRAINT [DF_LM_Users_TIP]  DEFAULT ((1)) FOR [TIP]
GO

ALTER TABLE [dbo].[Product] ADD  CONSTRAINT [DF_Product_Price]  DEFAULT ((0)) FOR [Price]
GO

ALTER TABLE [dbo].[Product] ADD  CONSTRAINT [DF_Product_Inv]  DEFAULT ((0)) FOR [Inv]
GO

ALTER TABLE [dbo].[Product] ADD  CONSTRAINT [DF_Product_Taxable]  DEFAULT ((0)) FOR [Taxable]
GO

ALTER TABLE [dbo].[Product] ADD  CONSTRAINT [DF_Product_PerAdj]  DEFAULT ((0)) FOR [PerAdj]
GO

ALTER TABLE [dbo].[ProStat] ADD  CONSTRAINT [DF_ProStat_Loc]  DEFAULT ((1)) FOR [LocationID]
GO

ALTER TABLE [dbo].[REC] ADD  CONSTRAINT [DF_REC_clientid]  DEFAULT ((0)) FOR [clientid]
GO

ALTER TABLE [dbo].[REC] ADD  CONSTRAINT [DF_REC_vehID]  DEFAULT ((0)) FOR [vehID]
GO

ALTER TABLE [dbo].[REC] ADD  CONSTRAINT [DF_REC_VehMan]  DEFAULT ((0)) FOR [VehMan]
GO

ALTER TABLE [dbo].[REC] ADD  CONSTRAINT [DF_REC_VehMod]  DEFAULT ((0)) FOR [VehMod]
GO

ALTER TABLE [dbo].[REC] ADD  CONSTRAINT [DF_REC_VehColor]  DEFAULT ((0)) FOR [VehColor]
GO

ALTER TABLE [dbo].[REC] ADD  CONSTRAINT [DF_REC_Status]  DEFAULT ((0)) FOR [Status]
GO

EXEC sys.sp_addextendedproperty @name=N'microsoft_database_tools_support', @value=1 , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'sysdiagrams'
GO


