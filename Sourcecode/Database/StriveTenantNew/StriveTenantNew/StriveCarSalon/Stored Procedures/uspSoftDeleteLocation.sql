CREATE   PROCEDURE [StriveCarSalon].[uspSoftDeleteLocation]
@TABLENAME VARCHAR (200)='tblLocation',
@ParamValue INT

/*
-----------------------------------------------------------------------------------------
Author              : Lenin
Create date         : 09-SEP-2020
Description         : Sample Procedure for Location table Soft Delete
FRS					: Soft Delete Location
-----------------------------------------------------------------------------------------
 Rev | Date Modified | Developer	| Change Summary
-----------------------------------------------------------------------------------------
    |                |      		| 

-----------------------------------------------------------------------------------------
*/
AS

DECLARE @SCHEMANAME VARCHAR (100),
@PrimaryKey VARCHAR(200),
@SQL NVARCHAR(MAX)

SET NOCOUNT ON

SET @SCHEMANAME= (SELECT DEFAULT_SCHEMA_NAME FROM SYS.DATABASE_PRINCIPALS WHERE NAME=ORIGINAL_LOGIN())

SELECT 	@PRIMARYKEY=C.NAME
FROM 
	SYS.OBJECTS O
JOIN 
	SYS.INDEXES I
ON		I.OBJECT_ID=O.OBJECT_ID
JOIN SYS.INDEX_COLUMNS IC
ON		IC.OBJECT_ID=I.OBJECT_ID AND IC.INDEX_ID=I.INDEX_ID
JOIN SYS.COLUMNS C
ON		C.COLUMN_ID=IC.COLUMN_ID AND C.OBJECT_ID=IC.OBJECT_ID
JOIN 
	SYS.SCHEMAS S
ON		S.SCHEMA_ID= O.SCHEMA_ID
WHERE 	O.NAME=@TABLENAME AND S.NAME=@SCHEMANAME AND IS_PRIMARY_KEY=1


DROP TABLE IF EXISTS #Dependenttable
CREATE TABLE #Dependenttable
(TableName Varchar(200),RecordCount INT,PrimaryKey VARCHAR(200))


INSERT INTO #Dependenttable 
EXEC uspSoftDeleteRestrictReturnTable @SCHEMANAME,@TABLENAME,@ParamValue

IF (SELECT COUNT(*) FROM #Dependenttable)>0

BEGIN
PRINT 'Cannot Delete Reference EXISTS'
--SELECT * FROM #Dependenttable
END

ELSE 
BEGIN

SET @SQL='UPDATE '+@TABLENAME+' SET IsDeleted = 1 WHERE '+@PrimaryKey+'='+ CAST (@ParamValue AS NVARCHAR)

EXEC (@SQL)

END