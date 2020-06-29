﻿
create proc GENERATEMODEL
(@Schema varchar(50), @Table varchar(50))
as
begin
declare @SchemaToGenerateModel varchar(50) =@Schema
declare @TableNameToGenerateModel varchar(50) =@Table
declare @tableName varchar(200)
declare @columnName varchar(200)
declare @nullable varchar(50)
declare @datatype varchar(50)
declare @maxlen int
declare @pos int

declare @Stype varchar(50)
declare @isnullable varchar(1)
declare @Sproperty varchar(200)


DECLARE table_cursor CURSOR FOR
SELECT TABLE_NAME
FROM [INFORMATION_SCHEMA].[TABLES] where TABLE_SCHEMA=@SchemaToGenerateModel and TABLE_NAME =@TableNameToGenerateModel

OPEN table_cursor

FETCH NEXT FROM table_cursor
INTO @tableName

WHILE @@FETCH_STATUS = 0
BEGIN

PRINT 'public class ' + @tableName + ' {'

DECLARE column_cursor CURSOR FOR 
SELECT COLUMN_NAME, IS_NULLABLE, DATA_TYPE, isnull(CHARACTER_MAXIMUM_LENGTH,'-1'), ORDINAL_POSITION
	, CASE WHEN (IS_NULLABLE = 'NO') THEN '' 
	ELSE 
		CASE WHEN (DATA_TYPE IN ('char','nchar','varchar','nvarchar')) THEN '' ELSE '?' END
	END

from [INFORMATION_SCHEMA].[COLUMNS] 
WHERE table_schema=@SchemaToGenerateModel and [TABLE_NAME] = @tableName
order by [ORDINAL_POSITION]

OPEN column_cursor
FETCH NEXT FROM column_cursor INTO @columnName, @nullable, @datatype, @maxlen, @pos, @isNullable

WHILE @@FETCH_STATUS = 0
BEGIN

-- datatype
select @sType = case @datatype
	when 'int' then 'int'
	when 'smallint' then 'short'
	when 'bigint' then 'int'
	when 'decimal' then 'Decimal'
	when 'money' then 'Decimal'
	when 'char' then 'string'
	when 'nchar' then 'string'
	when 'smallint' then 'short'
	when 'varchar' then 'string'
	when 'nvarchar' then 'string'
	when 'uniqueidentifier' then 'Guid'
	when 'datetime' then 'DateTime'
	when 'bit' then 'bool'
else 'string'
END

	If (@pos = 1)
		--PRINT '	[Key]'
	If (@nullable = 'NO' AND @pos > 1)
		PRINT '	[Required]'
	if (@sType = 'string' and @maxLen <> '-1')
		PRINT '	[MaxLength(' +  convert(varchar(4),@maxLen) + ')]'
	if (@sType = 'datetime')
		PRINT '	[Column(TypeName = "datetime")]'
	SELECT @sProperty = '	public ' + @sType + @isNullable + ' ' + @columnName + ' { get; set; }'
	PRINT @sProperty

	--print ''
	FETCH NEXT FROM column_cursor INTO @columnName, @nullable, @datatype, @maxlen, @pos, @isNullable
END
CLOSE column_cursor
DEALLOCATE column_cursor

print '}'
print ''
FETCH NEXT FROM table_cursor 
INTO @tableName
END
CLOSE table_cursor
DEALLOCATE table_cursor
end