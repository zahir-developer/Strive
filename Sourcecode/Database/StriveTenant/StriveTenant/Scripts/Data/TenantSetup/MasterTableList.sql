/****** Object:  Table [CON].[MasterTableList]    Script Date: 25-03-2022 11:03:23 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [CON].[MasterTableList](
	[MasterTableListID] [int] IDENTITY(1,1) NOT NULL,
	[MasterTableListNAME] [varchar](250) NULL,
	[IS0thRecord] [bit] NULL,
	[ISIdentityInsert] [bit] NULL,
		[ISActive] [bit] NULL,
	[CreatedDate] [datetime] NULL,
 CONSTRAINT [PK_MasterTableList_MasterTableListID] PRIMARY KEY CLUSTERED 
(
	[MasterTableListID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO



-- INSERT INTO StriveTenant_Setup.Strive_ZD16.tblPermission(PermissionName,
--IsActive,
--IsDeleted,
--CreatedBy,
--CreatedDate,
--UpdatedBy,
--UpdatedDate)

--(Select PermissionName,
--IsActive,
--IsDeleted,
--CreatedBy,
--CreatedDate,
--UpdatedBy,
--UpdatedDate from StriveTenant_Setup.Strive_TenantSetup.tblPermission)
