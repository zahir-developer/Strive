﻿

CREATE PROCEDURE [CON].[uspSoftDeleteRestrict]
--DECLARE
@SCHEMANAME VARCHAR (100),
@TABLENAME VARCHAR (200), 
@ParamValue INT

AS
--EXEC [CON].[uspSoftDeleterestrict] 'StriveCarSalon','TblLocation',1

BEGIN
SET NOCOUNT ON;

DECLARE @PRIMARYKEYCOLUMN VARCHAR (50), 
		@PARENTOBJECTID BIGINT, 
		@SQL NVARCHAR (MAX), 
		@IMIN INT, 
		@IMAX INT, 
		@MSG VARCHAR(100) ='REFERENCE DOES NOT EXISTS',
		@COUNT INT

SELECT 
	@PRIMARYKEYCOLUMN=C.NAME, 
	@PARENTOBJECTID=O.OBJECT_ID 
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
WHERE 
		O.NAME=@TABLENAME AND S.NAME=@SCHEMANAME AND IS_PRIMARY_KEY=1

DROP TABLE IF EXISTS #SQLQUERY 

CREATE TABLE #SQLQUERY ( [ID] INT IDENTITY(1,1), TABLE_NAME VARCHAR (200),COLUMN_NAME VARCHAR(100) )

INSERT INTO #SQLQUERY(TABLE_NAME,COLUMN_NAME)
SELECT 
	(S.NAME+'.'+O.NAME) AS TABLENAME,
	COL.NAME AS COLUMNNAME
FROM 
	SYS.COLUMNS C
JOIN
	SYS.foreign_key_columns FKCOL
ON FKCOL.REFERENCED_COLUMN_ID=C.COLUMN_ID AND FKCOL.REFERENCED_OBJECT_ID=C.OBJECT_ID
LEFT JOIN
	SYS.COLUMNS COL
ON		FKCOL.parent_column_id=Col.COLUMN_ID AND FKCOL.parent_object_id=Col.OBJECT_ID
JOIN 
	SYS.OBJECTS O
ON		O.OBJECT_ID=COL.OBJECT_ID
JOIN 
	SYS.SCHEMAS S
ON		S.SCHEMA_ID=O.SCHEMA_ID
WHERE C.NAME=@PRIMARYKEYCOLUMN AND O.TYPE='U' AND S.NAME =@SCHEMANAME

--SELECT 
--	(S.NAME+'.'+O.NAME) AS TABLENAME,
--	C.NAME AS COLUMNNAME
--FROM 
--	SYS.COLUMNS C
--JOIN 
--	SYS.OBJECTS O
--ON		O.OBJECT_ID=C.OBJECT_ID
--JOIN 
--	SYS.SCHEMAS S
--ON		S.SCHEMA_ID=O.SCHEMA_ID
--WHERE C.NAME=@PRIMARYKEYCOLUMN AND C.OBJECT_ID<>@PARENTOBJECTID AND O.TYPE='U'

SELECT @IMIN=MIN(ID),@IMAX=MAX(ID) FROM #SQLQUERY

WHILE @IMIN<=@IMAX AND @MSG='REFERENCE DOES NOT EXISTS'

BEGIN

DECLARE @TNAME NVARCHAR(200), @CNAME NVARCHAR(100),@ParValue NVARCHAR(10) =@PARAMVALUE,@PARA NVARCHAR(100)

SELECT @TNAME=TABLE_NAME,@CNAME=COLUMN_NAME FROM #SQLQUERY WHERE ID=@IMIN

SET  @SQL='SET @COUNT = (SELECT COUNT(*) FROM '+ @TNAME+' WHERE '+@CNAME+' = '+@ParValue+')' 
SET  @Para='@COUNT INT OUTPUT'
--PRINT @SQL

EXEC SP_EXECUTESQL @SQL,@PARA,@COUNT=@COUNT OUTPUT

--PRINT 'Executed TABLE'+@TNAME

--SELECT @COUNT
IF @COUNT>=1

SET @MSG='REFERENCE EXISTS'
--PRINT @MSG

SET @IMIN=@IMIN+1

END

PRINT @MSG

END