USE [StriveTenant_Setup]
GO

/****** Object:  StoredProcedure [CON].[uspMasterDataInsert]    Script Date: 25-03-2022 11:20:20 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO




CREATE PROCEDURE [CON].[uspMasterDataInsert]
--DECLARE 
@NewSchema Varchar(64),
@AUTHId INT,
@CloneSchemaName NVARCHAR(25)
AS

DECLARE 
@SQL VARCHAR(MAX),
@IMIN INT, @IMAX INT,
@CMIN INT, @CMAX INT,
@MasterTableName VARCHAR(250)

-- Master Insert
SET NOCOUNT ON

DROP TABLE IF EXISTS #SeedInsert

CREATE TABLE #SeedInsert
(
ID INT ,
TableNAME VARCHAR(250),
IsIdenity BIT,
InsertDESC VARCHAR(MAX)
)

DROP TABLE IF EXISTS #MasterTblCol
SELECT 
	MTL.MasterTableListID,
	MasterTableListNAME,
	ISIdentityInsert,
	QUOTENAME(C.[Name]) AS COLUMN_NAMES,
	C.column_id
INTO	
	#MasterTblCol
FROM 
	CON.MasterTableList MTL
JOIN
	SYS.TABLES T
ON		T.[name]=MTL.MasterTableListNAME
JOIN
	SYS.schemas S
ON		S.schema_id=T.schema_id
JOIN
	SYS.Columns C
ON		C.object_id=T.object_id
WHERE IS0thRecord<>1 AND S.[name]=@CloneSchemaName

DROP TABLE IF EXISTS #MasterTblSort

SELECT 
	MasterTableListID,MasterTableListNAME,ISIdentityInsert,MAX(column_id) As MaxId,MIN(column_id) As MinId 
INTO
	#MasterTblSort
FROM 
	#MasterTblCol
GROUP BY MasterTableListID,MasterTableListNAME,ISIdentityInsert

--Select * from #MasterTblSort
DROP TABLE IF EXISTS #MasterTblCol_Desc
SELECT 
	Mcol.MasterTableListID,
	Mcol.MasterTableListNAME,
	Mcol.ISIdentityInsert,
	COLUMN_NAMES,
	Mcol.column_id,
	CASE	WHEN Mcol.column_id=Msort.MinId THEN ' ( '+Mcol.COLUMN_NAMES +' ,' 
			WHEN Mcol.column_id=Msort.MaxId THEN Mcol.COLUMN_NAMES+' )'
			ELSE Mcol.COLUMN_NAMES+' ,'
	END AS Col_Desc
INTO
	#MasterTblCol_Desc
FROM 
	#MasterTblCol Mcol
LEFT JOIN 
	#MasterTblSort Msort
ON		Msort.MasterTableListNAME=Mcol.MasterTableListNAME

DROP TABLE IF EXISTS #MasterTblCol_Desc_Concat
SELECT DISTINCT 
	T1.MasterTableListID,
	T1.MasterTableListNAME,
	T1.ISIdentityInsert,
	'INSERT INTO '+QUOTENAME(@NewSchema)+'.'+QUOTENAME(MasterTableListNAME)+
	+STUFF((SELECT ' ' + t2.[Col_Desc]  FROM #MasterTblCol_Desc t2  WHERE t1.MasterTableListNAME = t2.MasterTableListNAME  FOR XML PATH ('')) , 1, 1, '')  
	+CHAR(10)+'SELECT * FROM ['+ @CloneSchemaName +'].'+QUOTENAME(MasterTableListNAME)
	AS InsertDESC
INTO
	#MasterTblCol_Desc_Concat
FROM 
	#MasterTblCol_Desc t1;

--Select * FRom #MasterTblCol_Desc_Concat
SELECT @IMIN=MIN(MasterTableListID),@IMAX=MAX(MasterTableListID) FROM #MasterTblCol_Desc_Concat
	
WHILE @IMAX>=@IMIN

BEGIN

SET @MasterTableName=(SELECT MasterTableListNAME FROM #MasterTblCol_Desc_Concat WHERE MasterTableListID=@IMIN)

IF (SELECT ISIdentityInsert FROM #MasterTblCol_Desc_Concat WHERE MasterTableListID=@IMIN)=1

BEGIN

SET @SQL=	'SET IDENTITY_INSERT '+QUOTENAME(@NewSchema)+'.'+QUOTENAME(@MasterTableName)+' ON'+CHAR(10)+CHAR(10)+
			(SELECT InsertDESC FROM #MasterTblCol_Desc_Concat WHERE MasterTableListID=@IMIN)+CHAR(10)+CHAR(10)+
			'SET IDENTITY_INSERT '+QUOTENAME(@NewSchema)+'.'+QUOTENAME(@MasterTableName)+' OFF'+ CHAR(10)
EXEC (@SQL)
--PRINT (@SQL)

END

ELSE 

BEGIN

SET @SQL=(SELECT InsertDESC FROM #SeedInsert WHERE Id=@IMIN) 
EXEC (@SQL)

--PRINT (@SQL)

END
----SELECT * FROM #SeedInsert
----PRINT @SQL


SET @IMIN=@IMIN+1

END

--DECLARE
--@NewSchema Varchar(64)='StriveSuperAdminTest',
--@SQL VARCHAR(MAX),
--@EmployeeId INT,
--@AuthId INT =15

SET @SQL='SET IDENTITY_INSERT '+QUOTENAME(@NewSchema)+'.tblEmployee ON'+CHAR(10)+CHAR(10)+
			'INSERT INTO '+QUOTENAME(@NewSchema)+'.tblEmployee(EmployeeId,FirstName,LastName,IsActive,IsDeleted,CreatedDate) 
			VALUES (1,''ADMIN'',''ADMIN'',1,0,GETDATE())'+CHAR(10)+CHAR(10)+
			'SET IDENTITY_INSERT '+QUOTENAME(@NewSchema)+'.tblEmployee OFF'+CHAR(10)
EXEC (@SQL)
--PRINT (@SQL)

SET @SQL='INSERT INTO '+QUOTENAME(@NewSchema)+'.tblEmployeeDetail(EmployeeId,EmployeeCode,AuthId,IsActive,IsDeleted,CreatedDate) 
			VALUES (1,''EMP000'','+CAST(@AUTHID AS VARCHAR) +',1,0,GETDATE())'

--PRINT @SQL
EXEC (@SQL)
--PRINT (@SQL)
SET @SQL='INSERT INTO '+QUOTENAME(@NewSchema)+'.tblemployeeRole(EmployeeId,RoleId,IsActive,IsDeleted,CreatedDate) 
			VALUES (1,1,1,0,GETDATE())'

--PRINT @SQL
EXEC (@SQL)
GO

