
/****** Object:  StoredProcedure [CON].[uspCopyDbobjects]    Script Date: 25-03-2022 11:14:28 PM ******/
DROP PROCEDURE [CON].[uspCopyDbobjects]
GO

/****** Object:  StoredProcedure [CON].[uspCopyDbobjects]    Script Date: 25-03-2022 11:14:28 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [CON].[uspCopyDbobjects]  
@DEFAULTSCHEMA NVARCHAR(64),  
@NewSchema NVARCHAR(64)  
  
/*  
--------------------------------------------------------------------------------------------  
Author              : Lenin  
Create date         : 28-08-2020  
Description         : Procedure to copy db objects to new schema  
FRS        : SuperAdmin FRS  
--------------------------------------------------------------------------------------  
 Rev | Date Modified | Developer   | Change Summary  
--------------------------------------------------------------------------------------  
  1  |  2020-09-01   | Lenin | Added condition for drop and recreate |   
  
---------------------------------------------------------------------------------------------  
*/  
  
AS  
  
BEGIN  
  
SET NOCOUNT ON  
  
DECLARE   
@IMin INT, @IMax INT,  
@SQL NVARCHAR(MAX),  
@TMax INT, @TMIN INT,  
@CMax INT, @CMIN INT,  
@FKMax INT, @FKMin INT,  
@UKMax INT, @UkMin INT,  
@IDXMax INT, @IDXMin INT  
  
/* DROP Procedure , Function , Trigger , View  For New Schema IF Exists */  
  
--DECLARE @DEFAULTSCHEMA NVARCHAR(64)='ACC',  
--@NewSchema NVARCHAR(64)='TEST',  
--@IMin INT, @IMax INT,  
--@SQL NVARCHAR(MAX)  
  
DROP TABLE IF EXISTS #DB_Objects_Drop  
Select * INTO #DB_Objects_Drop from sys.sql_modules  
  
DROP TABLE IF EXISTS #DB_Objects_Drop_Proc  
  
SELECT   
 S.[Name] AS OLDSchema,O.[Name] AS SPName, @NewSchema AS NEWSchema,TMP.[definition],  
 CAST('' AS varchar(MAX))  DROPSTAT, O.[type_desc]  
INTO   
 #DB_Objects_Drop_Proc  
FROM   
 #DB_Objects_Drop TMP  
JOIN  
 SYS.objects O  
ON  O.object_id=tmp.object_id  
JOIN   
 SYS.schemas S  
On  S.schema_id=O.schema_id  
WHERE S.Name=@DEFAULTSCHEMA   
  
ALTER TABLE #DB_Objects_Drop_Proc  
ADD ID INT IDENTITY(1,1)  
  
UPDATE   
 #DB_Objects_Drop_Proc   
SET DROPSTAT= CASE WHEN [type_desc]='SQL_STORED_PROCEDURE' THEN 'DROP PROCEDURE IF EXISTS ['+@NewSchema+'].'+QUOTENAME(SPNAME)  
      WHEN [type_desc] IN ('SQL_TABLE_VALUED_FUNCTION','SQL_SCALAR_FUNCTION') THEN 'DROP FUNCTION IF EXISTS ['+@NewSchema+'].'+QUOTENAME(SPNAME)  
      WHEN [type_desc]='VIEW' THEN 'DROP VIEW IF EXISTS ['+@NewSchema+'].'+QUOTENAME(SPNAME)  
      WHEN [type_desc]='SQL_TRIGGER' THEN 'DROP TRIGGER IF EXISTS ['+@NewSchema+'].'+QUOTENAME(SPNAME)  
      END    
-- SELECT * FROM #DB_Objects_Drop_Proc  
SELECT @IMax=MAX(ID) , @IMin=MIN(ID) FROM #DB_Objects_Drop_Proc  
  
--PRINT 'MAXID :'+CAST(@IMax as VARCHAR)+' MINID :'+CAST(@IMin AS VARCHAR)  
  
WHILE @IMin<=@IMax  
  
BEGIN  
  
SET @SQL=(SELECT DROPSTAT FROM #DB_Objects_Drop_Proc WHERE Id=@IMIN)  
  
EXEC (@SQL)  
  
--PRINT @SQL  
  
SET @IMin=@IMin+1  
  
END  
  
-- DROP FK IF EXISTS For the New Schema  
  
DROP TABLE IF EXISTS #DB_FK_Drop_Obj  
SELECT   
 ROW_NUMBER() OVER(ORDER BY FK.Name) AS ID,S.name AS OLDSchema ,FK.name,tab.name AS PTable_NAme,Col.name As PColumn_Name,Tab2.name AS RTable_Name,Col2.name As RColumn_Name  
INTO  
 #DB_FK_Drop_Obj  
FROM   
 Sys.foreign_keys FK  
JOIN  
 Sys.foreign_key_columns FK_col  
ON  FK.object_id=FK_col.constraint_object_id  
JOIN   
 sys.schemas S  
ON  S.schema_id=FK.schema_id  
LEFT JOIN  
 Sys.tables Tab  
ON  Tab.Object_id=FK_col.parent_object_id  
LEFT JOIN  
 SYS.Columns Col  
ON  COL.object_id=FK_col.Parent_object_id AND Col.Column_id=FK_col.parent_column_id  
LEFT JOIN  
 Sys.tables Tab2  
ON  Tab2.Object_id=FK_col.Referenced_object_id  
LEFT JOIN  
 SYS.Columns Col2  
ON  Col2.object_id=FK_col.Referenced_object_id AND Col2.Column_id=FK_col.Referenced_column_id  
WHERE S.Name = @NewSchema  
  
-- SELECT * from #DB_FK_Drop_Obj  
IF (SELECT COUNT(*) FROM #DB_FK_Drop_Obj) >0  
  
BEGIN  
DROP TABLE IF EXISTS #DB_FK_Drop_Obj_Desc  
SELECT   
 *,  
 'ALTER TABLE '+QUOTENAME(@NewSchema)+'.'+QUOTENAME(PTable_NAme)  AS ALTER_DESC,   
 'DROP CONSTRAINT FK_'+@NewSchema+'_'+PTable_NAme+'_'+PColumn_Name AS CON_Desc  
INTO  
 #DB_FK_Drop_Obj_Desc  
FROM   
 #DB_FK_Drop_Obj  
  
SELECT @FKMax=MAX(ID),@FKMin=MIN(ID) FROM #DB_FK_Drop_Obj_Desc  
  
SET @SQL=''  
  
WHILE @FKMax>=@FKMin  
  
BEGIN  
  
SET @SQL= (SELECT ALTER_DESC FROM #DB_FK_Drop_Obj_Desc WHERE ID=@FKMIN)+ CHAR(10)+(SELECT CON_DESC FROM #DB_FK_Drop_Obj_Desc WHERE ID=@FKMIN)+CHAR(10)  
  
EXEC (@SQL)  
SET @FKMIN=@FKMIN+1  
  
END  
  
END  
  
  
/* TABLE Creation */  
  
DROP TABLE IF EXISTS #DB_Obj  
SELECT   
 S.[Name] AS OLDSchema,O.[Name] AS TableName, @NewSchema AS NEWSchema,CAST('' AS varchar(MAX))  DROPSTAT,O.[type_desc],o.object_id  
INTO   
 #DB_Obj  
FROM   
 Sys.Tables Tab  
JOIN  
 SYS.objects O  
ON  O.object_id=tab.object_id  
JOIN   
 SYS.schemas S  
ON  S.schema_id=O.schema_id  
WHERE S.Name=@DEFAULTSCHEMA AND Tab.[name] NOT IN ('MasterTableList')  
  
INSERT INTO #DB_Obj  
SELECT   
 S.[Name] AS OLDSchema,tt.[Name] AS TableName, @NewSchema AS NEWSchema,'' AS DROPSTAT,O.[type_desc],o.object_id  
FROM   
 SYS.TABLE_TYPES tt  
LEFT JOIN  
 SYS.OBJECTS O  
ON  O.object_id=tt.type_table_object_id  
LEFT JOIN   
 SYS.schemas S  
ON  S.schema_id=tt.schema_id  
WHERE S.Name=@DEFAULTSCHEMA   
  
--SELECT * from #DB_Obj where type_desc='TYPE_TABLE'  
DROP TABLE IF EXISTS #DB_Obj_ColumnsAttributes  
SELECT   
 Tab.*,C.Name,C.column_id,C.System_type_id,C.user_type_id,C.max_length ,C.precision,C.scale,  
 CASE WHEN C.IS_Identity=1 THEN 'IDENTITY(1,1)' ELSE '' END AS [Identity] ,  
 CASE WHEN C.Is_Nullable=0 THEN 'NOT NULL' ELSE 'NULL' END AS Nullable  
INTO  
 #DB_Obj_ColumnsAttributes  
FROM   
 SYS.COLUMNS C  
JOIN   
 #DB_Obj tab  
ON  Tab.object_id=C.object_id  
ORDER BY Tab.OLDSchema,Tab.TableName,C.column_id  
--Select * from #DB_Obj_ColumnsAttributes   
  
DROP TABLE IF EXISTS #DB_Obj_DataTypes  
SELECT   
 DENSE_RANK() OVER (ORDER BY TABLENAME) AS Tbl_Rnk,  
 ROW_NUMBER () OVER (PARTITION BY TABLENAME ORDER BY Obj.cOLUMN_ID) AS COL_Rnk,  
 obj.*,  
 Ty.Name AS DataType,  
 CASE WHEN TY.NAME IN ('VARCHAR','NVARCHAR','CHAR','binary','varbinary') AND OBJ.max_length<>-1 THEN '( '+CAST(OBJ.max_length AS VARCHAR)+' )'   
   WHEN TY.NAME IN ('VARCHAR','NVARCHAR','CHAR','binary','varbinary') AND OBJ.max_length=-1 THEN '( MAX )'   
   WHEN TY.NAME IN ('DECIMAL','numeric') THEN '( '+CAST(OBJ.precision AS VARCHAR)+','++CAST(OBJ.Scale AS VARCHAR)+' )'  
   END AS [LENGTH],  
 CASE WHEN DF.NAME IS NOT NULL THEN 'DF_'+obj.NEWSchema+'_'+obj.TableName+'_'+Obj.[name] ELSE '' END AS [Default_Con],  
 DF.[name] AS Default_Name,  
 DF.[Definition] AS [Default_value],  
 CAST(MC.masking_function AS nvarchar) masking_function,  
 CC.[definition]  
INTO  
 #DB_Obj_DataTypes  
FROM   
 #DB_Obj_ColumnsAttributes Obj  
LEFT JOIN  
 SYS.TYPES TY  
ON  Ty.system_type_id = Obj.system_type_id AND Ty.user_type_id = Obj.user_type_id  
LEFT JOIN  
 Sys.default_constraints DF  
ON  DF.parent_object_id = obj.object_id AND DF.parent_column_id = obj.column_id  
LEFT JOIN  
 Sys.masked_columns MC  
ON  MC.Object_id = obj.Object_ID AND MC.Column_Id = Obj.Column_ID  
LEFT JOIN  
 Sys.Computed_Columns CC  
ON  CC.Object_Id = Obj.object_id AND CC.column_id = Obj.column_id  
ORDER BY OLDSchema,TableName,column_id  
  
 --SELECT * from #DB_Obj_DataTypes  
DROP TABLE IF EXISTS #DB_Obj_Table  
SELECT DISTINCT   
 OLDSchema,NEWSchema,TableName,Tbl_Rnk,  
 CASE WHEN [type_desc]='USER_TABLE' THEN 'CREATE TABLE ['+NEWSchema+'].['+TableName+']'   
   ELSE 'CREATE TYPE ['+NEWSchema+'].['+TableName+'] AS TABLE' END AS Create_comment,  
 CASE WHEN [type_desc]='USER_TABLE' THEN 'DROP TABLE IF EXISTS ['+NEWSchema+'].['+TableName+']'   
   ELSE 'DROP TYPE IF EXISTS ['+NEWSchema+'].['+TableName+']' END AS DROP_comment  
INTO   
 #DB_Obj_Table  
FROM   
 #DB_Obj_DataTypes  
ORDER BY Tbl_Rnk  
  
-- SELECT * from #DB_Obj_Table  
DROP TABLE IF EXISTS #DB_Obj_Columns  
SELECT   
 QUOTENAME(Col.[Name])+SPACE(4)  
 +UPPER(QUOTENAME(DATAType))+SPACE(1)  
 +CASE WHEN COl.[LENGTH] Is NOT NULL THEN COL.[LENGTH] ELSE '' END + SPACE(6)  
 +CASE WHEN Col.[Masking_function] IS NOT NULL THEN 'MASKED WITH (FUNCTION = ''DEFAULT()'')' ELSE '' END--''+ Mc.[Masking_function]+''  
 +CASE WHEN Col.[Identity]<>'' THEN Col.[Identity] ELSE '' END + SPACE(3)+ COl.Nullable+ SPACE(4)  
 +CASE WHEN Ic.index_id IS NOT NULL THEN 'CONSTRAINT PK_'+NEWSchema+'_'+TableName+'_'+COl.name+SPACE(2)+' PRIMARY KEY CLUSTERED '   
   WHEN col.Default_Con <>'' THEN  'CONSTRAINT '+ col.Default_Con + ' DEFAULT ' + COl.Default_value  
   ELSE '' END AS COL_DESC,  
 Col.*  
INTO   
 #DB_Obj_Columns  
FROM   
 #DB_Obj_DataTypes Col  
LEFT JOIN  
 sys.indexes i   
ON  i.object_id=Col.object_id AND i.is_primary_key = 1  
LEFT JOIN   
 sys.index_columns ic   
ON  i.OBJECT_ID = ic.OBJECT_ID AND i.index_id = ic.index_id AND Col.column_id = Ic.column_id   
ORDER BY Tbl_Rnk,COL.Column_id  
  
----SELECT * FROM #DB_Obj_Columns  
  
UPDATE #DB_Obj_Columns SET COL_DESC = QUOTENAME([Name])+SPACE(4)+' AS '+[definition] WHERE [definition] IS Not NULL  
  
SELECT @TMax=MAX(Tbl_Rnk),@TMin=MIN(Tbl_Rnk) FROM #DB_Obj_Table  
  
--PRINT CAST( @TMAX AS Varchar)+' , '+CAST( @TMIN AS Varchar)  
  
WHILE @TMax>=@TMin  
  
BEGIN  
  
SET @SQL=(SELECT DROP_comment FROM #DB_Obj_Table WHERE Tbl_Rnk=@TMIN) + CHAR(10) +(SELECT Create_comment FROM #DB_Obj_Table WHERE Tbl_Rnk=@TMIN)+CHAR(10)+'('+ CHAR(10)  
  
SELECT @CMIN=Min(COL_Rnk),@CMax=MAX(COL_Rnk) FROM #DB_Obj_Columns Where Tbl_Rnk=@Tmin  
--PRINT @SQL  
WHILE @CMax>=@CMIN  
BEGIN  
 IF @CMAX>@CMIN  
 BEGIN  
  SET @SQL= @SQL+ (SELECT COL_DESC FROM #DB_Obj_Columns WHERE Tbl_Rnk=@Tmin AND COL_Rnk=@CMIN)+' ,'+CHAR(10)  
 END  
 IF @CMAX=@CMIN  
 BEGIN  
  SET @SQL= @SQL+ (SELECT COL_DESC FROM #DB_Obj_Columns WHERE Tbl_Rnk=@Tmin AND COL_Rnk=@CMIN)+CHAR(10)  
 END  
SET @CMIN=@CMIN+1  
END  
  
SET @SQL= @SQL+')'+CHAR(10)+CHAR(10)  
--PRINT @SQL  
EXEC (@SQL)  
  
SET @TMIN=@TMIN+1  
END  
  
/* UNIQUE KEY Generation */  
  
DROP TABLE IF EXISTS #Db_Obj_Uk  
SELECT   
 S.Name AS OldSchema ,I.NAME AS Con_Name,COL.Name as Col_Name,T.Name As Table_Name  
INTO  
 #Db_Obj_Uk  
FROM   
 SYS.INDEXES I  
JOIN  
 SYS.INDEX_COLUMNS IC  
ON  i.OBJECT_ID = ic.OBJECT_ID AND i.index_id = ic.index_id   
JOIN   
 SYS.TABLES T  
ON  T.Object_ID =I.Object_ID  
JOIN  
 SYS.SCHEMAS S  
ON  S.Schema_Id=T.Schema_ID  
LEFT JOIN  
 SYS.COLUMNS Col  
ON  Col.object_Id=I.Object_ID AND Col.Column_Id=IC.Column_id  
WHERE IS_unique_constraint=1 AND S.[Name]=@DEFAULTSCHEMA  
  
DROP TABLE IF EXISTS #Db_Obj_Uk_Concat  
SELECT DISTINCT   
 t1.OLDSchema,Con_Name,Table_Name,    
 STUFF((SELECT ',' + t2.[Col_Name]  FROM #Db_Obj_Uk t2  WHERE t1.Con_Name = t2.Con_Name  FOR XML PATH ('')) , 1, 1, '')  AS UK_COL  
INTO  
 #Db_Obj_Uk_Concat  
FROM   
 #Db_Obj_Uk t1;  
  
DROP TABLE IF EXISTS #DB_UK_col_count  
SELECT   
 CON_NAME,COUNT(*) CNT  
INTO  
 #DB_UK_col_count  
FROM   
 #Db_Obj_Uk t1  
GROUP BY Con_Name  
  
DROP TABLE IF EXISTS #Db_Obj_Uk_Concat_Desc  
SELECT   
 ROW_NUMBER() OVER(ORder BY CON.Con_Name) AS ID,  
 CON.*,'ALTER TABLE '+QUOTENAME(@NewSchema)+'.'+QUOTENAME(Table_NAme)  AS ALTER_DESC,   
 CASE WHEN  CNt.CNT=1 THEN 'ADD CONSTRAINT UK_'+@NewSchema+'_'+Table_NAme+'_'+REPLACE(UK_COL,',','_')+' UNIQUE ('+UK_COL+')'   
   WHEN Cnt.CNT>1 THEN 'ADD CONSTRAINT UK_'+@NewSchema+'_'+Table_NAme+'_COMP'+' UNIQUE ('+UK_COL+')' END AS CON_Desc   
INTO   
 #Db_Obj_Uk_Concat_DESC  
FROM   
 #Db_Obj_Uk_Concat CON  
LEFT JOIN  
 #DB_UK_col_count Cnt  
ON  CNT.Con_Name = COn.Con_Name  
  
SELECT @UKMax=MAX(ID),@UkMin=MIN(ID) FROM #Db_Obj_Uk_Concat_DESC  
SET @SQL=''  
  
WHILE @UKMax>=@UKMin  
  
BEGIN  
  
SET @SQL= (SELECT ALTER_DESC FROM #Db_Obj_Uk_Concat_DESC WHERE ID=@UkMin)+ CHAR(10)+(SELECT CON_DESC FROM #Db_Obj_Uk_Concat_DESC WHERE ID=@UkMin)+CHAR(10)  
  
--PRINT (@SQL)  
EXEC (@SQL)  
SET @UkMin=@UkMin+1  
  
END  
-- select * from #Db_Obj_Uk_Concat_DESC  
  
--/* Foreign KEY Generation */  
  
DROP TABLE IF EXISTS #DB_FK_Obj  
SELECT   
 ROW_NUMBER() OVER(ORDER BY FK.Name) AS ID,  
 S.name AS OLDSchema ,FK.name,tab.name AS PTable_NAme,Col.name As PColumn_Name,  
 Tab2.name AS RTable_Name,Col2.name As RColumn_Name  
INTO  
 #DB_FK_Obj  
FROM   
 Sys.foreign_keys FK  
JOIN  
 Sys.foreign_key_columns FK_col  
ON  FK.object_id=FK_col.constraint_object_id  
JOIN   
 sys.schemas S  
ON  S.schema_id=FK.schema_id  
LEFT JOIN  
 Sys.tables Tab  
ON  Tab.Object_id=FK_col.parent_object_id  
LEFT JOIN  
 SYS.Columns Col  
ON  COL.object_id=FK_col.Parent_object_id AND Col.Column_id=FK_col.parent_column_id  
LEFT JOIN  
 Sys.tables Tab2  
ON  Tab2.Object_id=FK_col.Referenced_object_id  
LEFT JOIN  
 SYS.Columns Col2  
ON  Col2.object_id=FK_col.Referenced_object_id AND Col2.Column_id=FK_col.Referenced_column_id  
WHERE S.Name=@DEFAULTSCHEMA  
  
DROP TABLE IF EXISTS #DB_FK_Obj_Desc  
SELECT   
 *,  
 'ALTER TABLE '+QUOTENAME(@NewSchema)+'.'+QUOTENAME(PTable_NAme)  AS ALTER_DESC,   
 'ADD CONSTRAINT FK_'+@NewSchema+'_'+PTable_NAme+'_'+PColumn_Name+' FOREIGN KEY ('+PColumn_Name+') REFERENCES '+QUOTENAME(@NewSchema)+'.'+QUOTENAME(RTable_NAme)  
 + ' ('+RColumn_Name+')' AS CON_Desc  
INTO  
 #DB_FK_Obj_Desc  
FROM   
 #DB_FK_Obj  
  
SELECT @FKMax=MAX(ID),@FKMin=MIN(ID) FROM #DB_FK_Obj_Desc  
  
SET @SQL=''  
  
WHILE @FKMax>=@FKMin  
  
BEGIN  
  
SET @SQL= (SELECT ALTER_DESC FROM #DB_FK_Obj_Desc WHERE ID=@FKMIN)+ CHAR(10)+(SELECT CON_DESC FROM #DB_FK_Obj_Desc WHERE ID=@FKMIN)+CHAR(10)  
  
EXEC (@SQL)  
SET @FKMIN=@FKMIN+1  
  
END  
  
/* INDEX Generation */  
  
  
DROP TABLE IF EXISTS #Db_Obj_Index  
SELECT DISTINCT   
 S.name AS [Schema_Name],  
 I.Name as Index_Name,  
 O.Name as Table_Name,  
 CASE WHEN IC.is_included_column=0 THEN Col.Name ELSE '' END AS IDX_Column_Name,  
 CASE WHEN IC.is_included_column=1 THEN Col.Name ELSE '' END AS INC_Column_Name  
INTO  
 #Db_Obj_Index  
FROM   
 SYS.INDEXES I  
JOIN   
 SYS.INDEX_COLUMNS IC  
ON  IC.OBJECT_ID=I.OBJECT_ID  AND I.INDEX_ID=IC.INDEX_ID  
LEFT JOIN   
 SYS.OBJECTS O  
ON  O.OBJECT_ID=I.OBJECT_ID  
LEFT JOIN   
 SYS.COLUMNS COL  
ON  COL.OBJECT_ID=I.OBJECT_ID AND COL.COLUMN_ID=IC.COLUMN_ID  
LEFT JOIN   
 Sys.schemas S  
ON  S.schema_id=O.schema_id  
WHERE I.TYPE=2 AND I.IS_PRIMARY_KEY=0 AND I.IS_UNIQUE_CONSTRAINT=0  AND O.TYPE_DESC='USER_TABLE' AND S.name=@DEFAULTSCHEMA  
  
--SELECT * FROM #Db_Obj_Index  
  
DROP TABLE IF EXISTS #Db_Obj_Index_Concat  
SELECT DISTINCT   
 t1.[Schema_Name],T1.Index_Name,T1.Table_Name,    
 STUFF((SELECT ',' + t2.IDX_Column_Name  FROM #Db_Obj_Index t2  WHERE t1.Index_Name = t2.Index_Name AND t2.IDX_Column_Name<>''  FOR XML PATH ('')) , 1, 1, '')  AS IDX_COL,  
 STUFF((SELECT ',' + t3.INC_Column_Name  FROM #Db_Obj_Index t3  WHERE t1.Index_Name = t3.Index_Name AND t3.INC_Column_Name<>''  FOR XML PATH ('')) , 1, 1, '')  AS INC_COL  
INTO  
 #Db_Obj_Index_Concat  
FROM   
 #Db_Obj_Index t1;  
  
DROP TABLE IF EXISTS #Db_Obj_Index_count  
SELECT Index_Name,COUNT(*) CNT  
INTO  
 #Db_Obj_Index_count  
FROM   
 #Db_Obj_Index   
WHERE IDX_Column_Name<>''  
GROUP BY Index_Name  
  
--Select * from #Db_Obj_Index_count  
  
DROP TABLE IF EXISTS #Db_Obj_Index_Concat_Desc  
SELECT   
 ROW_NUMBER() OVER(ORder BY CON.Index_Name) AS ID,  
 CON.*,  
 ('CREATE  NONCLUSTERED INDEX IX_'+CON.Table_Name+'_'+REPLACE(CON.IDX_COL,',','_')+ ' ON '+ QUOTENAME(@NewSchema)+'.'+QUOTENAME(CON.Table_Name)+'('+IDX_COL+')'  
 +CASE WHEN CON.INC_COL IS NOT NULL THEN ' INCLUDE ('+CON.INC_COL+')' ELSE '' END) AS IDX_Desc  
INTO   
 #Db_Obj_Index_Concat_DESC  
FROM   
 #Db_Obj_Index_Concat CON  
LEFT JOIN  
 #Db_Obj_Index_count Cnt  
ON  CNT.Index_Name = COn.Index_Name  
  
SELECT @IDXMIN=MIN(ID),@IDXMax=MAX(ID) FROM #Db_Obj_Index_Concat_Desc  
  
WHILE @IDXMax>=@IDXMin  
  
BEGIN  
SET @SQL=''  
  
SELECT @SQL=IDX_Desc FROM #Db_Obj_Index_Concat_DESC WHERE ID=@IDXMin  
  
--PRINT @SQL  
EXEC (@SQL)  
SET @IDXMin=@IDXMin+1  
  
END  
  
/* Procedure , Function , Trigger , View  Generation */  
  
--DECLARE @DEFAULTSCHEMA NVARCHAR(64)='ACC',  
--@NewSchema NVARCHAR(64)='TEST',  
--@IMin INT, @IMax INT,  
--@SQL NVARCHAR(MAX)  
  
DROP TABLE IF EXISTS #DB_Objects  
Select * INTO #DB_Objects from sys.sql_modules  
  
DROP TABLE IF EXISTS #DB_Obj_Proc  
  
SELECT   
 S.[Name] AS OLDSchema,O.[Name] AS SPName, @NewSchema AS NEWSchema,TMP.[definition],  
 CAST('' AS varchar(MAX))  DROPSTAT, O.[type_desc]  
INTO   
 #DB_Obj_Proc  
FROM   
 #DB_Objects TMP  
JOIN  
 SYS.objects O  
ON  O.object_id=tmp.object_id  
JOIN   
 SYS.schemas S  
On  S.schema_id=O.schema_id  
WHERE S.Name=@DEFAULTSCHEMA   
AND o.name NOT IN ('uspCopyDbobjects','uspUserCreation','uspMasterDataInsert')  
--AND o.[type_desc]<>'SQL_STORED_PROCEDURE'  
  
ALTER TABLE #DB_Obj_Proc  
ADD ID INT IDENTITY(1,1)  
  
UPDATE   
 #DB_Obj_Proc   
SET [definition]= CASE WHEN [type_desc]='SQL_STORED_PROCEDURE' THEN REPLACE([definition],'CREATE PROCEDURE ','CREATE PROCEDURE ['+@NewSchema+'].'+QUOTENAME(SPNAME)+'--')  
      WHEN [type_desc] IN ('SQL_TABLE_VALUED_FUNCTION','SQL_SCALAR_FUNCTION') THEN REPLACE([definition],'CREATE FUNCTION ','CREATE FUNCTION ['+@NewSchema+'].'+QUOTENAME(SPNAME)+'--')  
      WHEN [type_desc]='VIEW' THEN REPLACE([definition],'CREATE VIEW ','CREATE VIEW ['+@NewSchema+'].'+QUOTENAME(SPNAME)+'--')  
      WHEN [type_desc]='SQL_TRIGGER' THEN REPLACE([definition],'CREATE TRIGGER ','CREATE TRIGGER ['+@NewSchema+'].'+QUOTENAME(SPNAME)+'--')  
      END ,  
 DROPSTAT= CASE WHEN [type_desc]='SQL_STORED_PROCEDURE' THEN 'DROP PROCEDURE IF EXISTS ['+@NewSchema+'].'+SPNAME  
      WHEN [type_desc] IN ('SQL_TABLE_VALUED_FUNCTION','SQL_SCALAR_FUNCTION') THEN 'DROP FUNCTION IF EXISTS ['+@NewSchema+'].'+SPNAME  
      WHEN [type_desc]='VIEW' THEN 'DROP VIEW IF EXISTS ['+@NewSchema+'].'+SPNAME  
      WHEN [type_desc]='SQL_TRIGGER' THEN 'DROP TRIGGER IF EXISTS ['+@NewSchema+'].'+SPNAME  
      END    
  
--SELECT *,CASE WHEN [type_desc]='SQL_STORED_PROCEDURE' THEN 'CREATE PROCEDURE ['+@DEFAULTSCHEMA+'].'  
--      WHEN [type_desc]='SQL_STORED_PROCEDURE' THEN 'CREATE PROCEDURE '+@DEFAULTSCHEMA+'.'  
--      WHEN [type_desc] IN ('SQL_TABLE_VALUED_FUNCTION','SQL_SCALAR_FUNCTION') THEN 'CREATE FUNCTION ['+@DEFAULTSCHEMA+'].'  
--      WHEN [type_desc] IN ('SQL_TABLE_VALUED_FUNCTION','SQL_SCALAR_FUNCTION') THEN 'CREATE FUNCTION'+@DEFAULTSCHEMA+'.'  
--      WHEN [type_desc]='VIEW' THEN 'CREATE VIEW ['+@DEFAULTSCHEMA+'].'  
--      WHEN [type_desc]='VIEW' THEN 'CREATE VIEW '+@DEFAULTSCHEMA+'.'  
--      WHEN [type_desc]='SQL_TRIGGER' THEN 'CREATE TRIGGER ['+@DEFAULTSCHEMA+'].'  
--      WHEN [type_desc]='SQL_TRIGGER' THEN 'CREATE TRIGGER'+@DEFAULTSCHEMA+'.'  
--      END AS [DEsc]   
--     FROM #DB_Obj_Proc  
  
SELECT @IMax=MAX(ID) , @IMin=MIN(ID) FROM #DB_Obj_Proc  
  
--PRINT 'MAXID :'+CAST(@IMax as VARCHAR)+' MINID :'+CAST(@IMin AS VARCHAR)  
  
WHILE @IMin<=@IMax  
  
BEGIN  
  
SET @SQL=(SELECT DROPSTAT FROM #DB_Obj_Proc WHERE Id=@IMIN)  
  
EXEC (@SQL)  
  
SET @SQL=(SELECT [definition] FROM #DB_Obj_Proc WHERE ID=@IMIN)  
  
EXEC (@SQL)  
--PRINT @SQL  
  
SET @IMin=@IMin+1  
  
END  
  
END  
  
GO

