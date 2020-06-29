USE [MPOS]
GO

/****** Object:  StoredProcedure [dbo].[Invoice]    Script Date: 1/17/2016 11:22:54 PM ******/
SET ANSI_NULLS OFF
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[Invoice]
@InvoiceID int

AS
Begin




SELECT client.fname + ' ' + client.lname AS name, client.addr1, 
    client.addr2, client.city, client.st, client.zip, 
    CustAccHist.TXRecID, REC.Closedte, (CustAccHist.TXAmt*-1) as TXAmt , Product.Descript, 
    CustAccHist.InvoiceID, CUSTACCSTM.STMDate, 
    CUSTACCSTM.STMAMT, CUSTACCSTM.CurrentAmt, CUSTACCSTM.amount,  LM_Locations.LocationDesc, 
    LM_Locations.Address1, LM_Locations.Address2, 
     LM_Locations.City as Lcity, 
    LM_Locations.State, LM_Locations.PostalCode, 
    LM_Locations.Phone, LM_Locations.Fax, 
    LM_Locations.LocationID
FROM CustAccHist INNER JOIN
    CustAcc ON 
    CustAccHist.CustAccID = CustAcc.CustAccID INNER JOIN
    client ON CustAcc.ClientID = client.clientid INNER JOIN
    REC ON CustAccHist.TXRecID = REC.recid INNER JOIN
    RECITEM ON REC.recid = RECITEM.recId INNER JOIN
    Product ON RECITEM.ProdID = Product.ProdID INNER JOIN
    CUSTACCSTM ON 
    CustAccHist.InvoiceID = CUSTACCSTM.InvoiceID, 
    LM_Locations
WHERE ((CustAccHist.InvoiceID = @InvoiceID) AND (Product.cat = 1) OR
    (CustAccHist.InvoiceID = @InvoiceID) AND (Product.cat = 2)) AND 
    (LM_Locations.LocationID = 1)


END
GO

/****** Object:  StoredProcedure [dbo].[LM_ListInsert]    Script Date: 1/17/2016 11:22:55 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO




CREATE PROCEDURE [dbo].[LM_ListInsert]
@Desc varchar (255),
@valueWidth smallint,
@DescWidth smallint,
@DataType char(1),
@Type tinyint

as
declare  @MaxType smallint;

begin

select @MaxType = isNull(max(ListType),0) +1 from LM_List;  
INSERT into LM_List(ListType,Description,ValueWidth,DescWidth,DataType,Type) 
Values 
(@MaxType,@Desc,@valueWidth,@DescWidth,@DataType,@Type ); 
  
 Select @MaxType            
end;
GO

/****** Object:  StoredProcedure [dbo].[LM_ListItemInsert]    Script Date: 1/17/2016 11:22:55 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO




CREATE PROCEDURE [dbo].[LM_ListItemInsert]
@PickType smallint,
@Value varchar(255),
@Desc varchar (255),
@Order integer,
@Active bit,
@system bit
as
declare  @MaxCode smallint;

begin

select @MaxCode = isNull(max(ListCode),0) +1 from LM_Listitem WHERE ListType=@PickType;  
INSERT into LM_ListItem(ListType,ListCode,ListValue,ListDesc,ItemOrder,Active,ItemType) 
Values 
(@PickType,@MaxCode,@Value,@Desc,@Order,@Active,@system); 
  
Select @PickType            
end;
GO

/****** Object:  StoredProcedure [dbo].[LM_LocationsInsert]    Script Date: 1/17/2016 11:22:55 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO




CREATE PROCEDURE [dbo].[LM_LocationsInsert] 
	@LocationID int,
	@LocationDesc varchar(255),
	@Address1 varchar(150),
	@Address2 varchar(150),
	@Address3 varchar(150),
	@City varchar(150),
	@State varchar(2),
	@PostalCode varchar(10),
	@Country varchar(100),
	@Phone varchar(25),
	@Fax varchar(25),
	@LocationType varchar(150)
as 
begin
INSERT into LM_Locations  
			(LocationID,LocationDesc,Address1,
		      Address2,Address3,
			City,State,PostalCode,
			Country,Phone,Fax,
                  LocationType) 
Values (@LocationID,@LocationDesc,@Address1,@Address2,
	            @Address3,@City,@State,@PostalCode,
                  @Country,@Phone,@Fax,@LocationType);

Select @LocationID
end;
GO

/****** Object:  StoredProcedure [dbo].[LM_LocationsUpdate]    Script Date: 1/17/2016 11:22:55 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO




CREATE PROCEDURE [dbo].[LM_LocationsUpdate] 
	@LocationDesc varchar(255),
	@Address1 varchar(150),
	@Address2 varchar(150),
	@Address3 varchar(150),
	@City varchar(150),
	@State varchar(2),
	@PostalCode varchar(10),
	@Country varchar(100),
	@Phone varchar(25),
	@Fax varchar(25),
	@LocationType varchar(150),
	@LocationID varchar(20)
as 
begin
UPDATE LM_Locations SET  
			LocationDesc=@LocationDesc,
		      Address1=@Address1,
		      Address2=@Address2,
			Address3=@Address3,
			City=@City,
			State=@State,
			PostalCode=@PostalCode,
			Country=@Country,
			Phone=@Phone,
	            Fax=@Fax,
                  LocationType=@LocationType  
            WHERE LocationID=@LocationID;
 
end;
GO

/****** Object:  StoredProcedure [dbo].[LM_UserInsert]    Script Date: 1/17/2016 11:22:55 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[LM_UserInsert]
@FirstName varchar(50),@LastName varchar(50),
@RoleID integer, @Phone varchar(25),  @Email varchar(50), @Active bit,
@LoginID varchar(16), @Password varchar(16), @Initials varchar(5),
@Address varchar(80),
@City varchar(50),
@State varchar(2),
@Zip varchar(5),
@PayRate nchar(10),
@SickRate nchar(10),
@VacRate nchar(10),
@ComRate nchar(10),
@SSNo varchar(11),
@Hire smalldatetime,
@Birth smalldatetime,
@LRT smalldatetime,
@Gender varchar(1),
@MStat varchar(1),
@Salery nchar(10),
@Tip bit,
@citizen tinyint,
@AlienNo varchar(50),
@AuthDate smalldatetime,
@exempt int,
@Fax varchar(25),@UserID integer,@LocationID int
as
declare @MaxUserID integer 
 
Begin
		select @MaxUserID = max(UserID)+1  from LM_Users where LocationID=@LocationID;  

		INSERT into LM_Users(UserID,FirstName,LastName,RoleID,Phone,Fax,Email,LoginID,Password,Active,Initials,Address,City,State,Zip,PayRate,SickRate,VacRate,ComRate,SSNo,Hire,Birth,LRT,Gender,MStat,Salery, exempt,Tip, citizen, AlienNo,AuthDate ,LocationID ) 
			Values 
				(@MaxUserID,@FirstName,@LastName,@RoleID,@Phone,@Fax,@Email,@LoginID,@Password,@Active,@Initials,@Address,@City,@State,@Zip,@PayRate,@SickRate,@VacRate,@ComRate,@SSNo,@Hire,@Birth,@LRT,@Gender,@MStat,@Salery,@exempt,@Tip,@citizen,@AlienNo,@AuthDate,@LocationID);  

	
		Select @MaxUserID
End;
GO

/****** Object:  StoredProcedure [dbo].[LM_UserUpdate]    Script Date: 1/17/2016 11:22:55 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[LM_UserUpdate]
@FirstName varchar(50),@LastName varchar(50),
@RoleID tinyint, @Phone varchar(25), @Email varchar(50),@Active bit,
@LoginID varchar(16), @Password varchar(16), @Initials varchar(5),
@Address varchar(80),
@City varchar(50),
@State varchar(2),
@Zip varchar(5),
@PayRate nchar(10),
@SickRate nchar(10),
@VacRate nchar(10),
@ComRate nchar(10),
@SSNo varchar(11),
@Hire smalldatetime,
@Birth smalldatetime,
@LRT smalldatetime,
@Gender varchar(1),
@MStat varchar(1),
@Salery nchar(10),
@tip Bit,
@citizen tinyint,
@AlienNo varchar(50),
@AuthDate smalldatetime,
@exempt int,
@Fax varchar(25),@UserID integer,@LocationID int
as
begin
Update LM_Users set
FirstName=@FirstName,
LastName=@LastName,
RoleID=@RoleID,
Phone=@Phone,
Email=@Email,
Active=@Active,
LoginID=@LoginID,
Password=@Password,
Initials=@Initials, 
Address=@Address ,
City=@City,
State=@State ,
Zip=@Zip,
PayRate=@PayRate,
SickRate=@SickRate,
VacRate=@VacRate,
ComRate=@ComRate,
SSNo=@SSNo,
Hire=@Hire,
Birth=@Birth,
LRT=@LRT,
Gender=@Gender,
MStat=@MStat,
Salery=@Salery,
Exempt = @Exempt,
Tip = @tip,
citizen = @citizen,
AlienNo = @AlienNo,
AuthDate = @AuthDate,
Fax=@Fax
where UserID=@UserID and LocationID=@LocationID; 
            
end;
GO

/****** Object:  StoredProcedure [dbo].[MonthlyWashes]    Script Date: 1/17/2016 11:22:55 PM ******/
SET ANSI_NULLS OFF
GO

SET QUOTED_IDENTIFIER OFF
GO



CREATE PROCEDURE [dbo].[MonthlyWashes] 

@Month  int,
@Year int

AS
begin


SELECT DISTINCT 
    1 AS Washes, REC.recid, 
    client.fname + ' ' + client.lname AS name, REC.CloseDte, 
    CustAcc.ClientID, REC.totalamt, vehical.vmodel, 
    LM_ListItem.ListDesc AS color
FROM REC INNER JOIN
    CustAcc ON REC.clientid = CustAcc.ClientID INNER JOIN
    client ON REC.clientid = client.clientid INNER JOIN
    vehical ON REC.vehID = vehical.vehid INNER JOIN
    LM_ListItem ON vehical.Color = LM_ListItem.ListValue AND 
    LM_ListItem.ListType = 5
WHERE (REC.accamt > 0) AND (CustAcc.Type = 2) AND  client.Ctype=2 AND
    (CustAcc.Status = 1) AND ({ fn MONTH(REC.CloseDte) } = @Month) AND ({ fn Year(REC.CloseDte) } = @Year)
GROUP BY client.fname, client.lname, REC.CloseDte, 
    CustAcc.ClientID, REC.totalamt, REC.recid, vehical.vmodel, 
    vehical.Color, vehical.make, LM_ListItem.ListDesc

End
GO

/****** Object:  StoredProcedure [dbo].[ProductInsert]    Script Date: 1/17/2016 11:22:55 PM ******/
SET ANSI_NULLS OFF
GO

SET QUOTED_IDENTIFIER OFF
GO

CREATE PROCEDURE [dbo].[ProductInsert]
@ProdID int, @dept int, @cat int,@createdby int,@created datetime
as    

begin
 
INSERT into Product(ProdID, cat, dept,Descript,Price, Comm,Inv,Kit, Taxable, createdby, created) 
	Values 
	(@ProdID, @cat, @dept, 'New',0.00,0.00,0,0,0,@createdby, @created);  
 
 end;
GO

/****** Object:  StoredProcedure [dbo].[ProductUpdate]    Script Date: 1/17/2016 11:22:55 PM ******/
SET ANSI_NULLS OFF
GO

SET QUOTED_IDENTIFIER ON
GO


CREATE PROCEDURE [dbo].[ProductUpdate]
@ProdID int,
@Descript varchar(20),
@Number tinyint,
@Price varchar(20),
@Cost varchar(20),
@Comm varchar(20),
@Inv tinyint,
@Kit tinyint,
@Opt tinyint,
@Taxable tinyint,
@PerAdj tinyint,
@Bcode varchar(16),
@EstTime numeric(10,2),
@Status tinyint,
@Editby int,
@Edited datetime

as
begin
UPDATE Product  set Descript = @Descript,
Number = @Number,
Price= cast(@Price as money) ,
Cost= cast(@Cost as money) ,
Comm= cast(@Comm as money) ,
Inv = @Inv,
Kit = @Kit,
Opt = @Opt,
Taxable = @Taxable,
PerAdj = @PerAdj,
Bcode = @Bcode,
EstTime = @EstTime,
Status = @Status,
Editby = @Editby,
Edited = @Edited
Where ProdID =@ProdID;  
           
end;
GO

/****** Object:  StoredProcedure [dbo].[sp_alterdiagram]    Script Date: 1/17/2016 11:22:55 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


	CREATE PROCEDURE [dbo].[sp_alterdiagram]
	(
		@diagramname 	sysname,
		@owner_id	int	= null,
		@version 	int,
		@definition 	varbinary(max)
	)
	WITH EXECUTE AS 'dbo'
	AS
	BEGIN
		set nocount on
	
		declare @theId 			int
		declare @retval 		int
		declare @IsDbo 			int
		
		declare @UIDFound 		int
		declare @DiagId			int
		declare @ShouldChangeUID	int
	
		if(@diagramname is null)
		begin
			RAISERROR ('Invalid ARG', 16, 1)
			return -1
		end
	
		execute as caller;
		select @theId = DATABASE_PRINCIPAL_ID();	 
		select @IsDbo = IS_MEMBER(N'db_owner'); 
		if(@owner_id is null)
			select @owner_id = @theId;
		revert;
	
		select @ShouldChangeUID = 0
		select @DiagId = diagram_id, @UIDFound = principal_id from dbo.sysdiagrams where principal_id = @owner_id and name = @diagramname 
		
		if(@DiagId IS NULL or (@IsDbo = 0 and @theId <> @UIDFound))
		begin
			RAISERROR ('Diagram does not exist or you do not have permission.', 16, 1);
			return -3
		end
	
		if(@IsDbo <> 0)
		begin
			if(@UIDFound is null or USER_NAME(@UIDFound) is null) -- invalid principal_id
			begin
				select @ShouldChangeUID = 1 ;
			end
		end

		-- update dds data			
		update dbo.sysdiagrams set definition = @definition where diagram_id = @DiagId ;

		-- change owner
		if(@ShouldChangeUID = 1)
			update dbo.sysdiagrams set principal_id = @theId where diagram_id = @DiagId ;

		-- update dds version
		if(@version is not null)
			update dbo.sysdiagrams set version = @version where diagram_id = @DiagId ;

		return 0
	END
	
GO

/****** Object:  StoredProcedure [dbo].[sp_creatediagram]    Script Date: 1/17/2016 11:22:56 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


	CREATE PROCEDURE [dbo].[sp_creatediagram]
	(
		@diagramname 	sysname,
		@owner_id		int	= null, 	
		@version 		int,
		@definition 	varbinary(max)
	)
	WITH EXECUTE AS 'dbo'
	AS
	BEGIN
		set nocount on
	
		declare @theId int
		declare @retval int
		declare @IsDbo	int
		declare @userName sysname
		if(@version is null or @diagramname is null)
		begin
			RAISERROR (N'E_INVALIDARG', 16, 1);
			return -1
		end
	
		execute as caller;
		select @theId = DATABASE_PRINCIPAL_ID(); 
		select @IsDbo = IS_MEMBER(N'db_owner');
		revert; 
		
		if @owner_id is null
		begin
			select @owner_id = @theId;
		end
		else
		begin
			if @theId <> @owner_id
			begin
				if @IsDbo = 0
				begin
					RAISERROR (N'E_INVALIDARG', 16, 1);
					return -1
				end
				select @theId = @owner_id
			end
		end
		-- next 2 line only for test, will be removed after define name unique
		if EXISTS(select diagram_id from dbo.sysdiagrams where principal_id = @theId and name = @diagramname)
		begin
			RAISERROR ('The name is already used.', 16, 1);
			return -2
		end
	
		insert into dbo.sysdiagrams(name, principal_id , version, definition)
				VALUES(@diagramname, @theId, @version, @definition) ;
		
		select @retval = @@IDENTITY 
		return @retval
	END
	
GO

/****** Object:  StoredProcedure [dbo].[sp_dropdiagram]    Script Date: 1/17/2016 11:22:56 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


	CREATE PROCEDURE [dbo].[sp_dropdiagram]
	(
		@diagramname 	sysname,
		@owner_id	int	= null
	)
	WITH EXECUTE AS 'dbo'
	AS
	BEGIN
		set nocount on
		declare @theId 			int
		declare @IsDbo 			int
		
		declare @UIDFound 		int
		declare @DiagId			int
	
		if(@diagramname is null)
		begin
			RAISERROR ('Invalid value', 16, 1);
			return -1
		end
	
		EXECUTE AS CALLER;
		select @theId = DATABASE_PRINCIPAL_ID();
		select @IsDbo = IS_MEMBER(N'db_owner'); 
		if(@owner_id is null)
			select @owner_id = @theId;
		REVERT; 
		
		select @DiagId = diagram_id, @UIDFound = principal_id from dbo.sysdiagrams where principal_id = @owner_id and name = @diagramname 
		if(@DiagId IS NULL or (@IsDbo = 0 and @UIDFound <> @theId))
		begin
			RAISERROR ('Diagram does not exist or you do not have permission.', 16, 1)
			return -3
		end
	
		delete from dbo.sysdiagrams where diagram_id = @DiagId;
	
		return 0;
	END
	
GO

/****** Object:  StoredProcedure [dbo].[sp_helpdiagramdefinition]    Script Date: 1/17/2016 11:22:56 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


	CREATE PROCEDURE [dbo].[sp_helpdiagramdefinition]
	(
		@diagramname 	sysname,
		@owner_id	int	= null 		
	)
	WITH EXECUTE AS N'dbo'
	AS
	BEGIN
		set nocount on

		declare @theId 		int
		declare @IsDbo 		int
		declare @DiagId		int
		declare @UIDFound	int
	
		if(@diagramname is null)
		begin
			RAISERROR (N'E_INVALIDARG', 16, 1);
			return -1
		end
	
		execute as caller;
		select @theId = DATABASE_PRINCIPAL_ID();
		select @IsDbo = IS_MEMBER(N'db_owner');
		if(@owner_id is null)
			select @owner_id = @theId;
		revert; 
	
		select @DiagId = diagram_id, @UIDFound = principal_id from dbo.sysdiagrams where principal_id = @owner_id and name = @diagramname;
		if(@DiagId IS NULL or (@IsDbo = 0 and @UIDFound <> @theId ))
		begin
			RAISERROR ('Diagram does not exist or you do not have permission.', 16, 1);
			return -3
		end

		select version, definition FROM dbo.sysdiagrams where diagram_id = @DiagId ; 
		return 0
	END
	
GO

/****** Object:  StoredProcedure [dbo].[sp_helpdiagrams]    Script Date: 1/17/2016 11:22:56 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


	CREATE PROCEDURE [dbo].[sp_helpdiagrams]
	(
		@diagramname sysname = NULL,
		@owner_id int = NULL
	)
	WITH EXECUTE AS N'dbo'
	AS
	BEGIN
		DECLARE @user sysname
		DECLARE @dboLogin bit
		EXECUTE AS CALLER;
			SET @user = USER_NAME();
			SET @dboLogin = CONVERT(bit,IS_MEMBER('db_owner'));
		REVERT;
		SELECT
			[Database] = DB_NAME(),
			[Name] = name,
			[ID] = diagram_id,
			[Owner] = USER_NAME(principal_id),
			[OwnerID] = principal_id
		FROM
			sysdiagrams
		WHERE
			(@dboLogin = 1 OR USER_NAME(principal_id) = @user) AND
			(@diagramname IS NULL OR name = @diagramname) AND
			(@owner_id IS NULL OR principal_id = @owner_id)
		ORDER BY
			4, 5, 1
	END
	
GO

/****** Object:  StoredProcedure [dbo].[sp_renamediagram]    Script Date: 1/17/2016 11:22:56 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


	CREATE PROCEDURE [dbo].[sp_renamediagram]
	(
		@diagramname 		sysname,
		@owner_id		int	= null,
		@new_diagramname	sysname
	
	)
	WITH EXECUTE AS 'dbo'
	AS
	BEGIN
		set nocount on
		declare @theId 			int
		declare @IsDbo 			int
		
		declare @UIDFound 		int
		declare @DiagId			int
		declare @DiagIdTarg		int
		declare @u_name			sysname
		if((@diagramname is null) or (@new_diagramname is null))
		begin
			RAISERROR ('Invalid value', 16, 1);
			return -1
		end
	
		EXECUTE AS CALLER;
		select @theId = DATABASE_PRINCIPAL_ID();
		select @IsDbo = IS_MEMBER(N'db_owner'); 
		if(@owner_id is null)
			select @owner_id = @theId;
		REVERT;
	
		select @u_name = USER_NAME(@owner_id)
	
		select @DiagId = diagram_id, @UIDFound = principal_id from dbo.sysdiagrams where principal_id = @owner_id and name = @diagramname 
		if(@DiagId IS NULL or (@IsDbo = 0 and @UIDFound <> @theId))
		begin
			RAISERROR ('Diagram does not exist or you do not have permission.', 16, 1)
			return -3
		end
	
		-- if((@u_name is not null) and (@new_diagramname = @diagramname))	-- nothing will change
		--	return 0;
	
		if(@u_name is null)
			select @DiagIdTarg = diagram_id from dbo.sysdiagrams where principal_id = @theId and name = @new_diagramname
		else
			select @DiagIdTarg = diagram_id from dbo.sysdiagrams where principal_id = @owner_id and name = @new_diagramname
	
		if((@DiagIdTarg is not null) and  @DiagId <> @DiagIdTarg)
		begin
			RAISERROR ('The name is already used.', 16, 1);
			return -2
		end		
	
		if(@u_name is null)
			update dbo.sysdiagrams set [name] = @new_diagramname, principal_id = @theId where diagram_id = @DiagId
		else
			update dbo.sysdiagrams set [name] = @new_diagramname where diagram_id = @DiagId
		return 0
	END
	
GO

/****** Object:  StoredProcedure [dbo].[sp_upgraddiagrams]    Script Date: 1/17/2016 11:22:56 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


	CREATE PROCEDURE [dbo].[sp_upgraddiagrams]
	AS
	BEGIN
		IF OBJECT_ID(N'dbo.sysdiagrams') IS NOT NULL
			return 0;
	
		CREATE TABLE dbo.sysdiagrams
		(
			name sysname NOT NULL,
			principal_id int NOT NULL,	-- we may change it to varbinary(85)
			diagram_id int PRIMARY KEY IDENTITY,
			version int,
	
			definition varbinary(max)
			CONSTRAINT UK_principal_name UNIQUE
			(
				principal_id,
				name
			)
		);


		/* Add this if we need to have some form of extended properties for diagrams */
		/*
		IF OBJECT_ID(N'dbo.sysdiagram_properties') IS NULL
		BEGIN
			CREATE TABLE dbo.sysdiagram_properties
			(
				diagram_id int,
				name sysname,
				value varbinary(max) NOT NULL
			)
		END
		*/

		IF OBJECT_ID(N'dbo.dtproperties') IS NOT NULL
		begin
			insert into dbo.sysdiagrams
			(
				[name],
				[principal_id],
				[version],
				[definition]
			)
			select	 
				convert(sysname, dgnm.[uvalue]),
				DATABASE_PRINCIPAL_ID(N'dbo'),			-- will change to the sid of sa
				0,							-- zero for old format, dgdef.[version],
				dgdef.[lvalue]
			from dbo.[dtproperties] dgnm
				inner join dbo.[dtproperties] dggd on dggd.[property] = 'DtgSchemaGUID' and dggd.[objectid] = dgnm.[objectid]	
				inner join dbo.[dtproperties] dgdef on dgdef.[property] = 'DtgSchemaDATA' and dgdef.[objectid] = dgnm.[objectid]
				
			where dgnm.[property] = 'DtgSchemaNAME' and dggd.[uvalue] like N'_EA3E6268-D998-11CE-9454-00AA00A3F36E_' 
			return 2;
		end
		return 1;
	END
	
GO

/****** Object:  StoredProcedure [dbo].[stp_DailyTip]    Script Date: 1/17/2016 11:22:56 PM ******/
SET ANSI_NULLS OFF
GO

SET QUOTED_IDENTIFIER OFF
GO


create PROCEDURE [dbo].[stp_DailyTip]
@LocationID int,
@rptdate varchar(10),
@tiptotal numeric(10,2)

AS
begin

Declare @UserID int,
	@LoginID varchar(16),
	@Payee varchar(50),
	@lname varchar(50),
	@ReportDate smalldatetime,
	@timeVal  datetime,
	@timeLast  datetime,
	@Cact bit,
	@Paid bit,
	@ListDesc varchar(20),
	@PayRate char(10),
	@ComRate money,
	@Hours decimal(10,2),
	@Startdate  datetime,
	@Enddate  datetime,
	@tiphour  decimal(10,4),
	@totalhours  decimal(10,4),
	@TIPOVER int,
	@TIP  int,
	@TIPon bit
	

create table #Temp1  (UserID int, LoginID  varchar(16), ReportDate smalldatetime, Payee varchar(50), lname varchar(50),ListDesc varchar(20), Hours decimal(10,2), PayRate char(10), ComRate money,Tipon bit)
create table #Temp2  (UserID int, LoginID  varchar(16), ReportDate smalldatetime, Payee varchar(50), lname varchar(50), Hours decimal(10,2), PayRate char(10), ComRate money, tip numeric(10,2), tipover int,Tipon bit )

set @Startdate = @rptdate+  ' 00:00:00'
set @Enddate = @rptdate+ ' 23:59:59'

Declare B1 Cursor local for 
	 SELECT    TimeClock.UserID, LM_Users.LoginID, LM_Users.tip as Tipon,
	   LM_Users.LastName+' '+ LM_Users.FirstName as Payee, LM_Users.LastName as lname,
	   TimeClock.Cdatetime,  TimeClock.Caction, TimeClock.Paid, 
	     isnull(LM_Users.PayRate,0) as PayRate
	FROM LM_Users(NOLOCK) INNER JOIN
	    TimeClock(NOLOCK) ON 
	    LM_Users.UserID = TimeClock.UserID
	WHERE (TimeClock.LocationID = @LocationID) and (LM_Users.LocationID = @LocationID) and (TimeClock.Cdatetime BETWEEN @Startdate AND 
   		 @Enddate) and ctype <> 9
	ORDER BY TimeClock.UserID,TimeClock.Cdatetime, TimeClock.clockid

	Open B1
	Fetch B1 into  @UserID,@LoginID,@TIPon,@Payee,@lname,@timeVal,@Cact,@Paid,@PayRate
	While @@Fetch_Status = 0
	Begin
		IF @Cact = 0 
			Begin
				set @timeLast = @timeVal
			End
		Else
			Begin
				set @Hours = (CAST(DATEDIFF(n, @timeLast, @timeVal) as decimal(10,2)) /60)
				Insert #Temp1  (UserID,ReportDate,LoginID,Payee, lname, Hours,PayRate,TIPon) values (@UserID, @rptDate, @LoginID, @Payee,@lname, @Hours,@PayRate,@TIPon)			
			End
	Fetch Next From B1 into @UserID,@LoginID,@TIPon,@Payee,@lname,@timeVal,@Cact,@Paid,@PayRate
	End
	
Close B1
DEALLOCATE B1


Declare B2 Cursor local for 
	SELECT DetailComp.UserID,  LM_Users.LoginID,LM_Users.tip as Tipon,
	     LM_Users.LastName+' '+ LM_Users.FirstName  AS Payee, LM_Users.LastName as lname,
	    SUM(DetailComp.ComAmt) AS ComRate,  isnull(LM_Users.PayRate,0) as PayRate
	FROM LM_Users(nolock) INNER JOIN
	    DetailComp(nolock) ON 
	    LM_Users.UserID = DetailComp.UserID
	WHERE (LM_Users.LocationID = @LocationID) and (DetailComp.LocationID = @LocationID) and (DetailComp.Cdatetime BETWEEN @Startdate AND 
   		 @Enddate)
	GROUP BY DetailComp.UserID,  LM_Users.LoginID,
	    LM_Users.LastName, 
	    LM_Users.FirstName, LM_Users.PayRate,LM_Users.tip
	ORDER BY DetailComp.UserID

	Open B2
	Fetch B2 into  @UserID,@LoginID,@TIPon,@Payee,@lname, @ComRate,@PayRate
	While @@Fetch_Status = 0
	Begin
		Insert #Temp1  (UserID,ReportDate,LoginID,Payee, lname, Hours,PayRate,TIPon) values (@UserID, @rptDate,@LoginID, @Payee, @lname, 0.0,@PayRate,@TIPon)
		update  #Temp1 set ComRate = @ComRate where UserID=@UserID
			
	Fetch Next From B2 into  @UserID,@LoginID,@TIPon,@Payee, @lname,@ComRate,@PayRate
	End

Close B2
DEALLOCATE B2
set @totalhours = 0
set @tipOver = 0.00

Declare B3 Cursor local for 

select UserID, ReportDate,LoginID, Payee, lname,SUM(Hours) as hours, PayRate, ComRate,TIPon from #temp1   group by UserID,ReportDate,LoginID, Payee, lname, PayRate, ComRate,TIPon order by Payee
	Open B3
	Fetch B3 into  @UserID, @ReportDate, @LoginID, @Payee,@lname, @Hours,@PayRate,@ComRate,@TIPon
	While @@Fetch_Status = 0
	Begin
		
		IF @TIPon = 1  AND @Hours>0 
			begin
		set @totalhours = @totalhours + @Hours
			end

		Insert #Temp2  (UserID,ReportDate,LoginID,Payee,lname, Hours,PayRate,ComRate,TIPon) values (@UserID, @ReportDate, @LoginID, @Payee,@lname, @Hours,@PayRate,@ComRate,@TIPon)		
	Fetch Next From B3 into  @UserID, @ReportDate, @LoginID, @Payee,@lname, @Hours,@PayRate,@ComRate,@TIPon
	End
Close B3
DEALLOCATE B3
print @tiptotal
print @totalhours

IF @tiptotal> 0 AND @totalhours>0 
	BEGIN
		set @tiphour = @tiptotal/@totalhours
	
	Declare B4 Cursor local for 
		select UserID,Hours from #temp2 where tipon=1
		Open B4
		Fetch B4 into  @UserID, @Hours
		While @@Fetch_Status = 0
		Begin
			set @tip =(@Hours *@tiphour) 
			--set @tip =CAST(@tip AS INT)
			set @tipOver = @tipOver+@tip
			update  #Temp2 set Tip = @tip where UserID=@UserID
		Fetch Next From B4 into  @UserID, @Hours
		End
	Close B4
	DEALLOCATE B4

	update  #Temp2 set tipOver = @tipOver

	END

Select * from #temp2 order by lname

End
GO

/****** Object:  StoredProcedure [dbo].[stp_EOD]    Script Date: 1/17/2016 11:22:56 PM ******/
SET ANSI_NULLS OFF
GO

SET QUOTED_IDENTIFIER OFF
GO



CREATE PROCEDURE [dbo].[stp_EOD]
@rptdate varchar(10),
@LocationID int

AS
begin

	
	Select COp, COn, COd, COq,CORp, CORn, CORd, CORq, COh, CO1, CO5,  CO10, CO20, CO50, CO100, CITotal, COTotal,
 COChecks, COCreditCards, COCreditCards2, COCreditCards3, COCreditCards4, COPayOuts,
(SELECT SUM(GiftCardAmt)
		FROM REC with (NOLOCK)
		WHERE (DATEPART(Month, REC.CloseDte) = Month(@rptdate))
		AND (DATEPART(Day, REC.CloseDte) =  Day(@rptdate))
		AND (DATEPART(Year, REC.CloseDte) = Year(@rptdate))
					AND (REC.LocationID = @LocationID) ) as GiftCards,
(SELECT Labor FROM stats with (NOLOCK) Where (stats.LocationID = @LocationID)) as Labor,
(SELECT SUM(accamt)
		FROM REC with (NOLOCK)
		WHERE (DATEPART(Month, REC.CloseDte) = Month(@rptdate))
		AND (DATEPART(Day, REC.CloseDte) =  Day(@rptdate))
		AND (DATEPART(Year, REC.CloseDte) = Year(@rptdate))
					AND (REC.LocationID = @LocationID) ) as Account, 
(SELECT SUM(totalamt)
		FROM REC (NOLOCK)
		WHERE (DATEPART(Month, REC.CloseDte) = Month(@rptdate))
		AND (DATEPART(Day, REC.CloseDte) =  Day(@rptdate))
		AND (DATEPART(Year, REC.CloseDte) = Year(@rptdate))
		AND REC.LocationID = @LocationID) as totalamt,
	 (SELECT SUM(tax)
		FROM REC (NOLOCK)
		WHERE (DATEPART(Month, REC.CloseDte) = Month(@rptdate))
		AND (DATEPART(Day, REC.CloseDte) =  Day(@rptdate))
		AND (DATEPART(Year, REC.CloseDte) = Year(@rptdate))
		AND REC.LocationID = @LocationID) as Tax,
	(SELECT SUM(chargeamt)
		FROM REC (NOLOCK)
		WHERE (DATEPART(Month, REC.CloseDte) = Month(@rptdate))
		AND (DATEPART(Day, REC.CloseDte) =  Day(@rptdate))
		AND (DATEPART(Year, REC.CloseDte) = Year(@rptdate))
		AND REC.LocationID = @LocationID) as chargeamt,
	(SELECT SUM(cashamt)
		FROM REC (NOLOCK)
		WHERE (DATEPART(Month, REC.CloseDte) = Month(@rptdate))
		AND (DATEPART(Day, REC.CloseDte) =  Day(@rptdate))
		AND (DATEPART(Year, REC.CloseDte) = Year(@rptdate))
		AND REC.LocationID = @LocationID) as cashamt,
	(SELECT SUM(gtotal)
		FROM REC (NOLOCK)
		WHERE (DATEPART(Month, REC.CloseDte) = Month(@rptdate))
		AND (DATEPART(Day, REC.CloseDte) =  Day(@rptdate))
		AND (DATEPART(Year, REC.CloseDte) = Year(@rptdate))
		AND REC.LocationID = @LocationID) as gtotal,
	(SELECT SUM(cashback)
		FROM REC (NOLOCK)
		WHERE (DATEPART(Month, REC.CloseDte) = Month(@rptdate))
		AND (DATEPART(Day, REC.CloseDte) =  Day(@rptdate))
		AND (DATEPART(Year, REC.CloseDte) = Year(@rptdate))
		AND REC.LocationID = @LocationID) as cashback,
	(SELECT SUM(CheckAmt)
		FROM REC (NOLOCK)
		WHERE (DATEPART(Month, REC.CloseDte) = Month(@rptdate))
		AND (DATEPART(Day, REC.CloseDte) =  Day(@rptdate))
		AND (DATEPART(Year, REC.CloseDte) = Year(@rptdate))
		AND REC.LocationID = @LocationID) as checkAmt,
		(SELECT LocationDesc
FROM            LM_Locations (NOLOCK)
WHERE        (LocationID = @LocationID)) as LocationDesc
 From Cashd with (NOLOCK)
		 WHERE nDate=@rptdate and drawerno=1 AND (Cashd.LocationID = @LocationID)


End
GO

/****** Object:  StoredProcedure [dbo].[stp_EODComm]    Script Date: 1/17/2016 11:22:56 PM ******/
SET ANSI_NULLS OFF
GO

SET QUOTED_IDENTIFIER OFF
GO




CREATE PROCEDURE [dbo].[stp_EODComm]
@rptdate varchar(10),
@LocationID int

AS
begin

	 SELECT LM_Users.LastName,DetailComp.ComAmt, DetailComp.recid
		FROM DetailComp (NOLOCK)
		INNER JOIN LM_Users(NOLOCK) ON DetailComp.UserID = LM_Users.UserID
		WHERE (DATEPART(Month, DetailComp.CdateTime) = Month(@rptdate))
		AND (DATEPART(Day, DetailComp.CdateTime) =  Day(@rptdate))
		AND (DATEPART(Year, DetailComp.CdateTime) = Year(@rptdate))
			AND (DetailComp.LocationID = @LocationID) 
			AND (LM_Users.LocationID = @LocationID) 
End
GO

/****** Object:  StoredProcedure [dbo].[stp_EODHours]    Script Date: 1/17/2016 11:22:56 PM ******/
SET ANSI_NULLS OFF
GO

SET QUOTED_IDENTIFIER OFF
GO





CREATE PROCEDURE [dbo].[stp_EODHours]
@rptdate varchar(10),
@LocationID int

AS
begin

Declare @UserID int,
	@LoginID varchar(16),
	@Payee varchar(50),
	@lname varchar(50),
	@ReportDate smalldatetime,
	@timeVal  datetime,
	@timeLast  datetime,
	@Cact bit,
	@Paid bit,
	@ListDesc varchar(20),
	@PayRate char(10),
	@ComRate money,
	@Hours decimal(10,2),
	@Startdate  datetime,
	@Enddate  datetime,
	@totalhours  decimal(10,4)

create table #Temp1  (UserID int, LoginID  varchar(16), ReportDate smalldatetime, Payee varchar(50), lname varchar(50),ListDesc varchar(20), Hours decimal(10,2))
delete  temp_EODHours where temp_EODHours.LocationID = @LocationID

set @Startdate = @rptdate+  ' 00:00:00'
set @Enddate = @rptdate+ ' 23:59:59'

Declare B1 Cursor local for 
	 SELECT    TimeClock.UserID, LM_Users.LoginID,
	   LM_Users.LastName+' '+ LM_Users.FirstName as Payee, LM_Users.LastName as lname,
	   TimeClock.Cdatetime,  TimeClock.Caction
	FROM LM_Users(NOLOCK) INNER JOIN
	    TimeClock(NOLOCK) ON 
	    LM_Users.UserID = TimeClock.UserID
	WHERE (TimeClock.Cdatetime BETWEEN @Startdate AND 
   		 @Enddate)  AND CAST(LM_Users.Salery AS Numeric) = 0  AND TimeClock.CType <> 9
			AND (TimeClock.LocationID = @LocationID) 
			AND (LM_Users.LocationID = @LocationID) 
	ORDER BY TimeClock.UserID,TimeClock.Cdatetime, TimeClock.clockid

	Open B1
	Fetch B1 into  @UserID,@LoginID,@Payee,@lname,@timeVal,@Cact
	While @@Fetch_Status = 0
	Begin
		IF @Cact = 0 
			Begin
				set @timeLast = @timeVal
			End
		Else
			Begin
				set @Hours = (CAST(DATEDIFF(n, @timeLast, @timeVal) as decimal(10,2)) /60)
				Insert #Temp1  (UserID,ReportDate,LoginID,Payee, lname, Hours) values (@UserID, @rptDate, @LoginID, @Payee,@lname, @Hours)			
			End
	Fetch Next From B1 into @UserID,@LoginID,@Payee,@lname,@timeVal,@Cact
	End
	
Close B1
DEALLOCATE B1



Declare B3 Cursor local for 

select UserID, ReportDate,LoginID, Payee, lname,SUM(Hours) as hours  from #temp1   group by UserID,ReportDate,LoginID, Payee, lname order by Payee
	Open B3
	Fetch B3 into  @UserID, @ReportDate, @LoginID, @Payee,@lname, @Hours
	While @@Fetch_Status = 0
	Begin
		
		IF  @Hours>0 
			begin
		set @totalhours = @totalhours + @Hours
			end

		Insert temp_EODHours  (LocationID,UserID,ReportDate,LoginID,Payee,lname, Hours) values (@LocationID,@UserID, @ReportDate, @LoginID, @Payee,@lname, @Hours)		
	Fetch Next From B3 into  @UserID, @ReportDate, @LoginID, @Payee,@lname, @Hours
	End
Close B3
DEALLOCATE B3

End
GO

/****** Object:  StoredProcedure [dbo].[stp_EODHours2]    Script Date: 1/17/2016 11:22:57 PM ******/
SET ANSI_NULLS OFF
GO

SET QUOTED_IDENTIFIER OFF
GO





create PROCEDURE [dbo].[stp_EODHours2]
@rptdate varchar(10),
@LocationID int

AS
begin

exec stp_EODHours @rptdate,@LocationID

select Payee,Hours from temp_EODHours where LocationID=@LocationID


End
GO

/****** Object:  StoredProcedure [dbo].[stp_EODHoursPerWeek]    Script Date: 1/17/2016 11:22:57 PM ******/
SET ANSI_NULLS OFF
GO

SET QUOTED_IDENTIFIER OFF
GO




CREATE PROCEDURE [dbo].[stp_EODHoursPerWeek]
@rptdate varchar(10),
@UserID int,
@LocationID int



AS
begin

Declare @DateOffset int,
	@timeVal  datetime,
	@timeLast  datetime,
	@Cact bit,
	@rows int,
	@cnt int,
	@Startdate  datetime,
	@Enddate  datetime,
	@totalhours decimal(10,2)


Set @DateOffset = datepart(DW,@rptdate)


IF @DateOffset = 1 
	Begin
		Set @DateOffset = 0
	End
ELSE
	Begin
		Set @DateOffset = (@DateOffset-1)*-1
	End
set @Startdate = DateAdd(day,@DateOffset,@rptdate)+  ' 00:00:00'
set @Enddate = @rptdate+ ' 23:59:59'


set @totalhours = 0
set @cnt = 1
Declare B1 Cursor local for 
	 SELECT  TimeClock.Cdatetime,  TimeClock.Caction
	FROM LM_Users(NOLOCK) INNER JOIN
	    TimeClock(NOLOCK) ON 
	    LM_Users.UserID = TimeClock.UserID
	WHERE (TimeClock.Cdatetime BETWEEN @Startdate AND 
   		 @Enddate)  AND CAST(LM_Users.Salery AS Numeric) = 0  AND TimeClock.CType <> 9 and TimeClock.UserID=@UserID  
			AND (TimeClock.LocationID = @LocationID) 
			AND (LM_Users.LocationID = @LocationID) 
	ORDER BY TimeClock.Cdatetime, TimeClock.clockid

	Open B1
	Fetch B1 into  @timeVal,@Cact
	While @@Fetch_Status = 0
	Begin

		SELECT @rows=@@CURSOR_ROWS
		IF @rows = @cnt and @Cact = 0
			Begin
				set @totalhours = @totalhours+round((CAST(DATEDIFF(n, @timeVal,getdate()) as decimal(10,2)) /60),2)
			End
		ELSE
			Begin
		
				IF @Cact = 0 
					Begin

						set @timeLast = @timeVal
					End
				Else
					Begin
						set @totalhours = @totalhours+round((CAST(DATEDIFF(n, @timeLast, @timeVal) as decimal(10,2)) /60),2)
					End
			End

		--print @cnt
		--print @totalhours


		set @cnt = @cnt+1
	Fetch Next From B1 into @timeVal,@Cact
	End
	
Close B1
DEALLOCATE B1

Select @totalhours

End
GO

/****** Object:  StoredProcedure [dbo].[stp_EODProd]    Script Date: 1/17/2016 11:22:57 PM ******/
SET ANSI_NULLS OFF
GO

SET QUOTED_IDENTIFIER OFF
GO





CREATE PROCEDURE [dbo].[stp_EODProd]
@rptdate varchar(10),
@LocationID int

AS
begin

Declare @Cat int,
	@Type varchar(10),
	@ProdID int,
	@Com int,
	@Sched int,
	@Descript varchar(30)
	
delete temp_EODProd where LocationID=@LocationID

Declare B1 Cursor local for 
	 SELECT Product.Descript,Product.ProdID FROM Product(NOLOCK)
		 WHERE (Product.cat = 1) ORDER BY Product.Number

	Open B1
	Fetch B1 into  @Descript,@ProdID
	While @@Fetch_Status = 0
	Begin
	
		SET @Com = (SELECT SUM(1)  FROM REC(NOLOCK)
			 INNER JOIN RECITEM (NOLOCK) ON REC.recid = RECITEM.recId
			 WHERE (DATEPART(Month, REC.Closedte) = Month(@rptdate))
			 AND (DATEPART(Day, REC.Closedte) = Day(@rptdate))
			 AND (DATEPART(Year, REC.Closedte) =Year(@rptdate))
			 AND (RECITEM.ProdID =@ProdID) and Rec.status >= 70   
			AND (REC.LocationID = @LocationID) 
			AND (RECITEM.LocationID = @LocationID))		

		SET @Sched = (SELECT SUM(1)  FROM REC(NOLOCK)
			 INNER JOIN RECITEM (NOLOCK) ON REC.recid = RECITEM.recId
			 WHERE (DATEPART(Month, REC.Closedte) = Month(@rptdate))
			 AND (DATEPART(Day, REC.Closedte) = Day(@rptdate))
			 AND (DATEPART(Year, REC.Closedte) =Year(@rptdate))
			 AND (RECITEM.ProdID =@ProdID)   
			AND (REC.LocationID = @LocationID) 
			AND (RECITEM.LocationID = @LocationID))		

		Insert temp_EODProd  (LocationID,Cat,ProdID,Type,Descript,Com, Sched) values (@LocationID,1,@ProdID,'Washes',@Descript,@Com, @Sched)			
		Fetch Next From B1 into @Descript,@ProdID
	End
	
Close B1
DEALLOCATE B1


Declare B2 Cursor local for 
	 SELECT Product.Descript,Product.ProdID FROM Product(NOLOCK)
		 WHERE (Product.cat = 2) ORDER BY Product.Number

	Open B2
	Fetch B2 into  @Descript,@ProdID
	While @@Fetch_Status = 0
	Begin
		SET @Com = (SELECT SUM(1)  FROM REC(NOLOCK)
			 INNER JOIN RECITEM (NOLOCK) ON REC.recid = RECITEM.recId
			 WHERE (DATEPART(Month, REC.Closedte) = Month(@rptdate))
			 AND (DATEPART(Day, REC.Closedte) = Day(@rptdate))
			 AND (DATEPART(Year, REC.Closedte) =Year(@rptdate))
			 AND (RECITEM.ProdID =@ProdID) and (Rec.status >= 70)  
			AND (REC.LocationID = @LocationID) 
			AND (RECITEM.LocationID = @LocationID))	

		SET @Sched = (SELECT SUM(1)  FROM REC(NOLOCK)
			 INNER JOIN RECITEM (NOLOCK) ON REC.recid = RECITEM.recId
			 WHERE (DATEPART(Month, REC.Closedte) = Month(@rptdate))
			 AND (DATEPART(Day, REC.Closedte) = Day(@rptdate))
			 AND (DATEPART(Year, REC.Closedte) =Year(@rptdate))
			 AND (RECITEM.ProdID =@ProdID) 
			AND (RECITEM.LocationID = @LocationID))	

		Insert temp_EODProd  (LocationID,Cat,ProdID,Type,Descript,Com, Sched) values (@LocationID,2,@ProdID,'Details',@Descript,@Com, @Sched)			
		Fetch Next From B2 into @Descript,@ProdID
	End
	
Close B2
DEALLOCATE B2


--Select  Type,Descript,Com, Sched  from #temp1 order by Cat,ProdID

End
GO

/****** Object:  StoredProcedure [dbo].[stp_EODProd2]    Script Date: 1/17/2016 11:22:57 PM ******/
SET ANSI_NULLS OFF
GO

SET QUOTED_IDENTIFIER OFF
GO





CREATE PROCEDURE [dbo].[stp_EODProd2]
@rptdate varchar(10),
@LocationID int

AS
begin

exec stp_EODProd @rptdate,@LocationID	

Select  Type,Descript,Com, Sched  from temp_EODProd where LocationID=@LocationID order by Cat,ProdID

End
GO

/****** Object:  StoredProcedure [dbo].[stp_getCurrWaitTime]    Script Date: 1/17/2016 11:22:57 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO












CREATE PROCEDURE [dbo].[stp_getCurrWaitTime]
@LocationID int


AS
begin

SET NOCOUNT ON

Declare @Cdatetime int,
		@CurrentWaitTime char(50),
		@CHour int,
		@CDay int,
		@openHour int,
		@closeHour int,
		@cars int,
		@AvgMin int,
		@SumMin int,
		@Washers int,
		@ProdID int,
		@CurrentStatus varchar(50)

Declare @ProDate char(6),
		@avgWaitTime decimal(18,2), 
		@MaxWaitTime decimal(18,2), 
		@MinWaitTime decimal(18,2)


SELECT @CurrentStatus = CurrentStatus FROM LM_Locations WHERE (LocationID = @LocationID)



IF @CurrentStatus <> 'Open'
	Begin
		set @CurrentWaitTime = @CurrentStatus
	end 
else
	Begin
		exec stp_Washers @LocationID
		SELECT @Washers = Washers FROM stats  WHERE (LocationID = @LocationID)

IF (DATEPART(hh, GETDATE()) > 8)
	begin
		SELECT @Cars = COUNT(*) FROM REC INNER JOIN
                      RECITEM ON REC.recid = RECITEM.recId  INNER JOIN
                      Product ON RECITEM.ProdID = Product.ProdID 
			WHERE (DAY(REC.datein) = DAY(GETDATE())) AND (MONTH(REC.datein) = MONTH(GETDATE())) 
			AND (YEAR(REC.datein) = YEAR(GETDATE())) AND (Product.cat = 1)
					AND (REC.dateout IS NULL) AND (REC.LocationID = @LocationID) AND (RECITEM.LocationID = @LocationID)
	end
else
	begin
		set @cars = 0
	end
					


--print '@washers=>'+str(@washers)
--print '@Cars=>'+str(@Cars)
		IF @Washers <= 3
			begin
				if @Cars <= 1
					begin
						set @SumMin = 25
					end
				else
					begin
						set @SumMin = (25+(@cars - 1)*8)
					end
			end
		IF @Washers <= 6
			begin
				if @Cars <= 1
					begin
						set @SumMin = 25
					end
				else
					begin
						set @SumMin = (25+(@cars - 1)*7)
					end
			end
		IF @Washers <= 9
			begin
				if @Cars <= 1
					begin
						set @SumMin = 25
					end
				else
					begin
						set @SumMin = (25+(@cars - 1)*6)
					end
			end
		IF @Washers <= 12
			begin
				if @Cars <= 3
					begin
						set @SumMin = 25
					end
				else
					begin
						set @SumMin = (25+(@cars - 3)*5)
					end
			end
		IF @Washers >= 13  and  @Washers <= 15
			begin
				if @Cars <= 5
					begin
						set @SumMin = 25
					end
				else
					begin
						set @SumMin = (25+(@cars - 5)*4)
					end
			end
		IF @Washers >= 16  and  @Washers <= 18
			begin
				if @Cars <= 6
					begin
						set @SumMin = 25
					end
				else
					begin
						set @SumMin = (25+(@cars - 6)*3)
					end
			end
		IF @Washers >= 19
			begin
				if @Cars <= 7
					begin
						set @SumMin = 25
					end
				else
					begin
						set @SumMin = (25+(@cars - 7)*2)
					end
			end

--print '@SumMin=>'+ str(@SumMin)

		/* Calc Upchage Offset */
		Declare U1 Cursor local for 
			SELECT RECITEM.ProdID FROM REC INNER JOIN
				RECITEM ON REC.recid = RECITEM.recId INNER JOIN
				Product ON RECITEM.ProdID = Product.ProdID
				WHERE (DAY(REC.datein) = DAY(GETDATE())) AND (MONTH(REC.datein) = MONTH(GETDATE())) AND (YEAR(REC.datein) = YEAR(GETDATE())) AND (REC.dateout IS NULL) AND 
				(DATEPART(hh, REC.datein) > 8) AND (Product.dept = 1) AND (RECITEM.ProdID >= 7) AND (RECITEM.ProdID <= 10)
				AND (REC.LocationID = @LocationID) AND (RECITEM.LocationID = @LocationID)
			Open U1
			Fetch U1 into  @ProdID
			While @@Fetch_Status = 0
			Begin
--print '@ProdID=>'+ str(@ProdID)
				IF @ProdID = 7
					begin
								set @SumMin = @SumMin+1
					end
				IF @ProdID = 8
					begin
								set @SumMin = @SumMin+2
					end
				IF @ProdID = 9
					begin
								set @SumMin = @SumMin+3
					end
				IF @ProdID = 10
					begin
								set @SumMin = @SumMin+4
					end
			Fetch Next From U1 into @ProdID
			End
	
		Close U1
		DEALLOCATE U1

		set @CurrentWaitTime = str(@SumMin,3,0) + ' Min'

	end
	update stats set CurrentWaitTime=@CurrentWaitTime

	if @CurrentWaitTime <> 'Closed'
		begin


			set @ProDate =''
			Select @ProDate=ProStat.ProDate,@avgWaitTime=isnull(avgWaitTime,0),@MaxWaitTime=isnull(MaxWaitTime,0), @MinWaitTime=isnull(MinWaitTime,45) from ProStat where ProStat.ProDate = right('00'+ltrim(str(MONTH(getdate()))),2)+right('00'+ltrim(str(day(getdate()))),2)+right('00'+ltrim(str(year(getdate()))),2) and ProStat.LocationID = @LocationID 
			if len(ltrim(@ProDate)) = 0
				begin
					insert ProStat (LocationID, ProDate, avgWaitTime, MaxWaitTime, MinWaitTime ) values(@LocationID, right('00'+ltrim(str(MONTH(getdate()))),2)+right('00'+ltrim(str(day(getdate()))),2)+right('00'+ltrim(str(year(getdate()))),2) , cast(@SumMin as decimal(18,3)),cast(@SumMin as decimal(18,3)),cast(@SumMin as decimal(18,3)))
				end
			else
				begin
					If cast(@SumMin as decimal(18,3)) > @MaxWaitTime
						begin
							set @MaxWaitTime = cast(@SumMin as decimal(18,3))
						end
					If cast(@SumMin as decimal(18,3)) < @MinWaitTime
						begin
							set @MinWaitTime = cast(@SumMin as decimal(18,3))
						end

					set @avgWaitTime = (@avgWaitTime+cast(@SumMin as decimal(18,3)))/2


					update ProStat set avgWaitTime=@avgWaitTime,MaxWaitTime=@MaxWaitTime,MinWaitTime=@MinWaitTime  where ProStat.ProDate = right('00'+ltrim(str(MONTH(getdate()))),2)+right('00'+ltrim(str(day(getdate()))),2)+right('00'+ltrim(str(year(getdate()))),2) and ProStat.LocationID = @LocationID


				end
		end

End
GO

/****** Object:  StoredProcedure [dbo].[stp_Labor]    Script Date: 1/17/2016 11:22:57 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO





CREATE PROCEDURE [dbo].[stp_Labor]
@LocationID int

AS
begin

SET NOCOUNT ON

Declare @UserID int,
	@Cdatetime smalldatetime,
	@Caction bit,
	@lastUserID int,
	@lastCaction Bit,
	@LastCdatetime smalldatetime,
	@onclock int,
	@Ctype int,
	@PayRate   dec(10,2),
	@Cost   dec(10,2),
	@hours dec(10,2),
	@hours2 dec(10,2),
	@Lasthours dec(10,2),
	@Totalhours dec(10,2),
	@Salery dec(10,2) ,
	@TotalLabor dec (10,2),
	@TotalRev Dec (10,2),
	@PerRev Dec (10,2),
	@DetailComp dec (10,2),
	@TotalSalery dec (10,2)

Declare @ProDate char(6),
		@avgLabor decimal(18,2), 
		@MaxLabor decimal(18,2), 
		@MinLabor decimal(18,2)


create table #Temp1  (LocationID int,UserID int, Caction int, Cost dec(10,2), hours dec(10,2) )

set @Totalhours = 0
set @TotalLabor = 0
set @DetailComp = 0
set @TotalSalery = 0

set @LastUserID = 0

Declare B1 Cursor local for 
	SELECT  TimeClock.UserID, TimeClock.Cdatetime, TimeClock.Caction, 
   		 TimeClock.CType, LM_Users.PayRate, LM_Users.Salery
	FROM TimeClock with (nolock) INNER JOIN
   		 LM_Users with (nolock) ON 
   		 TimeClock.UserID = LM_Users.UserID
		WHERE (DATEPART(Month, TimeClock.Cdatetime)   = MONTH(GETDATE())) AND (DATEPART(Day, 
  		  TimeClock.Cdatetime) = Day(GETDATE())) AND 
   		 (DATEPART(Year, TimeClock.Cdatetime) = YEAR(GETDATE()))  AND CAST(LM_Users.Salery AS Numeric) = 0 
		 AND TimeClock.CType <> 9 and TimeClock.LocationID = @LocationID
		 AND TimeClock.LocationID = @LocationID
		 AND LM_Users.LocationID = @LocationID
		ORDER BY TimeClock.UserID, TimeClock.Cdatetime
	

	Open B1
	Fetch B1 into  @UserID,@Cdatetime,@Caction, @CType, @PayRate, @Salery
	While @@Fetch_Status = 0
	Begin

	
		IF @UserID = @LastUserID  
			Begin

				IF @Caction = 0
					Begin

						set @hours =  (DATEDIFF(N,@Cdatetime,getdate()))
						Set @hours = @LastHours + @Hours
						Set @hours =(@hours/60)
						set @Cost = @hours * @PayRate
					End
				ELSE
					Begin
						set @hours = DATEDIFF(N, @LastCdatetime,@Cdatetime)
						Set @hours =   @LastHours + @Hours

						set @LastHours = @hours
						Set @hours = (@hours/60)
						set @Cost = @hours * @PayRate

				End

				update #temp1 set hours=@hours, Cost=@cost  where UserID=@UserID

			End
		Else
			Begin

				set @LastHours = 0
					Begin
						set @hours =  (DATEDIFF(N,@Cdatetime,getdate()))
						Set @hours =( @hours/60)
						set @Cost = @hours * @PayRate 
						set @lastCaction = @Caction
						Insert #Temp1  (LocationID,UserID, Caction, Cost, hours) values( @LocationID, @UserID, @Caction, @Cost, @hours)	
					End
			End
		set @LastUserID = @UserID
		set @LastCdatetime = @Cdatetime


	Fetch Next From B1 into @UserID,@Cdatetime,@Caction, @CType, @PayRate, @Salery
	End
Close B1
DEALLOCATE B1
	


Select @Totalhours = isnull(sum(Cost),0)   from #temp1  with (nolock)

IF @Totalhours > 0
Begin

SELECT @DetailComp = isnull(SUM(Comamt) ,0.0) FROM DetailComp with (nolock) WHERE  (DATEPART(Month, Cdatetime) = MONTH(GETDATE())) 
	AND (DATEPART(Day, Cdatetime)  = Day(GETDATE())) 
	AND (DATEPART(Year, Cdatetime) = YEAR(GETDATE()))
	AND DetailComp.LocationID = @LocationID

SELECT @TotalSalery = SUM(cast(salery AS dec(10, 2)))  FROM LM_Users with (nolock) WHERE Active = 1 AND LM_Users.LocationID = @LocationID

Set @TotalSalery = round(@TotalSalery/7,2)


set @TotalLabor = @Totalhours+@TotalSalery

end


SELECT DISTINCT 
    Rec.LocationID,rec.recid, ISNULL(REC.gtotal, 0.0) AS gtotal, 
    ISNULL(REC.accamt, 0.0) AS accamt, REC.CloseDte
into #temprec
FROM REC with (nolock) INNER JOIN
    RECITEM with (nolock) ON 
    REC.recid = RECITEM.recId INNER JOIN
    Product with (nolock) ON 
    RECITEM.ProdID = Product.ProdID
WHERE (Product.dept = 1) AND (REC.CloseDte > '1/1/2013') AND 
    (Product.ProdID = 1 OR
    Product.ProdID = 2 OR
    Product.ProdID = 3)
	AND REC.LocationID = @LocationID
	AND RECITEM.LocationID = @LocationID
ORDER BY REC.recid

SELECT @TotalRev = SUM(#temprec.gtotal - #temprec.accamt)
FROM #temprec with (nolock)
WHERE   (DATEPART(Month, closedte) = MONTH(GETDATE())) AND 
    (DATEPART(Day, closedte) = Day(GETDATE())) AND 
    (DATEPART(Year, closedte) = YEAR(GETDATE()))
		AND #temprec.LocationID = @LocationID

drop table #temprec

IF @TotalRev is null
begin
	set @TotalRev = 0.0
end

IF @TotalRev < 0.0
Begin
	set @TotalRev = 0.0

End

IF @TotalLabor =0.0 and @TotalRev = 0.0 

	Begin
		set @PerRev  = 0.0
	END
ELSE
	Begin
		IF @TotalRev - @TotalLabor > 0 
			Begin
	 			set @PerRev  = ( round( ((@TotalRev-@TotalLabor)/@TotalRev)*100,2))
			End
		Else
			Begin 
				--print 'Calc Loss'
	 			set @PerRev  = ( round( ((@TotalLabor-@TotalRev)/@TotalLabor)*-100,2))		
			End
	End
update stats set Labor=@PerRev,TotalLabor = @TotalLabor, TotalRev=@TotalRev,Totalhours=@Totalhours, TotalSalery=@TotalSalery,DetailComp=@DetailComp where stats.LocationID=@LocationID

drop table #temp1

	set @ProDate =''
	Select @ProDate=ProStat.ProDate,@avgLabor=isnull(avgLabor,0), @MaxLabor=isnull(MaxLabor,-100), @MinLabor=isnull(MinLabor,0) from ProStat with (nolock) where ProStat.ProDate = right('00'+ltrim(str(MONTH(getdate()))),2)+right('00'+ltrim(str(day(getdate()))),2)+right('00'+ltrim(str(year(getdate()))),2) and ProStat.LocationID=@LocationID
	if len(ltrim(@ProDate)) = 0
		begin
			insert ProStat (LocationID, ProDate, avgLabor, MaxLabor, MinLabor ) values(@LocationID, right('00'+ltrim(str(MONTH(getdate()))),2)+right('00'+ltrim(str(day(getdate()))),2)+right('00'+ltrim(str(year(getdate()))),2) , @PerRev, @PerRev, @PerRev)
		end
	else
		begin
			If @PerRev > @MaxLabor
				begin
					set @MaxLabor = @PerRev
				end
			If @PerRev < @MinLabor
				begin
					set @MinLabor = @PerRev
				end

			set @avgLabor = (@avgLabor+@PerRev)/2


			update ProStat set avgLabor=@avgLabor,MaxLabor=@MaxLabor,MinLabor=@MinLabor  where ProStat.ProDate = right('00'+ltrim(str(MONTH(getdate()))),2)+right('00'+ltrim(str(day(getdate()))),2)+right('00'+ltrim(str(year(getdate()))),2) and ProStat.LocationID=@LocationID 


		end


End
GO

/****** Object:  StoredProcedure [dbo].[stp_MonthlySales]    Script Date: 1/17/2016 11:22:57 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO



CREATE PROCEDURE [dbo].[stp_MonthlySales]
@LocationID int,
@rptmonth  varchar(4),
@rptYear  varchar(4)

AS
begin


SELECT     COUNT(*) AS Items,MONTH(REC.CloseDte) AS MM,YEAR(REC.CloseDte) AS YYYY, REC.SalesRep, Product.Descript, Product.Price, Product.dept, LM_Users.FirstName + ' ' + LM_Users.LastName AS name
FROM         REC INNER JOIN
                      RECITEM ON REC.recid = RECITEM.recId INNER JOIN
                      Product ON RECITEM.ProdID = Product.ProdID INNER JOIN
                      LM_Users ON REC.SalesRep = LM_Users.UserID
WHERE  (REC.LocationID = @LocationID) and (RECITEM.LocationID = @LocationID) and (LM_Users.LocationID = @LocationID)  and (MONTH(REC.CloseDte) = @rptmonth) AND (YEAR(REC.CloseDte) = @rptYEAR) AND (Product.Price > 0)
GROUP BY MONTH(REC.CloseDte), YEAR(REC.CloseDte),REC.SalesRep, Product.Descript, Product.Price, Product.dept, LM_Users.FirstName, LM_Users.LastName
Order by name,dept,Descript
End

GO

/****** Object:  StoredProcedure [dbo].[stp_MonthlyTip]    Script Date: 1/17/2016 11:22:57 PM ******/
SET ANSI_NULLS OFF
GO

SET QUOTED_IDENTIFIER OFF
GO


create PROCEDURE [dbo].[stp_MonthlyTip]
@LocationID int,
@rptMonth varchar(2),
@rptYear  varchar(4),
@tiptotal numeric(10,2)

AS
begin

Declare @UserID int,
	@LoginID varchar(16),
	@Payee varchar(50),
	@ReportDate smalldatetime,
	@rptDate smalldatetime,
	@timeVal  datetime,
	@timeLast  datetime,
	@Cact bit,
	@Paid bit,
	@ListDesc varchar(20),
	@PayRate char(10),
	@ComRate money,
	@Hours decimal(10,2),
	@tiphour  decimal(10,4),
	@totalhours  decimal(10,4),
	@TIPOVER int,
	@TIP  int,
	@TIPon bit
	

create table #Temp1  (UserID int, LoginID  varchar(16), ReportDate smalldatetime, Payee varchar(50),ListDesc varchar(20), Hours decimal(10,2), PayRate char(10), ComRate money,Tipon bit)
create table #Temp2  (UserID int, LoginID  varchar(16), ReportDate smalldatetime, Payee varchar(50), Hours decimal(10,2), PayRate char(10), ComRate money, tip numeric(10,2), tipover int,Tipon bit )

set @rptDate = @rptMonth+'/1/'+@rptYear

Declare B1 Cursor local for 
	 SELECT    TimeClock.UserID, LM_Users.LoginID, LM_Users.tip as Tipon,
	   LM_Users.LastName+' '+ LM_Users.FirstName as Payee, 
	   TimeClock.Cdatetime,  TimeClock.Caction, TimeClock.Paid, 
	     isnull(LM_Users.PayRate,0) as PayRate
	FROM LM_Users(NOLOCK) INNER JOIN
	    TimeClock(NOLOCK) ON 
	    LM_Users.UserID = TimeClock.UserID
	WHERE (TimeClock.LocationID = @LocationID) and (LM_Users.LocationID = @LocationID) and  Month(TimeClock.Cdatetime) =@rptMonth  AND year(TimeClock.Cdatetime) =@rptYear and ctype <> 9
   	ORDER BY TimeClock.UserID,TimeClock.Cdatetime, TimeClock.clockid

	Open B1
	Fetch B1 into  @UserID,@LoginID,@TIPon,@Payee,@timeVal,@Cact,@Paid,@PayRate
	While @@Fetch_Status = 0
	Begin
		IF @Cact = 0 
			Begin
				set @timeLast = @timeVal
			End
		Else
			Begin
				set @Hours = (CAST(DATEDIFF(n, @timeLast, @timeVal) as decimal(10,2)) /60)
				Insert #Temp1  (UserID,ReportDate,LoginID,Payee, Hours,PayRate,TIPon) values (@UserID, @rptDate, @LoginID, @Payee, @Hours,@PayRate,@TIPon)			
			End
	Fetch Next From B1 into @UserID,@LoginID,@TIPon,@Payee,@timeVal,@Cact,@Paid,@PayRate
	End
	
Close B1
DEALLOCATE B1


Declare B2 Cursor local for 
	SELECT DetailComp.UserID,  LM_Users.LoginID,LM_Users.tip as Tipon,
	     LM_Users.LastName+' '+ LM_Users.FirstName  AS Payee, 
	    SUM(DetailComp.ComAmt) AS ComRate,  isnull(LM_Users.PayRate,0) as PayRate
	FROM LM_Users(nolock) INNER JOIN
	    DetailComp(nolock) ON 
	    LM_Users.UserID = DetailComp.UserID
	WHERE (DetailComp.LocationID = @LocationID) and (LM_Users.LocationID = @LocationID) and  Month(DetailComp.Cdatetime) =@rptMonth  AND year(DetailComp.Cdatetime) =@rptYear 
	GROUP BY DetailComp.UserID,  LM_Users.LoginID,
	    LM_Users.LastName, 
	    LM_Users.FirstName, LM_Users.PayRate,LM_Users.tip
	ORDER BY DetailComp.UserID

	Open B2
	Fetch B2 into  @UserID,@LoginID,@TIPon,@Payee, @ComRate,@PayRate
	While @@Fetch_Status = 0
	Begin
		Insert #Temp1  (UserID,ReportDate,LoginID,Payee,Hours,PayRate,TIPon) values (@UserID, @rptDate,@LoginID, @Payee,0.0,@PayRate,@TIPon)
		update  #Temp1 set ComRate = @ComRate where UserID=@UserID
			
	Fetch Next From B2 into  @UserID,@LoginID,@TIPon,@Payee, @ComRate,@PayRate
	End

Close B2
DEALLOCATE B2
set @totalhours = 0
set @tipOver = 0.00

Declare B3 Cursor local for 

select UserID, ReportDate,LoginID, Payee, SUM(Hours) as hours, PayRate, ComRate,TIPon from #temp1   group by UserID,ReportDate,LoginID, Payee, PayRate, ComRate,TIPon order by Payee
	Open B3
	Fetch B3 into  @UserID, @ReportDate, @LoginID, @Payee, @Hours,@PayRate,@ComRate,@TIPon
	While @@Fetch_Status = 0
	Begin
		
		IF @TIPon = 1  AND @Hours>0 
			begin
		set @totalhours = @totalhours + @Hours
			end

		Insert #Temp2  (UserID,ReportDate,LoginID,Payee, Hours,PayRate,ComRate,TIPon) values (@UserID, @ReportDate, @LoginID, @Payee, @Hours,@PayRate,@ComRate,@TIPon)		
	Fetch Next From B3 into  @UserID, @ReportDate, @LoginID, @Payee, @Hours,@PayRate,@ComRate,@TIPon
	End
Close B3
DEALLOCATE B3
print @tiptotal
print @totalhours

IF @tiptotal> 0 AND @totalhours>0 
	BEGIN
		set @tiphour = @tiptotal/@totalhours
	
	Declare B4 Cursor local for 
		select UserID,Hours from #temp2 where tipon=1
		Open B4
		Fetch B4 into  @UserID, @Hours
		While @@Fetch_Status = 0
		Begin
			set @tip =(@Hours *@tiphour) 
			--set @tip =CAST(@tip AS INT)
			set @tipOver = @tipOver+@tip
			update  #Temp2 set Tip = @tip where UserID=@UserID
		Fetch Next From B4 into  @UserID, @Hours
		End
	Close B4
	DEALLOCATE B4

	update  #Temp2 set tipOver = @tipOver

	END

Select * from #temp2

End
GO

/****** Object:  StoredProcedure [dbo].[stp_MonthlyWashes]    Script Date: 1/17/2016 11:22:57 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================


create PROCEDURE [dbo].[stp_MonthlyWashes] 
@LocationID int,
@Month  int,
@Year int

AS
begin


SELECT DISTINCT 
    1 AS Washes, REC.recid, 
    client.fname + ' ' + client.lname AS name, REC.CloseDte, 
    CustAcc.ClientID, REC.totalamt, vehical.vmodel, 
    LM_ListItem.ListDesc AS color
FROM REC INNER JOIN
    CustAcc ON REC.clientid = CustAcc.ClientID INNER JOIN
    client ON REC.clientid = client.clientid INNER JOIN
    vehical ON REC.vehID = vehical.vehid INNER JOIN
    LM_ListItem ON vehical.Color = LM_ListItem.ListValue AND 
    LM_ListItem.ListType = 5
WHERE  (REC.LocationID = @LocationID) and (REC.accamt > 0) AND (CustAcc.Type = 2) AND  client.Ctype=2 AND
    (CustAcc.Status = 1) AND ({ fn MONTH(REC.CloseDte) } = @Month) AND ({ fn Year(REC.CloseDte) } = @Year)
GROUP BY client.fname, client.lname, REC.CloseDte, 
    CustAcc.ClientID, REC.totalamt, REC.recid, vehical.vmodel, 
    vehical.Color, vehical.make, LM_ListItem.ListDesc
End

GO

/****** Object:  StoredProcedure [dbo].[stp_Receipt]    Script Date: 1/17/2016 11:22:57 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE  [dbo].[stp_Receipt]
@LocationID int,
@RecID int

AS
begin

SELECT        REC.totalamt, REC.accamt, REC.GiftCardAmt, REC.gtotal, REC.cashamt, REC.CheckAmt, REC.chargeamt, REC.cashback, Product.Descript, RECITEM.Price, RECITEM.QTY, RECITEM.taxable, REC.Approval, 
                         LM_ListItem.ListDesc AS cardtype, LM_Locations.LocationDesc, LM_Locations.Address1, LM_Locations.City+', '+LM_Locations.State+' '+LM_Locations.PostalCode as CityStateZip, LM_Locations.Phone,Case when RECITEM.taxable=1 then (RECITEM.Price*LM_Locations.TaxRate) else 0.00 end as TaxRate 
FROM            REC WITH (nolock) INNER JOIN
                         RECITEM WITH (nolock) ON REC.recid = RECITEM.recId AND RECITEM.LocationID = @LocationID INNER JOIN
                         Product WITH (nolock) ON RECITEM.ProdID = Product.ProdID AND Product.Descript <> 'None' INNER JOIN
                         LM_Locations ON REC.LocationID = LM_Locations.LocationID LEFT OUTER JOIN
                         LM_ListItem WITH (nolock) ON REC.cardtype = LM_ListItem.ListValue AND LM_ListItem.ListType = 12
WHERE        (REC.recid = @recID) AND (REC.LocationID =  @LocationID)
ORDER BY RECITEM.RecItemID

End
GO

/****** Object:  StoredProcedure [dbo].[stp_Ticket]    Script Date: 1/17/2016 11:22:58 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[stp_Ticket]
@LocationID int,
@RecID int

AS
begin


SELECT        REC.recid, REC.DayCnt, REC.esttime, REC.datein, REC.Vmodel, REC.Status, LM_ListItem.ListDesc AS vehman, LM_ListItem2.ListDesc AS vehcolor, client.fname + ' ' + client.lname AS Name, client.Phone, 
                         REC.Notes, REC.Line, REC.clientid, vehical.UPC, dbo.stats.CurrentWaitTime
FROM            dbo.REC WITH (NOLOCK) INNER JOIN
                         dbo.LM_ListItem WITH (NOLOCK) ON REC.VehMan = LM_ListItem.ListValue AND LM_ListItem.ListType = 3 INNER JOIN
                         dbo.LM_ListItem AS LM_ListItem2 WITH (NOLOCK) ON REC.VehColor = LM_ListItem2.ListValue AND LM_ListItem2.ListType = 5 INNER JOIN
                         dbo.stats WITH (NOLOCK) ON dbo.REC.LocationID = dbo.stats.LocationID LEFT OUTER JOIN
                         dbo.client WITH (NOLOCK) ON REC.clientid = client.clientid LEFT OUTER JOIN
                         dbo.vehical WITH (NOLOCK) ON REC.vehID = vehical.vehid
WHERE (Rec.LocationID = @LocationID) AND (REC.recid = @recID)

End
GO

/****** Object:  StoredProcedure [dbo].[stp_TicketAir]    Script Date: 1/17/2016 11:22:58 PM ******/
SET ANSI_NULLS OFF
GO

SET QUOTED_IDENTIFIER OFF
GO



CREATE PROCEDURE [dbo].[stp_TicketAir]
@LocationID int,
@RecID int

AS
begin

		SELECT Descript FROM  dbo.Product
		 INNER JOIN dbo.RECITEM WITH (nolock) ON dbo.Product.ProdID = dbo.RECITEM.ProdID
		WHERE  (Product.cat = 21 ) AND (dbo.RECITEM.LocationID = @LocationID) AND (dbo.RECITEM.recId = @RecID)

End


GO

/****** Object:  StoredProcedure [dbo].[stp_TicketSer]    Script Date: 1/17/2016 11:22:58 PM ******/
SET ANSI_NULLS OFF
GO

SET QUOTED_IDENTIFIER OFF
GO



CREATE PROCEDURE [dbo].[stp_TicketSer]
@LocationID int,
@RecID int,
@start int,
@end int

AS
begin

Declare @Line int,
	 @ProdID int,
	 @Opt int,
	 @Descript varchar(20), 
	 @price money,
	 @number int,
	 @checked int

create table #Temp1  (descript  varchar(20), price money,checked int)


Declare B1 Cursor local for 
	SELECT DISTINCT  ProdID,Descript, Price, number,Opt
	FROM Product with (nolock)
	WHERE (Product.cat = 3) and Product.Number >= @start and  Product.Number <= @end ORDER BY number ASC
	
	Open B1
	Fetch B1 into  @ProdID,@Descript,@price,@number, @Opt
	While @@Fetch_Status = 0
	Begin
		IF @Opt = 1
			Begin

				Declare B2 Cursor local for 
						SELECT  Product.ProdID,Product.Descript, Product.Price
						 FROM Product(NOLOCK)
						 INNER JOIN ProdOpt(NOLOCK) ON Product.ProdID = ProdOpt.ProductID
						 LEFT OUTER JOIN RECITEM(NOLOCK) ON Product.ProdID = RECITEM.ProdID
						 AND RECITEM.recId =@RECID AND (RECITEM.LocationID = @LocationID)
						 WHERE (ProdOPT.OptID = @ProdID) AND RECITEM.QTY = 1
						 ORDER BY Product.Number
				Open B2
				Fetch B2  into  @ProdID,@Descript,@price
				IF @@Fetch_Status = 0
					Begin
						Insert #Temp1  (Descript,price,checked) values (@Descript,@price,1)

					End
				ELSE
					Begin
						Insert #Temp1  (Descript,price,checked) values (@Descript,@price,0)
					End
				Close B2
				DEALLOCATE B2

			End
		Else
			Begin

				set @checked = (SELECT 1 AS checked FROM RECITEM WHERE (RECITEM.recid = @recID) AND (RECITEM.LocationID = @LocationID) AND RECITEM.prodid = @ProdID )	
		
				IF len(@checked) > 0 
					Begin
						Insert #Temp1  (Descript,price,checked) values (@Descript,@price,@checked)
					End
				Else
					Begin
						Insert #Temp1  (Descript,price,checked) values (@Descript,@price,0)
					End
			End
	Fetch Next From B1 into @ProdID,@Descript,@price,@number, @Opt
	End
	


Close B1
DEALLOCATE B1



Select * from #temp1

drop table #temp1


End


GO

/****** Object:  StoredProcedure [dbo].[stp_TicketUP]    Script Date: 1/17/2016 11:22:58 PM ******/
SET ANSI_NULLS OFF
GO

SET QUOTED_IDENTIFIER OFF
GO



CREATE PROCEDURE [dbo].[stp_TicketUP]
@Line int,
@LocationID int,
@RecID int


AS
begin

IF @line <4 	-- Wash
	Begin

		SELECT        LM_ListItem.ListDesc, Product.Price
		FROM            dbo.Product INNER JOIN
								 dbo.LM_ListItem ON Product.cat = LM_ListItem.ListValue AND LM_ListItem.listtype = 1 INNER JOIN
								 dbo.RECITEM WITH (nolock) ON dbo.Product.ProdID = dbo.RECITEM.ProdID
		WHERE        (Product.ProdID = 5 OR
								 Product.ProdID = 7 OR
								 Product.ProdID = 8 OR
								 Product.ProdID = 9 OR
								 Product.ProdID = 10) AND (dbo.RECITEM.LocationID = @LocationID) AND (dbo.RECITEM.recId = @RecID)
		ORDER BY dbo.LM_ListItem.ListDesc

	End
Else		--Detail
	Begin


		SELECT        LM_ListItem.ListDesc, Product.Price
		FROM            dbo.Product INNER JOIN
								 dbo.LM_ListItem ON Product.cat = LM_ListItem.ListValue AND LM_ListItem.listtype = 1 INNER JOIN
								 dbo.RECITEM WITH (nolock) ON dbo.Product.ProdID = dbo.RECITEM.ProdID
		WHERE        (Product.ProdID = 16 OR
			Product.ProdID = 17 OR
			Product.ProdID = 18 OR
			Product.ProdID = 19 OR
			Product.ProdID = 20) AND (dbo.RECITEM.LocationID = @LocationID) AND (dbo.RECITEM.recId = @RecID)
		ORDER BY dbo.LM_ListItem.ListDesc






	End


End


GO

/****** Object:  StoredProcedure [dbo].[stp_TicketWD]    Script Date: 1/17/2016 11:22:58 PM ******/
SET ANSI_NULLS OFF
GO

SET QUOTED_IDENTIFIER OFF
GO



CREATE PROCEDURE [dbo].[stp_TicketWD]
@Line int,
@LocationID int,
@RecID int


AS
begin



IF @line <4 	-- Wash
	Begin

		SELECT DISTINCT dbo.Product.Descript, dbo.Product.Price
		FROM            dbo.Product with (nolock) INNER JOIN
								 dbo.RECITEM with (nolock) ON dbo.Product.ProdID = dbo.RECITEM.ProdID
		WHERE        (dbo.Product.cat = 1) AND (dbo.RECITEM.LocationID = @LocationID) AND (dbo.RECITEM.recId = @RecID)

	End
Else		--Detail
	Begin

		SELECT DISTINCT dbo.Product.Descript, dbo.Product.Price
		FROM            dbo.Product with (nolock) INNER JOIN
								 dbo.RECITEM with (nolock) ON dbo.Product.ProdID = dbo.RECITEM.ProdID
		WHERE        (dbo.Product.cat = 2) AND (dbo.RECITEM.LocationID = @LocationID) AND (dbo.RECITEM.recId = @RecID)

	End

End


GO

/****** Object:  StoredProcedure [dbo].[stp_TimeSheet]    Script Date: 1/17/2016 11:22:58 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE  PROCEDURE [dbo].[stp_TimeSheet]
@LocationID int,
@CurrentDate varchar(10)

AS
begin

Declare @UserID int,
	@SheetID int,
	@weekof smalldatetime,
	@Payee varchar(50),
	@timeVal  datetime,
	@timeLast  datetime,
	@Cact bit,
	@Paid bit,
	@ListDesc varchar(20),
	@PayRate char(10),
	@ComRate money,
	@Hours decimal(10,2)

create table #Temp1  (UserID int, weekof smalldatetime, Payee varchar(50), Hours decimal(10,2), PayRate char(10), ComRate money)

SELECT TOP (1) @SheetID = SheetID,@weekof=weekof FROM dbo.TimeSheet WHERE (weekof <= @CurrentDate) AND (LocationID = @LocationID) ORDER BY SheetID DESC

Declare B1 Cursor local for 
SELECT        TimeClock.UserID, LM_Users.LastName + ' ' + LM_Users.FirstName AS Payee, TimeClock.Cdatetime as timeVa, TimeClock.Caction as Cact, ISNULL(LM_Users.PayRate, 0) AS PayRate
FROM            dbo.TimeClock INNER JOIN
                         dbo.LM_Users ON dbo.TimeClock.LocationID = dbo.LM_Users.LocationID AND dbo.TimeClock.UserID = dbo.LM_Users.UserID
WHERE        (dbo.TimeClock.LocationID = @LocationID) AND (TimeClock.Cdatetime BETWEEN @weekof AND DATEADD(day, 7, @weekof)) AND (dbo.LM_Users.LocationID = @LocationID) AND (TimeClock.CType <> 9)
ORDER BY TimeClock.UserID, TimeClock.Cdatetime, TimeClock.clockid

	
	Open B1
	Fetch B1 into  @UserID,@Payee,@timeVal,@Cact,@PayRate
	While @@Fetch_Status = 0
	Begin
		IF @Cact = 0 
			Begin
				set @timeLast = @timeVal
			End
		Else
			Begin
				set @Hours = (CAST(DATEDIFF(n, @timeLast, @timeVal) as decimal(10,2)) /60)
				
			Insert #Temp1  (UserID,weekof, Payee, Hours,PayRate) values (@UserID, @weekof, @Payee, @Hours,@PayRate)
			
			End

	Fetch Next From B1 into @UserID,@Payee,@timeVal,@Cact,@PayRate
	End
	


Close B1
DEALLOCATE B1

Declare B2 Cursor local for 
	SELECT DetailComp.UserID, TimeSheet.weekof, 
	     LM_Users.FirstName+' '+LM_Users.LastName  AS Payee, 
	    SUM(DetailComp.ComAmt) AS ComRate, isnull(LM_Users.PayRate,0) as PayRate
	FROM LM_Users(nolock) INNER JOIN
	    DetailComp(nolock) ON 
	    LM_Users.UserID = DetailComp.UserID, TimeSheet(nolock)
	WHERE (DetailComp.LocationID = @LocationID) and (LM_Users.LocationID = @LocationID) and (TimeSheet.LocationID = @LocationID) and  (TimeSheet.SheetID = @SheetID) AND 
	    (DetailComp.Cdatetime BETWEEN TimeSheet.weekof AND 
	    DATEADD(day, 7, TimeSheet.weekof))
	GROUP BY DetailComp.UserID, TimeSheet.weekof, 
	    LM_Users.LastName, 
	    LM_Users.FirstName, LM_Users.PayRate
	ORDER BY DetailComp.UserID




	Open B2
	Fetch B2 into  @UserID,@weekof,@Payee, @ComRate,@PayRate
	While @@Fetch_Status = 0
	Begin
		Insert #Temp1  (UserID,weekof,Payee,Hours,PayRate) values (@UserID, @weekof, @Payee,0.0,@PayRate)
		update  #Temp1 set ComRate = @ComRate where UserID=@UserID
			
	Fetch Next From B2 into  @UserID,@weekof,@Payee, @ComRate,@PayRate
	End
	


Close B2
DEALLOCATE B2



select UserID,weekof, Payee, SUM(Hours) as hours, PayRate, isnull(ComRate,0.0) as ComRate,dateadd(dd,7,weekof) as weekEnd,dateadd(dd,13,weekof) as PayDate from #temp1 group by UserID,weekof, Payee, PayRate, ComRate
End

GO

/****** Object:  StoredProcedure [dbo].[stp_TimeSummary]    Script Date: 1/17/2016 11:22:58 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[stp_TimeSummary]
@LocationID int,
@CurrentDate varchar(10)


AS
begin

Declare @UserID int,
	@SheetID int,
	@weekof smalldatetime,
	@LoginID varchar(16),
	@Payee varchar(50),
	@timeVal  datetime,
	@timeLast  datetime,
	@Cact bit,
	@CType char(10),
	@Paid bit,
	@ListDesc varchar(20),
	@PayRate char(10),
	@ComRate money,
	@AdjAmt money,
	@CollAmt money,
	@UnifAmt money,
	@Salery money,
	@Hours decimal(10,2)

create table #Temp1  (UserID int, LoginID  varchar(16), weekof smalldatetime, Payee varchar(50),Hours decimal(10,2), PayRate char(10), ComRate money, Salery money, AdjAmt money, CollAmt money, UnifAmt money)

SELECT TOP (1) @SheetID = SheetID,@weekof=weekof FROM dbo.TimeSheet WHERE (weekof <= @CurrentDate) AND (LocationID = @LocationID) ORDER BY SheetID DESC


Declare B1 Cursor local for 

SELECT        TimeClock.UserID, LM_Users.LoginID, LM_Users.LastName + ' ' + LM_Users.FirstName AS Payee, CAST(LM_Users.Salery AS Money) AS Salery, TimeClock.Cdatetime, TimeClock.Caction, TimeClock.Paid, 
                         TimeClock.CType, ISNULL(LM_Users.PayRate, 0) AS PayRate
FROM            dbo.TimeClock INNER JOIN
                         dbo.LM_Users ON dbo.TimeClock.LocationID = dbo.LM_Users.LocationID AND dbo.TimeClock.UserID = dbo.LM_Users.UserID
WHERE        (dbo.TimeClock.LocationID = @LocationID) AND (TimeClock.Cdatetime BETWEEN @weekof AND DATEADD(day, 7, @weekof)) AND (dbo.LM_Users.LocationID = @LocationID) AND (TimeClock.CType <> 9)
ORDER BY TimeClock.UserID, TimeClock.Cdatetime, TimeClock.clockid

	
	Open B1
	Fetch B1 into  @UserID,@LoginID,@Payee,@Salery, @timeVal,@Cact,@Paid,@CType,@PayRate
	While @@Fetch_Status = 0
	Begin
	
			IF @Cact = 0 
				Begin
					set @timeLast = @timeVal
				End
			Else
				Begin
					set @Hours = (CAST(DATEDIFF(n, @timeLast, @timeVal) as decimal(10,2)) /60)
					
					Insert #Temp1  (UserID,weekof,LoginID,Payee, Hours,PayRate,Salery,UnifAmt,CollAmt,AdjAmt) values (@UserID, @weekof, @LoginID, @Payee, @Hours,@PayRate,@Salery,0,0,0)
				End
	Fetch Next From B1 into @UserID,@LoginID,@Payee,@Salery, @timeVal,@Cact, @Paid, @CType,@PayRate
	End
	


Close B1
DEALLOCATE B1



Declare B2 Cursor local for 
	SELECT DetailComp.UserID, TimeSheet.weekof, LM_Users.LoginID, 
	    LM_Users.LastName + ' ' + LM_Users.FirstName AS Payee,  cast(LM_Users.Salery as Money) as Salery,
	    SUM(DetailComp.ComAmt)  AS ComAmt,  isnull(LM_Users.PayRate,0) as PayRate
	FROM LM_Users(nolock) INNER JOIN
	    DetailComp(nolock) ON 
	    LM_Users.UserID = DetailComp.UserID, TimeSheet(nolock)
	WHERE  (DetailComp.LocationID = @LocationID) and (LM_Users.LocationID = @LocationID) and (TimeSheet.LocationID = @LocationID) and  (TimeSheet.SheetID = @SheetID) AND 
	    (DetailComp.Cdatetime BETWEEN TimeSheet.weekof AND 
	    DATEADD(day, 7, TimeSheet.weekof))
	GROUP BY DetailComp.UserID, TimeSheet.weekof, 
	    LM_Users.LoginID, LM_Users.LastName, 
	    LM_Users.FirstName,LM_Users.Salery, LM_Users.PayRate
	ORDER BY DetailComp.UserID




	Open B2
	Fetch B2 into  @UserID,@weekof,@LoginID,@Payee,@Salery, @ComRate,@PayRate
	While @@Fetch_Status = 0
	Begin
		Insert #Temp1  (UserID,weekof,LoginID,Payee,Hours,PayRate,Salery,UnifAmt,CollAmt,AdjAmt) values (@UserID, @weekof, @LoginID, @Payee,0.0,@PayRate,@Salery,0,0,0)
		update  #Temp1 set ComRate = @ComRate where UserID=@UserID
			
	Fetch Next From B2 into  @UserID,@weekof,@LoginID,@Payee,@Salery, @ComRate,@PayRate
	End
	


Close B2
DEALLOCATE B2

Declare B3 Cursor local for 
	SELECT USERUnif.UserID,SUM(USERUnif.actAmt) AS UnifAmt
	FROM USERUnif (nolock)
	WHERE   (USERUnif.LocationID = @LocationID) and (USERUnif.SheetID  = @SheetID) 
	GROUP BY UserID, SheetID
	ORDER BY UserID
	Open B3
	Fetch B3 into  @UserID, @UnifAmt
	While @@Fetch_Status = 0
	Begin
		update  #Temp1 set UnifAmt = @UnifAmt where UserID=@UserID		
	Fetch Next From B3 into  @UserID,@UnifAmt
	End
Close B3
DEALLOCATE B3

Declare B4 Cursor local for 
	SELECT USERCol.UserID,SUM(USERCol.actAmt) AS CollAmt
	FROM USERCol (nolock)
	WHERE  (USERCol.LocationID = @LocationID) and (USERCol.SheetID  = @SheetID) 
	GROUP BY UserID, SheetID
	ORDER BY UserID
	Open B4
	Fetch B4 into  @UserID, @CollAmt
	While @@Fetch_Status = 0
	Begin
		update  #Temp1 set CollAmt = @CollAmt where UserID=@UserID		
	Fetch Next From B4 into  @UserID,@CollAmt
	End
Close B4
DEALLOCATE B4

Declare B5 Cursor local for 
	SELECT USERAdj.UserID,SUM(USERAdj.actAmt) AS AdjAmt
	FROM USERAdj (nolock)
	WHERE (USERAdj.LocationID = @LocationID) and (USERAdj.SheetID  = @SheetID) 
	GROUP BY UserID, SheetID
	ORDER BY UserID
	Open B5
	Fetch B5 into  @UserID, @AdjAmt
	While @@Fetch_Status = 0
	Begin
		update  #Temp1 set AdjAmt = @AdjAmt where UserID=@UserID		
	Fetch Next From B5 into  @UserID,@AdjAmt
	End
Close B5
DEALLOCATE B5

select UserID, weekof,LoginID, Payee, SUM(Hours) as hours, PayRate, isnull(ComRate,0.0) as ComRate,Salery,AdjAmt, CollAmt, UnifAmt from #temp1   group by UserID,weekof,LoginID, Payee, PayRate, ComRate,Salery,AdjAmt, CollAmt, UnifAmt order by Payee
End


GO

/****** Object:  StoredProcedure [dbo].[stp_VehicalCopy]    Script Date: 1/17/2016 11:22:58 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[stp_VehicalCopy]
@LocationID int,
@RecID int

AS
begin


SELECT        REC.recid, REC.DayCnt, REC.esttime, REC.datein, REC.Vmodel, REC.Status, LM_ListItem.ListDesc AS vehman, LM_ListItem2.ListDesc AS vehcolor, client.fname + ' ' + client.lname AS Name, client.Phone, 
                         REC.Notes, REC.Line, REC.clientid, vehical.UPC, dbo.stats.CurrentWaitTime, CASE WHEN dbo. Product .cat = 1 THEN rtrim(dbo. Product .Descript) 
                         + ' - Wash' WHEN dbo. Product .cat = 2 THEN rtrim(dbo. Product .Descript) + ' - Detail' WHEN dbo. Product .cat = 21 THEN rtrim(dbo. Product .Descript) + ' - Air Fresh' ELSE rtrim(dbo. Product .Descript) 
                         END AS Descript
FROM            dbo.REC WITH (NOLOCK) INNER JOIN
                         dbo.LM_ListItem WITH (NOLOCK) ON REC.VehMan = LM_ListItem.ListValue AND LM_ListItem.ListType = 3 INNER JOIN
                         dbo.LM_ListItem AS LM_ListItem2 WITH (NOLOCK) ON REC.VehColor = LM_ListItem2.ListValue AND LM_ListItem2.ListType = 5 INNER JOIN
                         dbo.stats WITH (NOLOCK) ON dbo.REC.LocationID = dbo.stats.LocationID INNER JOIN
                         dbo.RECITEM WITH (NOLOCK) ON dbo.REC.LocationID = dbo.RECITEM.LocationID AND dbo.REC.recid = dbo.RECITEM.recId INNER JOIN
                         dbo.Product WITH (NOLOCK) ON dbo.RECITEM.ProdID = dbo.Product.ProdID LEFT OUTER JOIN
                         dbo.client WITH (NOLOCK) ON REC.clientid = client.clientid LEFT OUTER JOIN
                         dbo.vehical WITH (NOLOCK) ON REC.vehID = vehical.vehid
WHERE (Rec.LocationID = @LocationID) AND (REC.recid = @recID)

End
GO

/****** Object:  StoredProcedure [dbo].[stp_Washers]    Script Date: 1/17/2016 11:22:58 PM ******/
SET ANSI_NULLS OFF
GO

SET QUOTED_IDENTIFIER ON
GO


create PROCEDURE [dbo].[stp_Washers]
@LocationID int

AS
begin

SET NOCOUNT ON

Declare @UserID int,
	@Cdatetime smalldatetime,
	@Caction bit,
	@lastUserID int,
	@onclock int
	

create table #Temp1  (UserID int, Caction int)
create table #Temp2  (UserID int, Caction int)

set @LastUserID = 0
Declare B1 Cursor local for 
	SELECT DISTINCT TimeClock.UserID,TimeClock.Cdatetime,TimeClock.Caction
	 FROM TimeClock with (NOLOCK) INNER JOIN LM_Users with (NOLOCK) ON TimeClock.UserID = LM_Users.UserID
	 WHERE (DATEPART(Month, TimeClock.Cdatetime) =  Month(getDATE() ) )
	 AND (DATEPART(Day, TimeClock.Cdatetime) =  Day(getdate()))
	 AND (DATEPART(Year, TimeClock.Cdatetime) = Year(getDate()))
	 AND CAST(LM_Users.Salery AS Numeric) = 0 AND TimeClock.CType <> 9
	 AND (TimeClock.LocationID = @LocationID)
	 ORDER BY TimeClock.UserID,TimeClock.Cdatetime
	

	Open B1
	Fetch B1 into  @UserID,@Cdatetime,@Caction
	While @@Fetch_Status = 0
	Begin
		IF @UserID = @LastUserID 
			Begin
				update #temp1 set Caction=@Caction where UserID=@UserID
			End
		Else
			Begin
				Insert #Temp1  (UserID,Caction)	values(@UserID, @Caction)	
			End

	set @LastUserID = @UserID
	Fetch Next From B1 into @UserID,@Cdatetime,@Caction
	End
Close B1
DEALLOCATE B1
	


Select @onclock = isnull(sum(1),0)   from #temp1 with (NOLOCK) where Caction=0
update stats set washers=@onclock
	
set @LastUserID = 0
Declare B2 Cursor local for 
	SELECT DISTINCT TimeClock.UserID,TimeClock.Cdatetime,TimeClock.Caction
	 FROM TimeClock  with (NOLOCK) INNER JOIN LM_Users with (NOLOCK) ON TimeClock.UserID = LM_Users.UserID
	 WHERE (DATEPART(Month, TimeClock.Cdatetime) =  Month(getDATE() ) )
	 AND (DATEPART(Day, TimeClock.Cdatetime) =  Day(getdate()))
	 AND (DATEPART(Year, TimeClock.Cdatetime) = Year(getDate()))
	 AND CAST(LM_Users.Salery AS Numeric) = 0 AND TimeClock.CType =9
	 AND (TimeClock.LocationID = @LocationID)
	 ORDER BY TimeClock.UserID,TimeClock.Cdatetime
	

	Open B2
	Fetch B2 into  @UserID,@Cdatetime,@Caction
	While @@Fetch_Status = 0
	Begin
		IF @UserID = @LastUserID 
			Begin
				update #temp2 set Caction=@Caction where UserID=@UserID
			End
		Else
			Begin
				Insert #Temp2  (UserID,Caction)	values(@UserID, @Caction)	
			End

	set @LastUserID = @UserID
	Fetch Next From B2 into @UserID,@Cdatetime,@Caction
	End
Close B2
DEALLOCATE B2
	


Select @onclock = isnull(sum(1),0)   from #temp2 with (NOLOCK) where Caction=0
update stats set Detailers=@onclock
	
drop table #temp1
drop table #temp2

End
GO

/****** Object:  StoredProcedure [dbo].[stp_YearlyWashes]    Script Date: 1/17/2016 11:22:59 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================

CREATE PROCEDURE [dbo].[stp_YearlyWashes] 
@LocationID int,
@Year int

AS
begin

Select MM ,count(Washes) as Washes,count(DISTINCT name) as Customers
  from (
SELECT DISTINCT 
    1 AS Washes, MONTH(REC.CloseDte) as MM, REC.recid, 
    client.fname + ' ' + client.lname AS name, REC.CloseDte, 
    CustAcc.ClientID, REC.totalamt, vehical.vmodel, 
    LM_ListItem.ListDesc AS color
FROM REC INNER JOIN
    CustAcc ON REC.clientid = CustAcc.ClientID INNER JOIN
    client ON REC.clientid = client.clientid INNER JOIN
    vehical ON REC.vehID = vehical.vehid INNER JOIN
    LM_ListItem ON vehical.Color = LM_ListItem.ListValue AND 
    LM_ListItem.ListType = 5
WHERE (REC.LocationID = @LocationID) and (REC.accamt > 0) AND (CustAcc.Type = 2) AND  client.Ctype=2 AND
    (CustAcc.Status = 1)  AND ({ fn Year(REC.CloseDte) } = @Year)
GROUP BY client.fname, client.lname, REC.CloseDte, 
    CustAcc.ClientID, REC.totalamt, REC.recid, vehical.vmodel, 
    vehical.Color, vehical.make, LM_ListItem.ListDesc)x1
	group by MM

End


GO

/****** Object:  StoredProcedure [dbo].[SyncMPOS]    Script Date: 1/17/2016 11:22:59 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


CREATE PROCEDURE [dbo].[SyncMPOS]

AS
BEGIN
	SET NOCOUNT ON;

Declare @HasID int

--LM_Users
Declare @LocationID int,
		@UserID smallint,
		@LoginID varchar(16) ,
		@Password varchar(16),
		@Initials varchar(5),
		@LastName varchar(25),
		@FirstName varchar(25),
		@Phone varchar(25),
		@Fax varchar(25),
		@Email varchar(50),
		@Active bit ,
		@CurrentIP varchar(15),
		@RoleID int,
		@Address varchar(80),
		@City varchar(50),
		@State char(2),
		@Zip char(5),
		@PayRate nchar(10),
		@SickRate nchar(10),
		@VacRate nchar(10),
		@ComRate nchar(10),
		@Hire datetime,
		@birth datetime,
		@Gender char(10),
		@SSNo char(11),
		@MStat char(1),
		@Exempt smallint,
		@Salery nchar(10),
		@LRT datetime,
		@TIP bit,
		@citizen tinyint,
		@AlienNo varchar(50),
		@AuthDate smalldatetime

--cashd
declare	@cashdid int,
	@ndate datetime,
	@DrawerNo int,
	@CIuserID int,
	@COUserID int,
	@CIp int,
	@CIn int,
	@CId int,
	@CIq int,
	@CIh int,
	@CI1 int,
	@CI5 int,
	@CI10 int,
	@CI20 int,
	@CI50 int,
	@CI100 int,
	@COp int,
	@COn int,
	@COd int,
	@COq int,
	@COh int,
	@CO1 int,
	@CO5 int,
	@CO10 int,
	@CO20 int,
	@CO50 int,
	@CO100 int,
	@CITime smalldatetime,
	@COTime smalldatetime,
	@CITotal money,
	@COTotal money,
	@CIRp int,
	@CIRn int,
	@CIRd int,
	@CIRq int,
	@CORp int,
	@CORn int,
	@CORd int,
	@CORq int,
	@COChecks money,
	@COCreditCards money,
	@COCreditCards2 money,
	@COPayouts money,
	@COCreditCards3 money,
	@COCreditCards4 money,
	@cd money,
	@cdday int,
	@bcday int,
	@amxday int,
	@dcday int,
	@dsday int,
	@md money,
	@pcchrg money


-- client 
declare	@clientid int,
		@Newclientid int,
		@Contact varchar(150),
		@LastContact varchar(150),
		@LastLocationID int,
		@Lastclientid int,
		@fname varchar(50),
		@lname varchar(50),
		@Lastfname varchar(50),
		@Lastlname varchar(50),
		@addr1 varchar(50),
		@addr2 varchar(50),
		@st char(2),
		@Ctype int,
		@C_Corp int,
		@LastPhone char(14),
		@Phone2 char(14),
		@PhoneType char(10),
		@Phone2Type char(10),
		@NoEmail bit,
		@Score char(1),
		@status int,
		@account bit,
		@StartDT smalldatetime,
		@Notes varchar(8000),
		@RecNote varchar(8000)

-- vehical 
declare	@NewVehID int,
		@vehid int,
		@upc char(10),
		@Lastvehid int,
		@Lastupc char(10),
		@tag char(10),
		@Lasttag char(10),
		@vehnum int,
		@make char(10),
		@model char(10),
		@vyear nchar(4),
		@Color char(10),
		@vmodel char(40) 
-- rec 
declare @recid int,
		@esttime datetime,
		@VehMan int,
		@VehMod int,
		@VehColor int,
		@datein datetime,
		@dateout datetime,
		@accamt money,
		@tax money,
		@gtotal money,
		@cashamt money,
		@chargeamt money,
		@cashback money,
		@cardtype int,
		@totalamt money,
		@PerAdj numeric(18, 0),
		@SalesRep int,
		@Line int,
		@DayCnt int,
		@cashoutid int,
		@CardNo varchar(20),
		@Approval varchar(20),
		@CheckAmt money,
		@ChkNo char(10),
		@ChkPhone char(14),
		@chkDL char(16),
		@GiftCardAmt money,
		@GiftCardID char(10),
		@CloseDte smalldatetime,
		@VINL5 char(5),
		@reg tinyint,
		@adv int,
		@QABy int,
		@estMin varchar(50),
		@Labor decimal(18, 0) 

-- recItem 
declare	@RecItemID int,
		@ProdID int,
		@Comm money,
		@Price money,
		@QTY int,
		@stotal money,
		@taxable tinyint,
		@taxamt money
		 
--CustAcc
declare	@CustAccID int,
		@CurrentAmt money,
		@ActiveDte smalldatetime,
		@LastUpdate smalldatetime,
		@LastUpdateBy int,
		@Type int,
		@MonthlyCharge money,
		@Limit money,
		@CCno char(20),
		@cctype int,
		@CCEXP char(5) 

--CustAccHist
declare @CustAccTID int,
		@TXCustID int,
		@TXRecID int,
		@TXLocationID int,
		@TXType char(20),
		@TXAmt money,
		@TXDte smalldatetime,
		@TXuser int,
		@TXNote varchar(8000),
		@Archive bit,
		@InvoiceID int 

--DetailComp
declare	@DetailCompID int,
		@CdateTime datetime,
		@ComAmt money,
		@paid bit,
		@ComPer int 

--DetailItem 
declare	@DetailItemID int,
		@ItemDesc varchar(50) 


--GiftCardHist 
declare	@GID int,
		@GiftCardTID int,
		@LastGiftCardID int,
		@TransDte smalldatetime,
		@TransType char(10),
		@TransAmt money,
		@TransUserID int

--ProdKit 
declare	@KitID int,
		@ProductID int 


--Product 
declare	@cat int,
		@dept int,
		@Descript char(20),
		@Number tinyint,
		@Inv tinyint,
		@KIT tinyint,
		@Bcode char(16),
		@ProdEstTime numeric(5,2),
		@vendor int,
		@altvendor int,
		@vpartnumb varchar(20),
		@Manufact varchar(20),
		@Cost money,
		@vendcost money,
		@editby int,
		@edited datetime,
		@createdby int,
		@created datetime,
		@Opt tinyint,
		@OptID int

--ProStat 
declare	@ProDate char(6),
		@Washes int,
		@Details int,
		@Extras decimal(18, 2),
		@TotalHours decimal(18, 0),
		@TotalRev decimal(18, 0),
		@TotalLabor decimal(18, 0),
		@PerRev decimal(18, 0),
		@TotalSalery decimal(18, 0),
		@DetailComp decimal(18, 0),
		@Washers int,
		@Detailers int,
		@Greaters int,
		@Managers int,
		@AvgLabor decimal(18, 2),
		@MaxLabor decimal(18, 2),
		@MinLabor decimal(18, 2),
		@AvgWaitTime decimal(18, 2),
		@MaxWaitTime decimal(18, 2),
		@MinWaitTime decimal(18, 2) 

--TimeClock 
declare	@ClockID int,
		@Caction int,
		@editdate datetime

--TimeSheet 
declare	@SheetID int,
		@Weekno int,
		@yearno int,
		@weekof smalldatetime,
		@empcnt int,
		@submitdate datetime,
		@appdate datetime,
		@compdate datetime 

--UserAdj 
declare	@AdjID int,
		@actAmt money,
		@actdate smalldatetime,
		@actdesc varchar(600)

--UserCol 
declare @CollID int,
		@ActID int,
		@Acttype char(10)

--UserUnif 
declare	@UnifID int,
		@ActCost money,
		@ActQty int

--vendor 
declare	@VenID int,
		@ven_vendor varchar(50),
		@vencontact varchar(50),
		@venaddr1 varchar(50),
		@venaddr2 varchar(50),
		@vencity varchar(50),
		@venst varchar(2),
		@VenZip char(5),
		@venPhone char(14),
		@venFax char(14),
		@venURL varchar(100),
		@venEmail varchar(100),
		@venAcc varchar(50) 

--LM_List
declare	@ListType smallint,
	@Description varchar(255),
	@valuewidth tinyint,
	@descwidth tinyint,
	@DataType char(1),
	@ListCode smallint,
	@ListValue varchar(255),
	@ListDesc varchar(255),
	@ItemType tinyint,
	@ItemOrder smallint


/*

Delete [MPOS].[dbo].RECQA
Delete [MPOS].[dbo].RECQA2
Delete [MPOS].[dbo].Payment



print 'LM_List'
Delete [MPOS].[dbo].LM_List
/* MDS02 LM_List */
Declare S1 Insensitive Cursor  for  
	SELECT  ListType, Description, valuewidth, descwidth, Type, DataType
		FROM [MDS02].[dbo].LM_List order by ListType
	open S1
	Fetch next from S1 into @ListType, @Description, @valuewidth, @descwidth, @Type, @DataType
	While @@FETCH_STATUS=0
	Begin
		INSERT into  [MPOS].[dbo].LM_List(ListType, Description, valuewidth, descwidth, Type, DataType) 
			Values (@ListType, @Description, @valuewidth, @descwidth, @Type, @DataType);  
	Fetch Next From S1 into  @ListType, @Description, @valuewidth, @descwidth, @Type, @DataType
	End
Close S1
DEALLOCATE S1

print 'LM_ListItem'
Delete [MPOS].[dbo].LM_ListItem
/* MDS02 LM_ListItem */
Declare S1 Insensitive Cursor  for  
	SELECT   ListType, ListCode, ListValue, ListDesc, ItemType, ItemOrder, Active
		FROM [MDS02].[dbo].LM_ListItem order by ListType
	open S1
	Fetch next from S1 into @ListType, @ListCode, @ListValue, @ListDesc, @ItemType, @ItemOrder, @Active
	While @@FETCH_STATUS=0
	Begin
		INSERT into  [MPOS].[dbo].LM_ListItem(ListType, ListCode, ListValue, ListDesc, ItemType, ItemOrder, Active) 
			Values (@ListType, @ListCode, @ListValue, @ListDesc, @ItemType, @ItemOrder, @Active);  
	Fetch Next From S1 into @ListType, @ListCode, @ListValue, @ListDesc, @ItemType, @ItemOrder, @Active
	End
Close S1
DEALLOCATE S1

Delete [MPOS].[dbo].LM_Users
/* MDS01 LM_Users */
set @LocationID =1
Declare S1 Insensitive Cursor  for  
	SELECT   UserID, LoginID, Password, Initials, LastName, FirstName, Phone, Fax, Email, Active, CurrentIP, 
			RoleID, Address, City, State, Zip, PayRate, SickRate, VacRate, ComRate, Hire, birth, Gender, SSNo, 
            MStat, Exempt, Salery, LRT, TIP, citizen, AlienNo, AuthDate
		FROM [MDS01].[dbo].LM_Users order by UserID
	open S1
	Fetch next from S1 into @UserID, @LoginID, @Password, @Initials, @LastName, @FirstName, @Phone, @Fax, @Email, @Active, @CurrentIP, 
			@RoleID, @Address, @City, @State, @Zip, @PayRate, @SickRate, @VacRate, @ComRate, @Hire, @birth, @Gender, @SSNo,
            @MStat, @Exempt, @Salery, @LRT, @TIP, @citizen, @AlienNo, @AuthDate   
	While @@FETCH_STATUS=0
	Begin

		set @HasID = 0
		SELECT  @HasID = UserID FROM [MPOS].[dbo].LM_Users WHERE (LocationID = @LocationID) AND (UserID = @UserID)

		if @HasID = 0 
			begin
				print 'Insert'
				INSERT into  [MPOS].[dbo].LM_Users(LocationID, UserID, LoginID, Password, Initials, LastName, FirstName, Phone, Fax, Email, Active, CurrentIP,
					 RoleID, Address, City, State, Zip, PayRate, SickRate, VacRate, ComRate, Hire, birth, Gender, 
                     SSNo, MStat, Exempt, Salery, LRT, TIP, citizen, AlienNo, AuthDate ) 
					Values 
				(@LocationID, @UserID, @LoginID, @Password, @Initials, @LastName, @FirstName, @Phone, @Fax, @Email, @Active, @CurrentIP,
					 @RoleID, @Address, @City, @State, @Zip, @PayRate, @SickRate, @VacRate, @ComRate, @Hire, @birth, @Gender, 
                     @SSNo, @MStat, @Exempt, @Salery, @LRT, @TIP, @citizen, @AlienNo, @AuthDate);  

			end
		else
			begin
				print 'Update'
				Update [MPOS].[dbo].LM_Users set
				FirstName=@FirstName,
				LastName=@LastName,
				RoleID=@RoleID,
				Phone=@Phone,
				Email=@Email,
				Active=@Active,
				LoginID=@LoginID,
				Password=@Password,
				Initials=@Initials, 
				Address=@Address ,
				City=@City,
				State=@State ,
				Zip=@Zip,
				PayRate=@PayRate,
				SickRate=@SickRate,
				VacRate=@VacRate,
				ComRate=@ComRate,
				SSNo=@SSNo,
				Hire=@Hire,
				Birth=@Birth,
				LRT=@LRT,
				Gender=@Gender,
				MStat=@MStat,
				Salery=@Salery,
				Exempt = @Exempt,
				Tip = @tip,
				citizen = @citizen,
				AlienNo = @AlienNo,
				AuthDate = @AuthDate,
				Fax=@Fax
				where UserID=@UserID and LocationID=@LocationID;
			end



	Fetch Next From S1 into  @UserID, @LoginID, @Password, @Initials, @LastName, @FirstName, @Phone, @Fax, @Email, @Active, @CurrentIP, 
			@RoleID, @Address, @City, @State, @Zip, @PayRate, @SickRate, @VacRate, @ComRate, @Hire, @birth, @Gender, @SSNo,
            @MStat, @Exempt, @Salery, @LRT, @TIP, @citizen, @AlienNo, @AuthDate
	End
	Close S1
	DEALLOCATE S1

/* MDS02 LM_Users */
set @LocationID = 2
Declare S1 Insensitive Cursor  for  
	SELECT   UserID, LoginID, Password, Initials, LastName, FirstName, Phone, Fax, Email, Active, CurrentIP, 
			RoleID, Address, City, State, Zip, PayRate, SickRate, VacRate, ComRate, Hire, birth, Gender, SSNo, 
            MStat, Exempt, Salery, LRT, TIP, citizen, AlienNo, AuthDate
		FROM [MDS02].[dbo].LM_Users order by UserID
	open S1
	Fetch next from S1 into @UserID, @LoginID, @Password, @Initials, @LastName, @FirstName, @Phone, @Fax, @Email, @Active, @CurrentIP, 
			@RoleID, @Address, @City, @State, @Zip, @PayRate, @SickRate, @VacRate, @ComRate, @Hire, @birth, @Gender, @SSNo,
            @MStat, @Exempt, @Salery, @LRT, @TIP, @citizen, @AlienNo, @AuthDate   
	While @@FETCH_STATUS=0
	Begin

		set @HasID = 0
		SELECT  @HasID = UserID FROM [MPOS].[dbo].LM_Users WHERE (LocationID = @LocationID) AND (UserID = @UserID)

		if @HasID = 0 
			begin
				print 'Insert'
				INSERT into  [MPOS].[dbo].LM_Users(LocationID, UserID, LoginID, Password, Initials, LastName, FirstName, Phone, Fax, Email, Active, CurrentIP,
					 RoleID, Address, City, State, Zip, PayRate, SickRate, VacRate, ComRate, Hire, birth, Gender, 
                     SSNo, MStat, Exempt, Salery, LRT, TIP, citizen, AlienNo, AuthDate ) 
					Values 
				(@LocationID, @UserID, @LoginID, @Password, @Initials, @LastName, @FirstName, @Phone, @Fax, @Email, @Active, @CurrentIP,
					 @RoleID, @Address, @City, @State, @Zip, @PayRate, @SickRate, @VacRate, @ComRate, @Hire, @birth, @Gender, 
                     @SSNo, @MStat, @Exempt, @Salery, @LRT, @TIP, @citizen, @AlienNo, @AuthDate);  

			end
		else
			begin
				print 'Update'
				Update [MPOS].[dbo].LM_Users set
				FirstName=@FirstName,LastName=@LastName,RoleID=@RoleID,	Phone=@Phone,Email=@Email,
				Active=@Active,	LoginID=@LoginID,Password=@Password,Initials=@Initials,	Address=@Address ,
				City=@City,	State=@State ,Zip=@Zip,	PayRate=@PayRate,SickRate=@SickRate,VacRate=@VacRate,
				ComRate=@ComRate,SSNo=@SSNo,Hire=@Hire,	Birth=@Birth,LRT=@LRT,	Gender=@Gender,MStat=@MStat,
				Salery=@Salery,	Exempt = @Exempt,Tip = @tip,citizen = @citizen,	AlienNo = @AlienNo,
				AuthDate = @AuthDate,Fax=@Fax
				where UserID=@UserID and LocationID=@LocationID;
			end



	Fetch Next From S1 into  @UserID, @LoginID, @Password, @Initials, @LastName, @FirstName, @Phone, @Fax, @Email, @Active, @CurrentIP, 
			@RoleID, @Address, @City, @State, @Zip, @PayRate, @SickRate, @VacRate, @ComRate, @Hire, @birth, @Gender, @SSNo,
            @MStat, @Exempt, @Salery, @LRT, @TIP, @citizen, @AlienNo, @AuthDate
	End
	Close S1
	DEALLOCATE S1


Delete [MPOS].[dbo].cashd
/* MDS01 cashd */
set @LocationID =1
Declare S1 Insensitive Cursor  for  
	SELECT  cashdid, ndate, DrawerNo, CIuserID, COUserID, CIp, CIn, CId, CIq, CIh, CI1, CI5, CI10, CI20, CI50, CI100, COp, COn, COd, COq,
		 COh, CO1, CO5, CO10, CO20, CO50, CO100, CITime, COTime, 
		CITotal, COTotal, CIRp, CIRn, CIRd, CIRq, CORp, CORn, CORd, CORq, COChecks, COCreditCards, COCreditCards2, 
		COPayouts, COCreditCards3, COCreditCards4
		FROM [MDS01].[dbo].cashd order by cashdid
	open S1
	Fetch next from S1 into @cashdid, @ndate, @DrawerNo, @CIuserID, @COUserID, @CIp, @CIn, @CId, @CIq, @CIh, @CI1, @CI5, @CI10, @CI20, @CI50, @CI100, @COp, @COn, @COd, @COq, @COh, @CO1, @CO5, @CO10, @CO20, @CO50, @CO100, @CITime, @COTime, 
                         @CITotal, @COTotal, @CIRp, @CIRn, @CIRd, @CIRq, @CORp, @CORn, @CORd, @CORq, @COChecks, @COCreditCards, @COCreditCards2, @COPayouts, @COCreditCards3, @COCreditCards4
	While @@FETCH_STATUS=0
	Begin
		INSERT into  [MPOS].[dbo].cashd(LocationID, cashdid, ndate, DrawerNo, CIuserID, COUserID, CIp, CIn, CId, CIq, CIh, CI1, CI5, CI10, CI20, CI50, CI100, COp, COn, COd, COq,
		 COh, CO1, CO5, CO10, CO20, CO50, CO100, CITime, COTime, 
		CITotal, COTotal, CIRp, CIRn, CIRd, CIRq, CORp, CORn, CORd, CORq, COChecks, COCreditCards, COCreditCards2, 
		COPayouts, COCreditCards3, COCreditCards4) 
			Values 
		(@LocationID, @cashdid, @ndate, @DrawerNo, @CIuserID, @COUserID, @CIp, @CIn, @CId, @CIq, @CIh, @CI1, @CI5, @CI10, @CI20, @CI50, @CI100, @COp, @COn, @COd, @COq, @COh, @CO1, @CO5, @CO10, @CO20, @CO50, @CO100, @CITime, @COTime, 
                    @CITotal, @COTotal, @CIRp, @CIRn, @CIRd, @CIRq, @CORp, @CORn, @CORd, @CORq, @COChecks, @COCreditCards, @COCreditCards2, @COPayouts, @COCreditCards3, @COCreditCards4);  

		Fetch Next From S1 into  @cashdid, @ndate, @DrawerNo, @CIuserID, @COUserID, @CIp, @CIn, @CId, @CIq, @CIh, @CI1, @CI5, @CI10, @CI20, @CI50, @CI100, @COp, @COn, @COd, @COq, @COh, @CO1, @CO5, @CO10, @CO20, @CO50, @CO100, @CITime, @COTime, 
                         @CITotal, @COTotal, @CIRp, @CIRn, @CIRd, @CIRq, @CORp, @CORn, @CORd, @CORq, @COChecks, @COCreditCards, @COCreditCards2, @COPayouts, @COCreditCards3, @COCreditCards4
	End
	Close S1
	DEALLOCATE S1

/* MDS02 cashd */
set @LocationID = 2
Declare S1 Insensitive Cursor  for  
	SELECT  cashdid, ndate, DrawerNo, CIuserID, COUserID, CIp, CIn, CId, CIq, CIh, CI1, CI5, CI10, CI20, CI50, CI100, COp, COn, COd, COq, COh, CO1, CO5, CO10, CO20, CO50, CO100, CITime, COTime, CITotal, 
                         COTotal, CIRp, CIRn, CIRd, CIRq, CORp, CORn, CORd, CORq, COChecks, COCreditCards, COCreditCards2, COPayouts, COCreditCards3, COCreditCards4, cd, cdday, bcday, amxday, dcday, dsday, md, pcchrg
		FROM [MDS02].[dbo].cashd order by cashdid
	open S1
	Fetch next from S1 into @cashdid, @ndate, @DrawerNo, @CIuserID, @COUserID, @CIp, @CIn, @CId, @CIq, @CIh, @CI1, @CI5, @CI10, @CI20, @CI50, @CI100, @COp, @COn, @COd, @COq, @COh, @CO1, @CO5, @CO10, @CO20, @CO50, @CO100, @CITime, @COTime, 
                         @CITotal, @COTotal, @CIRp, @CIRn, @CIRd, @CIRq, @CORp, @CORn, @CORd, @CORq, @COChecks, @COCreditCards, @COCreditCards2, @COPayouts, @COCreditCards3, @COCreditCards4, @cd, @cdday, @bcday, @amxday, @dcday, @dsday, @md, 
                         @pcchrg   
	While @@FETCH_STATUS=0
	Begin

				INSERT into  [MPOS].[dbo].cashd(LocationID, cashdid, ndate, DrawerNo, CIuserID, COUserID, CIp, CIn, CId, CIq, CIh, CI1, CI5, CI10, CI20, CI50, CI100, COp, COn, COd, COq, COh, CO1, CO5, CO10, CO20, CO50, CO100, CITime, COTime, CITotal, 
                         COTotal, CIRp, CIRn, CIRd, CIRq, CORp, CORn, CORd, CORq, COChecks, COCreditCards, COCreditCards2, COPayouts, COCreditCards3, COCreditCards4, cd, cdday, bcday, amxday, dcday, dsday, md, pcchrg ) 
					Values 
				(@LocationID, @cashdid, @ndate, @DrawerNo, @CIuserID, @COUserID, @CIp, @CIn, @CId, @CIq, @CIh, @CI1, @CI5, @CI10, @CI20, @CI50, @CI100, @COp, @COn, @COd, @COq, @COh, @CO1, @CO5, @CO10, @CO20, @CO50, @CO100, @CITime, @COTime, 
                         @CITotal, @COTotal, @CIRp, @CIRn, @CIRd, @CIRq, @CORp, @CORn, @CORd, @CORq, @COChecks, @COCreditCards, @COCreditCards2, @COPayouts, @COCreditCards3, @COCreditCards4, @cd, @cdday, @bcday, @amxday, @dcday, @dsday, @md, 
                         @pcchrg);  



	Fetch Next From S1 into  @cashdid, @ndate, @DrawerNo, @CIuserID, @COUserID, @CIp, @CIn, @CId, @CIq, @CIh, @CI1, @CI5, @CI10, @CI20, @CI50, @CI100, @COp, @COn, @COd, @COq, @COh, @CO1, @CO5, @CO10, @CO20, @CO50, @CO100, @CITime, @COTime, 
                         @CITotal, @COTotal, @CIRp, @CIRn, @CIRd, @CIRq, @CORp, @CORn, @CORd, @CORq, @COChecks, @COCreditCards, @COCreditCards2, @COPayouts, @COCreditCards3, @COCreditCards4, @cd, @cdday, @bcday, @amxday, @dcday, @dsday, @md, 
                         @pcchrg
	End
	Close S1
	DEALLOCATE S1



print 'Rec'
Delete [MPOS].[dbo].REC
/* MDS01 REC */
set @LocationID =1
Declare S1 Insensitive Cursor  for  
	SELECT  recid, clientid, esttime, vehID, VehMan, VModel, VehMod, VehColor, datein, dateout, accamt, tax, gtotal, cashamt, chargeamt, cashback, cardtype, totalamt, PerAdj, Status, SalesRep, cast(Notes as varchar(8000)) as Notes, Line, DayCnt,
                          cashoutid, CardNo, Approval, CheckAmt, ChkNo, ChkPhone, chkDL, GiftCardAmt, GiftCardID, CloseDte, VINL5, Reg, adv, QABy, estMIN,  Labor
		FROM [MDS01].[dbo].REC order by recid
	open S1
	Fetch next from S1 into @recid, @clientid, @esttime, @vehID, @VehMan, @VModel, @VehMod, @VehColor, @datein, @dateout, @accamt, @tax, @gtotal, @cashamt, @chargeamt, @cashback, @cardtype, @totalamt, @PerAdj, @Status, @SalesRep, @Notes, @Line, @DayCnt,
                          @cashoutid, @CardNo, @Approval, @CheckAmt, @ChkNo, @ChkPhone, @chkDL, @GiftCardAmt, @GiftCardID, @CloseDte, @VINL5, @Reg, @adv, @QABy, @estMIN, @Labor
	While @@FETCH_STATUS=0
	Begin
		INSERT into  [MPOS].[dbo].REC(LocationID, recid, clientid, esttime, vehID, VehMan, VModel, VehMod, VehColor, datein, dateout, accamt, tax, gtotal, cashamt, chargeamt, cashback, cardtype, totalamt, PerAdj, Status, SalesRep, Notes, Line, DayCnt,
                          cashoutid, CardNo, Approval, CheckAmt, ChkNo, ChkPhone, chkDL, GiftCardAmt, GiftCardID, CloseDte, VINL5, Reg, adv, QABy, estMIN, Labor) 
			Values 
		(@LocationID, @recid, @clientid, @esttime, @vehID, @VehMan, @VModel, @VehMod, @VehColor, @datein, @dateout, @accamt, @tax, @gtotal, @cashamt, @chargeamt, @cashback, @cardtype, @totalamt, @PerAdj, @Status, @SalesRep, @Notes, @Line, @DayCnt,
                          @cashoutid, @CardNo, @Approval, @CheckAmt, @ChkNo, @ChkPhone, @chkDL, @GiftCardAmt, @GiftCardID, @CloseDte, @VINL5, @Reg, @adv, @QABy, @estMIN, @Labor);  

		Fetch Next From S1 into  @recid, @clientid, @esttime, @vehID, @VehMan, @VModel, @VehMod, @VehColor, @datein, @dateout, @accamt, @tax, @gtotal, @cashamt, @chargeamt, @cashback, @cardtype, @totalamt, @PerAdj, @Status, @SalesRep, @Notes, @Line, @DayCnt,
                          @cashoutid, @CardNo, @Approval, @CheckAmt, @ChkNo, @ChkPhone, @chkDL, @GiftCardAmt, @GiftCardID, @CloseDte, @VINL5, @Reg, @adv, @QABy, @estMIN,  @Labor
	End
	Close S1
	DEALLOCATE S1
/* MDS02 REC */
set @LocationID = 2
Declare S1 Insensitive Cursor  for  
	SELECT  recid, clientid, esttime, vehID, VehMan, VModel, VehMod, VehColor, datein, dateout, accamt, tax, gtotal, cashamt, chargeamt, cashback, cardtype, totalamt, PerAdj, Status, SalesRep, cast(Notes as varchar(8000)) as Notes, Line, DayCnt,
                          cashoutid, CardNo, Approval, CheckAmt, ChkNo, ChkPhone, chkDL, GiftCardAmt, GiftCardID, CloseDte, VINL5, Reg, adv, QABy, estMIN,  Labor
		FROM [MDS02].[dbo].REC order by recid
	open S1
	Fetch next from S1 into @recid, @clientid, @esttime, @vehID, @VehMan, @VModel, @VehMod, @VehColor, @datein, @dateout, @accamt, @tax, @gtotal, @cashamt, @chargeamt, @cashback, @cardtype, @totalamt, @PerAdj, @Status, @SalesRep, @Notes, @Line, @DayCnt,
                          @cashoutid, @CardNo, @Approval, @CheckAmt, @ChkNo, @ChkPhone, @chkDL, @GiftCardAmt, @GiftCardID, @CloseDte, @VINL5, @Reg, @adv, @QABy, @estMIN,  @Labor
	While @@FETCH_STATUS=0
	Begin
		INSERT into  [MPOS].[dbo].REC(LocationID, recid, clientid, esttime, vehID, VehMan, VModel, VehMod, VehColor, datein, dateout, accamt, tax, gtotal, cashamt, chargeamt, cashback, cardtype, totalamt, PerAdj, Status, SalesRep, Notes, Line, DayCnt,
                          cashoutid, CardNo, Approval, CheckAmt, ChkNo, ChkPhone, chkDL, GiftCardAmt, GiftCardID, CloseDte, VINL5, Reg, adv, QABy, estMIN, Labor) 
			Values 
		(@LocationID, @recid, @clientid, @esttime, @vehID, @VehMan, @VModel, @VehMod, @VehColor, @datein, @dateout, @accamt, @tax, @gtotal, @cashamt, @chargeamt, @cashback, @cardtype, @totalamt, @PerAdj, @Status, @SalesRep, @Notes, @Line, @DayCnt,
                          @cashoutid, @CardNo, @Approval, @CheckAmt, @ChkNo, @ChkPhone, @chkDL, @GiftCardAmt, @GiftCardID, @CloseDte, @VINL5, @Reg, @adv, @QABy, @estMIN, @Labor);  

		Fetch Next From S1 into  @recid, @clientid, @esttime, @vehID, @VehMan, @VModel, @VehMod, @VehColor, @datein, @dateout, @accamt, @tax, @gtotal, @cashamt, @chargeamt, @cashback, @cardtype, @totalamt, @PerAdj, @Status, @SalesRep, @Notes, @Line, @DayCnt,
                          @cashoutid, @CardNo, @Approval, @CheckAmt, @ChkNo, @ChkPhone, @chkDL, @GiftCardAmt, @GiftCardID, @CloseDte, @VINL5, @Reg, @adv, @QABy, @estMIN,  @Labor
	End
	Close S1
	DEALLOCATE S1

print 'RECITEM'

Delete [MPOS].[dbo].RECITEM
/* MDS01 RECITEM */
set @LocationID =1
Declare S1 Insensitive Cursor  for  
	SELECT  RecItemID, recId, ProdID, Comm, Price, QTY, stotal, taxable, taxamt, gtotal, peradj, GiftCardID
		FROM [MDS01].[dbo].RECITEM order by RecItemID
	open S1
	Fetch next from S1 into @RecItemID, @recId, @ProdID, @Comm, @Price, @QTY, @stotal, @taxable, @taxamt, @gtotal, @peradj, @GiftCardID
	While @@FETCH_STATUS=0
	Begin
		INSERT into  [MPOS].[dbo].RECITEM(RecItemID, LocationID, recId, ProdID, Comm, Price, QTY, stotal, taxable, taxamt, gtotal, peradj, GiftCardID) 
			Values 
		(@LocationID, @RecItemID, @recId, @ProdID, @Comm, @Price, @QTY, @stotal, @taxable, @taxamt, @gtotal, @peradj, @GiftCardID);  

		Fetch Next From S1 into  @RecItemID, @recId, @ProdID, @Comm, @Price, @QTY, @stotal, @taxable, @taxamt, @gtotal, @peradj, @GiftCardID
	End
	Close S1
	DEALLOCATE S1
/* MDS02 RECITEM */
set @LocationID = 2
Declare S1 Insensitive Cursor  for  
	SELECT RecItemID, recId, ProdID, Comm, Price, QTY, stotal, taxable, taxamt, gtotal, peradj, GiftCardID
		FROM [MDS02].[dbo].RECITEM order by RecItemID
	open S1
	Fetch next from S1 into @RecItemID, @recId, @ProdID, @Comm, @Price, @QTY, @stotal, @taxable, @taxamt, @gtotal, @peradj, @GiftCardID
	While @@FETCH_STATUS=0
	Begin
		INSERT into  [MPOS].[dbo].RECITEM(RecItemID, LocationID, recId, ProdID, Comm, Price, QTY, stotal, taxable, taxamt, gtotal, peradj, GiftCardID) 
			Values 
		(@LocationID, @RecItemID, @recId, @ProdID, @Comm, @Price, @QTY, @stotal, @taxable, @taxamt, @gtotal, @peradj, @GiftCardID);  

		Fetch Next From S1 into @RecItemID, @recId, @ProdID, @Comm, @Price, @QTY, @stotal, @taxable, @taxamt, @gtotal, @peradj, @GiftCardID
	End
	Close S1
	DEALLOCATE S1


print 'GiftCard'
Delete [MPOS].[dbo].GiftCard
/* MDS01 GiftCard */
set @LocationID =1
Declare S1 Insensitive Cursor  for  
	SELECT  GiftCardID, ActiveDte, CurrentAmt, RecID
		FROM [MDS01].[dbo].GiftCard order by GiftCardID
	open S1
	Fetch next from S1 into @GiftCardID, @ActiveDte, @CurrentAmt, @RecID
	While @@FETCH_STATUS=0
	Begin
		INSERT into  [MPOS].[dbo].GiftCard(LocationID, GiftCardID, ActiveDte, CurrentAmt, RecID) 
			Values 
		(@LocationID, @GiftCardID, @ActiveDte, @CurrentAmt, @RecID);  

		Fetch Next From S1 into  @GiftCardID, @ActiveDte, @CurrentAmt, @RecID
	End
	Close S1
	DEALLOCATE S1
/* MDS02 GiftCard */
set @LocationID = 2
Declare S1 Insensitive Cursor  for  
	SELECT  GiftCardID, ActiveDte, CurrentAmt, RecID
		FROM [MDS02].[dbo].GiftCard order by GiftCardID
	open S1
	Fetch next from S1 into @GiftCardID, @ActiveDte, @CurrentAmt, @RecID
	While @@FETCH_STATUS=0
	Begin
		INSERT into  [MPOS].[dbo].GiftCard(LocationID, GiftCardID, ActiveDte, CurrentAmt, RecID) 
			Values 
		(@LocationID, @GiftCardID, @ActiveDte, @CurrentAmt, @RecID);  

		Fetch Next From S1 into  @GiftCardID, @ActiveDte, @CurrentAmt, @RecID
	End
	Close S1
	DEALLOCATE S1

print 'GiftCardHist'
Delete [MPOS].[dbo].GiftCardHist
/* MDS01 GiftCardHist */
set @LocationID =1
Declare S1 Insensitive Cursor  for  
	SELECT  GiftCardTID, GiftCardID, TransDte, TransType, TransAmt, TransUserID, RecID
		FROM [MDS01].[dbo].GiftCardHist order by GiftCardTID
	open S1
	Fetch next from S1 into @GiftCardTID, @GiftCardID, @TransDte, @TransType, @TransAmt, @TransUserID, @RecID
	While @@FETCH_STATUS=0
	Begin
		INSERT into  [MPOS].[dbo].GiftCardHist(LocationID, GiftCardTID, GiftCardID, TransDte, TransType, TransAmt, TransUserID, RecID) 
			Values 
		(@LocationID, @GiftCardTID, @GiftCardID, @TransDte, @TransType, @TransAmt, @TransUserID, @RecID);  

		Fetch Next From S1 into  @GiftCardTID, @GiftCardID, @TransDte, @TransType, @TransAmt, @TransUserID, @RecID
	End
	Close S1
	DEALLOCATE S1
/* MDS02 GiftCardHist */
set @LocationID = 2
Declare S1 Insensitive Cursor  for  
	SELECT  GiftCardTID, GiftCardID, TransDte, TransType, TransAmt, TransUserID, RecID
		FROM [MDS02].[dbo].GiftCardHist order by GiftCardTID
	open S1
	Fetch next from S1 into @GiftCardTID, @GiftCardID, @TransDte, @TransType, @TransAmt, @TransUserID, @RecID
	While @@FETCH_STATUS=0
	Begin
		INSERT into  [MPOS].[dbo].GiftCardHist(LocationID, GiftCardTID, GiftCardID, TransDte, TransType, TransAmt, TransUserID, RecID) 
			Values 
		(@LocationID, @GiftCardTID, @GiftCardID, @TransDte, @TransType, @TransAmt, @TransUserID, @RecID);  

		Fetch Next From S1 into  @GiftCardTID, @GiftCardID, @TransDte, @TransType, @TransAmt, @TransUserID, @RecID
	End
	Close S1
	DEALLOCATE S1

print 'ProStat'
Delete [MPOS].[dbo].ProStat
/* MDS01 ProStat */
set @LocationID =1
Declare S1 Insensitive Cursor  for  
	SELECT  ProDate, Washes, Details, Extras, TotalHours, TotalRev, TotalLabor, PerRev, TotalSalery, DetailComp, Washers, Detailers, Greaters, Managers
		FROM [MDS01].[dbo].ProStat order by ProDate
	open S1
	Fetch next from S1 into @ProDate, @Washes, @Details, @Extras, @TotalHours, @TotalRev, @TotalLabor, @PerRev, @TotalSalery, @DetailComp, @Washers, @Detailers, @Greaters, @Managers
	While @@FETCH_STATUS=0
	Begin
		INSERT into  [MPOS].[dbo].ProStat(LocationID, ProDate, Washes, Details, Extras, TotalHours, TotalRev, TotalLabor, PerRev, TotalSalery, DetailComp, Washers, Detailers, Greaters, Managers) 
			Values 		(@LocationID, @ProDate, @Washes, @Details, @Extras, @TotalHours, @TotalRev, @TotalLabor, @PerRev, @TotalSalery, @DetailComp, @Washers, @Detailers, @Greaters, @Managers);  
		Fetch Next From S1 into  @ProDate, @Washes, @Details, @Extras, @TotalHours, @TotalRev, @TotalLabor, @PerRev, @TotalSalery, @DetailComp, @Washers, @Detailers, @Greaters, @Managers
	End
	Close S1
	DEALLOCATE S1
/* MDS02 ProStat */
set @LocationID = 2
Declare S1 Insensitive Cursor  for  
	SELECT  ProDate, Washes, Details, Extras, TotalHours, TotalRev, TotalLabor, PerRev, TotalSalery, DetailComp, Washers, Detailers, Greaters, Managers, AvgLabor, MaxLabor, MinLabor, AvgWaitTime, MaxWaitTime, MinWaitTime
		FROM [MDS02].[dbo].ProStat order by ProDate
	open S1
	Fetch next from S1 into @ProDate, @Washes, @Details, @Extras, @TotalHours, @TotalRev, @TotalLabor, @PerRev, @TotalSalery, @DetailComp, @Washers, @Detailers, @Greaters, @Managers, @AvgLabor, @MaxLabor, @MinLabor, @AvgWaitTime, @MaxWaitTime, @MinWaitTime
	While @@FETCH_STATUS=0
	Begin
		INSERT into  [MPOS].[dbo].ProStat(LocationID, ProDate, Washes, Details, Extras, TotalHours, TotalRev, TotalLabor, PerRev, TotalSalery, DetailComp, Washers, Detailers, Greaters, Managers, AvgLabor, MaxLabor, MinLabor, AvgWaitTime, MaxWaitTime, MinWaitTime) 
			Values 		(@LocationID, @ProDate, @Washes, @Details, @Extras, @TotalHours, @TotalRev, @TotalLabor, @PerRev, @TotalSalery, @DetailComp, @Washers, @Detailers, @Greaters, @Managers, @AvgLabor, @MaxLabor, @MinLabor, @AvgWaitTime, @MaxWaitTime, @MinWaitTime);  
		Fetch Next From S1 into  @ProDate, @Washes, @Details, @Extras, @TotalHours, @TotalRev, @TotalLabor, @PerRev, @TotalSalery, @DetailComp, @Washers, @Detailers, @Greaters, @Managers, @AvgLabor, @MaxLabor, @MinLabor, @AvgWaitTime, @MaxWaitTime, @MinWaitTime
	End
	Close S1
	DEALLOCATE S1




print 'client'
Delete [MPOS].[dbo].client
/* MDS01 client */
set @LocationID = 1
Declare S1 Insensitive Cursor  for  
	SELECT   clientid, fname, lname, addr1, addr2, city, st, zip, Ctype, C_Corp, Phone, Phone2, PhoneType, Phone2Type, Email, NoEmail, Score, account, status, StartDT, cast(Notes as varchar(8000)) as Notes, cast(RecNote as varchar(8000)) as RecNote
		FROM [MDS01].[dbo].client order by clientid
	open S1
	Fetch next from S1 into @clientid, @fname, @lname, @addr1, @addr2, @city, @st, @zip, @Ctype, @C_Corp, @Phone, @Phone2, @PhoneType, @Phone2Type, @Email, @NoEmail, @Score, @status, @account, @StartDT, @Notes, @RecNote   
	While @@FETCH_STATUS=0
	Begin

		INSERT into  [MPOS].[dbo].client(LocationID, clientid, fname, lname, addr1, addr2, city, st, zip, Ctype, C_Corp, Phone, Phone2, PhoneType, Phone2Type, Email, NoEmail, Score, status, account, StartDT, Notes, RecNote ) 
			Values (@LocationID, @clientid, @fname, @lname, @addr1, @addr2, @city, @st, @zip, @Ctype, @C_Corp, @Phone, @Phone2, @PhoneType, @Phone2Type, @Email, @NoEmail, @Score, @status, @account, @StartDT, @Notes, @RecNote );  

	Fetch Next From S1 into  @clientid, @fname, @lname, @addr1, @addr2, @city, @st, @zip, @Ctype, @C_Corp, @Phone, @Phone2, @PhoneType, @Phone2Type, @Email, @NoEmail, @Score, @status, @account, @StartDT, @Notes, @RecNote   
	End
Close S1
DEALLOCATE S1
/* MDS02 client */
set @LocationID = 2
Declare S1 Insensitive Cursor  for  
	SELECT   clientid, fname, lname, addr1, addr2, city, st, zip, Ctype, C_Corp, Phone, Phone2, PhoneType, Phone2Type, Email, NoEmail, Score, account, status, StartDT, cast(Notes as varchar(8000)) as Notes, cast(RecNote as varchar(8000)) as RecNote
		FROM [MDS02].[dbo].client order by clientid
	open S1
	Fetch next from S1 into @clientid, @fname, @lname, @addr1, @addr2, @city, @st, @zip, @Ctype, @C_Corp, @Phone, @Phone2, @PhoneType, @Phone2Type, @Email, @NoEmail, @Score, @status, @account, @StartDT, @Notes, @RecNote   
	While @@FETCH_STATUS=0
	Begin
		INSERT into  [MPOS].[dbo].client(LocationID, clientid, fname, lname, addr1, addr2, city, st, zip, Ctype, C_Corp, Phone, Phone2, PhoneType, Phone2Type, Email, NoEmail, Score, status, account, StartDT, Notes, RecNote ) 
			Values (@LocationID, @clientid, @fname, @lname, @addr1, @addr2, @city, @st, @zip, @Ctype, @C_Corp, @Phone, @Phone2, @PhoneType, @Phone2Type, @Email, @NoEmail, @Score, @status, @account, @StartDT, @Notes, @RecNote );  
	Fetch Next From S1 into  @clientid, @fname, @lname, @addr1, @addr2, @city, @st, @zip, @Ctype, @C_Corp, @Phone, @Phone2, @PhoneType, @Phone2Type, @Email, @NoEmail, @Score, @status, @account, @StartDT, @Notes, @RecNote   
	End
Close S1
DEALLOCATE S1

print 'CustAcc'
Delete [MPOS].[dbo].CustAcc
/* MDS01 CustAcc */
set @LocationID = 1
Declare S1 Insensitive Cursor  for  
	SELECT   CustAccID, ClientID, VehID, CurrentAmt, ActiveDte, LastUpdate, LastUpdateBy, Type, Status, MonthlyCharge, Limit, CCno, cctype, CCEXP
		FROM [MDS01].[dbo].CustAcc order by CustAccID
	open S1
	Fetch next from S1 into  @CustAccID, @ClientID, @VehID, @CurrentAmt, @ActiveDte, @LastUpdate, @LastUpdateBy, @Type, @Status, @MonthlyCharge, @Limit, @CCno, @cctype, @CCEXP
	While @@FETCH_STATUS=0
	Begin
		INSERT into  [MPOS].[dbo].CustAcc(CustAccID, LocationID, ClientID, VehID, CurrentAmt, ActiveDte, LastUpdate, LastUpdateBy, Type, Status, MonthlyCharge, Limit, CCno, cctype, CCEXP) 
			Values (@CustAccID, @LocationID, @ClientID, @VehID, @CurrentAmt, @ActiveDte, @LastUpdate, @LastUpdateBy, @Type, @Status, @MonthlyCharge, @Limit, @CCno, @cctype, @CCEXP);  
	Fetch Next From S1 into  @CustAccID, @ClientID, @VehID, @CurrentAmt, @ActiveDte, @LastUpdate, @LastUpdateBy, @Type, @Status, @MonthlyCharge, @Limit, @CCno, @cctype, @CCEXP
	End
Close S1
DEALLOCATE S1
/* MDS02 CustAcc */
set @LocationID = 2
Declare S1 Insensitive Cursor  for  
	SELECT   CustAccID, ClientID, VehID, CurrentAmt, ActiveDte, LastUpdate, LastUpdateBy, Type, Status, MonthlyCharge, Limit, CCno, cctype, CCEXP
		FROM [MDS02].[dbo].CustAcc order by CustAccID
	open S1
	Fetch next from S1 into  @CustAccID, @ClientID, @VehID, @CurrentAmt, @ActiveDte, @LastUpdate, @LastUpdateBy, @Type, @Status, @MonthlyCharge, @Limit, @CCno, @cctype, @CCEXP
	While @@FETCH_STATUS=0
	Begin
		INSERT into  [MPOS].[dbo].CustAcc(CustAccID, LocationID, ClientID, VehID, CurrentAmt, ActiveDte, LastUpdate, LastUpdateBy, Type, Status, MonthlyCharge, Limit, CCno, cctype, CCEXP) 
			Values (@CustAccID, @LocationID, @ClientID, @VehID, @CurrentAmt, @ActiveDte, @LastUpdate, @LastUpdateBy, @Type, @Status, @MonthlyCharge, @Limit, @CCno, @cctype, @CCEXP);  
	Fetch Next From S1 into  @CustAccID, @ClientID, @VehID, @CurrentAmt, @ActiveDte, @LastUpdate, @LastUpdateBy, @Type, @Status, @MonthlyCharge, @Limit, @CCno, @cctype, @CCEXP
	End
Close S1
DEALLOCATE S1

print 'CustAccHist'
Delete [MPOS].[dbo].CustAccHist
/* MDS01 CustAccHist */
set @LocationID = 1
Declare S1 Insensitive Cursor  for  
	SELECT   CustAccID, CustAccTID, TXCustID, TXRecID, TXType, TXAmt, TXDte, TXuser, TXNote, Archive, InvoiceID
		FROM [MDS01].[dbo].CustAccHist order by CustAccTID
	open S1
	Fetch next from S1 into  @CustAccID, @CustAccTID, @TXCustID, @TXRecID, @TXType, @TXAmt, @TXDte, @TXuser, @TXNote, @Archive, @InvoiceID
	While @@FETCH_STATUS=0
	Begin
	INSERT into  [MPOS].[dbo].CustAccHist(CustAccID, CustAccTID, TXCustID, TXRecID, TXLocationID, TXType, TXAmt, TXDte, TXuser, TXNote, Archive, InvoiceID) 
		Values (@CustAccID, @CustAccTID, @TXCustID, @TXRecID, @LocationID, @TXType, @TXAmt, @TXDte, @TXuser, @TXNote, @Archive, @InvoiceID);  
	Fetch Next From S1 into  @CustAccID, @CustAccTID, @TXCustID, @TXRecID, @TXType, @TXAmt, @TXDte, @TXuser, @TXNote, @Archive, @InvoiceID
	End
Close S1
DEALLOCATE S1
/* MDS02 CustAccHist */
set @LocationID = 2
Declare S1 Insensitive Cursor  for  
	SELECT   CustAccID, CustAccTID, TXCustID, TXRecID, TXType, TXAmt, TXDte, TXuser, TXNote, Archive, InvoiceID
		FROM [MDS02].[dbo].CustAccHist order by CustAccTID
	open S1
	Fetch next from S1 into  @CustAccID, @CustAccTID, @TXCustID, @TXRecID, @TXType, @TXAmt, @TXDte, @TXuser, @TXNote, @Archive, @InvoiceID
	While @@FETCH_STATUS=0
	Begin
		INSERT into  [MPOS].[dbo].CustAccHist(CustAccID, CustAccTID, TXCustID, TXRecID, TXLocationID, TXType, TXAmt, TXDte, TXuser, TXNote, Archive, InvoiceID) 
			Values (@CustAccID, @CustAccTID, @TXCustID, @TXRecID, @LocationID, @TXType, @TXAmt, @TXDte, @TXuser, @TXNote, @Archive, @InvoiceID);  
	Fetch Next From S1 into  @CustAccID, @CustAccTID, @TXCustID, @TXRecID, @TXType, @TXAmt, @TXDte, @TXuser, @TXNote, @Archive, @InvoiceID
	End
Close S1
DEALLOCATE S1

print 'DetailComp'
Delete [MPOS].[dbo].DetailComp
/* MDS01 DetailComp */
set @LocationID = 1
Declare S1 Insensitive Cursor  for  
	SELECT   DetailCompID, UserID, RecID, CdateTime, ComAmt, paid, ComPer, Comm
		FROM [MDS01].[dbo].DetailComp order by DetailCompID
	open S1
	Fetch next from S1 into @DetailCompID, @UserID, @RecID, @CdateTime, @ComAmt, @paid, @ComPer, @Comm
	While @@FETCH_STATUS=0
	Begin
		INSERT into  [MPOS].[dbo].DetailComp(LocationID, DetailCompID, UserID, RecID, CdateTime, ComAmt, paid, ComPer, Comm) 
			Values (@LocationID, @DetailCompID, @UserID, @RecID, @CdateTime, @ComAmt, @paid, @ComPer, @Comm);  
	Fetch Next From S1 into  @DetailCompID, @UserID, @RecID, @CdateTime, @ComAmt, @paid, @ComPer, @Comm
	End
Close S1
DEALLOCATE S1
/* MDS02 DetailComp */
set @LocationID = 2
Declare S1 Insensitive Cursor  for  
	SELECT   DetailCompID, UserID, RecID, CdateTime, ComAmt, paid, ComPer, Comm
		FROM [MDS02].[dbo].DetailComp order by DetailCompID
	open S1
	Fetch next from S1 into @DetailCompID, @UserID, @RecID, @CdateTime, @ComAmt, @paid, @ComPer, @Comm
	While @@FETCH_STATUS=0
	Begin
		INSERT into  [MPOS].[dbo].DetailComp(LocationID, DetailCompID, UserID, RecID, CdateTime, ComAmt, paid, ComPer, Comm) 
			Values (@LocationID, @DetailCompID, @UserID, @RecID, @CdateTime, @ComAmt, @paid, @ComPer, @Comm);  
	Fetch Next From S1 into  @DetailCompID, @UserID, @RecID, @CdateTime, @ComAmt, @paid, @ComPer, @Comm
	End
Close S1
DEALLOCATE S1

print 'DetailItem'
Delete [MPOS].[dbo].DetailItem
/* MDS02 DetailItem */
Declare S1 Insensitive Cursor  for  
	SELECT  DetailItemID, ItemDesc
		FROM [MDS02].[dbo].DetailItem order by DetailItemID
	open S1
	Fetch next from S1 into @DetailItemID, @ItemDesc
	While @@FETCH_STATUS=0
	Begin
		INSERT into  [MPOS].[dbo].DetailItem(DetailItemID, ItemDesc) 
			Values (@DetailItemID, @ItemDesc);  
	Fetch Next From S1 into  @DetailItemID, @ItemDesc
	End
Close S1
DEALLOCATE S1

print 'DetailProd'
Delete [MPOS].[dbo].DetailProd
/* MDS02 DetailProd */
Declare S1 Insensitive Cursor  for  
	SELECT   DetailItemID, ProdID
		FROM [MDS02].[dbo].DetailProd order by DetailItemID
	open S1
	Fetch next from S1 into @DetailItemID, @ProdID
	While @@FETCH_STATUS=0
	Begin
		INSERT into  [MPOS].[dbo].DetailProd(DetailItemID, ProdID) 
			Values (@DetailItemID, @ProdID);  
	Fetch Next From S1 into  @DetailItemID, @ProdID
	End
Close S1
DEALLOCATE S1

print 'ProdKit'
Delete [MPOS].[dbo].ProdKit
/* MDS02 ProdKit */
Declare S1 Insensitive Cursor  for  
	SELECT    KitID, ProductID
		FROM [MDS02].[dbo].ProdKit order by KitID
	open S1
	Fetch next from S1 into @KitID, @ProductID
	While @@FETCH_STATUS=0
	Begin
		INSERT into  [MPOS].[dbo].ProdKit( KitID, ProductID) 
			Values (@KitID, @ProductID);  
	Fetch Next From S1 into  @KitID, @ProductID
	End
Close S1
DEALLOCATE S1

print 'ProdOpt'
Delete [MPOS].[dbo].ProdOpt
/* MDS02 ProdOpt */
Declare S1 Insensitive Cursor  for  
	SELECT    OptID, ProductID
		FROM [MDS02].[dbo].ProdOpt order by OptID
	open S1
	Fetch next from S1 into @OptID, @ProductID
	While @@FETCH_STATUS=0
	Begin
		INSERT into  [MPOS].[dbo].ProdOpt( OptID, ProductID) 
			Values (@OptID, @ProductID);  
	Fetch Next From S1 into  @OptID, @ProductID
	End
Close S1
DEALLOCATE S1

print 'Product'
Delete [MPOS].[dbo].Product
/* MDS02 Product */
Declare S1 Insensitive Cursor  for  
	SELECT    ProdID, cat, dept, Descript, Number, Price, Comm, Inv, Taxable, PerAdj, KIT, KitID, Bcode, EstTime, vendor, altvendor, vpartnumb, Manufact, Model, Cost, vendcost, status, editby, edited, createdby, created,Opt, OptID, cast(Notes as varchar(8000)) as Notes
		FROM [MDS02].[dbo].Product order by ProdID
	open S1
	Fetch next from S1 into @ProdID, @cat, @dept, @Descript, @Number, @Price, @Comm, @Inv, @Taxable, @PerAdj, @KIT, @KitID, @Bcode, @ProdEstTime, @vendor, @altvendor, @vpartnumb, @Manufact, @Model, @Cost, @vendcost, @status, @editby, @edited, @createdby, @created, @Opt, @OptID, @Notes
	While @@FETCH_STATUS=0
	Begin
		INSERT into  [MPOS].[dbo].Product( ProdID, cat, dept, Descript, Number, Price, Comm, Inv, Taxable, PerAdj, KIT, KitID, Bcode, EstTime, vendor, altvendor, vpartnumb, Manufact, Model, Cost, vendcost, status, editby, edited, createdby, created, Opt, OptID,Notes) 
			Values (@ProdID, @cat, @dept, @Descript, @Number, @Price, @Comm, @Inv, @Taxable, @PerAdj, @KIT, @KitID, @Bcode, @ProdEstTime, @vendor, @altvendor, @vpartnumb, @Manufact, @Model, @Cost, @vendcost, @status, @editby, @edited, @createdby, @created,@Opt, @OptID, @Notes);  
	Fetch Next From S1 into  @ProdID, @cat, @dept, @Descript, @Number, @Price, @Comm, @Inv, @Taxable, @PerAdj, @KIT, @KitID, @Bcode, @ProdEstTime, @vendor, @altvendor, @vpartnumb, @Manufact, @Model, @Cost, @vendcost, @status, @editby, @edited, @createdby, @created, @Opt, @OptID, @Notes
	End
Close S1
DEALLOCATE S1

print 'vehical'
Delete [MPOS].[dbo].vehical
/* MDS01 vehical */
set @LocationID = 1
Declare S1 Insensitive Cursor  for  
	SELECT   vehid, upc, tag, clientid, vehnum, make, model, vyear, Color, vmodel
		FROM [MDS01].[dbo].vehical order by vehid
	open S1
	Fetch next from S1 into @vehid, @upc, @tag, @clientid, @vehnum, @make, @model, @vyear, @Color, @vmodel
	While @@FETCH_STATUS=0
	Begin
		INSERT into  [MPOS].[dbo].vehical(LocationID, vehid, upc, tag, clientid, vehnum, make, model, vyear, Color, vmodel ) 
			Values (@LocationID, @vehid, @upc, @tag, @clientid, @vehnum, @make, @model, @vyear, @Color, @vmodel);  
	Fetch Next From S1 into  @vehid, @upc, @tag, @clientid, @vehnum, @make, @model, @vyear, @Color, @vmodel
	End
Close S1
DEALLOCATE S1
/* MDS02 vehical */
set @LocationID = 2
Declare S1 Insensitive Cursor  for  
	SELECT   vehid, upc, tag, clientid, vehnum, make, model, vyear, Color, vmodel
		FROM [MDS02].[dbo].vehical order by vehid
	open S1
	Fetch next from S1 into @vehid, @upc, @tag, @clientid, @vehnum, @make, @model, @vyear, @Color, @vmodel
	While @@FETCH_STATUS=0
	Begin
		INSERT into  [MPOS].[dbo].vehical(LocationID, vehid, upc, tag, clientid, vehnum, make, model, vyear, Color, vmodel ) 
			Values (@LocationID,@vehid, @upc, @tag, @clientid, @vehnum, @make, @model, @vyear, @Color, @vmodel);  
	Fetch Next From S1 into  @vehid, @upc, @tag, @clientid, @vehnum, @make, @model, @vyear, @Color, @vmodel
	End
Close S1
DEALLOCATE S1

print 'TimeClock'
Delete [MPOS].[dbo].TimeClock
/* MDS01 TimeClock */
set @LocationID = 1
Declare S1 Insensitive Cursor  for  
	SELECT    UserID, ClockID, Cdatetime, Caction, editby, editdate, Paid, CType
		FROM [MDS01].[dbo].TimeClock order by ClockID
	open S1
	Fetch next from S1 into @UserID, @ClockID, @Cdatetime, @Caction, @editby, @editdate, @Paid, @CType
	While @@FETCH_STATUS=0
	Begin
		INSERT into  [MPOS].[dbo].TimeClock(LocationID, UserID, ClockID, Cdatetime, Caction, editby, editdate, Paid, CType ) 
			Values (@LocationID, @UserID, @ClockID, @Cdatetime, @Caction, @editby, @editdate, @Paid, @CType);  
	Fetch Next From S1 into  @UserID, @ClockID, @Cdatetime, @Caction, @editby, @editdate, @Paid, @CType
	End
Close S1
DEALLOCATE S1
/* MDS02 TimeClock */
set @LocationID = 2
Declare S1 Insensitive Cursor  for  
	SELECT    UserID, ClockID, Cdatetime, Caction, editby, editdate, Paid, CType
		FROM [MDS02].[dbo].TimeClock order by ClockID
	open S1
	Fetch next from S1 into @UserID, @ClockID, @Cdatetime, @Caction, @editby, @editdate, @Paid, @CType
	While @@FETCH_STATUS=0
	Begin
		INSERT into  [MPOS].[dbo].TimeClock(LocationID, UserID, ClockID, Cdatetime, Caction, editby, editdate, Paid, CType ) 
			Values (@LocationID, @UserID, @ClockID, @Cdatetime, @Caction, @editby, @editdate, @Paid, @CType);  
	Fetch Next From S1 into  @UserID, @ClockID, @Cdatetime, @Caction, @editby, @editdate, @Paid, @CType
	End
Close S1
DEALLOCATE S1

print 'TimeSheet'
Delete [MPOS].[dbo].TimeSheet
/* MDS01 TimeSheet */
set @LocationID = 1
Declare S1 Insensitive Cursor  for  
	SELECT    SheetID, Weekno, yearno, weekof, status, empcnt, totalamt, submitdate, appdate, compdate
		FROM [MDS01].[dbo].TimeSheet order by SheetID
	open S1
	Fetch next from S1 into @SheetID, @Weekno, @yearno, @weekof, @status, @empcnt, @totalamt, @submitdate, @appdate, @compdate
	While @@FETCH_STATUS=0
	Begin
		INSERT into  [MPOS].[dbo].TimeSheet(LocationID, SheetID, Weekno, yearno, weekof, status, empcnt, totalamt, submitdate, appdate, compdate ) 
			Values (@LocationID, @SheetID, @Weekno, @yearno, @weekof, @status, @empcnt, @totalamt, @submitdate, @appdate, @compdate);  
	Fetch Next From S1 into  @SheetID, @Weekno, @yearno, @weekof, @status, @empcnt, @totalamt, @submitdate, @appdate, @compdate
	End
Close S1
DEALLOCATE S1
/* MDS02 TimeSheet */
set @LocationID = 2
Declare S1 Insensitive Cursor  for  
	SELECT    SheetID, Weekno, yearno, weekof, status, empcnt, totalamt, submitdate, appdate, compdate
		FROM [MDS02].[dbo].TimeSheet order by SheetID
	open S1
	Fetch next from S1 into @SheetID, @Weekno, @yearno, @weekof, @status, @empcnt, @totalamt, @submitdate, @appdate, @compdate
	While @@FETCH_STATUS=0
	Begin
		INSERT into  [MPOS].[dbo].TimeSheet(LocationID, SheetID, Weekno, yearno, weekof, status, empcnt, totalamt, submitdate, appdate, compdate ) 
			Values (@LocationID, @SheetID, @Weekno, @yearno, @weekof, @status, @empcnt, @totalamt, @submitdate, @appdate, @compdate);  
	Fetch Next From S1 into  @SheetID, @Weekno, @yearno, @weekof, @status, @empcnt, @totalamt, @submitdate, @appdate, @compdate
	End
Close S1
DEALLOCATE S1

print 'UserAdj'
Delete [MPOS].[dbo].UserAdj
/* MDS01 UserAdj */
set @LocationID = 1
Declare S1 Insensitive Cursor  for  
	SELECT UserID, AdjID, sheetID, actAmt, actdate, actdesc, editby, editdate
		FROM [MDS01].[dbo].UserAdj order by AdjID
	open S1
	Fetch next from S1 into @UserID, @AdjID, @sheetID, @actAmt, @actdate, @actdesc, @editby, @editdate
	While @@FETCH_STATUS=0
	Begin
		INSERT into  [MPOS].[dbo].UserAdj(LocationID, UserID, AdjID, sheetID, actAmt, actdate, actdesc, editby, editdate ) 
			Values (@LocationID, @UserID, @AdjID, @sheetID, @actAmt, @actdate, @actdesc, @editby, @editdate);  
	Fetch Next From S1 into  @UserID, @AdjID, @sheetID, @actAmt, @actdate, @actdesc, @editby, @editdate
	End
Close S1
DEALLOCATE S1
/* MDS02 UserAdj */
set @LocationID = 2
Declare S1 Insensitive Cursor  for  
	SELECT UserID, AdjID, sheetID, actAmt, actdate, actdesc, editby, editdate
		FROM [MDS02].[dbo].UserAdj order by AdjID
	open S1
	Fetch next from S1 into @UserID, @AdjID, @sheetID, @actAmt, @actdate, @actdesc, @editby, @editdate
	While @@FETCH_STATUS=0
	Begin
		INSERT into  [MPOS].[dbo].UserAdj(LocationID, UserID, AdjID, sheetID, actAmt, actdate, actdesc, editby, editdate ) 
			Values (@LocationID, @UserID, @AdjID, @sheetID, @actAmt, @actdate, @actdesc, @editby, @editdate);  
	Fetch Next From S1 into  @UserID, @AdjID, @sheetID, @actAmt, @actdate, @actdesc, @editby, @editdate
	End
Close S1
DEALLOCATE S1


print 'UserCol'
Delete [MPOS].[dbo].UserCol
/* MDS01 UserCol */
set @LocationID = 1
Declare S1 Insensitive Cursor  for  
	SELECT  UserID, CollID, ActID, ActDate, Acttype, actAmt, ActDesc, EditBy, EditDate, sheetID
		FROM [MDS01].[dbo].UserCol order by CollID
	open S1
	Fetch next from S1 into @UserID, @CollID, @ActID, @ActDate, @Acttype, @actAmt, @ActDesc, @EditBy, @EditDate, @sheetID
	While @@FETCH_STATUS=0
	Begin
		INSERT into  [MPOS].[dbo].UserCol(LocationID, UserID, CollID, ActID, ActDate, Acttype, actAmt, ActDesc, EditBy, EditDate, sheetID ) 
			Values (@LocationID, @UserID, @CollID, @ActID, @ActDate, @Acttype, @actAmt, @ActDesc, @EditBy, @EditDate, @sheetID);  
	Fetch Next From S1 into @UserID, @CollID, @ActID, @ActDate, @Acttype, @actAmt, @ActDesc, @EditBy, @EditDate, @sheetID
	End
Close S1
DEALLOCATE S1
/* MDS02 UserCol */
set @LocationID = 2
Declare S1 Insensitive Cursor  for  
	SELECT  UserID, CollID, ActID, ActDate, Acttype, actAmt, ActDesc, EditBy, EditDate, sheetID
		FROM [MDS02].[dbo].UserCol order by CollID
	open S1
	Fetch next from S1 into @UserID, @CollID, @ActID, @ActDate, @Acttype, @actAmt, @ActDesc, @EditBy, @EditDate, @sheetID
	While @@FETCH_STATUS=0
	Begin
		INSERT into  [MPOS].[dbo].UserCol(LocationID, UserID, CollID, ActID, ActDate, Acttype, actAmt, ActDesc, EditBy, EditDate, sheetID ) 
			Values (@LocationID, @UserID, @CollID, @ActID, @ActDate, @Acttype, @actAmt, @ActDesc, @EditBy, @EditDate, @sheetID);  
	Fetch Next From S1 into @UserID, @CollID, @ActID, @ActDate, @Acttype, @actAmt, @ActDesc, @EditBy, @EditDate, @sheetID
	End
Close S1
DEALLOCATE S1


print 'UserUnif'
Delete [MPOS].[dbo].UserUnif
/* MDS01 UserUnif */
set @LocationID = 1
Declare S1 Insensitive Cursor  for  
	SELECT UserID, UnifID, ActID, ActDate, ActType, ActAmt, ProdID, Editby, EditDate, ActCost, ActQty, actDesc, SheetID
		FROM [MDS01].[dbo].UserUnif order by UnifID
	open S1
	Fetch next from S1 into @UserID, @UnifID, @ActID, @ActDate, @ActType, @ActAmt, @ProdID, @Editby, @EditDate, @ActCost, @ActQty, @actDesc, @SheetID
	While @@FETCH_STATUS=0
	Begin
		INSERT into  [MPOS].[dbo].UserUnif(LocationID, UserID, UnifID, ActID, ActDate, ActType, ActAmt, ProdID, Editby, EditDate, ActCost, ActQty, actDesc, SheetID) 
			Values (@LocationID, @UserID, @UnifID, @ActID, @ActDate, @ActType, @ActAmt, @ProdID, @Editby, @EditDate, @ActCost, @ActQty, @actDesc, @SheetID);  
	Fetch Next From S1 into @UserID, @UnifID, @ActID, @ActDate, @ActType, @ActAmt, @ProdID, @Editby, @EditDate, @ActCost, @ActQty, @actDesc, @SheetID
	End
Close S1
DEALLOCATE S1
/* MDS02 UserUnif */
set @LocationID = 2
Declare S1 Insensitive Cursor  for  
	SELECT UserID, UnifID, ActID, ActDate, ActType, ActAmt, ProdID, Editby, EditDate, ActCost, ActQty, actDesc, SheetID
		FROM [MDS02].[dbo].UserUnif order by UnifID
	open S1
	Fetch next from S1 into @UserID, @UnifID, @ActID, @ActDate, @ActType, @ActAmt, @ProdID, @Editby, @EditDate, @ActCost, @ActQty, @actDesc, @SheetID
	While @@FETCH_STATUS=0
	Begin
		INSERT into  [MPOS].[dbo].UserUnif(LocationID, UserID, UnifID, ActID, ActDate, ActType, ActAmt, ProdID, Editby, EditDate, ActCost, ActQty, actDesc, SheetID) 
			Values (@LocationID, @UserID, @UnifID, @ActID, @ActDate, @ActType, @ActAmt, @ProdID, @Editby, @EditDate, @ActCost, @ActQty, @actDesc, @SheetID);  
	Fetch Next From S1 into @UserID, @UnifID, @ActID, @ActDate, @ActType, @ActAmt, @ProdID, @Editby, @EditDate, @ActCost, @ActQty, @actDesc, @SheetID
	End
Close S1
DEALLOCATE S1


print 'vendor'
Delete [MPOS].[dbo].vendor
/* MDS01 vendor */
set @LocationID = 1
Declare S1 Insensitive Cursor  for  
	SELECT VenID, Vendor, vencontact, venaddr1, venaddr2, vencity, venst, VenZip, venPhone, venFax, venURL, venEmail, venAcc
		FROM [MDS01].[dbo].vendor order by VenID
	open S1
	Fetch next from S1 into @VenID, @ven_vendor, @vencontact, @venaddr1, @venaddr2, @vencity, @venst, @VenZip, @venPhone, @venFax, @venURL, @venEmail, @venAcc
	While @@FETCH_STATUS=0
	Begin
		INSERT into  [MPOS].[dbo].vendor(LocationID, VenID,  Vendor, vencontact, venaddr1, venaddr2, vencity, venst, VenZip, venPhone, venFax, venURL, venEmail, venAcc) 
			Values (@LocationID, @VenID, @ven_vendor, @vencontact, @venaddr1, @venaddr2, @vencity, @venst, @VenZip, @venPhone, @venFax, @venURL, @venEmail, @venAcc);  
	Fetch Next From S1 into @VenID, @ven_vendor, @vencontact, @venaddr1, @venaddr2, @vencity, @venst, @VenZip, @venPhone, @venFax, @venURL, @venEmail, @venAcc
	End
Close S1
DEALLOCATE S1
/* MDS02 vendor */
set @LocationID = 2
Declare S1 Insensitive Cursor  for  
	SELECT VenID, Vendor, vencontact, venaddr1, venaddr2, vencity, venst, VenZip, venPhone, venFax, venURL, venEmail, venAcc
		FROM [MDS02].[dbo].vendor order by VenID
	open S1
	Fetch next from S1 into @VenID, @ven_vendor, @vencontact, @venaddr1, @venaddr2, @vencity, @venst, @VenZip, @venPhone, @venFax, @venURL, @venEmail, @venAcc
	While @@FETCH_STATUS=0
	Begin
		INSERT into  [MPOS].[dbo].vendor(LocationID, VenID,  Vendor, vencontact, venaddr1, venaddr2, vencity, venst, VenZip, venPhone, venFax, venURL, venEmail, venAcc) 
			Values (@LocationID, @VenID, @ven_vendor, @vencontact, @venaddr1, @venaddr2, @vencity, @venst, @VenZip, @venPhone, @venFax, @venURL, @venEmail, @venAcc);  
	Fetch Next From S1 into @VenID, @ven_vendor, @vencontact, @venaddr1, @venaddr2, @vencity, @venst, @VenZip, @venPhone, @venFax, @venURL, @venEmail, @venAcc
	End
Close S1
DEALLOCATE S1



print 'LM_Users Clean up'
UPDAT LM_Users SET LoginID = ' ', Password = NULL WHERE (Active = 0)


print 'client Clean up'

DELETE FROM client
WHERE        (clientid NOT IN
                             (SELECT        clientid
                               FROM            REC))




Declare S1 Insensitive Cursor  for  
	SELECT LocationID, clientid, fname + ' ' + lname + ' ' + Phone as Contact
		FROM client
		WHERE ((fname + ' ' + lname + ' ' + Phone) IN
                             (SELECT        fname + ' ' + lname + ' ' + Phone AS Expr1
                               FROM            client AS client_1
                               GROUP BY fname, lname, Phone
                               HAVING         (COUNT(*) > 1)))
		ORDER BY lname, fname, Phone DESC, LocationID DESC, clientid DESC, Ctype
	open S1
	Fetch next from S1 into @LocationID, @clientid, @Contact
	While @@FETCH_STATUS=0
	Begin
		If @Contact = @LastContact
			begin
				DELETE FROM client WHERE LocationID = @LocationID and  clientid=@clientid
				DELETE FROM vehical WHERE LocationID = @LocationID and  clientid=@clientid
			end
		set  @LastContact =  @Contact
		set  @LastLocationID =  @LocationID
		set  @Lastclientid =  @clientid

	Fetch Next From S1 into @LocationID, @clientid, @Contact
	End
Close S1
DEALLOCATE S1

print 'vehical Clean up'

DELETE FROM vehical
WHERE        (clientid NOT IN
                             (SELECT        clientid
                               FROM            REC))
DELETE FROM vehical WHERE Len(ltrim(upc)) = 0
DELETE FROM vehical WHERE upc = '(NULL)'

Declare S1 Insensitive Cursor  for
	SELECT        LocationID, vehid, upc
		FROM            vehical
		WHERE        (upc IN
                             (SELECT        upc
                               FROM            vehical AS vehical_1
                               GROUP BY upc
                               HAVING         (COUNT(*) > 1)))
		ORDER BY upc, vehid, LocationID DESC
	open S1
	Fetch next from S1 into @LocationID, @vehid, @upc
	While @@FETCH_STATUS=0
	Begin
		If  @upc = @Lastupc
			begin
				DELETE FROM vehical WHERE LocationID = @LocationID and  vehid=@vehid and upc=@upc
			end
		set  @Lastvehid =  @vehid
		set  @Lastupc =  @upc
		set  @LastLocationID =  @LocationID

	Fetch Next From S1 into @LocationID, @vehid, @upc
	End
Close S1
DEALLOCATE S1

Declare S1 Insensitive Cursor  for
	SELECT        vehid, LocationID, clientid
	FROM            vehical
	WHERE (upc IS NULL) ORDER BY LocationID, vehid, clientid
	open S1
	Fetch next from S1 into @vehid, @LocationID, @clientid
	While @@FETCH_STATUS=0
	Begin
		Update REC set vehid=0 where vehid=@vehid and LocationID=@LocationID and  clientid = @clientid
		Delete vehical where vehid=@vehid and LocationID=@LocationID and  clientid = @clientid
	Fetch Next From S1 into @vehid, @LocationID, @clientid
	End
Close S1
DEALLOCATE S1

Declare S1 Insensitive Cursor  for
		SELECT        vehid, LocationID, clientid, upc, tag
		FROM            vehical
		WHERE        (vehid IN
									 (SELECT        vehid
									   FROM            vehical AS vehical_1
									   GROUP BY vehid
									   HAVING         (COUNT(*) > 1)))
		ORDER BY vehid, LocationID DESC, clientid, upc, tag, vehnum, make, model, vyear, Color, vmodel

	open S1
	Fetch next from S1 into @vehid, @LocationID, @clientid,@upc,@tag
	While @@FETCH_STATUS=0
	Begin
		If  @vehid = @Lastvehid
			begin
				Print 'Same VehID=>' + str(@vehid)
				If  @clientid = @Lastclientid
					begin
						Print 'Same @clientid=>' + str(@clientid)
						Delete vehical where vehid=@vehid and LocationID=@LocationID and  clientid = @clientid
						Delete CustAcc where vehid=@vehid and LocationID=@LocationID and  clientid = @clientid
					end
				Else
					begin
						Print 'Diff @clientid=>' + str(@clientid)
						SELECT @NewVehID=MAX(vehid) + 1  FROM vehical
						Update vehical set vehid=@NewVehID where vehid=@vehid and LocationID=@LocationID and  clientid = @clientid
						Update CustAcc set vehid=@NewVehID where vehid=@vehid and LocationID=@LocationID and  clientid = @clientid
						Update REC set vehid=@NewVehID where vehid=@vehid and LocationID=@LocationID and  clientid = @clientid
						
					end
			end
		set  @Lastvehid =  @vehid
		set  @Lastupc =  @upc
		set  @LastLocationID =  @LocationID
		set  @Lastclientid =  @clientid
		set  @Lasttag =  @tag
	Fetch Next From S1 into @vehid, @LocationID, @clientid,@upc,@tag
	End
Close S1
DEALLOCATE S1



Declare S1 Insensitive Cursor  for  
	SELECT        LocationID, clientid, fname, lname, Phone
	FROM            client
	WHERE        (clientid IN
								 (SELECT        clientid
								   FROM            client AS client_1
								   GROUP BY clientid
								   HAVING         (COUNT(*) > 1))) AND (clientid > 0)
	ORDER BY clientid, LocationID DESC
	open S1
	Fetch next from S1 into @LocationID, @clientid, @fname, @lname, @Phone
	While @@FETCH_STATUS=0
	Begin
		If @clientid = @Lastclientid 
			begin
				IF (@fname=@Lastfname OR @lname=@Lastlname OR @Phone=@LastPhone)
					begin
						DELETE FROM client WHERE LocationID = @LocationID and  clientid=@clientid
						DELETE FROM vehical WHERE LocationID = @LocationID and  clientid=@clientid
					end
				else
					begin
						SELECT @Newclientid=MAX(clientid) + 1  FROM client
						Update vehical set clientid=@Newclientid where clientid=@clientid and LocationID=@LocationID
						Update CustAcc set clientid=@Newclientid where clientid=@clientid and LocationID=@LocationID
						Update REC set clientid=@Newclientid where clientid=@clientid and LocationID=@LocationID
						Update client set clientid=@Newclientid where clientid=@clientid and LocationID=@LocationID
					end
			end
		set  @LastLocationID =  @LocationID
		set  @Lastclientid =  @clientid
		set  @Lastfname =  @fname
		set  @Lastlname =  @lname
		set  @LastPhone =  @Phone

	Fetch Next From S1 into  @LocationID, @clientid, @fname, @lname, @Phone
	End
Close S1
DEALLOCATE S1

DELETE FROM vehical WHERE (upc = '         ') OR (LEFT(upc, 1) = '(') OR (LEFT(upc, 1) = '-')



print 'GiftCard Clean up'


Update GiftCard set GiftCardID= replace(replace(replace(replace(GiftCardID,' ',''),'+',''),'-',''),'.','')
Update GiftCard set RECID = NULL where RECID = 0
Delete GiftCard where len(ltrim(GiftCardID)) < 4


CREATE TABLE #GiftCard2(
	[GID] [int] IDENTITY(1,1) NOT NULL,
	[LocationID] [int] NULL,
	[RecID] [int] NULL,
	[GiftCardID] [nchar](10) NULL,
	[ActiveDte] [smalldatetime] NULL,
	[CurrentAmt] [money] NULL
) 


Declare S1 Insensitive Cursor  for
	SELECT        LocationID, RecID, GiftCardID, ActiveDte, CurrentAmt
		FROM            GiftCard
		ORDER BY GiftCardID, ActiveDte DESC
	open S1
	Fetch next from S1 into @LocationID, @RecID, @GiftCardID, @ActiveDte, @CurrentAmt
	While @@FETCH_STATUS=0
	Begin
		Insert INTO #GiftCard2(LocationID, RecID, GiftCardID, ActiveDte, CurrentAmt)
			Values( @LocationID, @RecID, @GiftCardID, @ActiveDte, @CurrentAmt)

	Fetch Next From S1 into @LocationID, @RecID, @GiftCardID, @ActiveDte, @CurrentAmt
	End
Close S1
DEALLOCATE S1



Declare S1 Insensitive Cursor  for
	SELECT GID, GiftCardID
		FROM #GiftCard2
		ORDER BY GID
	open S1
	Fetch next from S1 into @GID, @GiftCardID
	While @@FETCH_STATUS=0
	Begin
		If  @GiftCardID = @LastGiftCardID
			begin
				DELETE FROM #GiftCard2 WHERE GID = @GID 
			end
		set  @LastGiftCardID  =  @GiftCardID

	Fetch Next From S1 into  @GID, @GiftCardID
	End
Close S1
DEALLOCATE S1

DELETE FROM GiftCard


Declare S1 Insensitive Cursor  for
	SELECT        LocationID, RecID, GiftCardID, ActiveDte, CurrentAmt
		FROM            #GiftCard2
		ORDER BY GiftCardID
	open S1
	Fetch next from S1 into @LocationID, @RecID, @GiftCardID, @ActiveDte, @CurrentAmt
	While @@FETCH_STATUS=0
	Begin
		Insert INTO GiftCard(LocationID, RecID, GiftCardID, ActiveDte, CurrentAmt)
			Values( @LocationID, @RecID, @GiftCardID, @ActiveDte, @CurrentAmt)

	Fetch Next From S1 into @LocationID, @RecID, @GiftCardID, @ActiveDte, @CurrentAmt
	End
Close S1
DEALLOCATE S1


Drop Table #GiftCard2
*/

Print 'End of Line'
END
GO


